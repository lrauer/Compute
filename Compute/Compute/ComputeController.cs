using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    //This is the Class to communicate with the external caller
    public class ComputeController
    {

        Eventsystem _Events;

        //these are the events a caller can subscribe to for frontend updates
        public Eventsystem Events
        {
            get { return _Events; }
            set { _Events = value; }
        }

        Root System;

        public ComputeController()
        {
            Events = new Eventsystem(this);
            System = Events.Owner;
        }

        public bool AddAssemblyLine(int posX, int posY, Direction direction)
        {
            return System.Rootplace.AddChild(new AssemblyLine(new Position(posX, posY, System.Rootplace), direction, true));
        }

        public bool AddIncrementer(int posX, int posY, Direction direction)
        {
            return System.Rootplace.AddChild(new Incrementer(new Position(posX, posY, System.Rootplace), direction, true));
        }

        public bool AddComparer(int posX, int posY, Direction trueDirection, Direction falseDirection, int compareValue)
        {
            return System.Rootplace.AddChild(new Comparer(new Position(posX, posY, System.Rootplace), trueDirection, falseDirection, compareValue, true));
        }

       // public bool AddComputer()
       // {
       //     //TODO add place mit laden der entsprechenden vorlage
       // }

        public bool ChangePosition(int oldPosX,int oldPosY,int newPosX,int newPosY)
        {
            return System.Rootplace.ChangeChildPosition(oldPosX, oldPosY, newPosX, newPosY);
        }

        public bool PositionAvailable(int posX, int posY)
        {
            return System.Rootplace.GetChildAt(posX, posY) == null;
        }

        public bool DeleteAtPosition(int posX, int posY)
        {
            return System.Rootplace.RemoveChild(posX, posY);
        }

        public void AddNextInput(Direction direction, int value)
        {
            System.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, System), value), direction));
        }

        public void AddNextExpectedOutput(Direction direction, int value)
        {
            System.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, System), value), direction));
        }

        public bool Step()
        {
            return System.Execute(ExecType.Substep);
        }

        public bool Circle()
        {
            return System.Execute(ExecType.Tick);
        }

        public bool Completion()
        {
            return System.Execute(ExecType.Complete);
        }

        public bool ResetSystem()
        {
            return System.Execute(ExecType.Reset);
        }
    }
}
