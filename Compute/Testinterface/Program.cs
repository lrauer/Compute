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
            Eventsystem e = new Eventsystem();
            Root r = e.Owner;
            r.Rootplace.Children.Add(new AssemblyLine(new Position(-4, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(-3, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new Incrementer(new Position(-2, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(-1, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new Comparer(new Position(0, 0, r.Rootplace), Direction.Right,Direction.Down,2, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(1, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(2, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(3, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(4, 0, r.Rootplace), Direction.Right, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(0, -1, r.Rootplace), Direction.Down, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(0, -2, r.Rootplace), Direction.Down, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(0, -3, r.Rootplace), Direction.Down, false));
            r.Rootplace.Children.Add(new AssemblyLine(new Position(0, -4, r.Rootplace), Direction.Down, false));
            
            r.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, r), 1), Direction.Left));
            r.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, r), 2), Direction.Right));
            r.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, r), 2), Direction.Left));
            r.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, r), 3), Direction.Down));
            r.Execute(ExecType.Substep);
            r.Execute(ExecType.Substep);
            r.Execute(ExecType.Substep);
            r.Reset();
            r.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, r), 1), Direction.Left));
            r.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, r), 2), Direction.Right));
            r.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, r), 2), Direction.Up));
            r.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, r), 3), Direction.Down));
            r.Execute(ExecType.Complete);
            r.Reset();
            r.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, r), 1), Direction.Left));
            r.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, r), 2), Direction.Right));
            r.ReceiveInput(new MoveOrder(new Moveable(new Position(0, 0, r), 2), Direction.Left));
            r.ReceiveValidation(new MoveOrder(new Moveable(new Position(0, 0, r), 3), Direction.Down));
            r.Execute(ExecType.Complete);
        }
    }
}
