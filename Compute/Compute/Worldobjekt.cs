using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public abstract class Worldobjekt
    {

        public event EventHandler<PositionEventArgs> Creation;

        public Worldobjekt(Position position)
        {
            Position = position;

            if (getEventsystem() != null)
            {
                Creation += getEventsystem().HandleCreation;
                Created();
            }
        }

        public void Created()
        {
            if (Creation != null)
                Creation.Invoke(this, new PositionEventArgs(0,Position.PosX,0,Position.PosY,null,Position.Parent));
        }

        //current Position
        Position _Position;
        public Position Position 
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
            }
        }

        public Root getRoot()
        {
            Worldobjekt w = this;
            while (w.Position != null && w.Position.Parent != null)
            {
                w = w.Position.Parent;
            }
            return (Root)w;
        }

        //returns the eventsystem in which this Objekts events are handled
        public virtual Eventsystem getEventsystem()
        {
            return getRoot().Eventsystem;
        }
    }
}
