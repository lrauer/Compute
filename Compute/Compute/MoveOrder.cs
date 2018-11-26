using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class MoveOrder
    {
        Moveable _Objekt;

        public Moveable Objekt
        {
            get { return _Objekt; }
            set { _Objekt = value; }
        }

        Direction _Order;

        public Direction Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        public MoveOrder(Moveable objekt, Direction order)
        {
            Objekt = objekt;
            Order = order;
        }

        public bool CompareTo(MoveOrder m)
        {
            if (m.Order == Order && m.Objekt.CompareTo(Objekt))
                return true;
            return false;
        }
    }
}
