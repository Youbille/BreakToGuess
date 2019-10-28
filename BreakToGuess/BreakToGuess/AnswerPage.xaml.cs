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
    public partial class AnswerPage : ContentPage
    {
        private Entry findImage;
        public static string answerInput;
        public AnswerPage()
        {
            InitializeComponent();
            findImage = this.FindByName<Entry>("GuessEntry");

        }
        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            answerInput = findImage.Text;
            bool exit = await DisplayAlert("", "Are you sure about your answer ?", "Yes", "No");
            if (exit)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}