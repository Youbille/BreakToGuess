using System;
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
        private int ball_delay;
        private Ball ball;
        private bool ball_is_under_platform;
        private Random rand_direction = new Random();
        private Brick templateBrick;
        private bool ball_brick_collision;
        private Brick[,] grid;
        public Page1()
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
            
            //lay.Children.Add(templateBrick.get_boxView());
            Stick.Initialize();
            ball = new Ball("Ball_breakToGuess.png",5,App.Current.MainPage.Height/2,App.Current.MainPage.Width/2);
            templateBrick=new Brick(Color.Red, 0,0, 30,70);
            lay.Children.Add(Stick.platform);
            lay.Children.Add(ball.get_sprite());
            //grid =new Brick[(int)App.Current.MainPage.Width/templateBrick.get_height(),(int)App.Current.MainPage.Width/templateBrick.get_width()];
            grid = new Brick[7,6];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Color color;
                    if((i+j)%2 ==0 ) color =Color.Red;
                    else color = Color.DarkOrange;
                    grid[i,j] = new Brick(color,j*templateBrick.get_width(),i*templateBrick.get_height(),templateBrick.get_height(),templateBrick.get_width());
                    lay.Children.Add(grid[i,j].get_boxView());
                }
            }
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
                Device.BeginInvokeOnMainThread(mainDraw);
            }
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
            }
        }
    }
}