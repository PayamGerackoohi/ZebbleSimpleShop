using Domain.Models.Interfaces;
using Domain.Utils;
using Olive;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [Table("user")]
    public class User : IValidable, ICopyable<User>
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = "";

        [Column("last_name")]
        public string LastName { get; set; } = "";

        [Ignore]
        public string FullName { get => FirstName + " " + LastName; }

        [ForeignKey(typeof(Address))]
        [Column("address_fk")]
        public int AddressId { get; set; }

        [OneToOne]
        [Column("address")]
        public Address Address { get; set; } = new Address();

        [Column("email")]
        public string Email { get; set; } = "";

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = "";

        [Column("photo")]
        public byte[] Photo { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; } = Gender.Unknown;

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        //[Column("fav_count")]
        //public int FavCount { get; set; } = -1;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [Column("favorite_list")]
        public List<Product> Favorites { get; set; } = new();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [Column("order_list")]
        public List<Order> Orders { get; set; } = new();

        [ForeignKey(typeof(Credential))]
        [Column("credential_fk")]
        public int CredentialId { get; set; }

        [OneToOne]
        [Column("credential")]
        public Credential Credential { get; set; } = new();

        #endregion

        #region Public Methods

        public string BirthDateString() => BirthDate?.FormattedDate() ?? "";

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
            user.Photo = new List<byte>(Photo).ToArray();
            return user;
        }
        #endregion
    }
}
