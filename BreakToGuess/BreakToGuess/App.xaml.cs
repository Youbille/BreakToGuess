using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreakToGuess
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new MainPage());
        }

        protected override void OnStart()
        {
            DependencyService.Get<IAudioService>().PlayAudioFile("musique.mp3");
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
