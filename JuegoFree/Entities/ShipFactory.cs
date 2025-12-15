using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JuegoFree.Core;
using JuegoFree.Properties;

namespace JuegoFree.Entities
{
    internal class ShipFactory
    {

        public static void CreateShip(
            PictureBox avion,
            ShipConfiguration ship,
            int angulo = 0,
            int escala = 1)
        {
            escala = Math.Max(1, Math.Min(4, escala));

            float factorEscala = 0.75f + (escala - 1) * 0.5833f;

            Point[] scaledPoints = EscalarPuntos(
                ship.Polygon,
                factorEscala,
                angulo,
                ship.Polygon.Max(p => p.Y) + 5
            );

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(scaledPoints);

            int maxX = scaledPoints.Max(p => p.X);
            int maxY = scaledPoints.Max(p => p.Y);

            avion.Size = new Size(maxX + 5, maxY + 5);
            avion.Region = new Region(path);

            Bitmap bmp = new Bitmap(avion.Width, avion.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (SolidBrush brush = new SolidBrush(ship.BaseColor))
                {
                    g.FillPolygon(brush, scaledPoints);
                }
            }

            avion.Image = bmp;
            avion.Tag = ship.InitialHealth;
            avion.Visible = true;
        }


        public static void CreateShipFromPolygon(
            PictureBox avion,
            Point[] polygon,
            Color color,
            int scale = 1)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(polygon);

            avion.Region = new Region(path);

            Bitmap bmp = new Bitmap(avion.Width, avion.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.FillPolygon(new SolidBrush(color), polygon);
            }

            avion.Image = bmp;
            avion.Visible = true;
        }

        // Método auxiliar para escalar puntos
        private static Point[] EscalarPuntos(Point[] puntosOriginales, float factorEscala, int angulo, int altoFinal)
        {
            Point[] puntosEscalados = new Point[puntosOriginales.Length];

            for (int i = 0; i < puntosOriginales.Length; i++)
            {
                // Escalar coordenadas
                int x = (int)(puntosOriginales[i].X * factorEscala);
                int y = (int)(puntosOriginales[i].Y * factorEscala);

                // Aplicar rotación si es necesario
                if (angulo == 180)
                {
                    puntosEscalados[i].X = x;
                    puntosEscalados[i].Y = altoFinal - 1 - y;
                }
                else
                {
                    puntosEscalados[i].X = x;
                    puntosEscalados[i].Y = y;
                }
            }

            return puntosEscalados;
        }

        //-------------EFECTOS DE LA NAVE PRINCIPAL-------------//
        public static void ShipRun(PictureBox Avion, int AngRotar, int velox)
        {
            Bitmap ImagenBase = (Avion.Image as Bitmap);
            if (ImagenBase == null)
            {
                return;
            }

            // Creamos una nueva imagen para dibujar los efectos encima.
            Bitmap ImagenConEfectos = new Bitmap(Avion.Width, Avion.Height);

            using (Graphics PintaImg = Graphics.FromImage(ImagenConEfectos))
            {
                // Dibujar la nave base
                PintaImg.DrawImage(ImagenBase, new Point(0, 0));

                // Puntos de Efectos de la Nave (Asumo que son de la Nave Tipo 1)
                Point[] puntoDer = { new Point(35, 28), new Point(35, 30), new Point(36, 30), new Point(37, 31),
                new Point(37, 37), new Point(38, 38), new Point(38, 40), new Point(39, 41), new Point(39, 44), new Point(40, 45),
                new Point(40, 46), new Point(42, 48), new Point(43, 48), new Point(44, 49), new Point(44, 64), new Point(43, 65),
                new Point(42, 65), new Point(41, 66), new Point(40, 66), new Point(38, 68), new Point(36, 68), new Point(36, 69),
                new Point(36, 63), new Point(35, 62), new Point(35, 28) };
                Point[] puntoIzq = { new Point(23,28), new Point(23,30), new Point(22,30), new Point(21,31), new Point(21,37),
                new Point(20,36), new Point(20,40), new Point(19,41), new Point(19,44), new Point(18,45), new Point(18,46),
                new Point(16,48), new Point(15,48), new Point(14,49), new Point(14,64), new Point(15,65), new Point(16,65),
                new Point(17,66), new Point(18,66), new Point(20,68), new Point(22,68), new Point(22,69), new Point(22,63),
                new Point(23,62), new Point(23,28) };
                Point[] puntoAtr = { new Point(29, 21), new Point(31, 19), new Point(32, 19), new Point(33, 20), new Point(33, 25),
                new Point(33,26), new Point(32,63), new Point(34,65), new Point(34,68), new Point(33,69), new Point(33,74),
                new Point(32,73), new Point(31,73), new Point(29,71), new Point(27,73), new Point(26,73), new Point(25,74),
                new Point(25,69), new Point(24,68), new Point(24,65), new Point(26,63), new Point(26,26), new Point(25,25),
                new Point(25,20), new Point(26,19), new Point(27,19), new Point(29,21)};



                if (velox == 1)
                {
                    PintaImg.FillRectangle(Brushes.Orange, 30, 75, 2, 5);

                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.White)))
                    {
                        PintaImg.FillPolygon(brush, puntoDer);
                        PintaImg.FillPolygon(brush, puntoIzq);
                    }
                }
                else if (velox == 2)
                {
                    using (SolidBrush shieldBrush = new SolidBrush(Color.FromArgb(150, Color.LightBlue)))
                    {
                        PintaImg.FillPolygon(shieldBrush, puntoDer);
                        PintaImg.FillPolygon(shieldBrush, puntoIzq);
                        PintaImg.FillPolygon(shieldBrush, puntoAtr);
                    }
                    PintaImg.FillRectangle(Brushes.Red, 30, 70, 2, 10);
                }
                else if (velox == 3)
                {
                    PintaImg.FillRectangle(Brushes.DarkRed, 15, 30, 1, 1);
                    PintaImg.FillRectangle(Brushes.DarkRed, 25, 1, 1, 16);
                    PintaImg.FillRectangle(Brushes.DarkRed, 37, 1, 1, 9);
                }

                Avion.Image = Utils.RotateImage(ImagenConEfectos, AngRotar);
            }
        }

        private static Point[] RotatePointsIfNecessary(Point[] originalPoints, int angle, int height)
        {
            if (angle == 180)
            {
                Point[] rotated = new Point[originalPoints.Length];
                for (int i = 0; i < originalPoints.Length; i++)
                {
                    rotated[i].X = originalPoints[i].X;
                    rotated[i].Y = height - 1 - originalPoints[i].Y;
                }
                return rotated;
            }
            return originalPoints;
        }
    }
}
