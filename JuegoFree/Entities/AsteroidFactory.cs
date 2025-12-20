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
    internal class AsteroidFactory
    {

        private static Point[] myAsteroide1 = {
            new Point(10, 0), new Point(20, 5), new Point(25, 15), new Point(20, 25),
            new Point(10, 30), new Point(0, 25), new Point(5, 15), new Point(0, 5)
        };
        private const int AST1_LARGO_BASE = 30;
        private const int AST1_ANCHO_BASE = 25;
        private const int AST1_VIDA_BASE = 5;

        private static Point[] myAsteroide2 = {
            new Point(15, 0), new Point(40, 5), new Point(45, 15), new Point(40, 25),
            new Point(15, 30), new Point(0, 20), new Point(5, 10), new Point(0, 5)
        };
        private const int AST2_LARGO_BASE = 30;
        private const int AST2_ANCHO_BASE = 45;
        private const int AST2_VIDA_BASE = 10;

        private static Point[] myAsteroide3 = {
            new Point(20, 0), new Point(35, 5), new Point(40, 15), new Point(50, 25),
            new Point(45, 40), new Point(30, 50), new Point(15, 45), new Point(0, 35),
            new Point(5, 20), new Point(0, 10), new Point(10, 5)
        };
        private const int AST3_LARGO_BASE = 50;
        private const int AST3_ANCHO_BASE = 50;
        private const int AST3_VIDA_BASE = 20;

        public static void CreateAsteroid(
            PictureBox Asteroide,
            int TipoAsteroide,
            Color Pintar,
            int escala = 1)
        {
            int largoBase = 0;
            int anchoBase = 0;
            int vidaBase = 0;
            Point[] selectedAsteroidPoints = null;

            if (TipoAsteroide == 1)
            {
                largoBase = AST1_LARGO_BASE;
                anchoBase = AST1_ANCHO_BASE;
                vidaBase = AST1_VIDA_BASE;
                selectedAsteroidPoints = myAsteroide1;
            }
            else if (TipoAsteroide == 2)
            {
                largoBase = AST2_LARGO_BASE;
                anchoBase = AST2_ANCHO_BASE;
                vidaBase = AST2_VIDA_BASE;
                selectedAsteroidPoints = myAsteroide2;
            }
            else if (TipoAsteroide == 3)
            {
                largoBase = AST3_LARGO_BASE;
                anchoBase = AST3_ANCHO_BASE;
                vidaBase = AST3_VIDA_BASE;
                selectedAsteroidPoints = myAsteroide3;
            }
            else
            {
                Asteroide.Visible = false;
                return;
            }

            escala = Math.Max(1, Math.Min(4, escala));

            float factorEscala = 1.0f * escala;

            int largoN = (int)(largoBase * factorEscala);
            int anchoN = (int)(anchoBase * factorEscala);

            Asteroide.Tag = vidaBase * escala;
            Asteroide.Name = $"ASTEROIDE_{TipoAsteroide}";

            if (selectedAsteroidPoints != null)
            {
                Point[] myAsteroide = EscalarPuntos(selectedAsteroidPoints, factorEscala);

                GraphicsPath ObjGrafico = new GraphicsPath();
                ObjGrafico.AddPolygon(myAsteroide);

                Asteroide.Size = new Size(anchoN, largoN);
                Asteroide.Region = new Region(ObjGrafico);
                Asteroide.Location = new Point(0, 0);

                Bitmap Imagen = new Bitmap(Asteroide.Width, Asteroide.Height);
                using (Graphics PintaImg = Graphics.FromImage(Imagen))
                {
                    PintaImg.Clear(Color.Transparent);
                    PintaImg.SmoothingMode = SmoothingMode.AntiAlias;

                    using (SolidBrush baseBrush = new SolidBrush(Pintar))
                    {
                        PintaImg.FillPolygon(baseBrush, myAsteroide);

                        using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, Color.Black)))
                        {
                            int detailSize = (int)(5 * factorEscala);
                            PintaImg.FillEllipse(shadowBrush, detailSize, detailSize, detailSize, detailSize);
                            PintaImg.FillRectangle(shadowBrush, anchoN - detailSize, detailSize, detailSize / 2, detailSize / 2);
                        }
                    }
                }
                Asteroide.Image = Imagen;
                Asteroide.Visible = true;
            }
        }

        private static Point[] EscalarPuntos(Point[] puntosOriginales, float factorEscala)
        {
            Point[] puntosEscalados = new Point[puntosOriginales.Length];

            for (int i = 0; i < puntosOriginales.Length; i++)
            {
                puntosEscalados[i].X = (int)(puntosOriginales[i].X * factorEscala);
                puntosEscalados[i].Y = (int)(puntosOriginales[i].Y * factorEscala);
            }

            return puntosEscalados;
        }

    }
}
