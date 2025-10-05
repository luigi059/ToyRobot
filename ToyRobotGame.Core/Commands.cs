using ToyRobotGame.Core.Models;

namespace ToyRobotGame.Core;

public interface ICommand
{
    void Execute(Board board);
}

public class PlaceRobotCommand : ICommand
{
    private readonly int _row;
    private readonly int _col;
    private readonly Direction _facing;

    public PlaceRobotCommand(int row, int col, Direction facing)
    {
        _row = row;
        _col = col;
        _facing = facing;
    }

    public void Execute(Board board) => board.PlaceRobot(_row, _col, _facing);
}

public class PlaceWallCommand : ICommand
{
    private readonly int _row;
    private readonly int _col;

    public PlaceWallCommand(int row, int col)
    {
        _row = row;
        _col = col;
    }

    public void Execute(Board board) => board.PlaceWall(_row, _col);
}

public class MoveCommand : ICommand
{
    public void Execute(Board board) => board.Move();
}

public class LeftCommand : ICommand
{
    public void Execute(Board board) => board.TurnLeft();
}

public class RightCommand : ICommand
{
    public void Execute(Board board) => board.TurnRight();
}

public class ReportCommand : ICommand
{
    public void Execute(Board board)
    {
        var result = board.Report();
        if (result != null)
        {
            Console.WriteLine(result);
        }
    }
}

public static class CommandParser
{
    public static ICommand? Parse(string input)
    {
        var trimmed = input.Trim();

        if (trimmed.StartsWith("PLACE_ROBOT "))
        {
            return ParsePlaceRobot(trimmed);
        }

        if (trimmed.StartsWith("PLACE_WALL "))
        {
            return ParsePlaceWall(trimmed);
        }

        switch (trimmed)
        {
            case "MOVE":
                return new MoveCommand();
            case "LEFT":
                return new LeftCommand();
            case "RIGHT":
                return new RightCommand();
            case "REPORT":
                return new ReportCommand();
            default:
                return null;
        }
    }

    private static ICommand? ParsePlaceRobot(string input)
    {
        var parts = input.Substring("PLACE_ROBOT ".Length).Split(',');

        if (parts.Length != 3)
        {
            return null;
        }

        if (!int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
        {
            return null;
        }

        if (!Enum.TryParse<Direction>(parts[2], out var facing))
        {
            return null;
        }

        if (row < 1 || row > 5 || col < 1 || col > 5)
        {
            return null;
        }

        return new PlaceRobotCommand(row, col, facing);
    }

    private static ICommand? ParsePlaceWall(string input)
    {
        var parts = input.Substring("PLACE_WALL ".Length).Split(',');

        if (parts.Length != 2)
        {
            return null;
        }

        if (!int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
        {
            return null;
        }

        return new PlaceWallCommand(row, col);
    }
}
