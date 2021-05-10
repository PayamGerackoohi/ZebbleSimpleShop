using Domain;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class HomePage
    {
        private readonly TextView CartBadge = new() { Id = "Badge", Text = "9+" };

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            await SetupUI();
            SetupViewModelBindings();
        }

        private void SetupViewModelBindings()
        {
            Model.CurrentPage.Changed += () => SetPage().RunInParallel();
            Model.Cart.Changed += () => SetCartBadge();
        }

        public void SetCartBadge()
        {
            int count = Model.Cart.Value.OrderItems.Count;

            if (count > 0)
            {
                CartBadge.Ignored = false;
                CartBadge.Text = $"{(count > 9 ? "9+" : count)}";
            }
            else
                CartBadge.Ignored = true;
        }

        private async Task SetPage(bool animate = true)
        {
            int page = (int)Model.CurrentPage.Value;
            await Body.MoveToSlide(page, animate);
            BottomNavBar.GetTabs().ElementAt(page).Selected = true;
        }

        private async Task SetupUI()
        {
            await SetupNavBars();
            SetupContent();
            await SetupCarousel();
            SetCartBadge();
        }

        private async Task SetupCarousel()
        {
            await Body.Add(new CategorySubPage().Set(x => x.Model.Holder = Model).MatchHeight(Body));
            await Body.Add(new MostVisitedSubPage().Set(x => x.Model.Holder = Model).MatchHeight(Body));
            await Body.Add(new PopularSubPage().Set(x => x.Model.Holder = Model).MatchHeight(Body));
            await Body.Add(new OfferSubPage().Set(x => x.Model.Holder = Model).MatchHeight(Body));
            await Body.Add(new NewSubPage().Set(x => x.Model.Holder = Model).MatchHeight(Body));
            Body.On(x => x.SlideChanged, () => OnSlideChanged());
        }

        private async Task OnSlideChanged()
        {
            Model.CurrentPage.Value = (PageType)Body.CurrentSlideIndex;
            await SetPage();
        }

        private async Task SetupNavBars()
        {
            await MakeTopNavBar();
            await SetupBottomNavBar();
        }

        private void SetupContent()
        {
            NavBarUtil.SetupContent(Body, NavBar, BottomNavBar);
        }

        private async Task SetupBottomNavBar()
        {
            BottomNavBar.AddShadow(-2);
            BottomNavBar.GetTabs().Do(tab => tab.On(x => x.Tapped, () => tab.Selected = true).On(x => x.SelectedChanged, () => OnTabSelectedChanged(tab)));
        }

        private void OnTabSelectedChanged(Tabs.Tab tab)
        {
            var active = tab.PseudoCssState.ContainsWholeWord("active");
            tab.Icon.Path(tab.Data[active ? "2" : "1"] as string);
            tab.Label.TextColor = active ? Colors.Red : Colors.Black;
        }

        private async Task MakeTopNavBar()
        {
            await NavBar.AddButton(ButtonLocation.Left,
                new Row { ClipChildren = false }.Set(c =>
                    {
                        c.Add(new ImageView { Path = Paths.Icons.Cart });
                        c.Add(CartBadge);
                    })
                    .On(v => v.Tapped, Model.OnCartTapped));
            await NavBar.AddButton(ButtonLocation.Left,
                new ImageView { Path = Paths.Icons.Search }
                    .Set(img => img.Css.Padding(4))
                    .On(v => v.Tapped, Model.OnSearchTapped)
            );
            await NavBar.AddButton(ButtonLocation.Right,
                new ImageView { Path = Paths.Icons.Profile }
                    .Set(img => img.Css.Padding(4))
                    .On(v => v.Tapped, Model.OnProfileTapped)
            );
        }

        override public async Task OnPreRender()
        {
            await Body.WhenShown(() => SetPage(false));
            await base.OnPreRender();
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
        }
    }
}
