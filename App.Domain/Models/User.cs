using Domain.Models.Interfaces;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    class User : IValidable, ICopyable<User>
    {
        private static int GlobalId = 0;
        public Guid Id { get; private set; } = (++GlobalId).ToGuid();
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName { get => FirstName + " " + LastName; }
        public Address Address { get; set; } = new Address();
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public byte[] Photo { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public DateTime? BirthDate { get; set; }
        public List<Product> Favorites { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public Credential Credential { get; set; } = new();

        public string BirthDateString() => BirthDate?.FormattedDate();

        public override string ToString() => new string[] {
            $"FullName: {FullName}",
            $"Address: {Address}",
            $"Email: {Email}",
            $"PhoneNumber: {PhoneNumber}",
            $"Gender: {Gender}",
            $"BirthDate: {BirthDate: dd/MM/yyyy}",
        }.ToString("\n");

        public bool IsValid() =>
            new bool[] {
                !FirstName.None(),
                !LastName.None(),
                Address.IsValid(),
                !Email.None(),
                !PhoneNumber.None(),
                BirthDate.HasValue,
            }.And();

        public override bool Equals(object obj) => obj != null && obj is User o && o.Id == Id;

        public override int GetHashCode()
        {
            int hashCode = -1139119937;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullName);
            hashCode = hashCode * -1521134295 + EqualityComparer<Address>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(Photo);
            hashCode = hashCode * -1521134295 + Gender.GetHashCode();
            hashCode = hashCode * -1521134295 + BirthDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Product>>.Default.GetHashCode(Favorites);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Order>>.Default.GetHashCode(Orders);
            return hashCode;
        }

        public User Copy()
        {
            var user = (User)MemberwiseClone();
            user.Address = Address.Copy();
            user.Favorites = new List<Product>(Favorites);
            user.Orders = new List<Order>(Orders);
            //public byte[] Photo { get; set; }
            return user;
        }
    }
}
