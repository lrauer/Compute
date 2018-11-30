using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class PositionEventArgs
    {
        int _OldPositionX;

        public int OldPositionX
        {
            get { return _OldPositionX; }
            set { _OldPositionX = value; }
        }

        int _NewPositionX;

        public int NewPositionX
        {
            get { return _NewPositionX; }
            set { _NewPositionX = value; }
        }

        int _OldPositionY;

        public int OldPositionY
        {
            get { return _OldPositionY; }
            set { _OldPositionY = value; }
        }

        int _NewPositionY;

        public int NewPositionY
        {
            get { return _NewPositionY; }
            set { _NewPositionY = value; }
        }

        PlaceableObjekt _OldPositionParent;

        public PlaceableObjekt OldPositionParent
        {
            get { return _OldPositionParent; }
            set { _OldPositionParent = value; }
        }

        PlaceableObjekt _NewPositionParent;

        public PlaceableObjekt NewPositionParent
        {
            get { return _NewPositionParent; }
            set { _NewPositionParent = value; }
        }

        //check if in rootplace either before or after the positionchange
        public bool InRootPlace(bool beforeChange)
        {
            bool b = false;
            PlaceableObjekt p = NewPositionParent;
            if (beforeChange)
            {
                p = OldPositionParent;
            }
            if (p != null)
            {
                //if in rootplace then this is the rootplace
                p = p.Position.Parent;
                if (p != null)
                {
                    //if in rootplace then this is the root
                    p = p.Position.Parent;
                    //parent of root is null, so this objekt is in the rootplace
                    if (p == null)
                    {
                        b = true;
                    }
                }
            }
            return b;
        }

        public PositionEventArgs(int oldPositionX, int newPositionX, int oldPositionY, int newPositionY, PlaceableObjekt oldPositionParent, PlaceableObjekt newPositionParent)
        {
            OldPositionX = oldPositionX;
            NewPositionX = newPositionX;
            OldPositionY = oldPositionY;
            NewPositionY = newPositionY;
            OldPositionParent = oldPositionParent;
            NewPositionParent = newPositionParent;
        }
    }
}
