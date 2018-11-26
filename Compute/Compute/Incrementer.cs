using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Incrementer: AssemblyLine
    {
        public Incrementer(Position position, Direction output, bool destructable)
            : base(position, output, destructable)
        {

        }

        public override void ExecuteTick()
        {
            CurrentInput[0].Value++;
            base.ExecuteTick();
        }
    }
}
