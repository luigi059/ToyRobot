namespace ToyRobotGame.Core;

using ToyRobotGame.Core.Models;

public class Board
{
    private const int BoardSize = 5;
    private readonly HashSet<Position> walls = new();
    private Robot? robot;

    public bool IsValidPosition(int row, int col)
    {
        return row >= 1 && row <= BoardSize && col >= 1 && col <= BoardSize;
    }

    public bool PlaceRobot(int row, int col, Direction facing)
    {
        if (!IsValidPosition(row, col))
        {
            return false;
        }

        var position = new Position(row, col);

        if (walls.Contains(position)) return false;

        robot = new Robot(position, facing);
        return true;
    }

    public bool PlaceWall(int row, int col)
    {
        if (!IsValidPosition(row, col))
        {
            return false;
        }

        var position = new Position(row, col);

        if (robot?.Position == position || walls.Contains(position)) return false;

        walls.Add(position);
        return true;
    }

    public string? Report()
    {
        return robot == null ? null : $"{robot.Position.Row},{robot.Position.Col},{robot.Facing}";
    }

    public bool Move()
    {
        if (robot == null)
            return false;

        var newPosition = CalculateNewPosition(robot.Position, robot.Facing);

        if (walls.Contains(newPosition))
        {
            return false;
        }

        robot.Position = newPosition;
        return true;
    }

    private Position CalculateNewPosition(Position current, Direction facing)
    {
        var (row, col) = current;

        switch (facing)
        {
            case Direction.NORTH:
                return new Position(row, WrapCoordinate(col + 1));
            case Direction.SOUTH:
                return new Position(row, WrapCoordinate(col - 1));
            case Direction.EAST:
                return new Position(WrapCoordinate(row + 1), col);
            case Direction.WEST:
                return new Position(WrapCoordinate(row - 1), col);
            default:
                return current;
        }
    }

    private int WrapCoordinate(int coordinate)
    {
        if (coordinate < 1) return BoardSize;
        if (coordinate > BoardSize) return 1;
        return coordinate;
    }

    public bool TurnLeft()
    {
        if (robot == null)
            return false;

        switch (robot.Facing)
        {
            case Direction.NORTH:
                robot.Facing = Direction.WEST;
                break;
            case Direction.WEST:
                robot.Facing = Direction.SOUTH;
                break;
            case Direction.SOUTH:
                robot.Facing = Direction.EAST;
                break;
            case Direction.EAST:
                robot.Facing = Direction.NORTH;
                break;
        }
        return true;
    }

    public bool TurnRight()
    {
        if (robot == null)
            return false;

        switch (robot.Facing)
        {
            case Direction.NORTH:
                robot.Facing = Direction.EAST;
                break;
            case Direction.EAST:
                robot.Facing = Direction.SOUTH;
                break;
            case Direction.SOUTH:
                robot.Facing = Direction.WEST;
                break;
            case Direction.WEST:
                robot.Facing = Direction.NORTH;
                break;
        }

        return true;
    }
}
