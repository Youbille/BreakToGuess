using System;
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
        bool ball_is_under_platform = false;
        private int ball_delay;
        Random rand_direction = new Random();
        private bool ball_brick_collision;
        private Brick template_brick;
        private Brick[,] grid_brick;


        public MainPage()
        {
            InitializeComponent();
            mainLayout = this.FindByName<AbsoluteLayout>("main_game_layout");
            grid_brick =new Brick[5,5];
            //template_brick = new Brick(Color.Red, 0, 0, (int)screenSize.Width /grid_brick.GetLength(0), (int)screenSize.Width / grid_brick.GetLength(1));
            template_brick = new Brick(Color.Red,0,0,10,30);
            for (int i = 0; i < grid_brick.GetLength(0); i++)
            {
                for (int j = 0; j < grid_brick.GetLength(1); j++)
                {
                    grid_brick[i, j] = new Brick(Color.Red, j* template_brick.get_width(), i * template_brick.get_height(), template_brick.get_height(), template_brick.get_width());
                    mainLayout.Children.Add(grid_brick[i, j].get_boxView());
                }
            }
            mainTimer = new Timer(timer_tick);
            mainTimer.Change(0, 50);
            Thread.Sleep(50);
        }

        private void timer_tick(object state)
        {
            //ball_delay--;
            //if (ball_delay <= 0)
            //{
            //    default_ball.setX(default_ball.getX() + default_ball.get_speedX());
            //    default_ball.setY(default_ball.getY() + default_ball.get_speedY());
            //    for (int i = 0; i < grid_brick.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < grid_brick.GetLength(1); j++)
            //        {
            //            ball_brick_collision = default_ball.brick_collision(grid_brick[i, j]);
            //            grid_brick[i, j].collision_with_ball(ball_brick_collision);
            //            ball_brick_collision = false;
            //        }
            //    }
            //    ball_is_under_platform = default_ball.handle_collisions(Width, new Stick()); //the collisions handler says if the ball is under the platform or not
            //    whenBallGetUnderPlatform();
            //    Device.BeginInvokeOnMainThread(mainDraw);
            //}
        }

        //private void whenBallGetUnderPlatform()
        //{
        //    if (ball_is_under_platform)
        //    {
        //        default_ball.setX(Width / 2);
        //        default_ball.setY(4*Height / 5);
        //        default_ball.set_speed(rand_direction.Next(-29,29), -9);
        //        if (default_ball.get_speedX() > -5 && default_ball.get_speedX() < 5)
        //        {
        //            default_ball.set_speed(20, default_ball.get_speedY());
        //        }

        //        if (default_ball.get_speedX() == 0|| default_ball.get_speedY() == 0)
        //        {
        //            default_ball.set_speed(15, -15);
        //        }

        //        ball_delay = 30;
        //        ball_is_under_platform = false;
        //    }
        //}

        //private void mainDraw()
        //{
        //    for (int i = 0; i < grid_brick.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < grid_brick.GetLength(1); j++)
        //        {
        //            grid_brick[i, j].draw();
        //        }
        //    }
        //    if (!ball_is_under_platform) default_ball.draw();
        //}
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1());
        }
    }
}
