using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Auth
{
    public class DBConnection
    {
        private static readonly string connStr = "Data Source=IN3710;Initial Catalog=MPCC_Connect;Integrated Security=SSPI";
        
        public static Hashtable ExecuteQuery(string sql, SqlParameter[] parameters)
        {

            var hash = new Hashtable();
            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    DbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(parameters);
                    var reader = cmd.ExecuteReader();

                    try
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                if(reader.IsDBNull(i))
                                {
                                    hash.Add(reader.GetName(i),null);
                                }
                                else
                                {
                                    hash.Add(reader.GetName(i), reader.GetValue(i));
                                }
                            }
                        }
                    }
                    finally 
                    {
                        reader.Close();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex.InnerException);
            }
            
            return hash;
        }
    }
}
