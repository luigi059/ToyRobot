using ToyRobotGame.Core;

namespace ToyRobotGame.Tests;

[TestFixture]
public class IntegrationTests
{
    private Board board;

    [SetUp]
    public void Setup()
    {
        board = new Board();
    }

    [Test]
    public void TestScenario1_ComplexMovementWithWall()
    {

        var commands = new[]
        {
            "PLACE_ROBOT 3,3,NORTH",
            "PLACE_WALL 3,5",
            "MOVE",
            "MOVE",
            "RIGHT",
            "MOVE",
            "MOVE",
            "MOVE",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("1,4,EAST"));
    }

    [Test]
    public void TestScenario2_MultipleWallsAndTurns()
    {

        var commands = new[]
        {
            "PLACE_ROBOT 2,2,WEST",
            "PLACE_WALL 1,1",
            "PLACE_WALL 2,2",  // This should be ignored as robot is there
            "PLACE_WALL 1,3",
            "LEFT",
            "LEFT",
            "MOVE",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("3,2,EAST"));
    }

    [Test]
    public void TestScenario3_EdgeWrapping()
    {
        var commands = new[]
        {
            "PLACE_ROBOT 1,1,SOUTH",
            "MOVE",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("1,5,SOUTH"));
    }

    [Test]
    public void TestScenario4_IgnoreInvalidCommands()
    {
        var commands = new[]
        {
            "PLACE_ROBOT 2,3,NORTH",
            "PLACE_ROBOT 2,3,CENTER",  // Invalid Direction
            "PLACE_ROBOT 2,6,EAST",    // Invalid Column
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("2,3,NORTH"));
    }

    [Test]
    public void TestScenario5_CommandsBeforePlacement()
    {
        var commands = new[]
        {
            "MOVE",
            "LEFT",
            "RIGHT",
            "REPORT",
            "PLACE_ROBOT 3,3,EAST",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("3,3,EAST"));
    }

    [Test]
    public void TestScenario6_RobotBlockedByWall()
    {
        var commands = new[]
        {
            "PLACE_ROBOT 2,2,NORTH",
            "PLACE_WALL 2,3",
            "MOVE",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("2,2,NORTH"));
    }

    [Test]
    public void TestScenario7_CompleteRotation()
    {
        var commands = new[]
        {
            "PLACE_ROBOT 3,3,NORTH",
            "RIGHT",
            "RIGHT",
            "RIGHT",
            "RIGHT",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("3,3,NORTH"));
    }

    [Test]
    public void TestScenario8_ReplacingRobot()
    {
        var commands = new[]
        {
            "PLACE_ROBOT 1,1,NORTH",
            "PLACE_ROBOT 5,5,SOUTH",
            "REPORT"
        };

        ExecuteCommands(commands);

        Assert.That(board.Report(), Is.EqualTo("5,5,SOUTH"));
    }

    private void ExecuteCommands(string[] commands)
    {
        foreach (var commandStr in commands)
        {
            var command = CommandParser.Parse(commandStr);
            command?.Execute(board);
        }
    }
}
