using Olive;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [Table("credential")]
    public class Credential : IValidable
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = "";

        [Column("password")]
        public string Password { get; set; } = "";

        [Column("stay_logged_in")]
        public bool StayLoggedIn { get; set; }

        #endregion

        #region Public Methods

        public bool IsValid() => !Username.None() && !Password.None();

        public override string ToString() => new string[] {
            $"Username: {Username}",
            $"Password: {Password}",
            $"StayLoggedIn: {StayLoggedIn}",
        }.ToString("\n");

        public bool IsAuthenticated(string username, string password) => username == Username && password == Password;

        #endregion
    }
}
