using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zebble;

namespace UI
{
    public static class Extensions
    {
        public static async Task NotifyUser(this string self) => await Alert.Show(self);
        public static void Toast(this string self) => Alert.Toast(self, false).RunInParallel();
        public static View MatchHeight(this View self, View view) => self.Set(s => s.Height.BindTo(view.Height));
    }
}
