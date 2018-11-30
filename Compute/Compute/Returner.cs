using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    class Returner : AssemblyLine
    {
        public Returner(Position position, Direction output, bool destructable)
            : base(position, output, destructable)
        {
            keep = new List<bool>();
        }

        List<bool> keep;

        public override bool PrepareTick()
        {
            //returner can process more than 1 input but will only output the first input per tick
            switch (CurrentInput.Count)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }

        public override void ReceiveInput(MoveOrder moveOrder)
        {
            CurrentInput.Add(moveOrder.Objekt);
            moveOrder.Objekt.Position.Parent = this;
            moveOrder.Objekt.Position.Set(0, 0);
            if (moveOrder.Order == Direction.Parent)
            {
                keep.Add(true);
            }
            else
            {
                keep.Add(false);
            }
        }

        public override void ReleaseOutput(MoveOrder moveOrder)
        {
            CurrentInput.RemoveAt(0);
            if (keep[0])
            {
                Position.Parent.ReleaseOutput(moveOrder);
            }
            else
            {
                //set position of output to own position to return it 1 level
                moveOrder.Objekt.Position = Position.Copy(Position);
                moveOrder.Order = DirectionHelper.Invert(moveOrder.Order);
                Position.Parent.ReleaseOutput(moveOrder);
            }
            keep.RemoveAt(0);
        }

    }
}
