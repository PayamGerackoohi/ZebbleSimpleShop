using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using Zebble;

namespace UI
{
    public static class NavBarUtil
    {
        public static void AddShadow(this View self, int y = 0)
        {
            self.BoxShadow(xOffset: 0, yOffset: y, blurRadius: 7, expand: -5, color: Colors.DarkGray);
        }

        public static void SetupContent(View content, View navBar, View bottomNavBar)
        {
            //var offset = 20; // todo: No idea what this number is for! Remove it ASAP.
            content.Margin.Top.BindTo(navBar.Height);
            //content.Height.BindTo(View.Root.Height, h => h - navBar.CalculateTotalHeight() - bottomNavBar.CalculateTotalHeight());
            //content.Height.BindTo(navBar.Height, nbh => View.Root.CalculateTotalHeight() - nbh - bottomNavBar.CalculateTotalHeight());
            //content.Margin.Bottom.BindTo(bottomNavBar.Height, bh => bottomNavBar.CalculateTotalHeight() + offset);
        }

        public static void Trim(this NavigationBar navBar)
        {
            foreach (var ib in navBar.Left.AllChildren.OfType<IconButton>())
            {
                ib.TextView.Set(t =>
                {
                    if (t.Text == "Back")
                    {
                        t.Text = "<";
                        t.ScaleY(1.5f);
                    }
                }).On(x => x.Tapped, () => Zebble.Mvvm.ViewModel.Back());
            }
        }
    }

    public class GradientUtil
    {
        public static GradientColor BottomNavShadow = new(Colors.Transparent, new Color(0, 0, 0, 70), 0);
        public static GradientColor TopNavShadow = new(new Color(0, 0, 0, 70), Colors.Transparent, 0);
    }

    public class Paths
    {
        public const string ImageDir = "Images/";
        public const string IconsDir = ImageDir + "Icons/";

        public class Icons
        {
            public const string Cart = IconsDir + "cart.png";
            public const string Category = IconsDir + "category.png";
            public const string Category2 = IconsDir + "category2.png";
            public const string Eye = IconsDir + "eye.png";
            public const string Eye2 = IconsDir + "eye2.png";
            public const string Gift = IconsDir + "gift.png";
            public const string Gift2 = IconsDir + "gift2.png";
            public const string Heart = IconsDir + "heart.png";
            public const string HeartGray = IconsDir + "heart-gray.png";
            public const string Home = IconsDir + "home.png";
            public const string Icon = IconsDir + "icon.png";
            public const string Menu = IconsDir + "menu.png";
            public const string New = IconsDir + "new.png";
            public const string New2 = IconsDir + "new2.png";
            public const string NotFound = IconsDir + "not-found.png";
            public const string Profile = IconsDir + "profile.png";
            public const string Search = IconsDir + "search.png";
            public const string Shoes = IconsDir + "shoes.png";
            public const string Spinner = IconsDir + "spinner.png";
            public const string Star = IconsDir + "star.png";
            public const string Star2 = IconsDir + "star2.png";
            public const string StarOutline = IconsDir + "star-outline.png";
        }
    }
}
