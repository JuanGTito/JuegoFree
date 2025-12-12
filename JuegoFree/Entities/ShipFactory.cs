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
        private static Point[] myNave1 = {
    new Point(35, 0),
    new Point(33, 0),
    new Point(33, 3),
    new Point(33, 4),
    new Point(33, 13),
    new Point(33, 13),
    new Point(33, 13),
    new Point(26, 13),
    new Point(26, 13),
    new Point(26, 4),
    new Point(26, 3),
    new Point(26, 0),
    new Point(24, 0),
    new Point(23, 1),
    new Point(10, 28),
    new Point(4, 35),
    new Point(1, 43),
    new Point(4, 44),
    new Point(3, 45),
    new Point(2, 45),
    new Point(2, 52),
    new Point(4, 53),
    new Point(4, 53),
    new Point(1, 55),
    new Point(1, 55),
    new Point(3, 62),
    new Point(5, 67),
    new Point(7, 69),
    new Point(8, 70),
    new Point(9, 71),
    new Point(9, 72),
    new Point(14, 76),
    new Point(20, 79),
    new Point(27, 80),
    new Point(32, 80),
    new Point(38, 79),
    new Point(44, 76),
    new Point(48, 74),
    new Point(50, 72),
    new Point(50, 71),
    new Point(51, 70),
    new Point(52, 69),
    new Point(55, 65),
    new Point(57, 60),
    new Point(59, 55),
    new Point(55, 53),
    new Point(55, 53),
    new Point(58, 52),
    new Point(58, 45),
    new Point(56, 45),
    new Point(56, 44),
    new Point(58, 43),
    new Point(56, 38),
    new Point(59, 33),
    new Point(59, 29),
    new Point(58, 24),
    new Point(56, 24),
    new Point(53, 24),
    new Point(51, 29),
    new Point(51, 29),
    new Point(49, 26),
    new Point(37, 1)
};

        private static Point[] myNave1_Cabina = {
        new Point(28, 5), new Point(32, 5), new Point(32, 20), new Point(28, 20)
    };

        private static Point[] myNave1_MotorLuz = {
        new Point(20, 75), new Point(40, 75), new Point(40, 80), new Point(20, 80)
    };

        private static Point[] myNave2 = {/*.......*/ };

        private static Point[] myNave3 = {/*.......*/ };

        public static void CreateShip(PictureBox Avion, int AngRotar, int Tipox, Color Pintar, int Vida, int escala = 1)
        {
            int largoBase = 1;
            int anchoBase = 1;
            Point[] selectedNavePoints = null;

            if (Tipox == 1)
            {
                largoBase = 80;
                anchoBase = 69;
                selectedNavePoints = myNave1;
            }
            else if (Tipox == 2)
            {
                largoBase = 50;
                anchoBase = 50;
                selectedNavePoints = myNave2;
            }
            else if (Tipox == 3)
            {
                largoBase = 51;
                anchoBase = 305;
                selectedNavePoints = myNave3;
            }

            // Limitar la escala entre 1 y 4
            escala = Math.Max(1, Math.Min(4, escala));

            // Calcular factor de escala
            // Escala 1 = 60x80 (base reducida)
            // Escala 4 = 200x240 (máximo)
            float factorEscala = 0.75f + (escala - 1) * 0.5833f; // De 0.75 a 2.5

            int largoN = (int)(largoBase * factorEscala);
            int anchoN = (int)(anchoBase * factorEscala);

            Avion.Tag = Vida;

            if (selectedNavePoints != null)
            {
                Point[] myNave = EscalarPuntos(selectedNavePoints, factorEscala, AngRotar, largoN);

                GraphicsPath ObjGrafico = new GraphicsPath();
                ObjGrafico.AddPolygon(myNave);

                Avion.Size = new Size(anchoN, largoN);
                Avion.Region = new Region(ObjGrafico);
                Avion.Location = new Point(0, 0);

                Bitmap Imagen = new Bitmap(Avion.Width, Avion.Height);
                using (Graphics PintaImg = Graphics.FromImage(Imagen))
                {
                    PintaImg.Clear(Color.Transparent);
                    PintaImg.SmoothingMode = SmoothingMode.AntiAlias; // Mejor calidad al escalar

                    using (SolidBrush baseBrush = new SolidBrush(Pintar))
                    {
                        PintaImg.FillPolygon(baseBrush, myNave);
                    }

                    if (Tipox == 1)
                    {
                        Point[] cabinaEscalada = EscalarPuntos(myNave1_Cabina, factorEscala, AngRotar, largoN);
                        using (SolidBrush cabinaBrush = new SolidBrush(Color.Silver))
                        {
                            PintaImg.FillPolygon(cabinaBrush, cabinaEscalada);
                        }

                        Point[] motorLuzEscalada = EscalarPuntos(myNave1_MotorLuz, factorEscala, AngRotar, largoN);
                        using (SolidBrush luzBrush = new SolidBrush(Color.Orange))
                        {
                            PintaImg.FillPolygon(luzBrush, motorLuzEscalada);
                        }
                    }
                }
                Avion.Image = Imagen;
            }
            Avion.Visible = true;
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
