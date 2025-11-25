using System;
using System.Drawing;
using System.Windows.Forms;
using JuegoFree.Entities;

namespace JuegoFree.Core
{
    public static class InputManager
    {
        public static void HandleKeyPress(
            KeyEventArgs e, 
            PictureBox navex, 
            PictureBox contiene, 
            Timer tiempo, 
            ref float angulo)
        {
            switch (e.KeyValue)
            {
                case 37: // flecha hacia la izquierda
                    if ((contiene.Left < navex.Left)) // PARAMETROS DE LIMITE
                    {
                        navex.Left -= 10;
                        angulo = -15;
                        // ** CORRECCIÓN: Pasar el ángulo real para la rotación (casteado a int) **
                        ShipFactory.ShipRun(navex, (int)angulo, 0);
                    }
                    break;

                case 38: // flecha hacia arriba
                    if (contiene.Top < navex.Top)
                    {
                        navex.Top -= 5;
                        ShipFactory.ShipRun(navex, 0, 1);
                    }
                    break;

                case 39: // flecha hacia la derecha
                    if ((contiene.Right > navex.Right))
                    {
                        navex.Left += 10;
                        angulo = 15;
                        // ** CORRECCIÓN: Pasar el ángulo real para la rotación (casteado a int) **
                        ShipFactory.ShipRun(navex, (int)angulo, 0);
                    }
                    break;

                case 40: // flecha hacia abajo
                    if ((contiene.Bottom > navex.Bottom))
                    {
                        navex.Top += 5;
                        ShipFactory.ShipRun(navex, 0, 1);
                    }
                    break;

                case 13:
                    tiempo.Start();
                    int x = navex.Location.X + (navex.Width / 2);
                    int y = navex.Location.Y + (navex.Height / 2);
                    MissileFactory.CreateMisil(
                        contiene, 
                        0, 
                        Color.DarkMagenta, 
                        "Misil", 
                        x, 
                        y
                    );
                    break;
            }
        }
    }
}