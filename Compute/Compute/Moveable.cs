using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Moveable: Worldobjekt
    {
        event EventHandler<ValueEventArgs> ValueChanged;

        int _Value;  

        public int Value
        {
            get { return _Value; }
            set 
            {
                int oldValue = _Value;
                _Value = value; 
                if(ValueChanged != null)
                    ValueChanged.Invoke(this,new ValueEventArgs(oldValue,_Value, this));

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
