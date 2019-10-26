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
            Debug.WriteLine("Width " + Width + " Height " + Height);
            lay.Children.Add(Stick.platform);
            Stick.Initialize();
            ball = new Ball("Ball_breakToGuess.png",5,App.Current.MainPage.Height/2,App.Current.MainPage.Width/2);
            lay.Children.Add(ball.get_sprite());
            Timer mainTimer = new Timer(timer_tick);
            mainTimer.Change(0, 33);
        }

        private void timer_tick(object state)
        {
            ball_delay--;
            if (ball_delay <= 0)
            {
                ball.setX(ball.getX() + ball.get_speedX());
                ball.setY(ball.getY() + ball.get_speedY());
                ball_is_under_platform = ball.handle_collisions(Width,Height, stick ); //the collisions handler says if the ball is under the platform or not
                //firstBrick.collision_with_ball(ball_brick_collision);
                whenBallGetUnderPlatform();
                Device.BeginInvokeOnMainThread(mainDraw);
            }
        }

        private void mainDraw()
        {
            if (!ball_is_under_platform) ball.draw();
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
                    ball.set_speed(20, ball.get_speedY());
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