using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zebble;

namespace UI
{
    public static class Extensions
    {
        private class TextInputActivationHandler
        {
            public const string Key = "TextInputActivationHandler";
            public Action FocusedChangedByInput { get; set; } = () => { };
            public Action UserTextChanged { get; set; } = () => { };
        }

        public static async Task NotifyUser(this string self) => await Alert.Show(self);

        public static void Toast(this string self) => Alert.Toast(self, false).RunInParallel();

        public static View MatchHeight(this View self, View view) => self.Set(s => s.Height.BindTo(view.Height));

        public static void Deactivate(this TextInput self) => self.Activate(false);

        public static void Activate(this TextInput self, bool activate = true)
        {
            self.Also(x =>
            {
                x.CssClass = activate ? "" : "DisabledText";
                if (!self.Data.ContainsKey(TextInputActivationHandler.Key))
                    self.Data[TextInputActivationHandler.Key] = new TextInputActivationHandler();
                var handler = (TextInputActivationHandler)self.Data[TextInputActivationHandler.Key];
                x.Focused.ChangedByInput -= handler.FocusedChangedByInput;
                x.UserTextChanged.Event -= handler.UserTextChanged;
                if (!activate)
                {
                    handler.FocusedChangedByInput = () => x.UnFocus();
                    x.Focused.ChangedByInput += handler.FocusedChangedByInput;
                    var text = x.Text;
                    handler.UserTextChanged = () => x.Text = text;
                    x.UserTextChanged.Event += handler.UserTextChanged;
                }
            });
        }
    }
}
