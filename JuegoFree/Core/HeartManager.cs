using System;
using JuegoFree.Properties;
using System.Windows.Forms;
using System.Drawing;

namespace JuegoFree.Core
{
    // La clase debe ser 'public static'
    public static class HeartManager
    {
        private const int HEART_MAX = 20; // 20 puntos por corazón (el umbral)

        public static void UpdateHearts(PictureBox[] hearts, int currentHealth)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                // Calcula el umbral de vida total que debe superar este corazón para estar LLENO.
                // Corazón 0: Umbral 20. Corazón 1: Umbral 40, etc.
                int heartThreshold = (i + 1) * HEART_MAX;

                // Si la vida actual es MAYOR o IGUAL al umbral del corazón,
                // significa que este segmento de 20 puntos está activo (LLENO).
                if (currentHealth >= heartThreshold)
                {
                    hearts[i].Image = Properties.Resources.Heart_Full;
                }
                // Si la vida actual es MENOR que el umbral, el corazón está VACÍO.
                else
                {
                    hearts[i].Image = Properties.Resources.Heart_Empty;
                }
            }
        }
    }
}