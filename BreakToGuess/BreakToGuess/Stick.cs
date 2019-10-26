using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Stick
    {
        static double posY;
        static /*volatile*/ double posX;
        public static BoxView platform = new BoxView 
        {
            BackgroundColor = Color.Black,
            ScaleX = 3,
            ScaleY = 0.5,
            
        };
        static double Height = App.Current.MainPage.Height;
        static double Width = App.Current.MainPage.Width;

        static public double getY()
        {
            return posY;
        }

        static public double getX()
        {
            return posX;

        }
        public static void Initialize()
        {
            //posX = platform.X;
            //posY = platform.Y;
            posX = 0.5 * Width;
            posY = 0.8 * Height;
            Debug.WriteLine("Width " + Width + " posx " + posX + " Height " + Height + " posY " + posY);
            platform.BackgroundColor = Color.Black;
            try
            {
                Accelerometer.Start(SensorSpeed.UI);
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }
            catch { }
        }

        private static void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            double d = e.Reading.Acceleration.X;
            posX -= d * 60;
            if (posX + platform.Width < Width || posX > 0) platform.TranslateTo(posX, posY);
            if (posX < 0) posX = 0;
            if (posX + platform.Width > Width) posX = Width - platform.Width;
        }
    }
}