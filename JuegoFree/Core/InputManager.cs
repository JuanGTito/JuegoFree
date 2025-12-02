using JuegoFree.Core;
using JuegoFree.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace JuegoFree.Core
{
    public static class InputManager
    {
        private static readonly HashSet<Keys> _keyDown = new HashSet<Keys>();

        private static readonly Stopwatch _stopwatch = new Stopwatch();

        private const long FIRE_RATE_MS = 250;
        private static long _lastShotTime = 0;

        private const int GAME_W = GameSettings.GAME_WIDTH;
        private const int GAME_H = GameSettings.GAME_HEIGHT;

        private static int offsetX = 0;
        private static int offsetY = 0;

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

            offsetX = (contiene.Width - GAME_W) / 2;
            offsetY = (contiene.Height - GAME_H) / 2;

            int minX = offsetX;
            int maxX = offsetX + GAME_W - navex.Width;
            int minY = offsetY;

            int maxY = offsetY + GAME_H - navex.Height - 50;

            int speed = 10;
            int currentLeft = navex.Left;
            int currentTop = navex.Top;


            if (CurrentKeys.Contains(Keys.Left))
            {
                currentLeft -= speed;
                angulo = -15;
            }
            if (CurrentKeys.Contains(Keys.Right))
            {
                currentLeft += speed;
                angulo = 15;
            }


            if (CurrentKeys.Contains(Keys.Up))
            {
                currentTop -= speed;
            }
            if (CurrentKeys.Contains(Keys.Down))
            {
                currentTop += speed;
            }


            // Restringir X al área de 600px central
            navex.Left = Math.Max(minX, Math.Min(currentLeft, maxX));

            // Restringir Y al área de 800px central
            navex.Top = Math.Max(minY, Math.Min(currentTop, maxY));

            // Si hubo movimiento horizontal, se ejecuta ShipRun para el ángulo/rotación
            if (CurrentKeys.Contains(Keys.Left) || CurrentKeys.Contains(Keys.Right))
            {
                ShipFactory.ShipRun(navex, (int)angulo, 0);
            }
            // Si hubo movimiento vertical, se ejecuta ShipRun para el efecto de velocidad
            else if (CurrentKeys.Contains(Keys.Up) || CurrentKeys.Contains(Keys.Down))
            {
                ShipFactory.ShipRun(navex, 0, 1);
            }
            else
            {
                // Si no hay movimiento, restauramos el ángulo a 0 (nave recta)
                ShipFactory.ShipRun(navex, 0, 0);
            }

            if (_keyDown.Contains(Keys.Enter))
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