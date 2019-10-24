using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Platform
    {
        
        private Image sprite;

        public Platform()
        {
            sprite = new Image {Source = ImageSource.FromFile("platform.png")};
            sprite.TranslateTo(500, 500);
        }

        public Image get_sprite()
        {
            return sprite;
        }

        public double get_posX()
        {
            return sprite.X;
        }

        public double get_posY()
        {
            return sprite.Y;
        }

        public void move_platform(double newX, double newY)
        {

            sprite.TranslateTo(newX, newY, 1);
        }

        public void setX(double newX)
        {
            sprite.TranslationX = newX;
        }

    }
}
