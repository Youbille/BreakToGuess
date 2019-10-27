﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreakToGuess
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private Stick stick = new Stick();
        private Ball ball;
        private Random rand_direction = new Random();
        private Brick templateBrick;
        private bool ball_brick_collision, ball_is_under_platform;
        private Brick[,] grid;
        private int lives, ball_delay;
        private Label livesLabel;
        public static string ball_name;
        public Page1(int width,int height, Color firstColor,Color secondColor)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                DisplayAlert("Exception", e.ToString(), "OK");
            }
            AbsoluteLayout lay = this.FindByName<AbsoluteLayout>("LayoutPrincipal");
            livesLabel = this.FindByName<Label>("LivesLabel");
            Stick.Initialize();
            lives = 3;
            ball = new Ball(ball_name,5,App.Current.MainPage.Height/2,App.Current.MainPage.Width/2);
            templateBrick=new Brick(Color.Red, 0,0, 30,70);
            lay.Children.Add(Stick.platform);
            lay.Children.Add(ball.get_sprite());
            //grid =new Brick[(int)App.Current.MainPage.Width/templateBrick.get_height(),(int)App.Current.MainPage.Width/templateBrick.get_width()];
            grid = new Brick[width,height];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Color color;
                    if((i+j)%2 ==0 ) color =firstColor;
                    else color = secondColor;
                    grid[i,j] = new Brick(color,j*templateBrick.get_width(),i*templateBrick.get_height(),templateBrick.get_height(),templateBrick.get_width());
                    lay.Children.Add(grid[i,j].get_boxView());
                }
            }
            Thread.Sleep(1000);
            Timer mainTimer = new Timer(timer_tick);
            mainTimer.Change(0, 33);
        }

        private void timer_tick(object state)
        {
            ball_delay--;
            if (ball_delay <= 0)
            {
                ball.setX(ball.getX() + ball.get_speedX());//Todo : Change Speed
                ball.setY(ball.getY() + ball.get_speedY());
                ball_is_under_platform = ball.handle_collisions(Width,Height, stick ); //the collisions handler says if the ball is under the platform or not
                //ball_brick_collision = ball.brick_collision(templateBrick);
                //templateBrick.collision_with_ball(ball_brick_collision);
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        ball_brick_collision = ball.brick_collision(grid[i, j]);
                        grid[i, j].collision_with_ball(ball_brick_collision);
                        ball_brick_collision = false;
                    }
                }
                whenBallGetUnderPlatform();
                if (lives == 0)
                {
                    triggerLoseView();
                }
                Device.BeginInvokeOnMainThread(mainDraw);
            }
        }

        private void triggerLoseView() //TODO  : handle the lives=0 case
        {
            //Label loseLabel = this.FindByName<Label>("DefeatLabel");
            //LivesLabel.Text = "You Lose !";
            ball.set_speed(0,0);
            Thread.Sleep(5000);
            Navigation.PopModalAsync();
            //string defeat = await DisplayActionSheet(
            //    "You lose !",
            //    "",
            //    null,
            //    "Retry",
            //    "Back to main menu",
            //    "Back to levels menu");
            //if (defeat == "retry")
            //{
            //    restartLevel();
            //}
        }

        private void restartLevel()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i,j].setX(j * templateBrick.get_width());
                    grid[i,j].setY(i * templateBrick.get_height());
                }
            }
            lives = 3;
            Thread.Sleep(2000);
            ball.set_pos(App.Current.MainPage.Width / 2, App.Current.MainPage.Height / 2);
        }

        private void mainDraw()
        {
            if (!ball_is_under_platform) ball.draw();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j].draw();
                }
            }
            livesLabel.Text = "Lives = " + lives;
        }

        private void whenBallGetUnderPlatform()
        {
            if (ball_is_under_platform)
            {
                ball.setX(Width / 2);
                ball.setY(3 * Height / 5);
                ball.set_speed(rand_direction.Next(-10, 10), -7);
                if (ball.get_speedX() > -5 && ball.get_speedX() < 5)
                {
                    ball.set_speed(10, ball.get_speedY());
                }
                if (ball.get_speedX() == 0 || ball.get_speedY() == 0)
                {
                    ball.set_speed(15, -15);
                }
                ball_delay = 30;
                ball_is_under_platform = false;
                lives--;
            }
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            int initSpeedX = ball.get_speedX();
            int initSpeedY = ball.get_speedY();
            ball.set_speed(0,0);
            Stick.cancelMovement();
            string answer = await DisplayActionSheet("Pause", "Return", null, "Restart Level", "Back to main menu");
            if (answer == "Restart Level")
            {
                restartLevel();
                ball.set_speed(initSpeedX, initSpeedY);
                Stick.getMovementBack();
            }
            if (answer=="Back to main menu")
            {
                await Navigation.PopModalAsync();
            }

            if (answer == "Return")
            {
                ball.set_speed(initSpeedX,initSpeedY);
                Stick.getMovementBack();
            }
        }
    }
}