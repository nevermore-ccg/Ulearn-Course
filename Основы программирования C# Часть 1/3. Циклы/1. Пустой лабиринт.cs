using System;
using System.Diagnostics.Contracts;

namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                MoveDirection(robot, width, height, Direction.Right);
                MoveDirection(robot, width, height, Direction.Down);
                MoveDirection(robot, width, height, Direction.Left);
                MoveDirection(robot, width, height, Direction.Up);
            }
        }

        public static void MoveDirection(Robot robot, int width, int height, Direction direction)
        {
            if (direction == Direction.Left || direction == Direction.Right)
            {
                while (robot.X < width - 2)
                    robot.MoveTo(direction);
            }
            else
            {
                while (robot.Y < height - 2)
                    robot.MoveTo(direction);
            }
        }
    }
}