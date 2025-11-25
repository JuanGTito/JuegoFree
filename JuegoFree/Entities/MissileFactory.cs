using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoFree.Entities
{
    internal class MissileFactory
    {
        public static void CreateMisil(
            PictureBox contiene,
            int AngRotar,
            Color pintar,
            string nombre,
            int x,
            int y)
        {
            PictureBox Balas = new PictureBox();
            int PosX = 1;
            int PosY = 1;
            int largoM = 11;
            int anchoM = 8;

            //declarar los array de puntos.
            Point[] myMisil1 = { new Point(4 * PosX, 0 * PosY), new Point(5 * PosX, 1 * PosY), new
            Point(6 * PosX, 2 * PosY), new Point(6 * PosX, 7 * PosY), new Point(7 * PosX, 8 * PosY), new
            Point(8 * PosX, 9 * PosY), new Point(7 * PosX, 9 * PosY), new Point(6 * PosX, 10 * PosY), new
            Point(2 * PosX, 10 * PosY), new Point(1 * PosX, 9 * PosY), new Point(0 * PosX, 9 * PosY), new
            Point(1 * PosX, 8 * PosY), new Point(2 * PosX, 7 * PosY), new Point(2 * PosX, 2 * PosY), new
            Point(3 * PosX, 1 * PosY), new Point(4 * PosX, 0 * PosY) };

            Point[] myMisil = new Point[myMisil1.Count()];
            for (int i = 0; i < myMisil1.Count(); i++)
            {
                myMisil[i].X = myMisil1[i].X;
                if (AngRotar == 180)
                    myMisil[i].Y = largoM - myMisil1[i].Y;
                else
                    myMisil[i].Y = myMisil1[i].Y;
            }

            GraphicsPath ObjGrafico = new GraphicsPath();
            ObjGrafico.AddPolygon(myMisil);

            // Configuración del PictureBox
            Balas.Location = new Point(x, y);
            Balas.BackColor = pintar;
            Balas.Size = new Size(anchoM * PosX, largoM * PosY);
            Balas.Region = new Region(ObjGrafico);

            // Añadir al contenedor
            contiene.Controls.Add(Balas);

            Balas.Visible = true;
            Balas.Tag = nombre;

            //*************** DIBUJAR COLORES *************//
            Bitmap flag = new Bitmap(anchoM, largoM);
            using (Graphics flagImagen = Graphics.FromImage(flag))
            {
                flagImagen.FillRectangle(Brushes.Orange, 2, 8, 5, 1);
                flagImagen.FillRectangle(Brushes.Yellow, 3, 10, 3, 1);
            }
            Balas.Image = flag;
        }
    }
}
