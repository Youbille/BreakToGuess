using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreakToGuess
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelsPage : ContentPage
    {
        public LevelsPage()
        {
            InitializeComponent();
        }

        private void Level1_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(6,7,Color.DarkOrange, Color.Red));
        }

        private void Level2_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(6, 7, Color.Blue, Color.BlueViolet));
        }

        private void Level3_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(6, 7, Color.DarkGreen, Color.ForestGreen));
        }

        private void Return_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}