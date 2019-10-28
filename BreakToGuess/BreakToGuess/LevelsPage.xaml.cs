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
            Navigation.PushModalAsync(new Page1(1, 50, Color.DarkOrange, Color.Red, "da vinci", "DaVinci.jpg"));
        }

        private void Level2_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(5, 20, Color.Blue, Color.BlueViolet, "china", "chinaWall.jpg"));
        }

        private void Level3_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(7, 15, Color.DarkGreen, Color.ForestGreen, "volleyball",
                "Volleyball.jpg"));
        }
    }
}