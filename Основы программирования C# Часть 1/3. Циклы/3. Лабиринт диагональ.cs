using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                if (width > height)
                {
                    MoveRight(robot, width, height);
                    MoveDown(robot, height, width);
                }
                else
                {
                    MoveDown(robot, height, width);
                    MoveRight(robot, width, height);
                }
            }
        }

        public static void MoveRight(Robot robot, int width, int height)
        {
            for (int i = 0; i <= (width / 2 - height / 2) / (height / 2 - 1) && robot.X < width - 2; i++)
            {
                robot.MoveTo(Direction.Right);
            }
        }

        public static void MoveDown(Robot robot, int height, int width)
        {
            for (int i = 0; i <= (height / 2 - width / 2) / (width / 2 - 1) && robot.Y < height - 2; i++)
            {
                robot.MoveTo(Direction.Down);
            }
        }
    }
}