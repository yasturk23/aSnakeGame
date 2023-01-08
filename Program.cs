using System;
using System.Collections.Generic;
using System.Linq;

namespace aSnakeGame
{
    class Snake
    {
        static void Main(string[] args)
        {
            // Set up the game board
            const int boardWidth = 20;
            const int boardHeight = 20;
            char[,] board = new char[boardWidth, boardHeight];

            // Set up the snake
            Queue<(int, int)> snake = new Queue<(int, int)>();
            snake.Enqueue((0, 0));
            board[0, 0] = 'S';

            // Set up the food
            Random random = new Random();
            (int, int) food = (random.Next(boardWidth), random.Next(boardHeight));
            board[food.Item1, food.Item2] = 'F';

            // Set up game variables
            int tailLength = 0;
            int score = 0;

            // Game loop
            bool gameOver = false;

            while (!gameOver)
            {
                Console.Clear();    


                // Draw the board
                for (int i = 0; i < boardHeight; i++)
                {
                    for (int j = 0; j < boardWidth; j++)
                    {
                        Console.Write(board[j, i] == 0 ? ' ' : board[j, i]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"Score: {score}");

                // Get the snake's next direction
                ConsoleKey key = Console.ReadKey().Key;
                (int, int) direction = (0, 0);
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        direction = (0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        direction = (0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = (-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        direction = (1, 0);
                        break;
                }

                // Update the snake's position
                (int, int) head = snake.Last();
                (int, int) newHead = (head.Item1 + direction.Item1, head.Item2 + direction.Item2);

                if (newHead.Item1 < 0 || newHead.Item1 >= boardWidth || newHead.Item2 < 0 || newHead.Item2 >= boardHeight)
                {
                    // Snake has gone off the board
                    gameOver = true;
                    Console.WriteLine("Game over. Snake has gone off the board");

                }
                else if (board[newHead.Item1, newHead.Item2] == 'S')
                {
                    // Snake has collided with itself
                    gameOver = true;
                    Console.WriteLine("Game over. Snake has collided with itself");
                }
                else
                {
                     snake.Enqueue(newHead);
                     //board[head.Item1, head.Item2] = ' ';
                     board[newHead.Item1, newHead.Item2] = 'S';

                    // Check if the snake has eaten the food
                    if (newHead.Item1 == food.Item1 && newHead.Item2 == food.Item2) 
                    {
                        food = (random.Next(boardWidth), random.Next(boardHeight));
                        board[food.Item1, food.Item2] = 'F';
                        score++;

                        if(score>0 && score %5 == 0) 
                        {
                             tailLength++;   
                        }
                    }
                }

            }
        }
    }
}
