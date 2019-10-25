using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Brick
    {
        private BoxView brick;
        private double X;
        private double Y;
        private Color color;
        public Brick(Color color_brick, double posX, double posY)
        {
            color = color_brick;
            brick = new BoxView
            {
                Color = color,
                WidthRequest = 30,
                HeightRequest = 10,
            };
            X = posX;
            Y = posY;
        }
        public BoxView get_boxView()
        {
            return brick;
        }

        public double get_posX()
        {
            return X;
        }
        public double get_posY()
        {
            return Y;
        }

        public double get_height()
        {
            return brick.Height;
        }
        public double get_width()
        {
            return brick.Width;
        }
         public  void draw()
         {
             brick.TranslateTo(X, Y, 1);
         }

    }
}
