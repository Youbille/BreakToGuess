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

        public Ball(string name_of_file, int speed)
        {
            sprite = new Image { Source = ImageSource.FromFile(name_of_file) };
            speedX = speed;
            speedY = speed;
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
            //sprite.TranslateTo(newX, newY, 1);//It looks like "translateTo doesn't inherently change the position of the image
        }

        public void set_speed(int new_speedX, int new_speedY)
        {
            speedX = new_speedX;
            speedY = new_speedY;
        }
        public void move(double incrementedX, double incrementedY)
        {
            
            set_pos(incrementedX, incrementedY);
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
            sprite.TranslateTo(X, Y, 1);
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
            if (Y + sprite.Height > platform.Y + platform.Height)
            {
                if (X + sprite.Width > platform.X && X < platform.X + platform.Width)
                {
                    speedY = -speedY;
                    set_pos(this.X, 0);
                }
                else
                {
                    under_platform = true;
                }
            }
            return under_platform;
        }

        public void maybe_simpler_brickcoll_interrogation_point(double actualX, double actualY, Brick brick)
        {
            //if (IsCloseEnough(actualX,actualY,brick))
            {
                if (actualX < brick.get_posX() + brick.get_width() && actualX + sprite.Width > brick.get_posX())
                {
                    if (actualY < brick.get_posY() + brick.get_height() && speedY <= 0)
                    {
                        Debug.WriteLine("collision");
                        speedY = -speedY;
                    }
                    
                    //if (actualY + sprite.Height > brick.get_posY() && speedY >= 0 )
                    //{
                    //    Debug.WriteLine("collision");
                    //    speedY = -speedY;
                    //}
                    if (actualX + sprite.Width > brick.get_posX() && actualY < brick.get_posY() + brick.get_height() && actualY + sprite.Height < brick.get_height() )
                    {
                        Debug.WriteLine("collision : leftside");
                        speedX = -speedX;
                    }

                }
            }
        }
        //public double handle_Y_brick_collision(double Y, double X, Brick brick)
        //{
        //    //if (IsCloseEnough(X, Y, brick))
        //    {
        //        if
        //        ((Y + sprite.Height > brick.get_posY() &&
        //          X + sprite.Width > brick.get_posX() &&
        //          X < brick.get_posX() + brick.get_boxView().Width) ||
        //         (Y < brick.get_posY() + brick.get_boxView().Height &&
        //          X + sprite.Width > brick.get_posX() &&
        //          X < brick.get_posX() + brick.get_boxView().Width))
        //       // ((Y + sprite.Height > brick.get_posY() &&
        //       // X + sprite.Width > brick.get_posX() &&
        //       // X < brick.get_posX() + brick.get_boxView().Width) ||
        //       //(Y < brick.get_posY() + brick.get_boxView().Height &&
        //       // X + sprite.Width > brick.get_posX() &&
        //       // X < brick.get_posX() + brick.get_boxView().Width) ||
        //       //     (X + sprite.Width > brick.get_posX() && Y + sprite.Height > brick.get_posY() &&
        //       //      Y < brick.get_posY() + brick.get_boxView().Height) ||
        //       //     (X < brick.get_posX() + brick.get_boxView().Width &&
        //       //      Y + sprite.Height > brick.get_posY() &&
        //       //      Y < brick.get_posY() + brick.get_boxView().Height))
        //        {
        //            if (speedY >= 0)
        //            {
        //                speedY = -speedY;
        //                //set_pos(X, (brick.get_posY() - sprite.Height));
        //                Debug.WriteLine("collision,speedY > 0" + " and brick Y position : " + brick.get_posY() + "\n" + "Ball position" + Y);
        //                //Y = brick.get_posY() - sprite.Height;
        //            }
        //            else if (speedY <= 0)
        //            {
        //                speedY = -speedY;
        //                //set_pos(X, (brick.get_posY() + brick.get_boxView().Height));
        //                Debug.WriteLine("collision, speedY < 0" + " and brick Y position : " + brick.get_posY() + "\n" + "Ball position" + Y);
        //                //Y = brick.get_posY() + brick.get_boxView().Height; 
        //            }
        //        }
        //    }

        //    return Y;
        //}
        //public double handle_north_brick_collision(double Y, double X, Brick brick)
        //{
        //    if (speedY >= 0)
        //    {
        //        if (Y + sprite.Height > brick.get_boxView().Y &&
        //            X + sprite.Width > brick.get_posX() && X < brick.get_posX() + brick.get_boxView().Width)
        //        {
        //            speedY = -speedY;
        //            set_pos(X, (brick.get_posY() - sprite.Height));
        //            //TODO : find why the two speedY conflict each other (maybe position updated poorly or distance not well rendered)
        //            Debug.WriteLine("collision");
        //            Y = brick.get_posY() - sprite.Height;
        //        }
        //    }
        //    return Y;
        //}

        //public double handle_south_brick_collision(double Y, double X, Brick brick)
        //{
        //    if (speedY <= 0)
        //    {
        //        if (Y < brick.get_posY() + brick.get_boxView().Height &&
        //            X + sprite.Width > brick.get_posX() &&
        //            X < brick.get_posX() + brick.get_boxView().Width)
        //        {
        //            //Debug.WriteLine(Y);
        //            speedY = -speedY;
        //            set_pos(X, (brick.get_posY() + brick.get_boxView().Height));
        //            Debug.WriteLine("collision");
        //            Y = brick.get_posY() + brick.get_boxView().Height;

        //        }
        //    }

        //    return Y;
        //}
        //public void handle_brick_collisions(double X, double Y, Brick brick)//This huge chunk of test is just to calculate if the actual distance is less than the maximum distance
        //{//This test needs to be heavily refactored by extracting a proper method
        //    if (IsCloseEnough(X, Y, brick))
        //    {
        //        if (speedX > 0)//may be an issue if speed is exactly equals to 0
        //        {
        //            if (X + sprite.Width > brick.get_posX() && Y + sprite.Height > brick.get_posY() && Y < brick.get_posY() + brick.get_boxView().Height)
        //            {
        //                speedX = -speedX;
        //                //set_pos(brick.get_posX() - sprite.Width, Y);
        //            }
        //        }
        //        else
        //        {
        //            if (X < brick.get_posX() + brick.get_boxView().Width && Y + sprite.Height > brick.get_posY() && Y < brick.get_posY() + brick.get_boxView().Height)
        //            {
        //                speedX = -speedX;
        //                //set_pos(brick.get_posX() + brick.get_boxView().Width, Y);
        //            }
        //        }

        //        if (speedY >= 0)//may be an issue if speed is exactly equals to 0
        //        {
        //            if (Y + sprite.Height > brick.get_boxView().Y &&
        //                X + sprite.Width > brick.get_posX() && X < brick.get_posX() + brick.get_boxView().Width)
        //            {
        //                speedY = -speedY;
        //                set_pos(X, (brick.get_posY() - sprite.Height));
        //                //TODO : find why the two speedY conflict each other (maybe position updated poorly or distance not well rendered)
        //                Debug.WriteLine("collision");
        //            }
        //        }
        //        else if (speedY <= 0)
        //        {
        //            if (Y < brick.get_posY() + brick.get_boxView().Height &&
        //                X + sprite.Width > brick.get_posX() &&
        //                X < brick.get_posX() + brick.get_boxView().Width)
        //            {
        //                //Debug.WriteLine(Y);
        //                speedY = -speedY;
        //                set_pos(X, (brick.get_posY() + brick.get_boxView().Height));
        //                Debug.WriteLine("collision");
        //            }
        //        }
        //    }

        //}

        private bool IsCloseEnough(double actual_posX, double actual_posY, Brick brick)
        {
            return Math.Sqrt(
                       Math.Pow(brick.get_posX() + brick.get_boxView().Width / 2 - (actual_posX + sprite.Width / 2),
                           2) +
                       Math.Pow(brick.get_posY() + brick.get_boxView().Height / 2 - (actual_posY + sprite.Height / 2),
                           2)) <
                   Math.Sqrt(Math.Pow(sprite.Height / 2, 2) + Math.Pow(sprite.Width / 2, 2)) + Math.Sqrt(
                                                                                                 Math.Pow(
                                                                                                     brick.get_boxView()
                                                                                                         .Height / 2, 2))
                                                                                             + Math.Pow(
                                                                                                 brick.get_boxView()
                                                                                                     .Width / 2, 2);
        }
    }
}
