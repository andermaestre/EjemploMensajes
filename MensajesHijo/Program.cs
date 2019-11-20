using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using System.Diagnostics;
using FuncionesLib;
namespace MensajesHijo
{
    class Program
    {   
        static void Main(string[] args)
        {
            int WM_MENSAJE = Funciones.RegisterWindowMessage("WM_MENSAJE");
            Process p = Process.GetProcessesByName("EnviarMensaje")[0];
            IntPtr h=  p.MainWindowHandle;
            NamedPipeClientStream npcs = new NamedPipeClientStream(".","mensajepipe",PipeDirection.Out);
            npcs.Connect();
            StreamWriter sw = new StreamWriter(npcs);
            sw.AutoFlush = true;
            Console.WriteLine("Palabra: ");
            string pal = Console.ReadLine();
            while(pal.ToLower().CompareTo("fin")!=0)
            {
                
             
                Funciones.PostMessage(h, WM_MENSAJE, IntPtr.Zero, IntPtr.Zero);
                sw.WriteLine(pal);
                Console.WriteLine("Palabra: ");
                pal = Console.ReadLine();
            }
            Funciones.PostMessage(h, WM_MENSAJE, IntPtr.Zero, IntPtr.Zero);
            sw.WriteLine("fin");
            npcs.Close();
        }
    }
}
