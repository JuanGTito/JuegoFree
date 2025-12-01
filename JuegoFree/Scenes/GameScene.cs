using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using JuegoFree.Entities;
using JuegoFree.Properties;

namespace JuegoFree.Scenes
{
    public static class GameScene
    {
        public static void Escenario(PictureBox contiene, Form1 mainForm, int tipo)
        {
            // 1. Limpieza y Dibujo del fondo (Mantenemos tu lógica original de fondo estático)
            contiene.Controls.Clear();

            Bitmap background = new Bitmap(contiene.Width, contiene.Height);
            Graphics g = Graphics.FromImage(background);

            g.Clear(Color.Black);

            Random r = new Random();
            int numEstrellas = 200;

            // Simular campo de estrellas
            for (int i = 0; i < numEstrellas; i++)
            {
                int x = r.Next(contiene.Width);
                int y = r.Next(contiene.Height);
                int size = r.Next(1, 3);
                g.FillRectangle(Brushes.White, x, y, size, size);
            }

            // Escenarios específicos
            if (tipo == 1)
            {
                g.FillRectangle(new SolidBrush(Color.DarkGray), 0, contiene.Height - 50, contiene.Width, 5);
            }
            else if (tipo == 2)
            {
                for (int i = 0; i < 50; i++)
                {
                    int x = r.Next(contiene.Width);
                    int y = r.Next(contiene.Height);
                    g.FillRectangle(Brushes.Yellow, x, y, 2, 2);
                }
            }

            contiene.Image = background;
            g.Dispose();

            // ----------------------------------------------------------------------
            // 3. Inicialización de Naves y sus Posiciones (REASIGNACIÓN DE REFERENCIAS)
            // ----------------------------------------------------------------------

            int aletX = r.Next(50, contiene.Width - 50);
            int sale = r.Next(1, 2);

            // --- NAVE DEL JUGADOR ---

            // Crear la instancia local del PictureBox
            PictureBox newNavex = new PictureBox();
            ShipFactory.CreateShip(newNavex, 0, 1, Color.SeaGreen, 100);

            // REASIGNAR: Actualizar la referencia del campo interno 'navex' en Form1
            mainForm.Navex = newNavex;
            contiene.Controls.Add(mainForm.Navex);

            // --- NAVE RIVAL ---

            // Crear la instancia local del PictureBox
            PictureBox newNaveRival = new PictureBox();
            ShipFactory.CreateShip(newNaveRival, 180, sale, Color.DarkBlue, 100);

            // REASIGNAR: Actualizar la referencia del campo interno 'naveRival' en Form1
            mainForm.NaveRival = newNaveRival;
            contiene.Controls.Add(mainForm.NaveRival);

            // Posicionamiento
            mainForm.Navex.Location = new Point(aletX, contiene.Height - mainForm.Navex.Height - 50);
            mainForm.NaveRival.Location = new Point(aletX, 50);

            // ----------------------------------------------------------------------
            // 4. Inicialización de Corazones (HUD) (ASIGNACIÓN A ARRAY)
            // ----------------------------------------------------------------------

            int heartSize = 32;
            int heartSpacing = 5;
            int startX = 10;
            int startYPlayer = contiene.Height - heartSize - 10;
            int startYRival = 10;

            for (int i = 0; i < 5; i++)
            {
                // Corazones del Jugador
                PictureBox heartP = new PictureBox
                {
                    Size = new Size(heartSize, heartSize),
                    Location = new Point(startX + (i * (heartSize + heartSpacing)), startYPlayer),
                    Image = Resources.Heart_Full,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                // ASIGNACIÓN: El PictureBox se inserta en la posición 'i' del array del Form1
                mainForm.PlayerHearts[i] = heartP;
                contiene.Controls.Add(heartP);

                // Corazones del Rival
                PictureBox heartR = new PictureBox
                {
                    Size = new Size(heartSize, heartSize),
                    Location = new Point(startX + (i * (heartSize + heartSpacing)), startYRival),
                    Image = Resources.Heart_Full,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                // ASIGNACIÓN: El PictureBox se inserta en la posición 'i' del array del Form1
                mainForm.RivalHearts[i] = heartR;
                contiene.Controls.Add(heartR);
            }
        }
    }
}