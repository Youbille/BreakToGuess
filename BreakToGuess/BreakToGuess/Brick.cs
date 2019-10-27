using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Brick
    {
        private BoxView brick;
        private double X;
        private double Y;
        private Color color;
        private int damage;
        private double trueHeight;
        private double trueWidth;
        //private SynchronizationContext context;
        public Brick(Color color_brick, double posX, double posY, double reqHeight, double reqWidth)
        {
            //context = SynchronizationContext.Current;
            damage = 0;
            color = color_brick;
            trueHeight = reqHeight;
            trueWidth = reqWidth;
            brick = new BoxView
            {
                Color = color,
                WidthRequest = trueWidth,
                HeightRequest = trueHeight,
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
        public void setX(double newX)
        {
            X = newX;
        }
        public void setY(double newY)
        {
            Y = newY;
        }

        public double get_posY()
        {
            return Y;
        }

        public double get_height()
        {
            return trueHeight;
        }
        public double get_width()
        {
            return trueWidth;
        }
        public void draw()
        {
            brick.TranslateTo(X, Y, 0);
        }

        public void collision_with_ball(bool ball_collides)
        {
            //context.Post(new SendOrPostCallback(this), brick);
            if (ball_collides)//SOlutions to modify the color : each color diminishes but we test for each color/we find something general
            {
                damage++; //I must try to understand the syncContext class in order to change the color properly
                //if (damage == 1) brick.Color = Color.Red;
                //if (damage == 2) brick.Color = Color.IndianRed;
                //if (damage == 3) 
                {
                    setX(5000);//Is this cheating ? You decide
                    setY(5000);
                }

            }
        }
    }
}
