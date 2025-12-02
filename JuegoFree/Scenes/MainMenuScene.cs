using JuegoFree.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Linq;
using TimerForms = System.Windows.Forms.Timer;

namespace JuegoFree.Scenes
{
    public static class MainMenuScene
    {
        private static TimerForms animationTimer;
        private static int backgroundOffset = 0;
        private const int STARS_SCROLL_SPEED = 2;
        private const int SHIP_SCROLL_SPEED = 1;

        private static int shipX = 0;
        private static int shipY = 0;

        private static Bitmap starPatternCache;

        public static void Load(Control container, Form1 mainForm)
        {
            int w = container.Width;
            int h = container.Height;
            CleanUpAnimation(container);

            container.Controls.Clear();
            container.BackColor = Color.Black;

            if (Resources.MenuShip != null)
            {
                shipX = (container.Width - Resources.MenuShip.Width) / 2;
                shipY = (container.Height - Resources.MenuShip.Height) / 2;
            }

            container.Paint += Container_Paint;

            animationTimer = new TimerForms();
            animationTimer.Interval = 25; // 40 FPS
            animationTimer.Tick += (sender, e) =>
            {
                // Solo necesitamos desplazar un offset (el más rápido: las estrellas)
                backgroundOffset = (backgroundOffset + STARS_SCROLL_SPEED) % container.Height;

                container.Invalidate();
            };
            animationTimer.Start();

            int buttonWidth = 200;
            int buttonHeight = 50;
            int startY = container.Height / 2 - (buttonHeight * 2);
            int centerX = (container.Width - buttonWidth) / 2;

            // Botón INICIAR JUEGO
            Button startButton = CreateMenuButton("Iniciar Juego", buttonWidth, buttonHeight, centerX, startY);
            startButton.Click += (sender, e) =>
            {
                // ** LLAMADA AL NUEVO GESTOR DE ESCENAS EN FORM1 **
                CleanUpAnimation(container);
                mainForm.LoadGameScene();
            };
            container.Controls.Add(startButton);

            // Botón OPCIONES
            Button optionsButton = CreateMenuButton("Opciones", buttonWidth, buttonHeight, centerX, startY + buttonHeight + 10);
            optionsButton.Click += (sender, e) =>
            {
                MessageBox.Show("¡Opciones en construcción!", "Menú");
            };
            container.Controls.Add(optionsButton);

            // Botón SALIR
            Button exitButton = CreateMenuButton("Salir", buttonWidth, buttonHeight, centerX, startY + (buttonHeight + 10) * 2);
            exitButton.Click += (sender, e) =>
            {
                Application.Exit();
            };
            container.Controls.Add(exitButton);
        }

        private static void Container_Paint(object sender, PaintEventArgs e)
        {
            Control container = (Control)sender;
            Graphics g = e.Graphics;
            int w = container.Width;
            int h = container.Height;

            // ----------------------------------------------------------------------
            // CAPA 1: Fondo Base (Estático)
            // ----------------------------------------------------------------------
            Image baseBackground = Resources.MenuBackgroundBase;

            if (baseBackground != null)
            {
                // Dibuja el fondo escalado a la PictureBox, sin offset (FIJO).
                g.DrawImage(baseBackground, new Rectangle(0, 0, w, h));
            }
            else
            {
                g.Clear(Color.Black);
            }

            // ----------------------------------------------------------------------
            // CAPA 2: Estrellas Móviles (GENERADAS POR CÓDIGO)
            // ----------------------------------------------------------------------
            DrawScrollingStars(g, w, h);
            // Esta función se encarga de dibujar el patrón de estrellas sobre el fondo de Capa 1.


            // ----------------------------------------------------------------------
            // CAPA 3: Nave (Movimiento Lento - Parallax)
            // ----------------------------------------------------------------------
            Image ship = Resources.MenuShip; // <--- RECURSO DE LA NAVE

            if (ship != null)
            {
                // Mueve la nave con el offset más lento.
                int slowOffset = (backgroundOffset * SHIP_SCROLL_SPEED) / STARS_SCROLL_SPEED;
                g.DrawImage(ship, shipX, shipY + slowOffset);
            }

            // ----------------------------------------------------------------------
            // Repintar Controles (asegura que botones y título estén visibles)
            // ----------------------------------------------------------------------
            container.Update();
        }

        private static void DrawScrollingStars(Graphics g, int width, int height)
        {
            // 1. Crear y cachear el patrón de estrellas (solo si es necesario)
            if (starPatternCache == null || starPatternCache.Width != width || starPatternCache.Height != height)
            {
                starPatternCache = new Bitmap(width, height);
                using (Graphics patternG = Graphics.FromImage(starPatternCache))
                {
                    // ** CLAVE: NO LIMPIAR A NEGRO. DEJAR EL FONDO DEL CACHÉ COMO TRANSPARENTE **
                    // patternG.Clear(Color.Black); <--- ¡ELIMINADO!

                    Random r = new Random(42);
                    using (Brush starBrush = new SolidBrush(Color.White))
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            int x = r.Next(width);
                            int y = r.Next(height);
                            int size = r.Next(1, 3);
                            patternG.FillRectangle(starBrush, x, y, size, size);
                        }
                    }
                }
            }

            // Dibuja el patrón dos veces para crear el efecto de scroll infinito
            // Primera copia (la parte visible, desplazada)
            g.DrawImage(starPatternCache, 0, backgroundOffset);

            // Segunda copia (la que entra por arriba)
            g.DrawImage(starPatternCache, 0, backgroundOffset - height);
        }

        public static void CleanUpAnimation(Control container)
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
                animationTimer = null;
            }
            // Desconectar el evento Paint para que no se ejecute en la GameScene
            container.Paint -= Container_Paint;

            if (starPatternCache != null)
            {
                starPatternCache.Dispose();
                starPatternCache = null;
            }
            backgroundOffset = 0;
        }

        // Función auxiliar para crear botones uniformes
        private static Button CreateMenuButton(string text, int width, int height, int x, int y)
        {
            return new Button
            {
                Text = text,
                Width = width,
                Height = height,
                Location = new Point(x, y),
                Font = new Font("Arial", 14, FontStyle.Regular),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }
    }
}