using Domain.Models.Interfaces;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    class Address : IValidable, ICopyable<Address>
    {
        public string Country { get; set; } = "";
        public string State { get; set; } = "";
        public string City { get; set; } = "";
        public string StreetAddress { get; set; } = "";
        public string ZipCode { get; set; } = "";

        public Address Copy() => (Address)MemberwiseClone();

        public bool IsValid() => new bool[] {
            !Country.None(),
            !State.None(),
            !City.None(),
            !StreetAddress.None(),
        }.And();

        public override string ToString() => $"{StreetAddress}, {City}, {State} {ZipCode}, {Country}";
    }
}
