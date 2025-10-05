using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotGame.Core.Models;

public class Robot
{
    public Position Position { get; set; }
    public Direction Facing { get; set; }

    public Robot(Position position, Direction facing)
    {
        Position = position;
        Facing = facing;
    }
}