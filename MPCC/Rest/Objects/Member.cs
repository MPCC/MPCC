using System;
using System.Collections.Generic;

namespace Rest.Objects
{
    [Serializable]
    public class Member
    {
        public virtual int Id { get; set; }
        public virtual Name Name { get; set; }
        public virtual string DateOfBirth { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Channel> Channels { get; set; }
    }

    [Serializable]
    public class Name
    {
        public virtual string First { get; set; }
        public virtual string Middle { get; set; }
        public virtual string Last { get; set; }
    }

    [Serializable]
    public class Address
    {
        public virtual string Street { get; set; }
        public virtual string Apt { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual int Zip { get; set; }
    }

    public enum MessageChannels
    {
        Email,
        Facebook,
        LinkedIn,
        Mail,
        Phone,
        SMS,
        Twitter
    }

    [Serializable]
    public class Channel
    {
        public virtual MessageChannels ChannelId { get; set; }
        public virtual string ChannelName { get; set; }
        public virtual string AccountId { get; set; }
        public virtual bool IsPreferred { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsOptIn { get; set; }
    }
}