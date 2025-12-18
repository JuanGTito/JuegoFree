using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using JuegoFree.Entities;
using JuegoFree.Data;
using JuegoFree.Core;

namespace JuegoFree
{
    public partial class SeleccionarJugador : Form
    {
        public ShipConfiguration SelectedShip { get; private set; }
        private List<Avion> ships;

        // Controles UI
        private Panel containerPreview;
        private PictureBox previewPictureBox;
        private Label statsLabel;
        private ListBox listBoxShips;
        private Button btnIniciarJuego;

        public SeleccionarJugador()
        {
            InitializeComponent();
            SetupFancyUI();
            LoadShips();
        }

        private void SetupFancyUI()
        {
            // Configuración del Formulario
            this.Text = "HANGAR DE COMBATE";
            this.Size = new Size(600, 450);
            this.FormBorderStyle = FormBorderStyle.None; // Estilo más limpio
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 15, 20); // Azul muy oscuro

            // Panel Contenedor de la Nave (con borde de color)
            containerPreview = new Panel
            {
                Size = new Size(220, 250),
                Location = new Point(30, 50),
                BackColor = Color.FromArgb(30, 30, 40),
                Padding = new Padding(5)
            };
            containerPreview.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, containerPreview.ClientRectangle, Color.Cyan, ButtonBorderStyle.Solid);
            };

            previewPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            containerPreview.Controls.Add(previewPictureBox);

            // Título de Estadísticas
            Label lblTitle = new Label
            {
                Text = "ESTADÍSTICAS DE NAVE",
                ForeColor = Color.Cyan,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(280, 30),
                AutoSize = true
            };

            // Etiqueta de Stats (mejorada abajo con dibujo manual)
            statsLabel = new Label
            {
                Location = new Point(280, 60),
                Size = new Size(280, 120),
                ForeColor = Color.White,
                Font = new Font("Consolas", 10f)
            };

            // ListBox con Estilo Oscuro
            listBoxShips = new ListBox
            {
                Location = new Point(280, 190),
                Size = new Size(280, 110),
                BackColor = Color.FromArgb(40, 40, 50),
                ForeColor = Color.Lime,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ItemHeight = 25
            };
            listBoxShips.SelectedIndexChanged += ListBoxShips_SelectedIndexChanged;

            // Botón Iniciar Juego "Neon"
            btnIniciarJuego = new Button
            {
                Text = "DESPLEGAR NAVE",
                Location = new Point(30, 330),
                Size = new Size(530, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 60, 0),
                ForeColor = Color.Lime,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnIniciarJuego.FlatAppearance.BorderColor = Color.Lime;
            btnIniciarJuego.FlatAppearance.BorderSize = 2;
            btnIniciarJuego.Click += BtnIniciarJuego_Click;

            // Botón Cerrar (X)
            Button btnClose = new Button
            {
                Text = "X",
                Size = new Size(30, 30),
                Location = new Point(560, 5),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Red,
                BackColor = Color.Transparent
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            Controls.Add(containerPreview);
            Controls.Add(lblTitle);
            Controls.Add(statsLabel);
            Controls.Add(listBoxShips);
            Controls.Add(btnIniciarJuego);
            Controls.Add(btnClose);
        }

        private void LoadShips()
        {
            ships = ShipRepository.GetAllShips();
            if (ships.Count == 0) return;

            listBoxShips.DataSource = ships;
            listBoxShips.DisplayMember = "tipo";
            listBoxShips.SelectedIndex = 0;
        }

        private void ListBoxShips_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxShips.SelectedItem is Avion entity)
            {
                SelectedShip = ShipMapper.FromEntity(entity);
                UpdatePreview();
                UpdateStats();
            }
        }

        private void UpdatePreview()
        {
            previewPictureBox.Image?.Dispose();
            previewPictureBox.Image = null;

            // Escala un poco mayor para la preview
            CrearAvion.CreateShip(previewPictureBox, SelectedShip, angulo: 0, escala: 2.5f);
        }

        private void UpdateStats()
        {
            // Creamos una representación visual con "barras" usando caracteres
            string vidaBar = GetProgressBar(SelectedShip.Vida, 300);
            string dañoBar = GetProgressBar(SelectedShip.Daño, 50);

            statsLabel.Text =
                $"NOMBRE: {SelectedShip.Nombre.ToUpper()}\n" +
                $"CLASE:  {SelectedShip.Tipo.ToUpper()}\n\n" +
                $"RESISTENCIA [{SelectedShip.Vida}]\n{vidaBar}\n" +
                $"DAÑO    [{SelectedShip.Daño}]\n{dañoBar}";
        }

        private string GetProgressBar(int value, int max)
        {
            int totalBlocks = 20;
            int filledBlocks = (int)((float)value / max * totalBlocks);
            if (filledBlocks > totalBlocks) filledBlocks = totalBlocks;

            return new string('█', filledBlocks).PadRight(totalBlocks, '░');
        }

        private void BtnIniciarJuego_Click(object sender, EventArgs e)
        {
            if (SelectedShip == null) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        // Permitir arrastrar el formulario sin bordes
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84) m.Result = (IntPtr)0x2;
        }
    }
}