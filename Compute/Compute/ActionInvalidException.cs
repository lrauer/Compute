using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    class ActionInvalidException: Exception
    {
        PlaceableObjekt _Sender;

        public PlaceableObjekt Sender
        {
          get { return _Sender; }
          set { _Sender = value; }
        }

        public ActionInvalidException(String message, PlaceableObjekt sender): base(message)
        {
            Sender = sender;
        }
    }
}
