using System;
using System.Runtime.Serialization;

namespace TicketTrader.Shared.Base.DDD
{
    [DataContract]
    public class Id
    {
        [DataMember]
        public string Identifier { get; protected set; }

        protected Id()
        {
            
        }

        protected Id(string identifier)
        {
            Identifier = identifier;
        }

        public static Id New()
        {
            return new Id(Guid.NewGuid().ToString());
        }


        public static Id From(string idString)
        {
            return new Id(idString);
        }

        public static Id From(int idInt)
        {
            return new Id(idInt.ToString());
        }

        public override string ToString()
        {
            return Identifier;
        }

        protected bool Equals(Id other)
        {
            return string.Equals(Identifier, other.Identifier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Id)obj);
        }

        public override int GetHashCode()
        {
            return (Identifier != null ? Identifier.GetHashCode() : 0);
        }
    }
}