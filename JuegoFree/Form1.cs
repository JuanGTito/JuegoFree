using JuegoFree.Core;
using JuegoFree.Entities;
using JuegoFree.Properties;
using JuegoFree.Scenes;
using System;
using System.Drawing;
using System.Windows.Forms;
using TimerForms = System.Windows.Forms.Timer;

namespace JuegoFree
{
    // Hacemos que la clase no sea parcial si todo el código está aquí.
    public partial class Form1 : Form
    {
        public PictureBox Navex
        {
            get => navex;
            set => navex = value;
        }
        public PictureBox NaveRival
        {
            get => naveRival;
            set => naveRival = value;
        }
        public PictureBox[] PlayerHearts => playerHearts;
        public PictureBox[] RivalHearts => rivalHearts;
        public bool IsGameActive { get; set; } = false;

        public ShipConfiguration PlayerShip { get; private set; }
        public int CurrentPlayerDamage { get; set; }

        private PictureBox[] playerHearts = new PictureBox[5];
        private PictureBox[] rivalHearts = new PictureBox[5];
        private const int FULL_HEART_VALUE = 20;

        private PictureBox navex = new PictureBox();
        private PictureBox naveRival = new PictureBox();
        private PictureBox contiene = new PictureBox();
        private TimerForms tiempo;
        private int Dispara = 0;
        private bool flag = false;
        private float angulo = 0;

        public void TeclaPresionada(object sender, KeyEventArgs e)
        {
            InputManager.SetKeyDown(e.KeyCode);
            //Console.WriteLine($"Tecla presionada: {e.KeyCode}");
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        public void TeclaLiberada(object sender, KeyEventArgs e)
        {
            InputManager.SetKeyUp(e.KeyCode);
        }

        public void LoadGameScene()
        {
            // Marcamos que el juego está activo
            IsGameActive = false;

            contiene.Controls.Clear();
            contiene.Image = null;

            Scenes.GameScene.Escenario(contiene, this, PlayerShip);
        }

        public void RestartGame()
        {
            IsGameActive = false;

            contiene.Controls.Clear();
            contiene.Image = null;

            // Volver a iniciar el juego con la nave seleccionada
            GameScene.Escenario(
                contiene,
                this,
                PlayerShip
            );
        }

        public void StartGame()
        {
            using (var selector = new SeleccionarJugador())
            {
                if (selector.ShowDialog() != DialogResult.OK)
                    return;

                PlayerShip = selector.SelectedShip;
            }

            LoadGameScene();
        }

        public void LoadMainMenu()
        {
            Scenes.MainMenuScene.Load(contiene, this);
        }


        public void Iniciar()
        {
            int screenW = Screen.PrimaryScreen.WorkingArea.Width;
            int screenH = Screen.PrimaryScreen.WorkingArea.Height;
            GameSettings.CurrentScreenWidth = screenW;
            GameSettings.CurrentScreenHeight = screenH;

            this.FormBorderStyle = FormBorderStyle.None;

            // 2. Establecer el estado a maximizado
            this.WindowState = FormWindowState.Maximized;

            // 3. (Opcional) Forzar el tamaño al área total del monitor
            this.Bounds = Screen.PrimaryScreen.Bounds;

            // Guardar dimensiones en tus GameSettings
            GameSettings.CurrentScreenWidth = this.Width;
            GameSettings.CurrentScreenHeight = this.Height;
            this.Text = "JUEGO DE AVIONES";

            contiene.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            contiene.Location = new Point(0, 0);
            contiene.BackColor = Color.Black;
            contiene.Visible = true;

            if (!Controls.Contains(contiene))
                Controls.Add(contiene);

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            int currentW = this.ClientSize.Width;
            int currentH = this.ClientSize.Height;

            contiene.Size = new Size(currentW, currentH);

            contiene.Location = new Point(0, 0);

            contiene.BackColor = Color.Black;
            contiene.Visible = true;
            Controls.Add(contiene);

            this.KeyDown += new KeyEventHandler(TeclaPresionada);
            this.KeyUp += new KeyEventHandler(TeclaLiberada);

            // Inicializar la escena del juego directamente para pruebas rápidas
            //LoadGameScene();

            // Cargar el Menú Principal al inicio
            Scenes.MainMenuScene.Load(contiene, this);

            this.KeyPreview = true;

            tiempo = new TimerForms();
            tiempo.Interval = 20;
            tiempo.Enabled = true;


            tiempo.Tick += (sender, e) =>
            {
                if (IsGameActive)
                {
                    
                    GameLoopManager.HandleGameTick(
                        naveRival,
                        navex,
                        contiene,
                        ref flag,
                        tiempo, // Aunque 'tiempo' no se usa aquí, si GameLoopManager lo necesita, se mantiene
                        ref angulo,
                        playerHearts,
                        rivalHearts,
                        this);
                }
            };
            tiempo.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Selección de nave del jugador -- comentado para pruebas rápidas
            //using (var selector = new SeleccionarJugador())
            //{
            //    if (selector.ShowDialog() != DialogResult.OK)
            //    {
            //        Close(); // Si cancela, se cierra el juego
            //        return;
            //    }
            //
            //    PlayerShip = selector.SelectedShip;
            //}

            Iniciar();
        }
    }
}