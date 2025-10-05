using ToyRobotGame.Core;

namespace ToyRobotGame;

class Program
{
    static void Main(string[] args)
    {
        var board = new Board();
        // Read from file
        if (args.Length > 0)
        {
            ProcessFile(args[0], board);
        }
        // Standard Input
        else
        {
            ProcessInput(board);
        }
    }

    static void ProcessFile(string filePath, Board board)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                ProcessCommand(line, board);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    static void ProcessInput(Board board)
    {
        System.Console.WriteLine("Toy Robot Game - Please Enter commands ('EXIT' to quit):");

        string? input;
        while ((input = System.Console.ReadLine()) != null)
        {
            if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            ProcessCommand(input, board);
        }
    }

    static void ProcessCommand(string input, Board board)
    {
        if (string.IsNullOrWhiteSpace(input) || input.TrimStart().StartsWith("#"))
        {
            return;
        }

        var command = CommandParser.Parse(input);
        command?.Execute(board);
    }
}