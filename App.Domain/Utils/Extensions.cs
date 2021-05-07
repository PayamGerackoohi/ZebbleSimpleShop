using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Utils
{
    static class ObjectExtensions
    {
        // Kotlin: fun <T, R> T.let(block: (T) -> R): R
        public static R Let<T, R>(this T self, Func<T, R> block)
        {
            return block(self);
        }

        // Kotlin: fun <T> T.also(block: (T) -> Unit): T
        // Equivalent ot Set in ZebbleExtensions
        public static T Also<T>(this T self, Action<T> block)
        {
            block(self);
            return self;
        }
        // no C# equivalent for Kotlin apply is found :'(

        public static decimal Saturate(this decimal self, decimal min, decimal max) => Math.Max(min, Math.Min(max, self));

        public static DateTime ToDateTime(this string self) => self.To<DateTime>();

        public static Gender ToGender(this string self) => self.To<Gender>();

        public static bool And(this IEnumerable<bool> self)
        {
            foreach (var item in self)
                if (!item)
                    return false;
            return true;
        }

        public static bool Or(this IEnumerable<bool> self)
        {
            foreach (var item in self)
                if (item)
                    return true;
            return false;
        }

        public static string FormattedDate(this DateTime self) => self.ToString("dd/MM/yyyy");

        //public string FormattedPrice() => string.Format("{0:C} £", Price);
        public static string CurrencyFormat(this decimal self) => string.Format("{0:#,##0.##} £", self);
    }
}
