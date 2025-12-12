using JuegoFree.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JuegoFree.Core.Utils;
using TimerForms = System.Windows.Forms.Timer;
using JuegoFree.Scenes;

namespace JuegoFree.Core
{
    internal class GameLoopManager
    {
        /***************DESTRUCTOR DEL MISIL Y LÓGICA DE JUEGO POR TICK********************/
        private static readonly Random random = new Random();
        private const int HORIZONTAL_SPEED = 1; // Velocidad X
        private const int RANDOM_CHANGE_FREQUENCY = 30; // Cambiar la dirección Y cada 30 ticks
        private static int verticalMoveTimer = 0;
        private static int currentYDirection = 0;
        public static void HandleGameTick(
            PictureBox naveRival,
            PictureBox navex,
            PictureBox contiene,
            ref int Dispara,
            ref bool flag,
            TimerForms tiempo,
            ref float angulo,
            PictureBox[] playerHearts,
            PictureBox[] rivalHearts)
        {

            InputManager.ProcessGameLogic(navex, contiene, tiempo, ref angulo);

            int X = naveRival.Location.X;
            int Y = naveRival.Location.Y;
            int W = naveRival.Width;
            int H = naveRival.Height;
            int PH = 10;
            int X2 = navex.Location.X;
            int Y2 = navex.Location.Y;
            int W2 = navex.Width;
            int H2 = navex.Height;
            int x = naveRival.Location.X;
            int y = naveRival.Location.Y;
            int verticalLimit = contiene.Height / 2;

            const int GAME_W = 600;
            const int GAME_H = 800;

            int offsetX = (contiene.Width - GAME_W) / 2;
            int offsetY = (contiene.Height - GAME_H) / 2;

            Dispara++;

            if (Dispara == 15 && naveRival.Visible == true)
            {
                int xRival = naveRival.Location.X + (naveRival.Width / 2);
                int yRival = naveRival.Location.Y + (naveRival.Height / 2);

                MissileFactory.CreateMisil(contiene, 180, Color.OrangeRed, "Rival", xRival, yRival);

                Dispara = 0;
            }

            int minX_Boundary = offsetX;
            int maxX_Boundary = offsetX + GAME_W - naveRival.Width;

            if (flag == false)
            {
                if (x + HORIZONTAL_SPEED >= maxX_Boundary)
                    flag = true;

                x += HORIZONTAL_SPEED;
            }
            else
            {
                if (x - HORIZONTAL_SPEED <= minX_Boundary)
                    flag = false;

                x -= HORIZONTAL_SPEED;
            }
            x = Math.Max(minX_Boundary, Math.Min(x, maxX_Boundary));



            verticalMoveTimer++;

            if (verticalMoveTimer >= RANDOM_CHANGE_FREQUENCY)
            {
                currentYDirection = random.Next(-1, 2);
                verticalMoveTimer = 0;
            }

            int newY = y + currentYDirection;

            if (newY < offsetY + 10)
            {
                newY = offsetY + 10;
                currentYDirection = 1;
            }
            else if (newY > offsetY + (GAME_H / 2) - naveRival.Height)
            {
                newY = offsetY + (GAME_H / 2) - naveRival.Height;
                currentYDirection = -1;
            }

            y = newY;
            naveRival.Location = new Point(x, y);

            // ELIMINACION DEL MISIL Y DESCONTAR PUNTOS DE IMPACTO
            foreach (Control c in contiene.Controls.OfType<PictureBox>().ToList())
            {
                int X1 = c.Location.X;
                int Y1 = c.Location.Y;
                int W1 = c.Width;
                int H1 = c.Height;
                string nombre = c.Tag?.ToString();

                if (nombre == "Misil" || nombre == "Rival")
                {
                    if (nombre == "Misil")
                    {
                        c.Top -= 10;
                    }
                    if (nombre == "Rival")
                    {
                        c.Top += 10;
                    }
                }


                // ACTIVIDAD DE IMPACTO CON LA NAVE RIVAL (Misil del jugador golpea)
                if (X < X1 && X1 + W1 < X + W && Y < Y1 && Y1 + H1 < Y + H && nombre == "Misil")
                {
                    c.Dispose();
                    // Lógica de daño
                    int daño = (X + PH < X1 && X1 + W1 < X + W - PH) ? 10 : 1;
                    int vidaActualRival = (int)naveRival.Tag;
                    vidaActualRival -= daño;
                    naveRival.Tag = vidaActualRival;
                    HeartManager.UpdateHearts(rivalHearts, vidaActualRival);
                }

                // Finalización del juego (Victoria del jugador)
                if (naveRival.Tag != null && int.Parse(naveRival.Tag.ToString()) <= 0)
                {
                    naveRival.Dispose();
                    Bitmap NuevoImg = new Bitmap(contiene.Width, contiene.Height);
                    using (Graphics flagImagen = Graphics.FromImage(NuevoImg))
                    {
                        String drawString = "Felicidades Ganaste...";
                        Font drawFont = new Font("Arial", 16);
                        SolidBrush drawBrush = new SolidBrush(Color.Blue);
                        flagImagen.DrawString(drawString, drawFont, drawBrush, new Point(40, 150));
                    }
                    contiene.Image = NuevoImg;
                    tiempo.Stop();
                    break;
                }

                // ACTIVIDAD DE IMPACTO CON MI NAVE (Misil del rival golpea)
                if (X2 < X1 + W1 && X1 + W1 < X2 + W2 && Y2 < Y1 + H1 && Y1 + H1 < Y2 + H2 && nombre == "Rival")
                {
                    c.Dispose();
                    // Lógica de daño
                    int daño = (X2 + PH < X1 + W1 && X1 + W1 < X2 + W2 - PH) ? 10 : 1;
                    int vidaActualJugador = (int)navex.Tag;
                    vidaActualJugador -= daño;
                    navex.Tag = vidaActualJugador;
                    HeartManager.UpdateHearts(playerHearts, vidaActualJugador);
                }

                // Finalización del juego (Derrota del jugador)
                if (navex.Tag != null && int.Parse(navex.Tag.ToString()) <= 0)
                {
                    navex.Dispose();
                    Bitmap NuevoImg = new Bitmap(contiene.Width, contiene.Height);
                    using (Graphics flagImagen = Graphics.FromImage(NuevoImg))
                    {
                        String drawString = "Perdiste el Juego";
                        Font drawFont = new Font("Arial", 16);
                        SolidBrush drawBrush = new SolidBrush(Color.Red);
                        flagImagen.DrawString(drawString, drawFont, drawBrush, new Point(40, 150));
                    }
                    contiene.Image = NuevoImg;
                    tiempo.Stop();
                    break;
                }

                // Destrucción de misiles fuera de límites
                if ((c.Location.Y <= 0 && nombre == "Misil") || (c.Location.Y >= contiene.Height && nombre == "Rival"))
                {
                    c.Dispose();
                }

                // Lógica de colisión directa entre naves
                if (W >= X2 && H >= Y2 && W2 >= X && H2 >= Y)
                {
                    naveRival.Dispose();
                    navex.Dispose();
                    tiempo.Stop(); // Detener el juego en colisión fatal
                    break;
                }
            }
        }
    }
}