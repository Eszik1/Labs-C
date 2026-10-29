
using Laba2;

class Program
{
    static void Main(string[] args)
    {
        const string inputFile = "ChaseData.txt";
        const string outputFile = "PursuitLog.txt";

        string firstLine = File.ReadLines(inputFile).First();
        int size = int.Parse(firstLine);

        Game game = new Game(size, inputFile, outputFile);
        game.Run();

        Console.WriteLine("Game finished.");
        Console.WriteLine($"Results saved to {outputFile}");
    }
}
