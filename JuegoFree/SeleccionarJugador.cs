using System;
using System.Drawing;
using System.Windows.Forms;
using JuegoFree.Entities;

namespace JuegoFree
{
    public partial class SeleccionarJugador : Form
    {
        public ShipConfiguration SelectedShip { get; private set; }

        private PictureBox previewPictureBox;
        private Label statsLabel;
        private Button btnType1;
        private Button btnType2;
        private Button btnType3;
        private Button btnIniciarJuego;

        public SeleccionarJugador()
        {
            InitializeComponent();
            CreateUI();
            UpdateShipSelection(1);
        }

        // ---------------- UI POR CÓDIGO ----------------
        private void CreateUI()
        {
            this.Text = "Seleccionar Nave";
            this.Size = new Size(520, 380);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            previewPictureBox = new PictureBox
            {
                Size = new Size(160, 200),
                Location = new Point(20, 20),
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.CenterImage
            };

            statsLabel = new Label
            {
                Location = new Point(200, 30),
                Size = new Size(280, 120),
                Font = new Font("Consolas", 10f),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            btnType1 = new Button
            {
                Text = "Fighter",
                Location = new Point(200, 160),
                Width = 90
            };
            btnType1.Click += (s, e) => UpdateShipSelection(1);

            btnType2 = new Button
            {
                Text = "Cruiser",
                Location = new Point(300, 160),
                Width = 90
            };
            btnType2.Click += (s, e) => UpdateShipSelection(2);

            btnType3 = new Button
            {
                Text = "Scout",
                Location = new Point(400, 160),
                Width = 90
            };
            btnType3.Click += (s, e) => UpdateShipSelection(3);

            btnIniciarJuego = new Button
            {
                Text = "Iniciar Juego",
                Location = new Point(200, 220),
                Width = 200
            };
            btnIniciarJuego.Click += BtnIniciarJuego_Click;

            this.Controls.Add(previewPictureBox);
            this.Controls.Add(statsLabel);
            this.Controls.Add(btnType1);
            this.Controls.Add(btnType2);
            this.Controls.Add(btnType3);
            this.Controls.Add(btnIniciarJuego);

            this.BackColor = Color.FromArgb(25, 25, 25);
        }

        // ---------------- LÓGICA ----------------
        private void UpdateShipSelection(int shipID)
        {
            SelectedShip = ShipCatalog.GetShip(shipID);

            statsLabel.Text =
                $"Nave: {SelectedShip.Name}\n" +
                $"Vida: {SelectedShip.InitialHealth}\n" +
                $"Daño Base: {SelectedShip.BaseDamage}\n" +
                $"Multiplicador: x{SelectedShip.DamageMultiplier:0.0}";

            previewPictureBox.Image?.Dispose();
            previewPictureBox.Image = null;

            ShipFactory.CreateShip(
                previewPictureBox,
                0,
                SelectedShip.ShipTypeID,
                SelectedShip.BaseColor,
                SelectedShip.InitialHealth,
                2
            );
        }

        private void BtnIniciarJuego_Click(object sender, EventArgs e)
        {
            if (SelectedShip == null) return;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
