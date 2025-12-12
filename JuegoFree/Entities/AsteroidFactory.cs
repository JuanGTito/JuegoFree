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
        /******************** DEFINICIÓN DE FORMAS DE ASTEROIDES ********************/

        // Asteroide Pequeño (Irregular)
        private static Point[] myAsteroide1 = {
            new Point(10, 0), new Point(20, 5), new Point(25, 15), new Point(20, 25),
            new Point(10, 30), new Point(0, 25), new Point(5, 15), new Point(0, 5)
        };
        private const int AST1_LARGO_BASE = 30; // Altura máxima
        private const int AST1_ANCHO_BASE = 25; // Ancho máximo
        private const int AST1_VIDA_BASE = 5;

        // Asteroide Mediano (más alargado)
        private static Point[] myAsteroide2 = {
            new Point(15, 0), new Point(40, 5), new Point(45, 15), new Point(40, 25),
            new Point(15, 30), new Point(0, 20), new Point(5, 10), new Point(0, 5)
        };
        private const int AST2_LARGO_BASE = 30;
        private const int AST2_ANCHO_BASE = 45;
        private const int AST2_VIDA_BASE = 10;

        // Asteroide Grande (forma de roca más compleja)
        private static Point[] myAsteroide3 = {
            new Point(20, 0), new Point(35, 5), new Point(40, 15), new Point(50, 25),
            new Point(45, 40), new Point(30, 50), new Point(15, 45), new Point(0, 35),
            new Point(5, 20), new Point(0, 10), new Point(10, 5)
        };
        private const int AST3_LARGO_BASE = 50;
        private const int AST3_ANCHO_BASE = 50;
        private const int AST3_VIDA_BASE = 20;

        /******************** MÉTODO PRINCIPAL DE CREACIÓN ********************/
        /// <summary>
        /// Crea y configura un PictureBox como un asteroide.
        /// </summary>
        /// <param name="Asteroide">El PictureBox que será el asteroide.</param>
        /// <param name="TipoAsteroide">1=Pequeño, 2=Mediano, 3=Grande.</param>
        /// <param name="Pintar">Color de relleno del asteroide (sugerido: gris o marrón).</param>
        /// <param name="escala">Factor de escala para el tamaño.</param>
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

            // 1. SELECCIÓN DE TIPO
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
                // Si el tipo no es válido, usa el tipo 1 por defecto
                Asteroide.Visible = false;
                return;
            }

            // 2. CÁLCULO DE ESCALA Y TAMAÑO FINAL
            // La escala de asteroides puede ser más flexible, limitamos para seguridad.
            escala = Math.Max(1, Math.Min(4, escala));

            // Factor de escala lineal simple
            float factorEscala = 1.0f * escala;

            int largoN = (int)(largoBase * factorEscala);
            int anchoN = (int)(anchoBase * factorEscala);

            // Guardar la vida en la etiqueta Tag
            Asteroide.Tag = vidaBase * escala; // Vida aumenta con la escala
            Asteroide.Name = $"ASTEROIDE_{TipoAsteroide}";

            // 3. GENERACIÓN DE GEOMETRÍA Y PINTADO
            if (selectedAsteroidPoints != null)
            {
                // Escalar puntos sin rotación (los asteroides se rotarán en el gameloop si es necesario)
                Point[] myAsteroide = EscalarPuntos(selectedAsteroidPoints, factorEscala);

                // Crea la región de colisión y dibujo a partir del polígono
                GraphicsPath ObjGrafico = new GraphicsPath();
                ObjGrafico.AddPolygon(myAsteroide);

                Asteroide.Size = new Size(anchoN, largoN);
                Asteroide.Region = new Region(ObjGrafico); // Colisión y forma no rectangular
                Asteroide.Location = new Point(0, 0); // La posición se ajustará al añadirlo al contenedor

                // 4. DIBUJAR LA IMAGEN DEL ASTEROIDE
                Bitmap Imagen = new Bitmap(Asteroide.Width, Asteroide.Height);
                using (Graphics PintaImg = Graphics.FromImage(Imagen))
                {
                    PintaImg.Clear(Color.Transparent);
                    PintaImg.SmoothingMode = SmoothingMode.AntiAlias;

                    using (SolidBrush baseBrush = new SolidBrush(Pintar))
                    {
                        PintaImg.FillPolygon(baseBrush, myAsteroide);

                        // Opcional: Añadir detalles para simular textura de roca
                        using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, Color.Black)))
                        {
                            // Simulación de sombras/detalles dentro del asteroide.
                            // Esto asume una forma básica y no requiere escalado complejo si es solo para detalle.
                            // Dibujar pequeños rectángulos o elipses en color oscuro o claro.
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

        /******************** MÉTODO AUXILIAR PARA ESCALAR PUNTOS ********************/
        /// <summary>
        /// Escala las coordenadas de los puntos del polígono.
        /// </summary>
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

        // NO NECESITAS ShipRun NI RotatePointsIfNecessary aquí.
        // La rotación de los asteroides se maneja usualmente en el GameLoop (movimiento)
        // usando 'Utils.RotateImage' antes de asignarla a Asteroide.Image, o usando 
        // una matriz de transformación si se quiere rotación de la región.
    }
}
