using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Compute;

namespace Testinterface
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            ComputeController c = new ComputeController();
            c.AddAssemblyLine(-4, 0, Direction.Right);
            c.AddAssemblyLine(-3, 0, Direction.Right);
            c.AddIncrementer(-2, 0, Direction.Right);
            c.AddAssemblyLine(-1, 0, Direction.Right);
            c.AddComparer(0, 0, Direction.Right, Direction.Down, 2);
            c.AddAssemblyLine(1, 0, Direction.Right);
            c.AddAssemblyLine(2, 0, Direction.Right);
            c.AddAssemblyLine(3, 0, Direction.Right);
            c.AddAssemblyLine(4, 0, Direction.Right);
            c.AddAssemblyLine(0, -1, Direction.Down);
            c.AddAssemblyLine(0, -2, Direction.Down);
            c.AddAssemblyLine(0, -3, Direction.Down);
            c.AddAssemblyLine(0, -4, Direction.Down);

            c.AddNextInput(Direction.Left, 1);
            c.AddNextExpectedOutput(Direction.Right, 2);
            c.AddNextInput(Direction.Left, 2);
            c.AddNextExpectedOutput(Direction.Down, 3);
            c.Step();
            c.Step();
            c.Step();
            c.ResetSystem();
            c.AddNextInput(Direction.Left, 1);
            c.AddNextExpectedOutput(Direction.Right, 2);
            c.AddNextInput(Direction.Up, 2);
            c.AddNextExpectedOutput(Direction.Down, 3);
            c.Completion();
            c.ResetSystem();
            c.AddNextInput(Direction.Left, 1);
            c.AddNextExpectedOutput(Direction.Right, 2);
            c.AddNextInput(Direction.Left, 2);
            c.AddNextExpectedOutput(Direction.Down, 3);
            c.Completion();
        }
    }
}
