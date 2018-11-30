using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class SystemStateFailedEventArgs: SystemStateEventArgs
    {
        ActionInvalidException _Failmessage;

        internal ActionInvalidException Failmessage
        {
            get { return _Failmessage; }
            set { _Failmessage = value; }
        }

        public string Errormessage()
        {
            return Failmessage.Message;
        }

        public Position ErrorPosition()
        {
            return Failmessage.Sender.Position;
        }


        public SystemStateFailedEventArgs(SystemState state, ExecType eventExecType, ActionInvalidException failmessage)
            : base(state, eventExecType)
        {
            Failmessage = failmessage;
        }
    }
}
