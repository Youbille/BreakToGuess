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
    public partial class BallSelectionPage : ContentPage
    {
        public BallSelectionPage()
        {
            InitializeComponent();
        }

        private void FootBall_OnClicked(object sender, EventArgs e)
        {
            Page1.ball_name = "footBall.png";
            footBall.BackgroundColor = Color.Black;
            tennisBall.BackgroundColor = Color.Transparent;
            frenchBall.BackgroundColor = Color.Transparent;
            poolBall.BackgroundColor = Color.Transparent;
            italyBall.BackgroundColor = Color.Transparent;
        }

        private void TennisBall_OnClicked(object sender, EventArgs e)
        {
            Page1.ball_name = "tennisBall.png";
            tennisBall.BackgroundColor = Color.Black;
            frenchBall.BackgroundColor = Color.Transparent;
            poolBall.BackgroundColor=Color.Transparent;
            italyBall.BackgroundColor = Color.Transparent;
            footBall.BackgroundColor = Color.Transparent;
        }

        private void PoolBall_OnClicked(object sender, EventArgs e)
        {
            Page1.ball_name = "poolBall.png";
            poolBall.BackgroundColor = Color.Black;
            tennisBall.BackgroundColor = Color.Transparent;
            frenchBall.BackgroundColor = Color.Transparent;
            italyBall.BackgroundColor = Color.Transparent;
            footBall.BackgroundColor = Color.Transparent;
        }

        private void FrenchBall_OnClicked(object sender, EventArgs e)
        {
            Page1.ball_name = "frenchBall.png";
            frenchBall.BackgroundColor = Color.Black;
            tennisBall.BackgroundColor = Color.Transparent;
            poolBall.BackgroundColor = Color.Transparent;
            italyBall.BackgroundColor = Color.Transparent;
            footBall.BackgroundColor = Color.Transparent;
        }

        private void ItalyBall_OnClicked(object sender, EventArgs e)
        {
            Page1.ball_name = "italyBall.png";
            italyBall.BackgroundColor = Color.Black;
            tennisBall.BackgroundColor = Color.Transparent;
            frenchBall.BackgroundColor = Color.Transparent;
            poolBall.BackgroundColor = Color.Transparent;
            footBall.BackgroundColor = Color.Transparent;
        }

        private void Return_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}