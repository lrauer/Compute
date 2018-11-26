using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Moveable: Worldobjekt
    {
        int _Value;

        public event EventHandler ValueChanged;

        public int Value
        {
            get { return _Value; }
            set 
            { 
                _Value = value; 
                if(ValueChanged != null)
                    ValueChanged.Invoke(this,EventArgs.Empty);
            }
        }

        public Moveable(Position position, int value) : base(position)
        {
            Value = value;
            Position = position;
            ValueChanged += getEventsystem().HandleValueChanged;
        }

        public bool CompareTo(Moveable m)
        {
            return m.Value == Value;
        }

    }
}
