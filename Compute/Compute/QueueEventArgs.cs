using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class QueueEventArgs
    {
        Queue<MoveOrder> _EventQueue;

        public Queue<MoveOrder> EventQueue
        {
            get { return _EventQueue; }
            set { _EventQueue = value; }
        }

        MoveOrder _EventOrder;

        public MoveOrder EventOrder
        {
            get { return _EventOrder; }
            set { _EventOrder = value; }
        }

        int _MaxSize;

        public int MaxSize
        {
            get { return _MaxSize; }
            set { _MaxSize = value; }
        }

        public int CurrentPosition()
        {
            return MaxSize - EventQueue.Count;
        }

        public QueueEventArgs(Queue<MoveOrder> eventQueue, MoveOrder eventOrder, int maxSize)
        {
            EventQueue = eventQueue;
            EventOrder = eventOrder;
            MaxSize = maxSize;
        }
    }
}
