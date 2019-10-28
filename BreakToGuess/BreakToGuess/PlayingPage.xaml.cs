using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private Label livesLabel,victoryLabel;
        public static string ball_name;
        private static string answer;
        private string winmessage;
        public Page1(int gridWidth,int gridHeight, Color firstColor,Color secondColor, string image_word)
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
            victoryLabel = this.FindByName<Label>("winLabel");
            Stick.Initialize();
            answer = image_word;
            winmessage = "";
            lives = 3;
            if (ball_name == null)
            {
                ball_name = "Ball_breakToGuess.png";
            }
            ball = new Ball(ball_name,5,App.Current.MainPage.Height/2,App.Current.MainPage.Width/2);
            //templateBrick=new Brick(Color.Red, 0,0, 30,70);
            lay.Children.Add(Stick.platform);
            lay.Children.Add(ball.get_sprite());
            //grid =new Brick[(int)App.Current.MainPage.Width/templateBrick.get_height(),(int)App.Current.MainPage.Width/templateBrick.get_width()];
            grid = new Brick[gridHeight,gridWidth];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Color color;
                    if((i+j)%2 ==0 ) color =firstColor;
                    else color = secondColor;
                    grid[i,j] = new Brick(color,j* (App.Current.MainPage.Width / gridWidth), i* (App.Current.MainPage.Width / gridHeight), App.Current.MainPage.Width / gridHeight, App.Current.MainPage.Width / gridWidth);
                    lay.Children.Add(grid[i,j].get_boxView());
                    grid[i, j].draw();
                    Thread.Sleep(50);
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
                ball.setX(ball.getX() + ball.get_speedX());
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
                        if (ball_brick_collision) grid[i, j].draw();
                        ball_brick_collision = false;
                    }
                }
                whenBallGetUnderPlatform();
                if (lives == 0)
                {
                    triggerLoseView();
                }

                if (!String.IsNullOrEmpty(AnswerPage.answerInput))
                {
                    if (AnswerPage.answerInput == answer)
                    {
                        winmessage = "You WIN!";
                        Thread.Sleep(5000);
                        winmessage = "";
                        Navigation.PopModalAsync();
                    }

                    //TestIfAnswerWorks(ball.get_speedX(),ball.get_speedY());
                }
                Device.BeginInvokeOnMainThread(mainDraw);
            }
        }

        private async void triggerLoseView() 
        {
            ball.set_speed(0, 0);
            Thread.Sleep(5000);
            ball.set_speed(5,5);
            await Navigation.PopModalAsync();

        }

        private void restartLevel()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i,j].setX(j * templateBrick.get_width());
                    grid[i,j].setY(i * templateBrick.get_height());
                    grid[i, j].draw();
                }
            }
            lives = 3;
            Stick.getMovementBack();
            ball.set_speed(5,5);
            Thread.Sleep(2000);
            ball.set_pos(App.Current.MainPage.Width / 2, App.Current.MainPage.Height / 2);
        }

        private void mainDraw()
        {
            if (!ball_is_under_platform) ball.draw();
            //for (int i = 0; i < grid.GetLength(0); i++)
            //{
            //    for (int j = 0; j < grid.GetLength(1); j++)
            //    {
            //        grid[i, j].draw();
            //    }
            //}
            livesLabel.Text = "Lives = " + lives;
            victoryLabel.Text = winmessage;
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

            ball.set_speed(0,0);
            Stick.cancelMovement();
            string answer = await DisplayActionSheet("Pause", "Return", null, "Restart Level", "Back to main menu");
            if (answer == "Restart Level")
            {
                restartLevel();
            }
            if (answer=="Back to main menu")
            {
                await Navigation.PopModalAsync();
                Stick.getMovementBack();
            }

            if (answer == "Return")
            {
                ball.set_speed(5,5);
                Stick.getMovementBack();
            }
        }

        private async void AnswerButton_OnClicked(object sender, EventArgs e)
        {
            ball.set_speed(0,0);    
            await Navigation.PushModalAsync(new AnswerPage());
        }

        private async void TestIfAnswerWorks(int initX, int initY)
        {
            string message;
            if (AnswerPage.answerInput == answer)
            {
                message = "You win !";
                bool winExit = await DisplayAlert(message, "Restart ?", "Yes", "No");
                if (winExit)
                {
                    restartLevel();
                }
                else
                {
                    Stick.getMovementBack();
                    ball.set_speed(initX, initY);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                lives--;
                message = "Wrong answer, try again !";
                string exit = await DisplayActionSheet(message, "Back to game", null, "Restart Level", "Back to main menu");
                if (exit == "Back to game")
                {
                    Stick.getMovementBack();
                    ball.set_speed(initX, initY);
                }

                if (exit == "Restart Level")
                {
                    restartLevel();
                }

                if (exit == "Back to main menu")
                {
                    Stick.getMovementBack();
                    ball.set_speed(initX, initY);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}