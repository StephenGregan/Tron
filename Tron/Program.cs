using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tron
{
    class Program
    {
        // Directions
        static int left = 0;
        static int right = 1;
        static int up = 2;
        static int down = 3;

        // Player 1's Info
        static int firstPlayerScore = 0;
        static int firstPlayerDirection = right;
        static int firstPlayerColumn = 0;
        static int firstPlayerRow = 0;

        //Player 2's Info
        static int secondPlayerScore = 0;
        static int secondPlayerDirection = left;
        static int secondPlayerColumn = 40;
        static int secondPlayerRow = 5;

        static bool[,] isUsed;

        static void Main(string[] args)
        {
            // Call the set game method
            SetGameField();
            // Call the start up screen method
            StartUpScreen();

            isUsed = new bool[Console.WindowWidth, Console.WindowHeight];

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ChangePlayerDirection(key);
                }
                MovePlayers();

                bool firstPlayerLoses = DoesPlayerLose(firstPlayerRow, firstPlayerColumn);
                bool secondPlayerLoses = DoesPlayerLose(firstPlayerRow, firstPlayerColumn);

                // If the game ends in a draw
                if (firstPlayerLoses && secondPlayerLoses)
                {
                    firstPlayerScore++;
                    secondPlayerScore++;
                    Console.WriteLine();
                    Console.WriteLine("Game over");
                    Console.WriteLine("The game is a draw!");
                    Console.WriteLine("Current score: {0} - {1}", firstPlayerScore, secondPlayerScore);
                    ResetGame();

                    // If player 1 loses
                    if (firstPlayerLoses)
                    {
                        secondPlayerScore++;
                        Console.WriteLine();
                        Console.WriteLine("Game over");
                        Console.WriteLine("Player 2 wins...Well Done!");
                        Console.WriteLine("Current score: {0} - {1}", firstPlayerScore, secondPlayerScore);
                        ResetGame();
                    }

                    // If player 2 loses
                    if (secondPlayerLoses)
                    {
                        firstPlayerScore++;
                        Console.WriteLine();
                        Console.WriteLine("Game over");
                        Console.WriteLine("Player 1 wins...Well done!");
                        Console.WriteLine("Current score: {0} - {1}", firstPlayerScore, secondPlayerScore);
                        ResetGame();
                    }

                    isUsed[firstPlayerColumn, firstPlayerRow] = true;
                    isUsed[firstPlayerColumn, firstPlayerRow] = true;

                    WriteOnPosition(firstPlayerColumn, firstPlayerRow, '*', ConsoleColor.Yellow);
                    WriteOnPosition(secondPlayerColumn, secondPlayerRow, '*', ConsoleColor.Cyan);

                    Thread.Sleep(100);
                }
            }
        }

        // Start up display
        static void StartUpScreen()
        {
            string heading = "A simple tron like game, enjoy";
            Console.CursorLeft = Console.BufferWidth / 2 - heading.Length / 2;
            Console.WriteLine(heading);

            Console.ForegroundColor = ConsoleColor.Yellow;

            // Player 1's controls
            Console.WriteLine("Player 1's controls:\n");
            Console.WriteLine("W - up");
            Console.WriteLine("A - Left");
            Console.WriteLine("S - Down");
            Console.WriteLine("D - Right");

            // Player 2's controls
            string longestString = "Player 2's controls:\n";
            int cursorLeft = Console.BufferWidth - longestString.Length;

            Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorLeft = cursorLeft;
            Console.WriteLine("Player 2's controls:\n");
            Console.CursorLeft = cursorLeft;
            Console.WriteLine("Up Arrow - Up");
            Console.CursorLeft = cursorLeft;
            Console.WriteLine("Left Arrow - Left");
            Console.CursorLeft = cursorLeft;
            Console.WriteLine("Down Arrow - Down");
            Console.CursorLeft = cursorLeft;
            Console.WriteLine("Right Arrow - Right");

            Console.ReadKey();
            Console.Clear();
        }

        static void ResetGame()
        {
            isUsed = new bool[Console.WindowWidth, Console.WindowHeight];
            SetGameField();
            firstPlayerDirection = right;
            secondPlayerDirection = left;
            Console.WriteLine("Press any key to start again...");
            Console.ReadKey();
            Console.Clear();
            MovePlayers();

        }

        static bool DoesPlayerLose(int row, int col)
        {
            if (row < 0)
            {
                return true;
            }
            if (col < 0)
            {
                return true;
            }
            if (row >= Console.WindowHeight)
            {
                return true;
            }
            if (col >= Console.WindowWidth)
            {
                return true;
            }
            if (isUsed[col, row])
            {
                return true;
            }

            return false;
        }

        static void SetGameField()
        {
            Console.WindowHeight = 30;
            Console.BufferHeight = 30;

            Console.WindowWidth = 100;
            Console.BufferWidth = 100;

            firstPlayerColumn = 0;
            firstPlayerRow = Console.WindowHeight / 2;

            secondPlayerColumn = Console.WindowWidth - 1;
            secondPlayerRow = Console.WindowHeight / 2;
        }

        static void MovePlayers()
        {
            if (firstPlayerDirection == right)
            {
                firstPlayerColumn++;
            }
            if (firstPlayerDirection == left)
            {
                firstPlayerDirection--;
            }
            if (firstPlayerDirection == up)
            {
                firstPlayerDirection--;
            }
            if (firstPlayerDirection == down)
            {
                firstPlayerDirection++;
            }

            if (secondPlayerDirection == right)
            {
                secondPlayerDirection++;
            }
            if (secondPlayerDirection == left)
            {
                secondPlayerDirection--;
            }
            if (secondPlayerDirection == up)
            {
                secondPlayerDirection--;
            }
            if (secondPlayerDirection == down)
            {
                secondPlayerDirection++;
            }
        }

        static void WriteOnPosition(int x, int y, char ch, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(ch);
        }

        static void ChangePlayerDirection(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.W && firstPlayerDirection != down)
            {
                firstPlayerDirection = up;
            }
            if (key.Key == ConsoleKey.A && firstPlayerDirection != right)
            {
                firstPlayerDirection = left;
            }
            if (key.Key == ConsoleKey.D && firstPlayerDirection != left)
            {
                firstPlayerDirection = right;
            }
            if (key.Key == ConsoleKey.S && firstPlayerDirection != up)
            {
                firstPlayerDirection = down;
            }

            if (key.Key == ConsoleKey.W && secondPlayerDirection != down)
            {
                secondPlayerDirection = up;
            }
            if (key.Key == ConsoleKey.A && secondPlayerDirection != right)
            {
                secondPlayerDirection = left;
            }
            if (key.Key == ConsoleKey.D && secondPlayerDirection != left)
            {
                secondPlayerDirection = right;
            }
            if (key.Key == ConsoleKey.S && secondPlayerDirection != up)
            {
                secondPlayerDirection = down;
            }
        }
    }
}
