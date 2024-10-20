using System;
using BoardGameAPP;

namespace BoardGameApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the BoardGameAPP!");
                Console.WriteLine("-———————————————————————————");
                Console.WriteLine("       Please enter: ");
                Console.WriteLine("       1.Start Game");
                Console.WriteLine("       2.Get Help");
                Console.WriteLine("       3.Load Game");
                Console.WriteLine("       4.Exit");
                
                int mainChoice;

                while (true)
                {
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out mainChoice))
                    {
                        if (mainChoice >= 1 && mainChoice <= 4)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice! Please enter a number between 1 and 4.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please enter an integer.");
                    }
                }

                if (mainChoice == 2)
                {
                    Console.WriteLine("Please choose Game: 1.Gomoku 2.Notakto");
                    int helpChoice;
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out helpChoice))
                        {
                            if (helpChoice == 1)
                            {
                                HelpSystem.ShowHelp("Gomoku");
                                break; 
                            }
                            else if (helpChoice == 2)
                            {
                                HelpSystem.ShowHelp("Notakto");
                                break; 
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice! Please enter 1 or 2");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid enter! Please enter an integer.");
                        }
                    }
                    continue;
                }

                BaseGame game = null;

                if (mainChoice == 1)
                {
                    Console.WriteLine("Please choose Game: 1.Gomoku 2.Notakto");
                    int gameChoice;
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out gameChoice))
                        {
                            if (gameChoice == 1)
                            {
                                game = new GomokuGame();
                                break; 
                            }
                            else if (gameChoice == 2)
                            {
                                game = new NotaktoGame();
                                break; 
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice! Please enter 1 or 2");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid enter! Please enter an integer.");
                        }
                    }

                    Console.WriteLine("Select Opponent: 1. AI 2. Human ");
                    int opponentChoice;
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out opponentChoice))
                        {
                            if (opponentChoice == 1)
                            {
                                Console.WriteLine("You've chosen an AI opponent.");
                                break; 
                            }
                            else if (opponentChoice == 2)
                            {
                                Console.WriteLine("You have chosen a human opponent.");
                                break; 
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice! Please enter 1 or 2");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid enter! Please enter an integer.");
                        }
                    }

                    Player player1 = new HumanPlayer(1);
                    Player player2 = opponentChoice == 1 ? new AIPlayer() : new HumanPlayer(2);

                    Player currentPlayer = player1;
                    Player opponentPlayer = player2;

                    while (game.GameState == GameState.Ongoing)
                    {
                        Console.Clear();
                        Console.WriteLine("Current Board: ");
                        
                        if (game is NotaktoGame notaktoGame)
                        {
                            notaktoGame.DisplayBoards();
                        }
                        else
                        {
                            GomokuGame.DisplayBoard(game);
                        }

                        Console.WriteLine($"Player{game.CurrentPlayer}'s turn");
                        Move move = currentPlayer.GetMove(game);

                        if (game.MakeMove(move))
                        {
                            Console.Clear();
                            Console.WriteLine("Board Updates: ");
                            if (game is NotaktoGame notaktoGame2)
                            {
                                notaktoGame2.DisplayBoards();
                            }
                            else
                            {
                                GomokuGame.DisplayBoard(game);
                            }

                            if (game.GameState == GameState.Ongoing)
                            {
                                Console.WriteLine("Undo or not？(y/n)");
                                if (Console.ReadLine()?.ToLower() == "y")
                                {
                                    game.UndoMove();
                                    continue;
                                }
                                Console.WriteLine("Save game or not? (y/n)");
                                
                                if (Console.ReadLine()?.ToLower() == "y")
                                {
                                    Console.WriteLine("Please enter the filename (e.g. saved_game.xml):");
                                    string fileName = Console.ReadLine();

                                    //  .xml
                                    if (!fileName.EndsWith(".xml"))
                                    {
                                        fileName += ".xml";
                                    }

                                    GameSaver.SaveGame(game, fileName);
                                    Console.WriteLine($"Game has been saved in {fileName}");
                                    break;
                                    
                                }
                            }

                            // AI move if not human vs human
                            if (opponentChoice == 1 && game.GameState == GameState.Ongoing)
                            {
                                Move aiMove = opponentPlayer.GetMove(game);
                                game.MakeMove(aiMove);
                                (currentPlayer, opponentPlayer) = (opponentPlayer, currentPlayer);
                            }

                            // Switch players
                            (currentPlayer, opponentPlayer) = (opponentPlayer, currentPlayer);
                        }
                        else
                        {
                            Console.WriteLine("Invalid move! Please re-enter!");
                        }
                    }

                    Console.WriteLine($"Game Over! Result: {game.GameState}");
                    continue;
                }
                
                else if (mainChoice == 3)//Load
                {
                    Console.WriteLine("Please enter the filename of the saved game: ");
                    string filePath;

                    while (true)
                    {
                        filePath = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(filePath))
                        {
                            Console.WriteLine("The filename cannot be empty, please re-enter!");
                            continue;
                        }

                        if (!filePath.EndsWith(".xml"))
                        {
                            filePath += ".xml";
                        }

                        if (!File.Exists(filePath))
                        {
                            Console.WriteLine("The file does not exist, please check the file name and re-enter.");
                            continue;
                        }

                        break; 
                    }

                    game = GameSaver.LoadGame(filePath);

                    if (game != null)
                    {
                        Console.WriteLine("The game loaded successfully!");
                        Console.WriteLine("Select Opponent: 1. AI 2. Human ");
                        int opponentChoice;
                        while (true)
                        {
                            if (int.TryParse(Console.ReadLine(), out opponentChoice))
                            {
                                if (opponentChoice == 1)
                                {
                                    Console.WriteLine("You've chosen an AI opponent.");
                                    break;
                                }
                                else if (opponentChoice == 2)
                                {
                                    Console.WriteLine("You have chosen a human opponent.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice! Please enter 1 or 2");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid enter! Please enter an integer.");
                            }
                        }

                        Player player1 = new HumanPlayer(1);
                        Player player2 = opponentChoice == 1 ? new AIPlayer() : new HumanPlayer(2);

                        Player currentPlayer = player1;
                        Player opponentPlayer = player2;

                        while (game.GameState == GameState.Ongoing)
                        {
                            Console.Clear();
                            Console.WriteLine("Current Board: ");
                            if (game is NotaktoGame notaktoGame)
                            {
                                notaktoGame.DisplayBoards();
                            }
                            else
                            {
                                GomokuGame.DisplayBoard(game);
                            }

                            Console.WriteLine($"Player{game.CurrentPlayer}'s turn");
                            Move move = currentPlayer.GetMove(game);

                            if (game.MakeMove(move))
                            {
                                Console.Clear();
                                Console.WriteLine("Board Updates: ");
                                if (game is NotaktoGame notaktoGame2)
                                {
                                    notaktoGame2.DisplayBoards();
                                }
                                else
                                {
                                    GomokuGame.DisplayBoard(game);
                                }

                                if (game.GameState == GameState.Ongoing)
                                {
                                    if (currentPlayer is HumanPlayer)
                                    {
                                        Console.WriteLine("Undo or not? (y/n)");
                                        if (Console.ReadLine()?.ToLower() == "y")
                                        {
                                            game.UndoMove();
                                            continue;
                                        }

                                        Console.WriteLine("Save game or not? (y/n)");
                                        if (Console.ReadLine()?.ToLower() == "y")
                                        {
                                            Console.WriteLine("Please enter the filename (e.g. saved_game.xml):");
                                            string fileName = Console.ReadLine();

                                            if (!fileName.EndsWith(".xml"))
                                            {
                                                fileName += ".xml";
                                            }

                                            GameSaver.SaveGame(game, fileName);
                                            Console.WriteLine($"Game has been saved in {fileName}");

                                            break;
                                        }
                                    }

                                    
                                }

                                (currentPlayer, opponentPlayer) = (opponentPlayer, currentPlayer);
                            }

                            else
                            {
                                Console.WriteLine("Invalid move! Please re-enter!");
                            }
                            Console.WriteLine($"Game Over! Result: {game.GameState}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to read the archive! returned to the start menu.");
                    }
                    continue;
                }
                else if (mainChoice == 4)
                {
                    break;
                }
            }
        }
        


    }

}
