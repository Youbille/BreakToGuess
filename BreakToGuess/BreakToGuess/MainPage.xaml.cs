using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace BreakToGuess
{
    public partial class MainPage : ContentPage
    {
        
        private Timer mainTimer; //timer for the timertick
        private AbsoluteLayout mainLayout; //absolute layout for the game
        private Label timerLayout; //label that shows the time
        private Ball default_ball;
        Image sprite_platform = new Image {Source = ImageSource.FromFile("platform.png")};
        bool ball_is_under_platform = false;
        private int ball_delay;
        Random rand_direction = new Random();
        private Brick firstBrick = new Brick(Color.Red,400,86);
        private Brick secondBrick = new Brick(Color.Blue,800,520);

    public MainPage()
        {
            InitializeComponent();
            timerLayout = this.FindByName<Label>("timerLabel");
            mainLayout = this.FindByName<AbsoluteLayout>("main_game_layout");

            mainLayout.Children.Add(sprite_platform, new Point(500,700)) ;
            mainLayout.Children.Add(firstBrick.get_boxView());
            default_ball = new Ball("Ball_breakToGuess.png", 5, 800, 390);
            mainLayout.Children.Add(default_ball.get_sprite());
            mainLayout.Children.Add(secondBrick.get_boxView());
            mainTimer = new Timer(timer_tick);
            mainTimer.Change(0, 33);
            Thread.Sleep(50);
        }
        
        private void timer_tick(object state)
        {
            ball_delay--;
            if (ball_delay <= 0)
            {
                default_ball.setX(default_ball.getX() + default_ball.get_speedX());
                default_ball.setY(default_ball.getY() + default_ball.get_speedY());
                default_ball.brick_collision(firstBrick);
                default_ball.brick_collision(secondBrick);
                ball_is_under_platform = default_ball.handle_collisions(Width, sprite_platform); //the collisions handler says if the ball is under the platform or not
                if (ball_is_under_platform)
                {
                    default_ball.setX(Width / 2);
                    default_ball.setY(Height / 2);
                    default_ball.set_speed(rand_direction.Next(-20,20), rand_direction.Next(-20,5));
                    if (default_ball.get_speedX() == 0 && default_ball.get_speedY() == 0)
                    {
                        default_ball.set_speed(15,-15);
                    }
                    ball_delay = 30;
                    ball_is_under_platform = false;
                }
                Device.BeginInvokeOnMainThread(mainDraw);
            }
        }

        private void mainDraw()
        {
            firstBrick.draw();
            secondBrick.draw();
            if (!ball_is_under_platform) default_ball.draw();
        }
    }
}
