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

        public Eventsystem()
        {

            Owner = new Root(1, 1, this);
        }

        public void HandlePositionChanged(object sender, EventArgs eventArgs)
        {
            
        }

        public void HandleCreation(object sender, EventArgs eventArgs)
        {

        }

        public void HandleValueChanged(object sender, EventArgs eventArgs)
        {
            
        }


    }
}
