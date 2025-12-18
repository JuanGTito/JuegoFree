using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JuegoFree.Scenes
{
    public static class WinScene
    {
        private static Timer winTimer;
        private static float glowIntensity = 0;
        private static bool increasingGlow = true;
        private static List<PointF> chispas = new List<PointF>();
        private static Random rng = new Random();

        public static void Show(PictureBox contiene, Form1 mainForm)
        {
            contiene.Controls.Clear();
            mainForm.IsGameActive = false;

            // Inicializamos partículas brillantes (estrellas de victoria)
            chispas.Clear();
            for (int i = 0; i < 30; i++)
                chispas.Add(new PointF(rng.Next(contiene.Width), rng.Next(contiene.Height)));

            // Timer para la animación de victoria
            winTimer = new Timer { Interval = 30 };
            winTimer.Tick += (s, e) => {
                ActualizarAnimacion(contiene);
                Renderizar(contiene);
            };
            winTimer.Start();

            // Botón con estilo moderno
            Button btnMenu = new Button
            {
                Text = "VOLVER AL MENÚ",
                Size = new Size(200, 45),
                Location = new Point((contiene.Width - 200) / 2, 300),
                BackColor = Color.FromArgb(0, 40, 0),
                ForeColor = Color.Lime,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnMenu.FlatAppearance.BorderColor = Color.Lime;
            btnMenu.FlatAppearance.BorderSize = 2;

            btnMenu.Click += (s, e) => {
                winTimer.Stop();
                winTimer.Dispose();
                mainForm.LoadMainMenu();
            };

            contiene.Controls.Add(btnMenu);
        }

        private static void ActualizarAnimacion(PictureBox contiene)
        {
            // Efecto de brillo oscilante
            if (increasingGlow) { glowIntensity += 0.05f; if (glowIntensity >= 1.0f) increasingGlow = false; }
            else { glowIntensity -= 0.05f; if (glowIntensity <= 0.3f) increasingGlow = true; }

            // Movimiento de las chispas (caen suavemente)
            for (int i = 0; i < chispas.Count; i++)
            {
                chispas[i] = new PointF(chispas[i].X, chispas[i].Y + 1.5f);
                if (chispas[i].Y > contiene.Height)
                    chispas[i] = new PointF(rng.Next(contiene.Width), -10);
            }
        }

        private static void Renderizar(PictureBox contiene)
        {
            Bitmap bmp = new Bitmap(contiene.Width, contiene.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // 1. Fondo Degradado "Victoria"
                using (LinearGradientBrush lgb = new LinearGradientBrush(
                    contiene.ClientRectangle,
                    Color.FromArgb(0, 30, 0), // Verde muy oscuro
                    Color.Black,
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(lgb, contiene.ClientRectangle);
                }

                // 2. Dibujar chispas brillantes
                foreach (var p in chispas)
                {
                    int alpha = (int)(150 * glowIntensity);
                    using (SolidBrush starBrush = new SolidBrush(Color.FromArgb(alpha, Color.YellowGreen)))
                    {
                        g.FillEllipse(starBrush, p.X, p.Y, 4, 4);
                    }
                }

                // 3. Texto "YOU WIN" con resplandor neón
                string msg = "¡YOU WIN!";
                using (Font f = new Font("Impact", 55, FontStyle.Bold))
                {
                    SizeF size = g.MeasureString(msg, f);
                    float x = (contiene.Width - size.Width) / 2;
                    float y = 120;

                    // Efecto de resplandor (Glow)
                    int glowAlpha = (int)(100 * glowIntensity);
                    using (SolidBrush glowBrush = new SolidBrush(Color.FromArgb(glowAlpha, Color.Lime)))
                    {
                        g.DrawString(msg, f, glowBrush, x - 2, y - 2);
                        g.DrawString(msg, f, glowBrush, x + 2, y + 2);
                    }

                    // Texto frontal
                    g.DrawString(msg, f, Brushes.White, x, y);
                }
            }

            contiene.Image?.Dispose();
            contiene.Image = bmp;
        }
    }
}