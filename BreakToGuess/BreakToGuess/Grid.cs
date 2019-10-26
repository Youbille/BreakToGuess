using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BreakToGuess
{
    class Grid
    {
        public Brick[,] grid;
        private int internHeight;
        private int internWidth;

        public Grid(int height, int width)
        {
            internHeight = height;
            internWidth = width;
            grid = new Brick[height,width];
        }

        public void fillGrid(Brick templateBrick)
        {
            grid = new Brick[internHeight,internWidth];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i,j] = new Brick(Color.Red,j*grid[i,j].get_width(),i*grid[i,j].get_height(),templateBrick.get_height(),templateBrick.get_width());
                }
            }
        }
    }
}
