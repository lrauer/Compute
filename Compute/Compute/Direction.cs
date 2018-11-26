using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public static class DirectionHelper
    {

        public static Direction Invert(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
            }

            throw new ArgumentException("Impossible direction for invert");
        }

    }

    public enum Direction
    {
        Left, Right, Up, Down, Parent

    }
}
