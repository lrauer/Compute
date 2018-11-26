using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Comparer: AssemblyLine
    {

        int _TargetValue;

        public int TargetValue
        {
                get { return _TargetValue; }
                set { _TargetValue = value; }
        }

        public Comparer(Position position, Direction output, Direction alternative, int targetValue, bool destructable)
            : base(position, output, destructable)
        {
            TargetValue = targetValue;
            OutputDirections.Add(alternative);
        }

        public override void ExecuteTick()
        {
            if (TargetValue == CurrentInput[0].Value)
            {
                ReleaseOutput(new MoveOrder(CurrentInput[0], OutputDirections[0]));
            }
            else
            {
                ReleaseOutput(new MoveOrder(CurrentInput[0], OutputDirections[1]));
            }
        }

    }
}
