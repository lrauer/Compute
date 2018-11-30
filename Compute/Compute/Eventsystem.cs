using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class Eventsystem
    {

        Root _Owner;

        public Root Owner
        {
          get { return _Owner; }
          set { _Owner = value; }
        }

        ComputeController _Controller;

        public ComputeController Controller
        {
            get { return _Controller; }
            set { _Controller = value; }
        }

        public event EventHandler<QueueEventArgs> SystemInput;
        public event EventHandler<QueueValidationEventArgs> SystemOutput;
        public event EventHandler<QueueEventArgs> InputReceived;
        public event EventHandler<QueueEventArgs> OutputReceived;
        public event EventHandler<SystemStateEventArgs> ExecuteFinished;
        public event EventHandler<SystemStateFailedEventArgs> ExecuteFailed;
        public event EventHandler<SystemStateEventArgs> SystemResetted;
        public event EventHandler<PositionEventArgs> RootPlaceObjektPositionChanged;
        public event EventHandler<PositionEventArgs> RootPlaceObjektCreation;
        public event EventHandler<ValueEventArgs> RootPlaceMoveableValueChanged;

        public Eventsystem(ComputeController controller)
        {
            Controller = controller;
            Owner = new Root(1, 1, this);
        }

        public void HandlePositionChanged(object sender, PositionEventArgs eventArgs)
        {
            //only invoke if in Rootplace
            if (eventArgs.InRootPlace(true) || eventArgs.InRootPlace(false))
                if(RootPlaceObjektPositionChanged != null)
                    RootPlaceObjektPositionChanged.Invoke(sender,eventArgs);
        }

        public void HandleCreation(object sender, PositionEventArgs eventArgs)
        {
            //only invoke if in Rootplace
            if (eventArgs.InRootPlace(true) || eventArgs.InRootPlace(false))
                if (RootPlaceObjektCreation != null)
                    RootPlaceObjektCreation.Invoke(sender, eventArgs);
        }

        public void HandleValueChanged(object sender, ValueEventArgs eventArgs)
        {
            //only invoke if in Rootplace
            if (eventArgs.InRootPlace(true))
                if (RootPlaceMoveableValueChanged != null)
                    RootPlaceMoveableValueChanged.Invoke(sender, eventArgs);
        }

        public void HandleSystemInput(object sender, QueueEventArgs eventArgs)
        {
            if (SystemInput != null)
                SystemInput.Invoke(sender, eventArgs);
        }

        public void HandleSystemOutput(object sender, QueueValidationEventArgs eventArgs)
        {
            if (SystemOutput != null)
                SystemOutput.Invoke(sender, eventArgs);
        }

        public void HandleInputReceived(object sender, QueueEventArgs eventArgs)
        {
            if (InputReceived != null)
                InputReceived.Invoke(sender, eventArgs);
        }

        public void HandleOutputReceived(object sender, QueueEventArgs eventArgs)
        {
            if (OutputReceived != null)
                OutputReceived.Invoke(sender, eventArgs);
        }

        public void HandleExecuteFinished(object sender, SystemStateEventArgs eventArgs)
        {
            if (ExecuteFinished != null)
                ExecuteFinished.Invoke(sender, eventArgs);
        }

        public void HandleExecuteFailed(object sender, SystemStateFailedEventArgs eventArgs)
        {
            if (ExecuteFailed != null)
                ExecuteFailed.Invoke(sender, eventArgs);
        }

        public void HandleSystemResetted(object sender, SystemStateEventArgs eventArgs)
        {
            if (SystemResetted != null)
                SystemResetted.Invoke(sender, eventArgs);
        }


    }
}
