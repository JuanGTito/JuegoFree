using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using JuegoFree.Entities;

namespace JuegoFree.Core
{
    public static class InputManager
    {
        private static readonly HashSet<Keys> _keyDown = new HashSet<Keys>();

        private static readonly Stopwatch _stopwatch = new Stopwatch();

        private const long FIRE_RATE_MS = 250;
        private static long _lastShotTime = 0;

        static InputManager()
        {
            _stopwatch.Start();
        }

        public static ISet<Keys> CurrentKeys => _keyDown;

        public static void SetKeyDown(Keys key)
        {
            _keyDown.Add(key);
        }

        public static void SetKeyUp(Keys key)
        {
            _keyDown.Remove(key);
        }
        public static void ProcessGameLogic(
            PictureBox navex,
            PictureBox contiene,
            Timer tiempo,
            ref float angulo)
        {
            angulo = 0;
            // Movimiento Nave izquierda
            if (CurrentKeys.Contains(Keys.Left) || _keyDown.Contains((Keys)37))
            {
                if ((navex.Left - 10 >= 0))
                {
                    navex.Left -= 10;
                    angulo = -15;
                    ShipFactory.ShipRun(navex, (int)angulo, 0);
                }
                else if (navex.Left > 0)
                {
                    navex.Left = 0;
                }
            }
            // Movimiento Nave derecha
            if (_keyDown.Contains(Keys.Right) || _keyDown.Contains((Keys)39))
            {
                if ((navex.Left + navex.Width + 10 <= contiene.Width))
                {
                    navex.Left += 10;
                    angulo = 15;
                    ShipFactory.ShipRun(navex, (int)angulo, 0);
                }
                else
                {
                    navex.Left = contiene.Width - navex.Width;
                }
            }
            // Movimiento Nave arriba
            if (CurrentKeys.Contains(Keys.Up) || _keyDown.Contains((Keys)38))
            {
                if ((contiene.Top < navex.Top))
                {
                    navex.Top -= 10;
                    ShipFactory.ShipRun(navex, 0, 1);
                }
            }
            // Movimiento Nave abajo
            if (_keyDown.Contains(Keys.Down) || _keyDown.Contains((Keys)40))
            {
                if ((contiene.Bottom > navex.Bottom + 50))
                {
                    navex.Top += 10;
                    ShipFactory.ShipRun(navex, 0, 1);
                }
            }

            if (_keyDown.Contains(Keys.Enter) || _keyDown.Contains((Keys)13))
            {
                Fire(navex, contiene, tiempo);
            }
        }

        private static void Fire(PictureBox navex, PictureBox contiene, Timer tiempo)
        {
            long currentTime = _stopwatch.ElapsedMilliseconds;
            long timeSinceLastShot = currentTime - _lastShotTime;

            if (timeSinceLastShot >= FIRE_RATE_MS)
            {
                tiempo.Start();
                int x = navex.Location.X + (navex.Width / 2);
                int y = navex.Location.Y;
                MissileFactory.CreateMisil(
                    contiene,
                    0,
                    Color.DarkMagenta,
                    "Misil",
                    x,
                    y
                    );
                _lastShotTime = currentTime;
            }
            
        }
    }
}