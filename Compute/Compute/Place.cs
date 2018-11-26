using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Place: PlaceableObjekt
    {


        //List of the children that live inside this place
        List<PlaceableObjekt> _Children = new List<PlaceableObjekt>();

        public List<PlaceableObjekt> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        List<MoveTarget> _InputBuffer = new List<MoveTarget>();

        internal List<MoveTarget> InputBuffer
        {
            get { return _InputBuffer; }
            set { _InputBuffer = value; }
        }

        public Place(Position position, bool destructable, int inputCount, int outputCount) :base(position)
        {
            Position = position;
            OutputDirections.Add(Direction.Down);
            OutputDirections.Add(Direction.Up);
            OutputDirections.Add(Direction.Left);
            OutputDirections.Add(Direction.Right);
            InputDirections.Add(Direction.Down);
            InputDirections.Add(Direction.Up);
            InputDirections.Add(Direction.Left);
            InputDirections.Add(Direction.Right);
            Destructable = destructable;
            InputCount = inputCount;
            OutputCount = outputCount;
            //each place has a fixed position fo the returners
            Children.Add(new Returner(new Position(-5, 0, this), Direction.Right, false));
            Children.Add(new Returner(new Position(5, 0, this), Direction.Left, false));
            Children.Add(new Returner(new Position(0, 5, this), Direction.Down, false));
            Children.Add(new Returner(new Position(0, -5, this), Direction.Up, false));
            //TODO load startkonfiguration
        }

        public override bool PrepareTick()
        {

            if (CurrentInput.Count == 0)
                return false;
            if (CurrentInput.Count == InputCount)
            {
                //reset outputcounter
                ReleasedOutput = 0;
                return true;
            }

            throw new ActionInvalidException("This Place can only process " + InputCount + " inputs",this);
        }

        //how much output is processed
        int ReleasedOutput;

        //tick childs until this place finished 1 tick
        public override void ExecuteTick()
        {
            //step the children until this place finished 1 tick
            while (ReleasedOutput < OutputCount)
            {
                try
                {
                    Substep();
                }
                catch (ActionInvalidException e)
                {
                    throw new ActionInvalidException("Place cant process its input: " + e.Message,this);
                }
            }
        }

        //just do 1 tick for each child
        public void Substep()
        {
            foreach (PlaceableObjekt p in Children)
            {
                if (p.PrepareTick())
                    p.ExecuteTick();
            }
            //after all ticks are finished we distribute new inputs
            foreach (MoveTarget m in InputBuffer)
            {
                m.Target.ReceiveInput(new MoveOrder(m.Mover, m.Origin));
            }
            InputBuffer.Clear();
        }

        public override void Reset()
        {
            InputBuffer.Clear();
            CurrentInput.Clear();
            ReleasedOutput = 0;
            foreach (PlaceableObjekt p in Children)
            {
                p.Reset();
            }
        }

        public override void ReceiveInput(MoveOrder moveOrder)
        {
            CurrentInput.Add(moveOrder.Objekt);
            moveOrder.Objekt.Position.Parent = this;
            Direction oldDirection = moveOrder.Order;
            moveOrder.Order = Direction.Parent;
            switch (oldDirection)
            {
                case Direction.Left:
                    moveOrder.Objekt.Position.Copy(Children[0].Position);
                    Children[0].ReceiveInput(moveOrder);
                    break;
                case Direction.Right:
                    moveOrder.Objekt.Position.Copy(Children[1].Position);
                    Children[1].ReceiveInput(moveOrder);
                    break;
                case Direction.Up:
                    moveOrder.Objekt.Position.Copy(Children[2].Position);
                    Children[2].ReceiveInput(moveOrder);
                    break;
                case Direction.Down:
                    moveOrder.Objekt.Position.Copy(Children[3].Position);
                    Children[3].ReceiveInput(moveOrder);
                    break;
            }
        }

        public override void ReleaseOutput(MoveOrder moveOrder)
        {
            //check if its an output for this element or a child
            if (moveOrder.Objekt.Position.Parent == this)
            {
                Position.Parent.ReleaseOutput(moveOrder);
                //has released an output
                ReleasedOutput++;
                CurrentInput.Remove(moveOrder.Objekt);
            }
            else
            {
                //return it to level of this place before it will be input for the next child
                moveOrder.Objekt.Position.Copy(moveOrder.Objekt.Position.Parent.Position);
                //determine the parent at target position
                int x = moveOrder.Objekt.Position.PosX;
                int y = moveOrder.Objekt.Position.PosY;
                switch (moveOrder.Order)
                {
                    case Direction.Down:
                        y--;
                        break;
                    case Direction.Up:
                        y++;
                        break;
                    case Direction.Left:
                        x--;
                        break;
                    case Direction.Right:
                        x++;
                        break;
                }
                moveOrder.Order = DirectionHelper.Invert(moveOrder.Order);
                PlaceableObjekt child = Children.Find(i => i.Position.PosX == x && i.Position.PosY == y);

                if (child == null)
                {
                    throw new ActionInvalidException("Can't input to empty position", this);
                }
                //add to bufferlist with new parent
                InputBuffer.Add(new MoveTarget(child, moveOrder.Objekt, moveOrder.Order));
            }
        }
    }
}
