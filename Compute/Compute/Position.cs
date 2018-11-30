using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compute
{
    public class Position
    {

        public event EventHandler<PositionEventArgs> PositionChanged;

        int _PosX;

        public int PosX
        {
            get { return _PosX; }
            set 
            {
                int oldValue = _PosX;
                _PosX = value;
                if (PositionChanged != null)
                    PositionChanged.Invoke(this, new PositionEventArgs(oldValue, _PosX, PosY,PosY,Parent, Parent));
            }
        }

        int _PosY;

        public int PosY
        {
            get { return _PosY; }
            set 
            {
                int oldValue = _PosY;
                _PosY = value;
                if (PositionChanged != null)
                    PositionChanged.Invoke(this, new PositionEventArgs(PosX,PosX,oldValue,_PosY,Parent,Parent));
            }
        }

        public void Set(int x, int y)
        {
            PosX = x;
            PosY = y;
        }

        PlaceableObjekt _Parent;

        public PlaceableObjekt Parent
        {
            get { return _Parent; }
            set 
            {
                PlaceableObjekt oldValue = _Parent;
                _Parent = value;
                if (PositionChanged != null)
                {
                    PositionChanged.Invoke(this, new PositionEventArgs(PosX,PosX,PosY,PosY,oldValue,_Parent));
                }
            }
        }

        public Position(int posX, int posY, PlaceableObjekt parent)
        {
            _PosX = posX;
            _PosY = posY;
            _Parent = parent;
            if (parent != null)
            {
                PositionChanged += parent.getEventsystem().HandlePositionChanged;
            }
        }

        public static Position Copy(Position position)
        {
            return new Position(position.PosX, position.PosY, position.Parent);
        }
    }
}
