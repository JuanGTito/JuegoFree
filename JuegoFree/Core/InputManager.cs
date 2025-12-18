using JuegoFree.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TimerForms = System.Windows.Forms.Timer;

namespace JuegoFree.Core
{
    public static class InputManager
    {
        private static readonly HashSet<Keys> _currentKeys = new HashSet<Keys>();

        private const int BURST_MAX_SIZE = 10;
        private const int BURST_SHOT_TICK_RATE = 3;
        private const int BURST_RECHARGE_TICKS = 60;

        private static int _shotsInCurrentBurst = BURST_MAX_SIZE;
        private static int _burstCooldownTimer = 0;
        private static int _shotTickCounter = 0;

        private const int GAME_W = GameSettings.GAME_WIDTH;
        private const int GAME_H = GameSettings.GAME_HEIGHT;

        private const int MOVEMENT_SPEED = 10;
        private const int BOTTOM_MARGIN = 50;

        private static int _offsetX = 0;
        private static int _offsetY = 0;

        static InputManager()
        {
        }

        public static ISet<Keys> CurrentKeys => _currentKeys;

        public static void SetKeyDown(Keys key)
        {
            _currentKeys.Add(key);
        }

        public static void SetKeyUp(Keys key)
        {
            _currentKeys.Remove(key);
        }

        public static void ProcessGameLogic(
            PictureBox shipPictureBox,
            PictureBox container,
            TimerForms gameTimer,
            ref float angle)
        {
            angle = 0;

            CalculateGameBounds(container, shipPictureBox);

            int newLeft = shipPictureBox.Left;
            int newTop = shipPictureBox.Top;

            ProcessMovement(ref newLeft, ref newTop, ref angle);

            ApplyMovement(shipPictureBox, newLeft, newTop);

            ApplyShipEffect(shipPictureBox, (int)angle);

            if (_burstCooldownTimer > 0)
            {
                _burstCooldownTimer--;
            }

            if (_shotTickCounter > 0)
            {
                _shotTickCounter--;
            }

            if (_currentKeys.Contains(Keys.Enter))
            {
                if (_burstCooldownTimer == 0)
                {
                    Fire(shipPictureBox, container);
                }
            }

            if (!_currentKeys.Contains(Keys.Enter) && _shotsInCurrentBurst < BURST_MAX_SIZE && _burstCooldownTimer == 0)
            {
                _shotsInCurrentBurst = BURST_MAX_SIZE;
            }
        }

        private static void CalculateGameBounds(PictureBox container, PictureBox shipPictureBox)
        {
            _offsetX = (container.Width - GAME_W) / 2;
            _offsetY = (container.Height - GAME_H) / 2;
        }

        private static void ProcessMovement(ref int currentLeft, ref int currentTop, ref float angle)
        {
            if (_currentKeys.Contains(Keys.Left))
            {
                currentLeft -= MOVEMENT_SPEED;
                angle = -15;
            }
            if (_currentKeys.Contains(Keys.Right))
            {
                currentLeft += MOVEMENT_SPEED;
                angle = 15;
            }

            if (_currentKeys.Contains(Keys.Up))
            {
                currentTop -= MOVEMENT_SPEED;
            }
            if (_currentKeys.Contains(Keys.Down))
            {
                currentTop += MOVEMENT_SPEED;
            }
        }

        private static void ApplyMovement(PictureBox shipPictureBox, int newLeft, int newTop)
        {
            int minX = _offsetX;
            int maxX = _offsetX + GAME_W - shipPictureBox.Width;
            int minY = _offsetY;
            int maxY = _offsetY + GAME_H - shipPictureBox.Height - BOTTOM_MARGIN;

            shipPictureBox.Left = Math.Max(minX, Math.Min(newLeft, maxX));
            shipPictureBox.Top = Math.Max(minY, Math.Min(newTop, maxY));
        }

        private static void ApplyShipEffect(PictureBox shipPictureBox, int angle)
        {
            bool isMovingHorizontal = _currentKeys.Contains(Keys.Left) || _currentKeys.Contains(Keys.Right);
            bool isMovingVertical = _currentKeys.Contains(Keys.Up) || _currentKeys.Contains(Keys.Down);

            if (isMovingHorizontal)
            {
                CrearAvion.ShipRun(shipPictureBox, angle, 0);
            }
            else if (isMovingVertical)
            {
                CrearAvion.ShipRun(shipPictureBox, 0, 1);
            }
            else
            {
                CrearAvion.ShipRun(shipPictureBox, 0, 0);
            }
        }

        private static void Fire(PictureBox shipPictureBox, PictureBox container)
        {
            if (_shotsInCurrentBurst > 0 && _shotTickCounter == 0)
            {
                Color projectileColor = Color.DarkMagenta;
                if (int.TryParse(shipPictureBox.Name, out int argb))
                {
                    projectileColor = Color.FromArgb(argb);
                }

                int x = shipPictureBox.Left + (shipPictureBox.Width / 2);
                int y = shipPictureBox.Top;

                MissileFactory.CreateMisil(
                    container,
                    0,
                    projectileColor,
                    "Misil",
                    x,
                    y
                );

                _shotsInCurrentBurst--;

                _shotTickCounter = BURST_SHOT_TICK_RATE;

                if (_shotsInCurrentBurst == 0)
                {
                    _burstCooldownTimer = BURST_RECHARGE_TICKS;
                }
            }
        }
    }
}