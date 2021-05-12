using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
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

        public static void Toggle(this CheckBox self) => self.Checked = !self.Checked;

        public static void AddShadow(this View self, int yOffset = 0)
        {
            self.BoxShadow(xOffset: 0, yOffset: yOffset, blurRadius: 7, expand: -5, color: Colors.DarkGray);
        }

        public static void Trim(this NavigationBar navBar, EzPage navHandler)
        {
            foreach (var ib in navBar.Left.AllChildren.OfType<IconButton>())
            {
                ib.TextView.Set(t =>
                {
                    if (t.Text == "Back")
                    {
                        t.Text = "<";
                        t.ScaleY(1.5f);
                        ib.On(x => x.Tapped, () => navHandler.OnBack());
                    }
                });
            }
        }
    }
}
