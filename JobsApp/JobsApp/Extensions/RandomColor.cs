using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobsApp.Extensions
{
    public class RandomColor : IMarkupExtension
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            List<string> colors = new List<string>
            {
                "#ea80fc","#b388ff","#8c9eff","#82b1ff","#80d8ff",
                "#e1bee7","#d1c4e9","#c5cae9","#bbdefb","#b3e5fc",
                "#ffb2ff","#e7b9ff","#e6ceff","#ffc4ff","#d1d9ff"
            };

            Random r = new Random();
            return Color.FromHex(colors[r.Next(15)]);

        }
    }
}
