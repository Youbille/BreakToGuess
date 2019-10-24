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
        
        private Platform game_platform = new Platform();
        private Timer mainTimer;
        private AbsoluteLayout mainLayout;
        private Label timerLayout;
        private Ball default_ball;
        Image sprite_platform = new Image {Source = ImageSource.FromFile("platform.png")};
        double  ballX,ballY;
        bool ball_is_under_platform = false;
        private int ball_delay;
        Random rand_direction = new Random();
        private Brick firstBrick = new Brick(Color.Red,800,100);

    public MainPage()
        {
            InitializeComponent();
            default_ball = new Ball("Ball_breakToGuess.png", 5);
            mainTimer = new Timer(timer_tick);
            mainTimer.Change(0, 33);
            Thread.Sleep(50);
            timerLayout = this.FindByName<Label>("timerLabel");
            mainLayout = this.FindByName<AbsoluteLayout>("main_game_layout");
            //firstBrick = new Brick(Color.Red, 5 * Width / 6, 5 * Height / 6);
            mainLayout.Children.Add(sprite_platform, new Point(500,700)) ;
            mainLayout.Children.Add(default_ball.get_sprite(), new Point(0,0));
            mainLayout.Children.Add(firstBrick.get_boxView(),new Point(500,20));
            ballX = default_ball.get_pos_x();
            ballY = default_ball.get_pos_y();
        }
        
        private void timer_tick(object state)
        {
            ball_delay--;
            if (ball_delay <= 0)
            {
                ballX += default_ball.get_speedX();
                ballY += default_ball.get_speedY();
                default_ball.maybe_simpler_brickcoll_interrogation_point(ballX, ballY, firstBrick);
                ball_is_under_platform = default_ball.handle_collisions(Width, sprite_platform, ballX, ballY);
                if (ball_is_under_platform)
                {
                    ballX = Width/2;
                    ballY = Height/2;
                    default_ball.set_speed(rand_direction.Next(0,0), rand_direction.Next(-15, -15));
                    ball_delay = 30;
                    ball_is_under_platform = false;
                }
                Device.BeginInvokeOnMainThread(MainThreadCode);
            }
        }

        private void MainThreadCode()
        {
            timerLayout.Text = DateTime.Now.ToString();
            if(!ball_is_under_platform)default_ball.move(ballX, ballY);
            
        }
    }
}
