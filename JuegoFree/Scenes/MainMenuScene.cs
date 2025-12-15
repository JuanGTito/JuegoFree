using JuegoFree.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
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
            CleanUpAnimation(container);

            container.Controls.Clear();
            container.BackColor = Color.Black;

            if (Resources.MenuShip != null)
            {
                shipX = (container.Width - Resources.MenuShip.Width) / 2;
                shipY = (container.Height - Resources.MenuShip.Height) / 2;
            }

            container.Paint += Container_Paint;

            animationTimer = new TimerForms
            {
                Interval = 25
            };

            animationTimer.Tick += (s, e) =>
            {
                backgroundOffset = (backgroundOffset + STARS_SCROLL_SPEED) % container.Height;
                container.Invalidate();
            };

            animationTimer.Start();

            int buttonWidth = 200;
            int buttonHeight = 50;
            int startY = container.Height / 2 - (buttonHeight * 2);
            int centerX = (container.Width - buttonWidth) / 2;

            Button startButton = CreateMenuButton("Iniciar Juego", buttonWidth, buttonHeight, centerX, startY);
            startButton.Click += (s, e) =>
            {
                CleanUpAnimation(container);
                mainForm.StartGame();
            };
            container.Controls.Add(startButton);

            Button optionsButton = CreateMenuButton("Opciones", buttonWidth, buttonHeight, centerX, startY + buttonHeight + 10);
            optionsButton.Click += (s, e) =>
            {
                MessageBox.Show("Opciones en construcción");
            };
            container.Controls.Add(optionsButton);

            Button exitButton = CreateMenuButton("Salir", buttonWidth, buttonHeight, centerX, startY + (buttonHeight + 10) * 2);
            exitButton.Click += (s, e) =>
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

            Image baseBackground = Resources.MenuBackgroundBase;

            if (baseBackground != null)
                g.DrawImage(baseBackground, new Rectangle(0, 0, w, h));
            else
                g.Clear(Color.Black);

            DrawScrollingStars(g, w, h);

            Image ship = Resources.MenuShip;
            if (ship != null)
            {
                int slowOffset = (backgroundOffset * SHIP_SCROLL_SPEED) / STARS_SCROLL_SPEED;
                g.DrawImage(ship, shipX, shipY + slowOffset);
            }

            container.Update();
        }

        private static void DrawScrollingStars(Graphics g, int width, int height)
        {
            if (starPatternCache == null || starPatternCache.Width != width || starPatternCache.Height != height)
            {
                starPatternCache = new Bitmap(width, height);
                using (Graphics pg = Graphics.FromImage(starPatternCache))
                {
                    Random r = new Random(42);
                    using (Brush b = new SolidBrush(Color.White))
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            int x = r.Next(width);
                            int y = r.Next(height);
                            int size = r.Next(1, 3);
                            pg.FillRectangle(b, x, y, size, size);
                        }
                    }
                }
            }

            g.DrawImage(starPatternCache, 0, backgroundOffset);
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

            container.Paint -= Container_Paint;

            if (starPatternCache != null)
            {
                starPatternCache.Dispose();
                starPatternCache = null;
            }

            backgroundOffset = 0;
        }

        private static Button CreateMenuButton(string text, int width, int height, int x, int y)
        {
            return new Button
            {
                Text = text,
                Width = width,
                Height = height,
                Location = new Point(x, y),
                Font = new Font("Arial", 14),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }
    }
}
