using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compute
{
    public class Position
    {

        public event EventHandler PositionChanged;

        int _PosX;

        public int PosX
        {
            get { return _PosX; }
            set 
            { 
                _PosX = value;
                if (PositionChanged != null)
                    PositionChanged.Invoke(this, EventArgs.Empty);
            }
        }

        int _PosY;

        public int PosY
        {
            get { return _PosY; }
            set 
            {
                _PosY = value;
                if (PositionChanged != null)
                    PositionChanged.Invoke(this, EventArgs.Empty);
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
                _Parent = value;
                if (PositionChanged != null)
                    PositionChanged.Invoke(this, EventArgs.Empty);
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

        public void Copy(Position position)
        {
            PosX = position.PosX;
            PosY = position.PosY;
            Parent = position.Parent;
        }
    }
}
