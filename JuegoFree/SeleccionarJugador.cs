using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JuegoFree.Entities;
using JuegoFree.Data;
using JuegoFree.Core;

namespace JuegoFree
{
    public partial class SeleccionarJugador : Form
    {
        public ShipConfiguration SelectedShip { get; private set; }

        private List<ShipEntity> ships;

        private PictureBox previewPictureBox;
        private Label statsLabel;
        private ListBox listBoxShips;
        private Button btnIniciarJuego;

        public SeleccionarJugador()
        {
            InitializeComponent();
            CreateUI();
            LoadShips();
        }

        // ---------------- UI ----------------
        private void CreateUI()
        {
            Text = "Seleccionar Nave";
            Size = new Size(520, 360);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.FromArgb(25, 25, 25);

            previewPictureBox = new PictureBox
            {
                Size = new Size(160, 200),
                Location = new Point(20, 20),
                BackColor = Color.Black
            };

            statsLabel = new Label
            {
                Location = new Point(200, 20),
                Size = new Size(280, 100),
                ForeColor = Color.White,
                Font = new Font("Consolas", 10f)
            };

            listBoxShips = new ListBox
            {
                Location = new Point(200, 130),
                Size = new Size(280, 80)
            };
            listBoxShips.SelectedIndexChanged += ListBoxShips_SelectedIndexChanged;

            btnIniciarJuego = new Button
            {
                Text = "Iniciar Juego",
                Location = new Point(200, 230),
                Size = new Size(280, 40)
            };
            btnIniciarJuego.Click += BtnIniciarJuego_Click;

            Controls.Add(previewPictureBox);
            Controls.Add(statsLabel);
            Controls.Add(listBoxShips);
            Controls.Add(btnIniciarJuego);
        }

        // ---------------- DATOS ----------------
        private void LoadShips()
        {
            ships = ShipRepository.GetAllShips();

            if (ships.Count == 0) return;

            listBoxShips.DataSource = ships;
            listBoxShips.DisplayMember = "Name";
            listBoxShips.SelectedIndex = 0;
        }

        private void ListBoxShips_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxShips.SelectedItem is ShipEntity entity)
            {
                // Color temporal (luego puede venir de BD)
                Color baseColor = Color.CornflowerBlue;

                SelectedShip = ShipMapper.FromEntity(entity);

                UpdatePreview();
                UpdateStats();
            }
        }

        // ---------------- VISUAL ----------------
        private void UpdatePreview()
        {
            previewPictureBox.Image?.Dispose();
            previewPictureBox.Image = null;

            // Usamos EXACTAMENTE el ShipFactory (preview = juego)
            ShipFactory.CreateShip(
                previewPictureBox,
                SelectedShip,
                angulo: 0,
                escala: 2
            );
        }

        private void UpdateStats()
        {
            statsLabel.Text =
                $"Nave: {SelectedShip.Name}\n" +
                $"Vida: {SelectedShip.InitialHealth}\n" +
                $"Daño Base: {SelectedShip.BaseDamage}";
        }

        // ---------------- ACCIÓN ----------------
        private void BtnIniciarJuego_Click(object sender, EventArgs e)
        {
            if (SelectedShip == null) return;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
