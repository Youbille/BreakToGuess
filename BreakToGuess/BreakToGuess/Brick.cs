using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Brick
    {
        private BoxView brick;

        public Brick(Color color_brick, double posX, double posY)
        {
            brick = new BoxView
            {
                
                Color = color_brick,
                WidthRequest = 30,
                HeightRequest = 10,
                AnchorX = posX,
                AnchorY = posY
            };
        }
        public BoxView get_boxView()
        {
            return brick;
        }

        public double get_posX()
        {
            return brick.X;
        }
        public double get_posY()
        {
            return brick.Y;
        }

        public double get_height()
        {
            return brick.Height;
        }
        public double get_width()
        {
            return brick.Width;
        }


    }
}
