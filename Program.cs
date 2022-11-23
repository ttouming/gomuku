using System;
using static System.Console;
using System.IO;

namespace gomuku
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();

        }

        static void Menu()
        {
            Clear();
            WriteLine("***************************************");
            WriteLine("*                                     *");
            WriteLine("*  Welcom to the Board Game           *");
            WriteLine("*   Press 'a' to start the new game   *");
            WriteLine("*   Press 'b' to load the saved game  *");
            WriteLine("*   Press 'c' to exit the game        *");
            WriteLine("*                                     *");
            WriteLine("***************************************");

            string useropt;

            useropt = ReadLine();

            switch (useropt)
            {
                case "a":
                    StartNewGame();
                    break;
                case "b":
                    LoadSavedGame();
                    break;
                case "c":
                    ExitGame();
                    break;
            }

            Menu();
        }

        static void StartNewGame()
        {
            Clear();
            WriteLine("***************************************");
            WriteLine("*                                     *");
            WriteLine("*   Press 'a' to start a PvP game     *");
            WriteLine("*   Press 'b' to start a PvC game     *");
            WriteLine("*   Press 'c' to exit the game        *");
            WriteLine("*                                     *");
            WriteLine("***************************************");

            string opt = ReadLine();
            switch (opt)
            {
                case "a":
                    PvP();
                    break;
                case "b":
                    PvC();
                    break;
                case "c":
                    break;
            }


        }

        static void PvP()
        {
            Clear();
            Player player1 = new Player(1, -2, -1);
            Player player2 = new Player(2, -1, -2);
            Game(player1, player2);
        }
        static void PvC()
        {
            Clear();
            Player player1 = new Player(1, -2, -1);
            Player computer = new Player(3, -1, -2);
            Game(player1, computer);
        }
        static void Game(Player p1, Player p2)
        {
            Player[] parr = new Player[300];
            Player[] parr_c = new Player[300];
            for (int i = 0; i < 300; i++)
            {
                parr[i] = new Player(0, 0, 0);
                parr_c[i] = new Player(0, 0, 0);
            }
            int c = 0;
            int winner = 0;

            Board.ShowBoard();
            while (winner == 0)
            {
                (c, winner) = Gomuku.Move(p1, p2, parr, parr_c, c);
                if (winner != 0)
                {
                    break;
                }
                //Gomuku.CheckValidity(p1, p2);
                //Write("{0}{1}\n", p1.PositionX, p1.PositionY);
                Board.UpateBoard(p1);
                parr[c].PlayerID = p1.PlayerID;
                parr[c].PositionX = p1.PositionX;
                parr[c].PositionY = p1.PositionY;
                c++;
                //Write(c);
                (c, winner) = Gomuku.Move(p2, p1, parr, parr_c, c);
                //Write("\n{0}+++", winner);
                //Gomuku.CheckValidity(p2, p1);
                //Write("{0}{1}\n", p2.PositionX, p2.PositionY);
                Board.UpateBoard(p2);
                parr[c].PlayerID = p2.PlayerID;
                parr[c].PositionX = p2.PositionX;
                parr[c].PositionY = p2.PositionY;
                c++;
                //Write(c);
            }
            WriteLine("\nPlayer{0} is the Winner!", winner);
            WriteLine("Press any key to return to the menu.");
            ReadLine();
        }
        static void LoadSavedGame()
        {
            Clear();
            const string k = @"\";
            string path = System.IO.Directory.GetCurrentDirectory();
            string[] files = System.IO.Directory.GetFiles(path, "*.txt");
            string[] files2;
            string[] files3 = new string[files.Length];
            int index;


            WriteLine("Saved Files: ");
            WriteLine("Index   File Name");
            for (int x = 0; x < files.Length; x++)
            {
                //WriteLine("{0}", files[x]);
                files2 = files[x].Split(k);
                files3[x] = files2[files2.Length - 1];
                WriteLine("{0}     : {1}", x + 1, files3[x]);
            }

            WriteLine("\nPlease enter index number to load the corresponding file:");
            index = Convert.ToInt32(ReadLine());
            index = --index;
            WriteLine("Loading {0}...", files3[index]);

            int winner = 0;

            while (true)
            {
                Clear();
                const char DELIM = ',';
                Player p1 = new Player(1, -2, -1);
                Player p2 = new Player(2, -1, -2);

                FileStream inFile = new FileStream(files3[index], FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                string recordIn;
                string[] fields;
                recordIn = reader.ReadLine();

                Player[] parr = new Player[300];
                Player[] parr_c = new Player[300];
                for (int i = 0; i < 300; i++)
                {
                    parr[i] = new Player(0, 0, 0);
                    parr_c[i] = new Player(0, 0, 0);
                }
                int c_steps = 0;
                while (recordIn != null)
                {
                    fields = recordIn.Split(DELIM);
                    if (Convert.ToInt32(fields[1]) == 1)
                    {
                        parr[c_steps].PlayerID = 1;
                    }
                    else if (Convert.ToInt32(fields[1]) == 2)
                    {
                        parr[c_steps].PlayerID = 2;

                    }
                    else if (Convert.ToInt32(fields[1]) == 3)
                    {
                        parr[c_steps].PlayerID = 3;
                    }

                    parr[c_steps].PositionX = Convert.ToInt32(fields[2]);
                    parr[c_steps].PositionY = Convert.ToInt32(fields[3]);
                    //WriteLine("{0,-10}{1,-18}{2, 10}", parr[c_steps].PlayerID, parr[c_steps].PositionX, parr[c_steps].PositionY);
                    c_steps++;
                    recordIn = reader.ReadLine();
                    //patient.Name, patient.Balance.ToString("C"));
                }

                reader.Close();
                inFile.Close();
                /*
                while (true)
                {

                }*/

                int c = c_steps;

                Board.ShowBoard();

                int i_loadboard = 0;
                while (i_loadboard < c)
                {
                    //c = Gomuku.Move(p1, p2, parr, parr_c, c);
                    p1.PlayerID = parr[i_loadboard].PlayerID;
                    p1.PositionX = parr[i_loadboard].PositionX;
                    p1.PositionY = parr[i_loadboard].PositionY;
                    Board.UpateBoard(p1);
                    i_loadboard++;
                    p2.PlayerID = parr[i_loadboard].PlayerID;
                    p2.PositionX = parr[i_loadboard].PositionX;
                    p2.PositionY = parr[i_loadboard].PositionY;
                    Board.UpateBoard(p2);
                    i_loadboard++;
                }


                while (winner == 0)
                {
                    (c, winner) = Gomuku.Move(p1, p2, parr, parr_c, c);
                    if (winner != 0)
                    {
                        break;
                    }
                    Write("{0}{1}\n", p1.PositionX, p1.PositionY);
                    Board.UpateBoard(p1);
                    parr[c].PlayerID = p1.PlayerID;
                    parr[c].PositionX = p1.PositionX;
                    parr[c].PositionY = p1.PositionY;
                    c++;
                    //Write(c);
                    (c, winner) = Gomuku.Move(p2, p1, parr, parr_c, c);
                    //Gomuku.CheckValidity(p2, p1);
                    Write("{0}{1}\n", p2.PositionX, p2.PositionY);
                    Board.UpateBoard(p2);
                    parr[c].PlayerID = p2.PlayerID;
                    parr[c].PositionX = p2.PositionX;
                    parr[c].PositionY = p2.PositionY;
                    c++;
                    //Write(c);
                }
                break;

            }
            WriteLine("\nPlayer{0} is the Winner!", winner);
            WriteLine("Press any key to return to the menu.");
            ReadLine();
        }

        public static void ExitGame()
        {
            System.Environment.Exit(1);
        }
    }//end class


    class Player
    {
        public int PlayerID { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Player(int playerid, int positionx, int positiony)
        {
            PlayerID = playerid;
            PositionX = positionx;
            PositionY = positiony;
        }

    }//end class Player

    //class BoardGame { }

    class Gomuku
    {

        public static (int, int) Move(Player a, Player b, Player[] arr, Player[] arr_r, int c)
        {
            int x = 0, y = 0;
            int counter = 0;
            int validator;
            int winner_m = 0;
            //int k = c;
            if (a.PlayerID == 3)
            {
                a.PositionX = arr[c - 1].PositionX + 1;
                a.PositionY = arr[c - 1].PositionY + 1;
                validator = CheckValidity(a, arr);
                if (validator == 1)
                {
                    Move(a, b, arr, arr_r, c);
                }
                winner_m = CheckWinner(a, arr);

                SetCursorPosition(0, 34);
                Write(" ");
                SetCursorPosition(0, 35);
                Write(" ");
                return (c, winner_m);
            }
            else
            {
                SetCursorPosition(0, 33);
                WriteLine("Player{0} enter position:", a.PlayerID);

                x = Convert.ToInt32(ReadLine());
                while (x == 999) // x == 999, undo
                {
                    //WriteLine("sadasda");
                    Undo(arr, arr_r, c, counter);
                    SetCursorPosition(0, 33);
                    WriteLine("Player{0} enter position:", a.PlayerID);
                    x = Convert.ToInt32(ReadLine());
                    c -= 2;
                    WriteLine("kkkkkk{0}", c);
                    //return k;
                    //counter++;
                }
                while (x == 111) // x == 111. redo
                {
                    Redo(arr, arr_r, c, counter);
                    SetCursorPosition(0, 33);
                    WriteLine("Player{0} enter position:", a.PlayerID);
                    x = Convert.ToInt32(ReadLine());
                    counter--;
                }
                if (x == 333) // x == 333, save game
                {
                    SaveGame(arr, c);
                }
                else if (x == 555)
                {
                    System.Environment.Exit(1);
                }
                while (x < 0 || x > 15)
                {
                    WriteLine("Please Enter Valid Number 0~15.");
                    x = Convert.ToInt32(ReadLine());
                }
                a.PositionX = x;

                y = Convert.ToInt32(ReadLine());
                while (y < 0 || y > 15)
                {
                    WriteLine("Please Enter Valid Number 0~15.");
                    y = Convert.ToInt32(ReadLine());
                }
                a.PositionY = y;
                validator = CheckValidity(a, arr);
                if (validator == 1)
                {
                    Move(a, b, arr, arr_r, c);
                }
                winner_m = CheckWinner(a, arr);

                SetCursorPosition(0, 34);
                Write("     ");
                SetCursorPosition(0, 35);
                Write("     ");
                return (c, winner_m);
            }
            //return (c, winner_m);

        }

        public static void Undo(Player[] arr, Player[] arr_r, int c, int t) // undo method
        {
            arr_r[t].PlayerID = arr_r[c - 1].PlayerID;
            //Write("{0}*/**{1}**5656456", arr_r[t].PlayerID, arr[c - 1].PlayerID);
            arr_r[t].PositionX = arr[c - 1].PositionX;
            //Write("{0}*/****5656456", arr_r[t].PositionX);
            arr_r[t].PositionY = arr[c - 1].PositionY;
            arr_r[t + 1].PlayerID = arr_r[c - 2].PlayerID;
            arr_r[t + 1].PositionX = arr[c - 2].PositionX;
            arr_r[t + 1].PositionY = arr[c - 2].PositionY;
            SetCursorPosition(arr[c - 1].PositionX + (3 * arr[c - 1].PositionX) + 3, arr[c - 1].PositionY + (1 * arr[c - 1].PositionY) + 1);
            WriteLine("-");
            SetCursorPosition(arr[c - 2].PositionX + (3 * arr[c - 2].PositionX) + 3, arr[c - 2].PositionY + (1 * arr[c - 2].PositionY) + 1);
            WriteLine("-");
            //Board.UpateBoard(arr[c-2]);
        }

        public static void Redo(Player[] arr, Player[] arr_r, int c, int t) // redo method
        {
            Write("{0}*/****5656456", arr_r[t].PositionX);
            Write("{0}*/****5656456", arr_r[t].PlayerID);
            //arr[c] = arr_r[t];
            if (arr_r[c - 1].PlayerID == 1)
            {
                SetCursorPosition(arr_r[t].PositionX + (3 * arr_r[t].PositionX) + 3, arr_r[t].PositionY + (1 * arr_r[t].PositionY) + 1);
                WriteLine("X,****++--{0}", arr_r[t].PositionX);
                SetCursorPosition(arr_r[t - 1].PositionX + (3 * arr_r[t - 1].PositionX) + 3, arr_r[t - 1].PositionY + (1 * arr_r[t - 1].PositionY) + 1);
                WriteLine("0");
            }
            else
            {
                SetCursorPosition(arr[c].PositionX + (3 * arr[c].PositionX) + 3, arr[c].PositionY + (1 * arr[c].PositionY) + 1);
                WriteLine("0++3");
                SetCursorPosition(arr[c + 1].PositionX + (3 * arr[c + 1].PositionX) + 3, arr[c + 1].PositionY + (1 * arr[c + 1].PositionY) + 1);
                WriteLine("X");
            }
        }

        public static void SaveGame(Player[] arr, int c) // savegame method
        {
            const string DELIM = ",";
            WriteLine("Enter your file name: ");
            string FILENAME = ReadLine() + ".txt";
            while (File.Exists(FILENAME))
            {
                WriteLine("Please rename your save file: ");
                FILENAME = ReadLine() + ".txt";
            }
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            for (int i = 0; i < c; i++)
            {
                writer.WriteLine(i + DELIM + arr[i].PlayerID + DELIM + arr[i].PositionX + DELIM + arr[i].PositionY);
            }
            writer.Close();
            outFile.Close();

        }

        //check if player a enter same position with player b 
        //this method still need modified.
        public static int CheckValidity(Player a, Player[] arr) // check moves' validity
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (a.PositionX == arr[i].PositionX && a.PositionY == arr[i].PositionY)
                {
                    return 1;
                }
            }
            return 0;
        }

        public static int CheckWinner(Player a, Player[] arr) // check winner
        {
            int[,] checkarr = new int[16, 16];
            int winner_c = 0;
            int w = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].PlayerID == a.PlayerID)
                {
                    checkarr[arr[i].PositionX, arr[i].PositionY] = 1;
                }
            }
            checkarr[a.PositionX, a.PositionY] = 1;
            //Write("\n{0}**", a.PlayerID);

            // scan the board for searching the winner from top left side of the board.
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (checkarr[i, j] == 1)
                    {
                        if (i <= 11)
                        {
                            for (int g1 = i; g1 <= i + 4; g1++)//from left to right
                            {
                                if (checkarr[g1, j] == 1)
                                {
                                    winner_c++;
                                }
                                if (winner_c > 4)
                                {
                                    //SetCursorPosition(0, 50);
                                    //Write("winner");
                                    w = a.PlayerID;
                                    return w;
                                }
                            }
                        }
                        winner_c = 0;
                        if (j <= 11)
                        {
                            for (int g2 = j; g2 <= j + 4; g2++)//from top to down
                            {
                                if (checkarr[i, g2] == 1)
                                {
                                    winner_c++;
                                }
                                if (winner_c > 4)
                                {
                                    //SetCursorPosition(0, 50);
                                    //Write("winner");
                                    w = a.PlayerID;
                                    return w;
                                }
                            }
                        }
                        winner_c = 0;
                        if (j <= 11 && i <= 11)
                        {
                            for (int g3 = i; g3 <= i + 4; g3++)// down right diagnol
                            {
                                for (int g4 = j; g4 <= j + 4; g4++)
                                {
                                    if (g3 - i == g4 - j)
                                    {
                                        if (checkarr[g3, g4] == 1)
                                        {
                                            winner_c++;
                                        }
                                        if (winner_c > 4)
                                        {
                                            //SetCursorPosition(0, 50);
                                            //Write("winner");
                                            w = a.PlayerID;
                                            return w;
                                        }
                                    }
                                }
                            }
                        }
                        winner_c = 0;
                        if (j >= 4 && i <= 11)
                        {
                            for (int g3 = i; g3 <= i + 4; g3++)// top right diagnol
                            {
                                for (int g4 = j; g4 >= j - 4; g4--)
                                {
                                    if (g3 - i == j - g4)
                                    {
                                        if (checkarr[g3, g4] == 1)
                                        {
                                            winner_c++;
                                        }
                                        if (winner_c > 4)
                                        {
                                            // SetCursorPosition(0, 50);
                                            //Write("winner");
                                            w = a.PlayerID;
                                            return w;
                                        }
                                    }
                                }
                            }
                        }
                        winner_c = 0;
                    }
                }
            }
            return 0;
        }


    }//end class Gomuku

    class Board
    {
        public static void ShowBoard() // initiate gomuku board
        {
            WriteLine("   0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  ");
            WriteLine("0  --------------------------------------------------------------");

            for (int i = 0; i < 9; i++)
            {
                WriteLine("   |   |   |   |   |   |   |   |   |   |   |   |   |   |   |   |   ");
                WriteLine("{0}  --------------------------------------------------------------", i + 1);
            }
            for (int i = 9; i < 15; i++)
            {
                WriteLine("   |   |   |   |   |   |   |   |   |   |   |   |   |   |   |   |   ");
                WriteLine("{0} --------------------------------------------------------------", i + 1);
            }


            SetCursorPosition(70, 2);
            Write("Instructions:");
            SetCursorPosition(70, 3);
            Write("input 999");
        }
        public static void UpateBoard(Player p) // update board after moves or any instruction
        {
            if (p.PlayerID == 1)
            {
                SetCursorPosition(p.PositionX + (3 * p.PositionX) + 3, p.PositionY + (1 * p.PositionY) + 1);
                Write('O');
            }
            else
            {
                SetCursorPosition(p.PositionX + (3 * p.PositionX) + 3, p.PositionY + (1 * p.PositionY) + 1);
                Write('X');
            }
        }
    }

    class Storage { }

    class ExitGameIns
    {
    }

    class SaveLoadGame
    {
        public static void Save()
        {

        }

        public static void Load()
        {

        }
    }

}

