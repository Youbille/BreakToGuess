using System;
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
            sprite.TranslateTo(X, Y, 60);
        }
        public bool handle_collisions(double xLayoutLimit,double yLayoutLimit, Stick platform)
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
            if ( Y >Stick.getY())
            {
                
                if ((Y >= Stick.getY() + 3 || Y <= Stick.getY() - 3) && X + sprite.Width > Stick.getX() && X  < Stick.getX() + Stick.platform.Width)
                {
                    speedY = -speedY;
                    set_pos(this.X, Stick.getY() - sprite.Height);
                }
                else if(Y + sprite.Height > yLayoutLimit)
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
                        setY(brick.get_posY() + brick.get_height());
                        collides = true;
                    }
                    else if (Y <= brick.get_posY() && Y + sprite.Height > brick.get_posY() && speedY >= 0)
                    {
                        Debug.WriteLine("collision toppom");
                        speedY = -speedY;
                        setY(brick.get_posY() - sprite.Height);
                        collides = true;
                    }
                }
                if (Y + sprite.Height > brick.get_posY() && Y < brick.get_posY() + brick.get_height())
                {
                    if (X < brick.get_posX() && X + sprite.Width > brick.get_posX() && speedX >= 0)
                    {
                        Debug.WriteLine("collision : leftside");
                        speedX = -speedX;
                        setX(brick.get_posX() - sprite.Width);
                        collides = true;
                    }
                    else if (X + sprite.Height > brick.get_posX() + brick.get_width() && X < brick.get_posX() + brick.get_width() && speedX <= 0)
                    {
                        Debug.WriteLine("collision : rightside"); //TODO : change collisions, right collides doesn't seem t owork properly
                        speedX = -speedX;
                        setX(brick.get_posX() + brick.get_width());
                        collides = true;
                    }
                }
            }
            return collides;
        }//Not perfectly operational, still should test some things

    }
}
