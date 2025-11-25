using JuegoFree.Core;
using JuegoFree.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerForms = System.Windows.Forms.Timer;

namespace JuegoFree
{
    // Clase dummy para Modulo.Escenario que se usa en Iniciar(

    public partial class Form1 : Form
    {
        private PictureBox navex = new PictureBox();
        private PictureBox naveRival = new PictureBox();
        private PictureBox contiene = new PictureBox();
        private TimerForms tiempo;
        private int Dispara = 0;
        private bool flag = false;
        private float angulo = 0;

        public void ActividadTecla(object sender, KeyEventArgs e)
        {
            // Usamos el gestor de entrada modularizado
            InputManager.HandleKeyPress(e, navex, contiene, tiempo, ref angulo);
        }

        /************* ACTIVAR ACCIONES DE INICIALIZACION *************/
        public void Iniciar()
        {
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.Width = 445;
            this.Height = 550;
            this.Text = "JUEGO DE AVIONES";

            label1.Location = new Point(10, 10);
            label2.Location = new Point(10, 30);
            label1.Text = "Mi Rival: (50)"; // Inicializar vida
            label2.Text = "Mi Nave: (20)"; // Inicializar vida

            this.KeyDown += new KeyEventHandler(ActividadTecla);

            contiene.Location = new Point(0, 0);
            contiene.BackColor = Color.Black; // Cambiado a negro para mejor contraste
            contiene.Size = new Size(300, 420);
            contiene.Dock = DockStyle.Fill;
            Controls.Add(contiene);
            contiene.Visible = true;

            Controls.Add(label1);
            Controls.Add(label2);

            // Contenido del formulario
            Random r = new Random();
            int aletY = r.Next(250, 330);
            int aletX = r.Next(50, contiene.Width - 50);

            ShipFactory.CreateShip(navex, 0, 1, Color.SeaGreen, 20);
            contiene.Controls.Add(navex);

            Random sal = new Random();
            int sale = sal.Next(1, 2);
            ShipFactory.CreateShip(naveRival, 180, sale, Color.DarkBlue, 50);
            contiene.Controls.Add(naveRival); // Añadir nave rival al contenedor

            Scenes.GameScene.Escenario(contiene, sale);

            navex.Location = new Point(aletX, aletY);
            naveRival.Location = new Point(aletX, 50);

            tiempo = new TimerForms();
            tiempo.Interval = 1;
            tiempo.Enabled = true;

            // CONEXIÓN DEL TICK AL NUEVO GESTOR DE BUCLE
            tiempo.Tick += (sender, e) =>
            {
                GameLoopManager.HandleGameTick(
                    naveRival,
                    navex,
                    contiene,
                    ref Dispara,
                    ref flag,
                    label1,
                    label2,
                    tiempo);
            };
            tiempo.Start();
        }

        /************* ARGUMENTOS GENERADOS POR EL PROGRAMA *************/
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Iniciar();
        }
    }
}