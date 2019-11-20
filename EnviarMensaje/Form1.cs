using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;
using System.IO;
using System.Diagnostics;
using FuncionesLib;
namespace EnviarMensaje
{
    public partial class Form1 : Form
    {
        NamedPipeServerStream npss;
        StreamReader sr;
        int WM_MENSAJE;
        
        public Form1()
        {
            InitializeComponent();
            WM_MENSAJE = Funciones.RegisterWindowMessage("WM_MENSAJE");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            npss = new NamedPipeServerStream("mensajepipe", PipeDirection.In);
            Process.Start("..\\..\\..\\MensajesHijo\\bin\\Debug\\MensajesHijo.exe");
            npss.WaitForConnection();                
            sr = new StreamReader(npss);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MENSAJE)
            {
                String pal = sr.ReadLine();
                if(pal.ToLower().CompareTo("fin")!=0)
                {
                    listBox1.Items.Add(pal);
                }
                else
                {
                    MessageBox.Show("Fin");
                    npss.Close();
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
