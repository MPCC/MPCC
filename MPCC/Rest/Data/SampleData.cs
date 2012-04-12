using System;
using System.Collections.Generic;
using Rest.Objects;

namespace Rest.Data
{
    public class SampleData
    {
        public static List<Member> GetMemberList()
        {
            var list = new List<Member>();
            list.Add(SampleMemberOne());
            list.Add(SampleMemberTwo());
            return list;
        }

        public static Member GetMemberById(string id)
        {
            return SampleMemberOne();
        }

        public static Member SaveMember(Member member)
        {
            return member;
        }

        public static void DeleteMember(string id) { }

        public static Member SampleMemberOne()
        {
            var name = new Name();
            name.First = "Eric";
            name.Last = "Jones";
            name.Middle = "Who Cares";

            var address = new Address();
            address.Street = "1234 East St.";
            address.Apt = String.Empty;
            address.City = "Greenwood";
            address.State = "IN";
            address.Zip = 46142;

            var twitter = new MessageChannel();
            twitter.Name = Enums.MessageChannels.Twitter.ToString();
            twitter.Id = Enums.MessageChannels.Twitter;

            var email = new MessageChannel();
            email.Name = Enums.MessageChannels.Email.ToString();
            email.Id = Enums.MessageChannels.Email;

            var c1 = new Channel();
            c1.Type = twitter;
            c1.AccountId = "@erjjones";
            c1.IsPreferred = true;
            c1.IsActive = true;
            c1.IsOptIn = true;

            var c2 = new Channel();
            c2.Type = email;
            c2.AccountId = "erjjones@gmail.com";
            c2.IsPreferred = false;
            c2.IsActive = true;
            c2.IsOptIn = true;

            var clist = new List<Channel>();
            clist.Add(c1);
            clist.Add(c2);

            var dob = new DateTime(1981, 4, 25);
            var startDate = new DateTime(1986, 1, 1);
            var lastVisitDate = new DateTime(2012, 4, 8);

            var m = new Membership();
            m.StartDate = Utility.ToISO86(startDate);
            m.LastVisitDate = Utility.ToISO86(lastVisitDate);
            m.EndDate = "";
            m.IsActive = true;

            return new Member()
            {
                Id = 1,
                Name = name,
                DateOfBirth = Utility.ToISO86(dob),
                Address = address,
                Channels = clist,
                Membership = m
            };
        }

        public static Member SampleMemberTwo()
        {
            var name = new Name();
            name.First = "Bob";
            name.Last = "Smith";
            name.Middle = "Riley";

            var address = new Address();
            address.Street = "123 Elm St";
            address.Apt = String.Empty;
            address.City = "Indianapolis";
            address.State = "IN";
            address.Zip = 46225;

            var twitter = new MessageChannel();
            twitter.Name = Enums.MessageChannels.Twitter.ToString();
            twitter.Id = Enums.MessageChannels.Twitter;

            var email = new MessageChannel();
            email.Name = Enums.MessageChannels.Email.ToString();
            email.Id = Enums.MessageChannels.Email;

            var c1 = new Channel();
            c1.Type = twitter;
            c1.AccountId = "@bobsmith";
            c1.IsPreferred = false;
            c1.IsActive = true;
            c1.IsOptIn = true;

            var c2 = new Channel();
            c2.Type = email;
            c2.AccountId = "bobsmith@yahoo.com";
            c2.IsPreferred = true;
            c2.IsActive = true;
            c2.IsOptIn = true;

            var clist = new List<Channel>();
            clist.Add(c1);
            clist.Add(c2);

            var dob = new DateTime(1979, 9, 19);
            var startDate = new DateTime(2001, 4, 1);
            var lastVisitDate = new DateTime(2002, 11, 6);
            var endDate = new DateTime(2003, 1, 1);

            var m = new Membership();
            m.StartDate = Utility.ToISO86(startDate);
            m.LastVisitDate = Utility.ToISO86(lastVisitDate);
            m.EndDate = Utility.ToISO86(endDate);
            m.IsActive = false;

            return new Member()
            {
                Id = 2,
                Name = name,
                DateOfBirth = Utility.ToISO86(dob),
                Address = address,
                Channels = clist,
                Membership = m
            };
        }
    }
}