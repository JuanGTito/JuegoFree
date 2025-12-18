using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JuegoFree.Scenes
{
    public static class GameOverScene
    {
        private static Timer animationTimer;
        private static float scaleFactor = 1.0f;
        private static bool growing = true;
        private static List<PointF> humoParticulas = new List<PointF>();
        private static Random rng = new Random();

        public static void Show(PictureBox contiene, Form1 mainForm)
        {
            contiene.Controls.Clear();
            mainForm.IsGameActive = false;

            // Inicializamos algunas partículas de "humo"
            for (int i = 0; i < 20; i++)
                humoParticulas.Add(new PointF(rng.Next(contiene.Width), rng.Next(contiene.Height)));

            animationTimer = new Timer { Interval = 30 };
            animationTimer.Tick += (s, e) => {
                ActualizarEfectos(contiene);
                Renderizar(contiene);
            };
            animationTimer.Start();

            // Botón Reintentar
            Button btn = new Button
            {
                Text = "REINTENTAR",
                Size = new Size(180, 45),
                Location = new Point((contiene.Width - 180) / 2, contiene.Height / 2 + 50),
                BackColor = Color.Black,
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat
            };
            btn.Click += (s, e) => { animationTimer.Stop(); mainForm.RestartGame(); };
            contiene.Controls.Add(btn);
        }

        private static void ActualizarEfectos(PictureBox contiene)
        {
            // Pulso del texto
            if (growing) { scaleFactor += 0.005f; if (scaleFactor >= 1.05f) growing = false; }
            else { scaleFactor -= 0.005f; if (scaleFactor <= 0.95f) growing = true; }

            // Movimiento del humo (hacia arriba)
            for (int i = 0; i < humoParticulas.Count; i++)
            {
                humoParticulas[i] = new PointF(humoParticulas[i].X, humoParticulas[i].Y - 2);
                if (humoParticulas[i].Y < -20) // Resetear cuando salen de pantalla
                    humoParticulas[i] = new PointF(rng.Next(contiene.Width), contiene.Height + 10);
            }
        }

        private static void Renderizar(PictureBox contiene)
        {
            Bitmap bmp = new Bitmap(contiene.Width, contiene.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // 1. FONDO DEGRADADO (Aquí es donde quitas el negro sólido)
                using (LinearGradientBrush lgb = new LinearGradientBrush(
                    contiene.ClientRectangle,
                    Color.FromArgb(40, 0, 0), // Rojo oscuro arriba
                    Color.Black,               // Negro abajo
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(lgb, contiene.ClientRectangle);
                }

                // 2. DIBUJAR HUMO (Círculos grises semitransparentes)
                foreach (var p in humoParticulas)
                {
                    using (SolidBrush humoBrush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                    {
                        g.FillEllipse(humoBrush, p.X, p.Y, 30, 30);
                    }
                }

                // 3. TEXTO "GAME OVER" CON PULSACIÓN
                string msg = "GAME OVER";
                using (Font f = new Font("Impact", 60 * scaleFactor))
                {
                    SizeF size = g.MeasureString(msg, f);
                    g.DrawString(msg, f, Brushes.DarkRed, (contiene.Width - size.Width) / 2 + 3, 103); // Sombra
                    g.DrawString(msg, f, Brushes.Red, (contiene.Width - size.Width) / 2, 100);       // Texto
                }
            }

            contiene.Image?.Dispose();
            contiene.Image = bmp;
        }
    }
}