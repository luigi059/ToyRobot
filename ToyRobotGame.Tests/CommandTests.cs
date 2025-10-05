using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ToyRobotGame.Core;

namespace ToyRobotGame.Tests;

[TestFixture]
public class CommandParserTests
{
    [Test]
    public void Parse_ValidPlaceRobot_ReturnsPlaceRobotCommand()
    {
        var command = CommandParser.Parse("PLACE_ROBOT 2,3,NORTH");

        Assert.That(command, Is.Not.Null);
        Assert.That(command, Is.TypeOf<PlaceRobotCommand>());
    }

    [Test]
    public void Parse_PlaceRobotWithSpaces_ReturnsCommand()
    {
        var command = CommandParser.Parse("  PLACE_ROBOT 2,3,NORTH  ");

        Assert.That(command, Is.Not.Null);
        Assert.That(command, Is.TypeOf<PlaceRobotCommand>());
    }

    [TestCase("PLACE_ROBOT 2,3,CENTER")]
    [TestCase("PLACE_ROBOT 2,6,EAST")]
    [TestCase("PLACE_ROBOT 2,3")]
    [TestCase("PLACE_ROBOT 2,abc,NORTH")]
    [TestCase("PLACE_ROBOT")]
    public void Parse_InvalidPlaceRobot_ReturnsNull(string input)
    {
        var command = CommandParser.Parse(input);

        Assert.That(command, Is.Null);
    }

    [Test]
    public void Parse_ValidPlaceWall_ReturnsPlaceWallCommand()
    {
        var command = CommandParser.Parse("PLACE_WALL 2,3");

        Assert.That(command, Is.Not.Null);
        Assert.That(command, Is.TypeOf<PlaceWallCommand>());
    }

    [TestCase("PLACE_WALL 2")]
    [TestCase("PLACE_WALL abc,3")]
    [TestCase("PLACE_WALL")]
    public void Parse_InvalidPlaceWall_ReturnsNull(string input)
    {
        var command = CommandParser.Parse(input);

        Assert.That(command, Is.Null);
    }

    [Test]
    public void Parse_Move_ReturnsMoveCommand()
    {
        var command = CommandParser.Parse("MOVE");

        Assert.That(command, Is.TypeOf<MoveCommand>());
    }

    [Test]
    public void Parse_Left_ReturnsLeftCommand()
    {
        var command = CommandParser.Parse("LEFT");

        Assert.That(command, Is.TypeOf<LeftCommand>());
    }

    [Test]
    public void Parse_Right_ReturnsRightCommand()
    {
        var command = CommandParser.Parse("RIGHT");

        Assert.That(command, Is.TypeOf<RightCommand>());
    }

    [Test]
    public void Parse_Report_ReturnsReportCommand()
    {
        var command = CommandParser.Parse("REPORT");

        Assert.That(command, Is.TypeOf<ReportCommand>());
    }

    [TestCase("JUMP")]
    [TestCase("INVALID")]
    [TestCase("")]
    [TestCase("   ")]
    public void Parse_UnknownCommand_ReturnsNull(string input)
    {
        var command = CommandParser.Parse(input);

        Assert.That(command, Is.Null);
    }

    [Test]
    public void Parse_CaseInsensitiveCommands_ParsesCorrectly()
    {
        var commandUpper = CommandParser.Parse("MOVE");
        var commandLower = CommandParser.Parse("move");

        Assert.That(commandUpper, Is.TypeOf<MoveCommand>());
        Assert.That(commandLower, Is.Null);
    }
}
