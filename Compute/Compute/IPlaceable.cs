using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public interface IPlaceable
    {

        //from which direction can it receive input/output
        List<Direction> InputDirections { get; set; }
        List<Direction> OutputDirections { get; set; }

        //amount of input/output it receives before ticking
        int InputCount { get; set; }
        List<Moveable> CurrentInput { get; set; }
        int OutputCount { get; set; }

        //checks if this objekt can be removed
        bool Destructable { get; set; }

        //determines if it has to tick
        bool PrepareTick();

        //execute a tick
        void ExecuteTick();

        //new input received
        void ReceiveInput(MoveOrder moveOrder);

        //new output produced
        void ReleaseOutput(MoveOrder moveOrder);
    }
}
