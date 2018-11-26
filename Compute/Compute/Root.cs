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
            //todo load startkonfiguration
        }

        Eventsystem _Eventsystem;

        public Eventsystem Eventsystem
        {
            get { return _Eventsystem; }
            set { _Eventsystem = value; }
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

        //WEITERE TODOs (habe angefangen aber noch nicht fertig:
        //enventsystem in root haben und statt exeptions immer events erzeugen die im eventsystem aufgehen 
        //+ überall events einbauen die interessant sein könnten damit gui darauf reagieren und anzeige aktualisieren kann


         Place _Rootplace;

         public Place Rootplace
         {
             get { return _Rootplace; }
             set { 
                 _Rootplace = value;
                 //new ticks are prepared when output is full, so signify it for first tick
                 tickoutput = _Rootplace.OutputCount;
             }
         }


        public override bool PrepareTick()
        {   
            throw new NotImplementedException("The Root doesnt need to prepare");
        }

        int tickoutput;

        public void Execute(ExecType execType)
        {
            try
            {
                if (Rootplace.OutputCount == tickoutput)
                {
                    tickoutput = 0;
                    while (Rootplace.CurrentInput.Count < Rootplace.InputCount)
                    {
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
                    //TODO event dass fertig
                }
            }
            catch
            {
                //TODO hier event erzeugen, dass fehler in puzzleanordnung
                //todo evtl neu exeption typ(en) und nur diese abfangen
                int i = 1;
            }
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
            tickoutput++;
            //TODO position in queue event
            //TODO event mit bool ob richtig
            //TODO falls beide queues leer sind event dass komplett fertig ist
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
