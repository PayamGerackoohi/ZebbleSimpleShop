using Domain.Models.Interfaces;
using Domain.Utils;
using Olive;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [Table("address")]
    public class Address : IValidable, ICopyable<Address>
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("country")]
        public string Country { get; set; } = "";

        [Column("state")]
        public string State { get; set; } = "";

        [Column("city")]
        public string City { get; set; } = "";

        [Column("street_address")]
        public string StreetAddress { get; set; } = "";

        [Column("zip_code")]
        public string ZipCode { get; set; } = "";

        #endregion

        #region Public Methods

        public Address Copy() => (Address)MemberwiseClone();

        public bool IsValid() => new bool[] {
            !Country.None(),
            !State.None(),
            !City.None(),
            !StreetAddress.None(),
        }.And();

        public override string ToString() => $"{StreetAddress}, {City}, {State} {ZipCode}, {Country}";

        #endregion
    }
}
