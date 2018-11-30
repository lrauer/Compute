using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class ValueEventArgs: PositionEventArgs
    {
        int _OldValue;

        public int OldValue
        {
            get { return _OldValue; }
            set { _OldValue = value; }
        }

        int _NewValue;

        public int NewValue
        {
            get { return _NewValue; }
            set { _NewValue = value; }
        }

        public ValueEventArgs(int oldValue, int newValue, Moveable position)
            : base(position.Position.PosX, position.Position.PosY, position.Position.PosX, position.Position.PosY, position.Position.Parent, position.Position.Parent)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
