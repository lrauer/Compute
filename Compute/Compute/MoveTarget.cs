using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class MoveTarget
    {
        PlaceableObjekt _Target;

        internal PlaceableObjekt Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
        Moveable _Mover;

        public Moveable Mover
        {
            get { return _Mover; }
            set { _Mover = value; }
        }

        Direction _Origin;

        public Direction Origin
        {
            get { return _Origin; }
            set { _Origin = value; }
        }

        public MoveTarget(PlaceableObjekt target, Moveable mover, Direction origin)
        {
            Target = target;
            Mover = mover;
            Origin = origin;
        }
    }
}
