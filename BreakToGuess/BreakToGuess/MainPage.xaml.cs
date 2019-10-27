using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace BreakToGuess
{
    public interface IAudioService
    {
        void PlayAudioFile(string fileName);
    }
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string options = await DisplayActionSheet(
                "Settings",
                "Cancel",
                null,
                "Music",
                "No Music",
                "Volume",
                "No Volume",
                "Ball Color");
            if (options == "Music")
            {
                DependencyService.Get<IAudioService>().PlayAudioFile("musique.mp3");
            }
            else if (options == "No Music")
            {
                
            }

            if (options == "Ball Color")
            {
               await Navigation.PushAsync(new BallSelectionPage());
            }

        }
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LevelsPage());
        }
    }
}
