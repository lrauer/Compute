using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class AssemblyLine: PlaceableObjekt
    {

        public AssemblyLine(Position position, Direction output, bool destructable) :base(position)
        {
            Position = position;
            OutputDirections.Add(output);
            InputDirections.Add(Direction.Down);
            InputDirections.Add(Direction.Up);
            InputDirections.Add(Direction.Left);
            InputDirections.Add(Direction.Right);
            Destructable = destructable;
            InputCount = 1;
            OutputCount = 1;
        }

        public override bool PrepareTick()
        {
            switch (CurrentInput.Count)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new ArgumentException("Assemblyline can only process 1 input");
            }
        }

        public override void ExecuteTick()
        {
            ReleaseOutput(new MoveOrder(CurrentInput[0],OutputDirections[0]));
        }

        public override void ReceiveInput(MoveOrder moveOrder)
        {
            CurrentInput.Add(moveOrder.Objekt);
            moveOrder.Objekt.Position.Parent = this;
            moveOrder.Objekt.Position.Set(0, 0);
        }

        public override void ReleaseOutput(MoveOrder moveOrder)
        {
            CurrentInput.Clear();
            Position.Parent.ReleaseOutput(moveOrder);
        }
    }
}
