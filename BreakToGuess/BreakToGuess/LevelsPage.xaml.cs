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
            Navigation.PushModalAsync(new Page1(3,5,Color.DarkOrange, Color.Red,"Da Vinci","DaVinci.jpg"));
        }

        private void Level2_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(2, 4, Color.Blue, Color.BlueViolet,"blue","DaVinci.jpg"));
        }

        private void Level3_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1(10, 20, Color.DarkGreen, Color.ForestGreen,"green","DaVinci.jpg"));
        }

        //private void Return_OnClicked(object sender, EventArgs e)
        //{
        //    Navigation.PopModalAsync();
        //}
    }
}