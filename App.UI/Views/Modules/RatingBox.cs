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

namespace UI.Modules
{
    partial class RatingBox
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            var rate = Model.GetRating();
            for (var i = 1; i <= 5; i++)
            {
                string path;
                if (i > rate)
                    path = Paths.Icons.StarOutline;
                else
                    path = Paths.Icons.Star;
                await Holder.Add(new ImageView { CssClass = "RatingIcon", Path = path });
            }
            await Holder.Add(new TextView { Id = "RateText" }.Bind("Text", () => Model.Data, d => d.RatingText()));
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
        }
    }
}
