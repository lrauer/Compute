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

        }

        bool keep;

        public override void ReceiveInput(MoveOrder moveOrder)
        {
            CurrentInput.Add(moveOrder.Objekt);
            moveOrder.Objekt.Position.Parent = this;
            moveOrder.Objekt.Position.Set(0, 0);
            if (moveOrder.Order == Direction.Parent)
            {
                keep = true;
            }
            else
            {
                keep = false;
            }
        }

        public override void ReleaseOutput(MoveOrder moveOrder)
        {
            CurrentInput.Clear();
            if (keep)
            {
                Position.Parent.ReleaseOutput(moveOrder);
            }
            else
            {
                //set position of output to own position to return it 1 level
                moveOrder.Objekt.Position.Copy(Position);
                moveOrder.Order = DirectionHelper.Invert(moveOrder.Order);
                Position.Parent.ReleaseOutput(moveOrder);
            }
        }

    }
}
