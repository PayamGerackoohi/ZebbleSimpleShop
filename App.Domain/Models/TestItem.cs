using Olive;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [Table("test_item")]
    public class TestItem
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        #endregion

        #region Public Methods

        public override string ToString() => new string[] {
            $"Id: {Id}",
            $"Name: {Name}",
        }.ToString("\n");

        #endregion
    }
}
