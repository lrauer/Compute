using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class QueueValidationEventArgs: QueueEventArgs
    {

        //the actual Order that the system calculated
        MoveOrder _ActualOrder;

        public MoveOrder ActualOrder
        {
            get { return _ActualOrder; }
            set { _ActualOrder = value; }
        }

        public bool Correct()
        {
            return EventOrder.CompareTo(ActualOrder);
        }

        public QueueValidationEventArgs(Queue<MoveOrder> eventQueue, MoveOrder eventOrder, int maxSize, MoveOrder actualOrder)
            : base(eventQueue, eventOrder, maxSize)
        {
            ActualOrder = actualOrder;
        }
    }
}
