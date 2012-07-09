﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Auth
{
    public class AuthManager
    {
        private static readonly byte[] key1 = new byte[]
                                                    {
                                                        218, 93, 45, 117, 95, 201, 32, 108, 115, 224, 222, 15, 78, 43,
                                                        32, 56, 176, 98, 155, 103, 49, 99, 19, 200, 81, 11, 87, 21,
                                                        241, 90, 46, 192
                                                    };

        private static readonly byte[] key2 = new byte[]
                                                    {
                                                        109, 27, 43, 90, 142, 98, 219, 131, 66, 224, 197, 125, 83, 22,
                                                        211, 97
                                                    };

        public static string GenerateToken(int enterpriseId, int businessUnitId, int memberId, Guid providerUserKey, string ipAddress, string userAgent)
        {
            return EncryptContext(enterpriseId, businessUnitId, memberId, providerUserKey, ipAddress, userAgent);
        }

        public static string GenerateToken(Guid providerUserKey, string ipAddress, string userAgent)
        {
            var principal = GetPrincipal(providerUserKey);
            return GenerateToken(principal.EnterpriseID, principal.BusinessUnitID, principal.MemberID, providerUserKey, ipAddress, userAgent);
        }

        public static void DisposeToken(string token)
        {
            const string sql = @"update dbo.Token set IsActive = 0 where token = @Token";
            var sqlParams = new[] { new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token } };
            DBConnection.ExecuteQuery(sql, sqlParams);
        }

        public static Principal GetPrincipal(string token)
        {
            char[] delim = { ',' };
            var x = DecryptContext(token);
            var context = x.Split(delim);
            var principal = new Principal()
                {
                    EnterpriseID = Convert.ToInt32(context[0]),
                    BusinessUnitID = Convert.ToInt32(context[1]),
                    MemberID = Convert.ToInt32(context[2])
                };

            return principal;
        }

        public static Principal GetPrincipal(Guid providerUserKey)
        {
            var principal = new Principal();
            
            const string sql = @"select EnterpriseId, BusinessUnitId, MemberId from dbo.Member with (nolock) where ProviderUserKey = @ProviderUserKey";
            var sqlParams = new[] { new SqlParameter("@ProviderUserKey", SqlDbType.UniqueIdentifier) { Value = providerUserKey } };
            var row = DBConnection.ExecuteQuery(sql, sqlParams);

            if(row != null)
            {
                principal.EnterpriseID = (int)row["EnterpriseId"];
                principal.BusinessUnitID = (int)row["BusinessUnitId"];
                principal.MemberID = (int)row["MemberId"];
                principal.ProviderUserKey = providerUserKey;
            }

            return principal;
        }

        private static string EncryptContext(int enterpriseId, int businessUnitId, int memberId, Guid providerUserKey, string ipAddress, string userAgent)
        {
            var aes = new AesManaged();
            aes.GenerateIV();
            var salt = aes.IV;
            var data = String.Join(",", new[] { enterpriseId, businessUnitId, memberId });
            var token = string.Format("{0}_{1}", ByteArrayToHexString(salt), ByteArrayToHexString(Encrypt.EncryptStringToBytes(data, key1, key2)));
            StoreToken(token, salt, enterpriseId, businessUnitId, memberId, providerUserKey, ipAddress, userAgent);
            return token;
        }

        private static void StoreToken(string token, byte[] salt, int enterpriseId, int businessUnitId, int memberId, Guid providerUserKey, string ipAddress, string userAgent)
        {
            var newSalt = ByteArrayToHexString(salt);
            var expire = DateTime.Now.AddHours(3);

            const string sql = @"insert into dbo.Token (Token, Salt, EnterpriseId, BusinessUnitId, ProviderUserKey, MemberId, ExpirationDate, IpAddress, UserAgent) values (@Token, @Salt, @EnterpriseId, @BusinessUnitId, @ProviderUserKey,@MemberId, @ExpirationDate, @IpAddress,@UserAgent)";
            var sqlParams = new[]
                                { 
                                    new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token },
                                    new SqlParameter("@Salt", SqlDbType.NVarChar, 255) { Value = newSalt },
                                    new SqlParameter("@EnterpriseId", SqlDbType.Int) { Value = enterpriseId },
                                    new SqlParameter("@BusinessUnitId", SqlDbType.Int) { Value = businessUnitId },
                                    new SqlParameter("@ProviderUserKey", SqlDbType.UniqueIdentifier) { Value = providerUserKey },
                                    new SqlParameter("@MemberId", SqlDbType.Int) { Value = memberId },
                                    new SqlParameter("@ExpirationDate", SqlDbType.DateTime) { Value = expire },
                                    new SqlParameter("@IpAddress", SqlDbType.NVarChar, 15) { Value = ipAddress },
                                    new SqlParameter("@UserAgent", SqlDbType.NVarChar, 255) { Value = userAgent }
                                };
            DBConnection.ExecuteQuery(sql, sqlParams);
        }

        public static bool ValidateToken(string token)
        {
            var salt = "";
            var expire = DateTime.Now.AddHours(-1);
            bool isValid = false;

            // pull out salt
            if (String.IsNullOrEmpty(token))
            {
                return false;
            }
            char[] delim = { '_' };
            var splitKey = token.Split(delim);
            salt = splitKey[0];

            const string sql = @"select ExpirationDate, IsActive from dbo.Token with (nolock) where token = @Token and salt = @Salt";
            var sqlParams = new[]
                                {
                                    new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token },
                                    new SqlParameter("@Salt", SqlDbType.NVarChar, 255) { Value = salt }
                                };
            var row = DBConnection.ExecuteQuery(sql, sqlParams);

            if (row != null )
            {
                if (row.Count > 0)
                {
                    isValid = (bool) row["IsActive"];
                    expire = (DateTime) row["ExpirationDate"];

                    if (isValid)
                    {
                        if (expire < DateTime.Now)
                        {
                            isValid = false;
                            DisposeToken(token);
                        }
                    }
                }
            }

            return isValid;
        }

        public static void CreateMember(string username, string email, Guid providerUserKey)
        {
            const string sql = @"insert into dbo.Member (EnterpriseId, BusinessUnitId, ProviderUserKey, Email, Username) values (@EnterpriseId, @BusinessUnitId, @ProviderUserKey, @Email, @Username)";
            var sqlParams = new[]
                                {
                                    new SqlParameter("@EnterpriseId", SqlDbType.Int) { Value = 0 },
                                    new SqlParameter("@BusinessUnitId", SqlDbType.Int) { Value = 1 },
                                    new SqlParameter("@ProviderUserKey", SqlDbType.UniqueIdentifier) { Value = providerUserKey },
                                    new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = email },
                                    new SqlParameter("@Username", SqlDbType.NVarChar, 255) { Value = username }
                                };
            DBConnection.ExecuteQuery(sql, sqlParams);
        }

        private static string DecryptContext(string key)
        {
            if(String.IsNullOrEmpty(key)) { throw new ArgumentNullException(); }
            char[] delim = {'_'};
            var splitKey = key.Split(delim);
            byte[] b = HexStringToByteArray(splitKey[1]);
            return Encrypt.DecryptStringFromBytes(b, key1, key2);
        }

        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            var buffer = new byte[s.Length / 2];

            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }

            return buffer;
        }

        private static string ByteArrayToHexString(byte[] bytes)
        {
            int length = bytes.Length;

            char[] chars = new char[length << 1];

            for (int i = 0, j = 0; i < length; i++, j++)
            {
                byte b = (byte)(bytes[i] >> 4);
                chars[j] = (char)(b > 9 ? b + 0x37 : b + 0x30);

                j++;

                b = (byte)(bytes[i] & 0x0F);
                chars[j] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }

            return new String(chars);
        }
    }
}