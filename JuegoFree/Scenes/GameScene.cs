using JuegoFree.Core;
using JuegoFree.Data;
using JuegoFree.Entities;
using JuegoFree.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace JuegoFree.Scenes
{
    public static class GameScene
    {
        public static void Escenario(
            PictureBox contiene,
            Form1 mainForm,
            ShipConfiguration playerShip)
        {
            contiene.Controls.Clear();
            mainForm.IsGameActive = true;

            const int AREA_W = GameSettings.GAME_WIDTH;
            const int AREA_H = GameSettings.GAME_HEIGHT;

            int offsetX = (contiene.Width - AREA_W) / 2;
            int offsetY = (contiene.Height - AREA_H) / 2;

            // ---------- FONDO ----------
            Bitmap background = new Bitmap(contiene.Width, contiene.Height);
            using (Graphics g = Graphics.FromImage(background))
            {
                g.Clear(Color.Black);
                g.SetClip(new Rectangle(offsetX, offsetY, AREA_W, AREA_H));

                Image baseBackground = Resources.GameBackgroundBase;
                if (baseBackground != null)
                {
                    g.DrawImage(
                        baseBackground,
                        new Rectangle(offsetX, offsetY, AREA_W, AREA_H),
                        new Rectangle(0, 0, baseBackground.Width, baseBackground.Height),
                        GraphicsUnit.Pixel);
                }
                else
                {
                    g.FillRectangle(Brushes.DarkBlue, offsetX, offsetY, AREA_W, AREA_H);
                }
            }

            contiene.Image = background;

            List<Avion> shipEntities = ShipRepository.GetAllShips();
            List<ShipConfiguration> allShips =
                shipEntities
                .Select(entity => ShipMapper.FromEntity(
                    entity))
            .ToList();


            // ---------- NAVE JUGADOR ----------
            PictureBox playerPB = new PictureBox();
            CrearAvion.CreateShip(playerPB, playerShip, angulo: 0, escala: 1);

            playerPB.Tag = playerShip.Vida;
            mainForm.CurrentPlayerDamage = playerShip.Daño;

            mainForm.Navex = playerPB;
            contiene.Controls.Add(playerPB);
            playerPB.BringToFront();

            // ---------- NAVE RIVAL (TEMPORAL) ----------
            ShipConfiguration enemyShip =
                ShipSelector.GetRandomEnemyShip(allShips, playerShip);

            PictureBox rivalPB = new PictureBox();
            CrearAvion.CreateShip(rivalPB, enemyShip, angulo: 180, escala: 1);

            rivalPB.Tag = enemyShip.Vida;
            rivalPB.Name = "naveRival";

            mainForm.NaveRival = rivalPB;
            contiene.Controls.Add(rivalPB);
            rivalPB.BringToFront();

            // ---------- POSICIONES ----------
            Random r = new Random();
            int x = r.Next(50, AREA_W - 50);

            mainForm.Navex.Location = new Point(
                x + offsetX,
                AREA_H - mainForm.Navex.Height - 50 + offsetY
            );

            mainForm.NaveRival.Location = new Point(
                x + offsetX,
                50 + offsetY
            );

            // ---------- CORAZONES ----------
            int heartSize = 32;
            int spacing = 5;
            int startX = offsetX + 10;

            int playerY = offsetY + AREA_H - heartSize - 10;
            int rivalY = offsetY + 10;

            for (int i = 0; i < 5; i++)
            {
                PictureBox hp = new PictureBox
                {
                    Size = new Size(heartSize, heartSize),
                    Image = Resources.Heart_Full,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Location = new Point(startX + i * (heartSize + spacing), playerY)
                };

                PictureBox hr = new PictureBox
                {
                    Size = new Size(heartSize, heartSize),
                    Image = Resources.Heart_Full,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Location = new Point(startX + i * (heartSize + spacing), rivalY)
                };

                mainForm.PlayerHearts[i] = hp;
                mainForm.RivalHearts[i] = hr;

                contiene.Controls.Add(hp);
                contiene.Controls.Add(hr);

                hp.BringToFront();
                hr.BringToFront();
            }
        }
    }

}