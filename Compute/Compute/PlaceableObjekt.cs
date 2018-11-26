using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public abstract class PlaceableObjekt : Worldobjekt, IPlaceable
    {

        public PlaceableObjekt(Position position)
            : base(position)
        {

        }

        List<Direction> _InputDirections = new List<Direction>();
        public List<Direction> InputDirections
        {
            get
            {
                return _InputDirections;
            }
            set
            {
                _InputDirections = value;
            }
        }
        List<Direction> _OutputDirections = new List<Direction>();
        public List<Direction> OutputDirections
        {
            get
            {
                return _OutputDirections;
            }
            set
            {
                _OutputDirections = value;
            }
        }

        int _InputCount;
        public int InputCount
        {
            get
            {
                return _InputCount;
            }
            set
            {
                _InputCount = value;
            }
        }

        List<Moveable> _CurrentInput = new List<Moveable>();
        public List<Moveable> CurrentInput
        {
            get
            {
                return _CurrentInput;
            }
            set
            {
                _CurrentInput = value;
            }
        }
        int _OutputCount;
        public int OutputCount
        {
            get
            {
                return _OutputCount;
            }
            set
            {
                _OutputCount = value;
            }
        }

        bool _Destructable;
        public bool Destructable
        {
            get
            {
                return _Destructable;
            }
            set
            {
                _Destructable = value;
            }
        }

        public abstract bool PrepareTick();

        public abstract void ExecuteTick();

        public abstract void ReceiveInput(MoveOrder moveOrder);

        public abstract void ReleaseOutput(MoveOrder moveOrder);
    }
}
