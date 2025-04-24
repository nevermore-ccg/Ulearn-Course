using SkiaSharp;

namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                MoveRight(robot, width);
                MoveDown(robot, height);
                MoveLeft(robot, width);
                MoveDown(robot, height);
            }
        }

        public static void MoveRight(Robot robot, int width)
        {
            while (robot.X < width - 2)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveLeft(Robot robot, int width)
        {
            while (robot.X != 1)
                robot.MoveTo(Direction.Left);
        }

        public static void MoveDown(Robot robot, int height)
        {
            for (int i = 0; i < 2 && robot.Y < height - 2; i++)
                robot.MoveTo(Direction.Down);
        }
    }
}