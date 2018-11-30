using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class SystemStateEventArgs
    {
        SystemState _State;

        internal SystemState State
        {
            get { return _State; }
            set { _State = value; }
        }

        ExecType _EventExecType;

        public ExecType EventExecType
        {
            get { return _EventExecType; }
            set { _EventExecType = value; }
        }

        public bool NeedsReset()
        {
            if (State == SystemState.Broken || State == SystemState.Completed)
                return true;
            return false;
        }

        public bool Completed()
        {
            return State == SystemState.Completed;
        }

        public SystemStateEventArgs(SystemState state, ExecType eventExecType)
        {
            State = state;
            EventExecType = eventExecType;
        }
    }
}
