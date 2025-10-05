namespace ToyRobotGame.Tests;

using NUnit.Framework;
using ToyRobotGame.Core;
using ToyRobotGame.Core.Models;

[TestFixture]
public class BoardTests
{
    private Board board;

    [SetUp]
    public void Setup()
    {
        board = new Board();
    }

    [Test]
    public void PlaceRobot_ValidPosition_ReturnsTrue()
    {
        var result = board.PlaceRobot(2, 3, Direction.NORTH);

        Assert.That(result, Is.True);
        Assert.That(board.Report(), Is.EqualTo("2,3,NORTH"));
    }

    [TestCase(0, 3)]
    [TestCase(6, 3)]
    [TestCase(3, 0)]
    [TestCase(3, 6)]
    public void PlaceRobot_InvalidPosition_ReturnsFalse(int row, int col)
    {
        var result = board.PlaceRobot(row, col, Direction.NORTH);

        Assert.That(result, Is.False);
        Assert.That(board.Report(), Is.Null);
    }

    [Test]
    public void PlaceRobot_MovesExistingRobot()
    {
        board.PlaceRobot(1, 1, Direction.NORTH);
        board.PlaceRobot(2, 2, Direction.SOUTH);

        Assert.That(board.Report(), Is.EqualTo("2,2,SOUTH"));
    }

    [Test]
    public void PlaceWall_ValidPosition_ReturnsTrue()
    {
        var result = board.PlaceWall(2, 3);

        Assert.That(result, Is.True);
    }

    [Test]
    public void PlaceWall_OnRobot_ReturnsFalse()
    {
        board.PlaceRobot(2, 3, Direction.NORTH);
        var result = board.PlaceWall(2, 3);

        Assert.That(result, Is.False);
    }

    [Test]
    public void PlaceWall_OnExistingWall_ReturnsFalse()
    {
        board.PlaceWall(2, 3);
        var result = board.PlaceWall(2, 3);

        Assert.That(result, Is.False);
    }

    [Test]
    public void PlaceRobot_OnWall_ReturnsFalse()
    {
        board.PlaceWall(2, 3);
        var result = board.PlaceRobot(2, 3, Direction.NORTH);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Move_North_IncreasesRow()
    {
        board.PlaceRobot(1, 1, Direction.NORTH);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("1,2,NORTH"));
    }

    [Test]
    public void Move_South_DecreasesRow()
    {
        board.PlaceRobot(2, 2, Direction.SOUTH);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("2,1,SOUTH"));
    }

    [Test]
    public void Move_East_IncreasesCol()
    {
        board.PlaceRobot(2, 2, Direction.EAST);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("2,3,EAST"));
    }

    [Test]
    public void Move_West_DecreasesCol()
    {
        board.PlaceRobot(2, 2, Direction.WEST);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("2,1,WEST"));
    }

    [Test]
    public void Move_AtSouthEdge_WrapsToNorth()
    {
        board.PlaceRobot(1, 1, Direction.SOUTH);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("1,5,SOUTH"));
    }

    [Test]
    public void Move_AtNorthEdge_WrapsToSouth()
    {
        board.PlaceRobot(1, 5, Direction.NORTH);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("1,1,NORTH"));
    }

    [Test]
    public void Move_AtWestEdge_WrapsToEast()
    {
        board.PlaceRobot(1, 1, Direction.WEST);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("1,5,WEST"));
    }

    [Test]
    public void Move_AtEastEdge_WrapsToWest()
    {
        board.PlaceRobot(1, 5, Direction.EAST);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("1,1,EAST"));
    }

    [Test]
    public void Move_IntoWall_StaysInPlace()
    {
        board.PlaceRobot(1, 1, Direction.NORTH);
        board.PlaceWall(1, 2);
        board.Move();

        Assert.That(board.Report(), Is.EqualTo("1,1,NORTH"));
    }

    [Test]
    public void Move_NoRobot_ReturnsFalse()
    {
        var result = board.Move();

        Assert.That(result, Is.False);
    }

    [Test]
    public void TurnLeft_FromNorth_FacesWest()
    {
        board.PlaceRobot(1, 1, Direction.NORTH);
        board.TurnLeft();

        Assert.That(board.Report(), Is.EqualTo("1,1,WEST"));
    }

    [Test]
    public void TurnLeft_FromWest_FacesSouth()
    {
        board.PlaceRobot(1, 1, Direction.WEST);
        board.TurnLeft();

        Assert.That(board.Report(), Is.EqualTo("1,1,SOUTH"));
    }

    [Test]
    public void TurnLeft_FromSouth_FacesEast()
    {
        board.PlaceRobot(1, 1, Direction.SOUTH);
        board.TurnLeft();

        Assert.That(board.Report(), Is.EqualTo("1,1,EAST"));
    }

    [Test]
    public void TurnLeft_FromEast_FacesNorth()
    {
        board.PlaceRobot(1, 1, Direction.EAST);
        board.TurnLeft();

        Assert.That(board.Report(), Is.EqualTo("1,1,NORTH"));
    }

    [Test]
    public void TurnRight_FromNorth_FacesEast()
    {
        board.PlaceRobot(1, 1, Direction.NORTH);
        board.TurnRight();

        Assert.That(board.Report(), Is.EqualTo("1,1,EAST"));
    }

    [Test]
    public void TurnRight_FromEast_FacesSouth()
    {
        board.PlaceRobot(1, 1, Direction.EAST);
        board.TurnRight();

        Assert.That(board.Report(), Is.EqualTo("1,1,SOUTH"));
    }

    [Test]
    public void TurnRight_FromSouth_FacesWest()
    {
        board.PlaceRobot(1, 1, Direction.SOUTH);
        board.TurnRight();

        Assert.That(board.Report(), Is.EqualTo("1,1,WEST"));
    }

    [Test]
    public void TurnRight_FromWest_FacesNorth()
    {
        board.PlaceRobot(1, 1, Direction.WEST);
        board.TurnRight();

        Assert.That(board.Report(), Is.EqualTo("1,1,NORTH"));
    }

    [Test]
    public void TurnLeft_NoRobot_ReturnsFalse()
    {
        var result = board.TurnLeft();

        Assert.That(result, Is.False);
    }

    [Test]
    public void TurnRight_NoRobot_ReturnsFalse()
    {
        var result = board.TurnRight();

        Assert.That(result, Is.False);
    }

    [Test]
    public void Report_NoRobot_ReturnsNull()
    {
        Assert.That(board.Report(), Is.Null);
    }

    [Test]
    public void IsValidPosition_ValidCoordinates_ReturnsTrue()
    {
        Assert.That(board.IsValidPosition(1, 1), Is.True);
        Assert.That(board.IsValidPosition(5, 5), Is.True);
        Assert.That(board.IsValidPosition(3, 3), Is.True);
    }

    [Test]
    public void IsValidPosition_InvalidCoordinates_ReturnsFalse()
    {
        Assert.That(board.IsValidPosition(0, 1), Is.False);
        Assert.That(board.IsValidPosition(6, 1), Is.False);
        Assert.That(board.IsValidPosition(1, 0), Is.False);
        Assert.That(board.IsValidPosition(1, 6), Is.False);
    }
}