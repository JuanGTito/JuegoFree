﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JuegoFree.Scenes
{
    internal class GameScene
    {
        public static void Escenario(PictureBox contiene, int tipo)
        {
            // Creamos un nuevo Bitmap del tamaño del contenedor
            Bitmap background = new Bitmap(contiene.Width, contiene.Height);
            Graphics g = Graphics.FromImage(background);

            // Establecer el fondo del espacio (negro)
            g.Clear(Color.Black);

            // Simular un campo de estrellas
            Random r = new Random();
            int numEstrellas = 200;

            for (int i = 0; i < numEstrellas; i++)
            {
                int x = r.Next(contiene.Width);
                int y = r.Next(contiene.Height);
                int size = r.Next(1, 3); // Tamaño de la estrella

                // Dibujar la estrella (círculo simple o cuadrado)
                g.FillRectangle(Brushes.White, x, y, size, size);
            }

            // Dependiendo del 'tipo' (que en el código original se generaba de 1 a 3),
            // se pueden agregar elementos visuales específicos al fondo.
            if (tipo == 1)
            {
                // Escenario 1: Añadir una línea de horizonte distante (ejemplo)
                g.FillRectangle(new SolidBrush(Color.DarkGray), 0, contiene.Height - 50, contiene.Width, 5);
            }
            else if (tipo == 2)
            {
                // Escenario 2: Color de estrellas diferente
                for (int i = 0; i < 50; i++)
                {
                    int x = r.Next(contiene.Width);
                    int y = r.Next(contiene.Height);
                    g.FillRectangle(Brushes.Yellow, x, y, 2, 2);
                }
            }

            // Asignar el fondo dibujado al PictureBox
            contiene.Image = background;
            g.Dispose();
        }
    }
}
