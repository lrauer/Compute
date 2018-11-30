using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Root: PlaceableObjekt
    {

        public event EventHandler<QueueEventArgs> InputQueueProgressed;
        public event EventHandler<QueueValidationEventArgs> ValidationQueueProgressed;
        public event EventHandler<QueueEventArgs> InputQueueEnqueued;
        public event EventHandler<QueueEventArgs> ValidationQueueEnqueued;
        public event EventHandler<SystemStateEventArgs> ExecuteFinished;
        public event EventHandler<SystemStateFailedEventArgs> ExecuteFailed;
        public event EventHandler<SystemStateEventArgs> SystemResetted;

        public Root(int inputs, int outputs, Eventsystem eventsystem)
            : base(new Position(0, 0, null))
        {
            Eventsystem = eventsystem;
            Rootplace = new Place(new Position(0, 0, this), false, inputs, outputs);
            Position = new Position(0,0,null);

            Creation += Eventsystem.HandleCreation;
            InputQueueProgressed += Eventsystem.HandleSystemInput;
            ValidationQueueProgressed += Eventsystem.HandleSystemOutput;
            InputQueueEnqueued += Eventsystem.HandleInputReceived;
            ValidationQueueEnqueued += Eventsystem.HandleOutputReceived;
            ExecuteFinished += Eventsystem.HandleExecuteFinished;
            ExecuteFailed += Eventsystem.HandleExecuteFailed;
            SystemResetted += Eventsystem.HandleSystemResetted;

            Created();
            //TODO load startkonfiguration (oder in jedem place machen da dort infos über childs liegen?)
            //TODO-- zusätzlich funktion zum laden/speichern von konfigurationen machen
        }

        //
        //
        //TODO als nächstes kleine oberfläche machen die wie programm erzeugt und danach auf events reagiert um sich aufzubauen und zu aktualisieren (am einfachsten als text aus array [,] mit X und zahlen und so der immer aktialisiert wird)
        //
        //

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

        public bool Execute(ExecType execType)
        {
            bool b = true;
            if (execType == ExecType.Reset)
            {
                Reset();
            }
            else
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
                            MoveOrder m = InputQueue.Dequeue();
                            Rootplace.ReceiveInput(m);
                            InputQueueProgressed.Invoke(this, new QueueEventArgs(InputQueue,m,maxInputSize));
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
                    //if finshed stop
                    if (InputQueue.Count == 0 && ValidationQueue.Count == 0)
                    {
                        CurrentState = SystemState.Completed;
                    }
                }
                catch (ActionInvalidException e)
                {
                    b = false;
                    //signal that broken, so no step can be executed anymore
                    CurrentState = SystemState.Broken;
                    //event with the position, state and Error
                    ExecuteFailed.Invoke(this, new SystemStateFailedEventArgs(CurrentState,execType,e));
                }
                finally
                {
                    // if exectype complete continue with next input
                    if (execType == ExecType.Complete && InputQueue.Count > 0 && CurrentState == SystemState.Running)
                    {
                        Execute(execType);
                    }
                    //else we are finshed with this execution
                    else
                    {
                        //event with the state that this Execution is finished
                        ExecuteFinished.Invoke(this, new SystemStateEventArgs(CurrentState, execType));
                    }
                }
            }
            return b;
        }

        //removes all inputs from the System and reverts to default state
        public override void Reset()
        {
            CurrentState = SystemState.Ready;
            Rootplace.Reset();
            InputQueue.Clear();
            ValidationQueue.Clear();
            maxInputSize = 0;
            maxValidationSize = 0;
            Tickoutput = 0;
            //event that system is resetted
            SystemResetted.Invoke(this, new SystemStateEventArgs(CurrentState, ExecType.Reset));
        }

        public override void ExecuteTick()
        {
            Rootplace.ExecuteTick();
        }

        int maxInputSize;

        public override void ReceiveInput(MoveOrder moveOrder)
        {
            InputQueue.Enqueue(moveOrder);
            maxInputSize++;
            //add to queue event
            InputQueueEnqueued.Invoke(this, new QueueEventArgs(InputQueue,moveOrder,maxInputSize));
        }

        int maxValidationSize;

        public void ReceiveValidation(MoveOrder moveOrder)
        {
            ValidationQueue.Enqueue(moveOrder);
            maxValidationSize++;
            //add to queue event
            ValidationQueueEnqueued.Invoke(this, new QueueEventArgs(ValidationQueue, moveOrder, maxValidationSize));
        }

        public override void ReleaseOutput(MoveOrder moveOrder)
        {
            Tickoutput++;

            if (ValidationQueue.Count == 0)
            {
                throw new ActionInvalidException("Need validationoutput for each input", this);
            }

            //send Event with expected and actual Moveorder for Comparison
            ValidationQueueProgressed.Invoke(this, new QueueValidationEventArgs(ValidationQueue, ValidationQueue.Dequeue(), maxValidationSize, moveOrder));

        }
    }
}
