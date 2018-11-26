using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Root: PlaceableObjekt
    {

        public Root(int inputs, int outputs, Eventsystem eventsystem)
            : base(new Position(0, 0, null))
        {
            Eventsystem = eventsystem;
            Rootplace = new Place(new Position(0, 0, this), false, inputs, outputs);
            Position = new Position(0,0,null);

            Creation += getEventsystem().HandleCreation;
            Created();
            //todo load startkonfiguration (oder in jedem place machen da dort infos über childs liegen?)
        }

        Eventsystem _Eventsystem;

        public Eventsystem Eventsystem
        {
            get { return _Eventsystem; }
            set { _Eventsystem = value; }
        }

        SystemState _CurrentState = SystemState.Ready;

        internal SystemState CurrentState
        {
            get { return _CurrentState; }
            set { _CurrentState = value; }
        }

        Queue<MoveOrder> _InputQueue = new Queue<MoveOrder>();

        public Queue<MoveOrder> InputQueue
        {
            get { return _InputQueue; }
            set { _InputQueue = value; }
        }
        Queue<MoveOrder> _ValidationQueue = new Queue<MoveOrder>();

        public Queue<MoveOrder> ValidationQueue
        {
            get { return _ValidationQueue; }
            set { _ValidationQueue = value; }
        }

         Place _Rootplace;

         public Place Rootplace
         {
             get { return _Rootplace; }
             set { 
                 _Rootplace = value;
             }
         }


        public override bool PrepareTick()
        {   
            throw new NotImplementedException("The Root doesnt need to prepare");
        }

        int Tickoutput;

        public void Execute(ExecType execType)
        {
            try
            {
                if (CurrentState == SystemState.Broken || CurrentState == SystemState.Completed)
                {
                    throw new ActionInvalidException("The System must be resetted first", this);
                }
                if (Rootplace.OutputCount == Tickoutput || CurrentState != SystemState.Running)
                {
                    CurrentState = SystemState.Running;
                    Tickoutput = 0;
                    while (Rootplace.CurrentInput.Count < Rootplace.InputCount)
                    {
                        if (InputQueue.Count == 0)
                        {
                            throw new ActionInvalidException("Can't process without enough inputs", this);
                        }
                        Rootplace.ReceiveInput(InputQueue.Dequeue());
                        //TODO position in queue event
                        Rootplace.PrepareTick();
                    }
                }
                if (execType == ExecType.Substep)
                {
                    Rootplace.Substep();
                }
                else
                {
                    ExecuteTick();
                }
                // if complete continue with next input
                if (execType == ExecType.Complete && InputQueue.Count > 0)
                {
                    Execute(execType);
                }
                else
                {
                    CurrentState = SystemState.Completed;
                    //TODO event dass fertig
                }
            }
            catch (ActionInvalidException e)
            {
                //signal that broken, so no step can be executed anymore
                CurrentState = SystemState.Broken;
                //TODO hier event erzeugen, dass fehler in puzzleanordnung mit sender position und nachricht
            }
        }

        //removes all inputs from the System and reverts to default state
        public override void Reset()
        {
            CurrentState = SystemState.Ready;
            Rootplace.Reset();
            InputQueue.Clear();
            ValidationQueue.Clear();
            Tickoutput = 0;
            //TODO event, dass zurückgesetzt
        }

        public override void ExecuteTick()
        {
            Rootplace.ExecuteTick();
        }

        public override void ReceiveInput(MoveOrder moveOrder)
        {
            InputQueue.Enqueue(moveOrder);
            //TODO add to queue event
        }

        public void ReceiveValidation(MoveOrder moveOrder)
        {
            ValidationQueue.Enqueue(moveOrder);
            //TODO add to queue event
        }

        public override void ReleaseOutput(MoveOrder moveOrder)
        {
            Tickoutput++;
            //TODO position in queue event
            //TODO event mit bool ob richtig
            //TODO falls beide queues leer sind event dass komplett fertig ist

            if (ValidationQueue.Count == 0)
            {
                throw new ActionInvalidException("Need validationoutput for each input", this);
            }
            if (moveOrder.CompareTo(ValidationQueue.Dequeue()))
            {
                //richtig
                int i = 1;
            }
            else
            {
                //falsch
                int i = 1;
            }
        }
    }
}
