﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Ball
    {
        private Image sprite;
        private int speedX;
        private int speedY;
        private double X;
        private double Y;

        public Ball(string name_of_file, int speed, double initX, double initY)
        {
            sprite = new Image { Source = ImageSource.FromFile(name_of_file) };
            speedX = speed;
            speedY = speed;
            X = initX;
            Y = initY;
        }

        public int get_speedX()
        {
            return speedX;
        }
        public int get_speedY()
        {
            return speedY;
        }

        public Image get_sprite()
        {
            return sprite;
        }
        public void set_pos(double newX, double newY)
        {
            X = newX;
            Y = newY;
        }

        public void set_speed(int new_speedX, int new_speedY)
        {
            speedX = new_speedX;
            speedY = new_speedY;
        }
        public double getX()
        {
            return X;
        }

        public void setX(double newX)
        {
            X = newX;
        }

        public double getY()
        {
            return Y;
        }
        public void setY(double newY)
        {
            Y = newY;
        }

        public void draw()
        {
            sprite.TranslateTo(X, Y, 50);
        }
        public bool handle_collisions(double xLayoutLimit, Image platform)
        {
            bool under_platform = false;
            if (X < 0)
            {
                speedX = -speedX;
                set_pos(0, this.Y);
            }
            if (X + sprite.Width > xLayoutLimit)
            {
                speedX = -speedX;
                set_pos(xLayoutLimit - sprite.Width, this.Y);
            }
            if (Y < 0)
            {
                speedY = -speedY;
                set_pos(this.X, 0);
            }
            if (platform.Height != -1 && Y + sprite.Height > platform.Y + platform.Height)
            {
                if (X + sprite.Width > platform.X && X < platform.X + platform.Width)
                {
                    speedY = -speedY;
                    set_pos(this.X, platform.Y - sprite.Height);
                }
                else
                {
                    under_platform = true;
                }
            }
            return under_platform;
        }

        public bool brick_collision(Brick brick)
        {
            bool collides = false;
            {
                if (X < brick.get_posX() + brick.get_width() && X + sprite.Width > brick.get_posX())
                {
                    if (Y + sprite.Height > brick.get_posY() + brick.get_height() && Y < brick.get_posY() + brick.get_height() && speedY <= 0)
                    {
                        Debug.WriteLine("collision bottom");
                        speedY = -speedY;
                        collides = true;
                    }
                    else if (Y <= brick.get_posY() && Y + sprite.Height > brick.get_posY() && speedY >= 0)
                    {
                        Debug.WriteLine("collision toppom");
                        speedY = -speedY;
                        collides = true;
                    }

                }
                else if (Y + sprite.Height > brick.get_posY() && Y < brick.get_posY() + brick.get_height())
                {
                    if (X < brick.get_posX() && X + sprite.Width > brick.get_posX() && speedX >= 0)
                    {
                        Debug.WriteLine("collision : leftside");
                        speedX = -speedX;
                        collides = true;
                    }
                    else if (X + sprite.Height > brick.get_posX() + brick.get_height() && X < brick.get_posX() + brick.get_width() && speedX <= 0)
                    {
                        Debug.WriteLine("collision : rightside");
                        speedX = -speedX;
                        collides = true;
                    }

                }
            }
            return collides;
        }//Not perfectly operational, still should test some things

    }
}
