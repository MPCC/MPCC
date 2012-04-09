using System;
using System.Collections.Generic;
using Rest.Objects;
using Rest.Routes;

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
            Name name = new Name();
            name.First = "Eric";
            name.Last = "Jones";
            name.Middle = "Who Cares";

            Address address = new Address();
            address.Street = "1234 East St.";
            address.City = "Greenwood";
            address.State = "IN";
            address.Zip = 46142;

            Channel c1 = new Channel();
            c1.ChannelName = MessageChannels.Twitter.ToString();
            c1.AccountId = "@erjjones";
            c1.ChannelId = MessageChannels.Twitter;
            c1.IsPreferred = true;
            c1.IsActive = true;
            c1.IsOptIn = true;

            Channel c2 = new Channel();
            c2.ChannelName = MessageChannels.Email.ToString();
            c2.AccountId = "erjjones@gmail.com";
            c2.ChannelId = MessageChannels.Email;
            c2.IsPreferred = false;
            c2.IsActive = true;
            c2.IsOptIn = true;

            List<Channel> clist = new List<Channel>();
            clist.Add(c1);
            clist.Add(c2);

            var dob = new DateTime(1981, 4, 25);

            return new Member()
            {
                Id = 1,
                Name = name,
                DateOfBirth = Utility.ToISO86(dob),
                Address = address,
                Channels = clist
            };
        }

        public static Member SampleMemberTwo()
        {
            Name name = new Name();
            name.First = "Bob";
            name.Last = "Smith";
            name.Middle = "Riley";

            Address address = new Address();
            address.Street = "123 Elm St";
            address.City = "Indianapolis";
            address.State = "IN";
            address.Zip = 46225;

            Channel c1 = new Channel();
            c1.ChannelName = MessageChannels.Twitter.ToString();
            c1.AccountId = "@bobsmith";
            c1.ChannelId = MessageChannels.Twitter;
            c1.IsPreferred = false;
            c1.IsActive = true;
            c1.IsOptIn = true;

            Channel c2 = new Channel();
            c2.ChannelName = MessageChannels.Email.ToString();
            c2.AccountId = "bobsmith@yahoo.com";
            c2.ChannelId = MessageChannels.Email;
            c2.IsPreferred = true;
            c2.IsActive = true;
            c2.IsOptIn = true;

            List<Channel> clist = new List<Channel>();
            clist.Add(c1);
            clist.Add(c2);

            var dob = new DateTime(1979, 9, 19);

            return new Member()
            {
                Id = 2,
                Name = name,
                DateOfBirth = Utility.ToISO86(dob),
                Address = address,
                Channels = clist
            };
        }
    }
}