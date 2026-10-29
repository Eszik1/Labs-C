using System;
using System.Collections.Generic;
using System.IO;

namespace Laba2
{

    public class Game
    {
        public int size;
        public Player cat;
        public Player mouse;

        private readonly string inputFile;
        private readonly string outputFile;
        private readonly List<string> printRows = new List<string>();

        public Game(int size, string inputFile = "ChaseData.txt",
            string outputFile = "PursuitLog.txt")
        {
            this.size = size;
            this.inputFile = inputFile;
            this.outputFile = outputFile;

            cat = new Player("Cat");
            mouse = new Player("Mouse");
        }

        public void Run()
        {
            using (StreamReader reader = new StreamReader(inputFile))
            {
                
                reader.ReadLine();

                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length == 0)
                        continue;

                    string[] parts = line.Split(
                        ' ', StringSplitOptions.RemoveEmptyEntries);

                    char command = char.ToUpperInvariant(parts[0][0]);

                    if (command == 'P')
                    {
                        DoPrintCommand();
                    }
                    else if (command == 'M' || command == 'C')
                    {
                        int steps = int.Parse(parts[1]);
                        DoMoveCommand(command, steps);
                    }
                }
            }

           
            File.WriteAllLines(outputFile, printRows);
        }

        private void DoMoveCommand(char command, int steps)
        {
            switch (command)
            {
                case 'M':
                    mouse.Move(steps, size);
                    break;

                case 'C':
                    cat.Move(steps, size);
                    break;
            }
        }

        private int GetDistance()
        {
            if (cat.state == Player.State.NotInGame ||
                mouse.state == Player.State.NotInGame)
            {
                return -1;
            }

            return Math.Abs(cat.location - mouse.location);
        }

        private void DoPrintCommand()
        {
            int distance = GetDistance();

            string catPosition =
                cat.state == Player.State.NotInGame
                    ? "??" : cat.location.ToString();

            string mousePosition =
                mouse.state == Player.State.NotInGame
                    ? "??" : mouse.location.ToString();

            string distanceText =
                distance < 0 ? "??" : distance.ToString();

            printRows.Add(
                $"{catPosition,-10}{mousePosition,-10}{distanceText}");
        }
    }

}
