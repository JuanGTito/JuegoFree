using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using JuegoFree.Entities;
using JuegoFree.Properties;
using JuegoFree.Core;

namespace JuegoFree.Scenes
{
    public static class GameScene
    {
        public static void Escenario(PictureBox contiene, Form1 mainForm, ShipConfiguration playerShip)
        {
            contiene.Controls.Clear();
            mainForm.IsGameActive = true;

            const int AREA_W = GameSettings.GAME_WIDTH;
            const int AREA_H = GameSettings.GAME_HEIGHT;

            int offsetX = (contiene.Width - AREA_W) / 2;
            int offsetY = (contiene.Height - AREA_H) / 2;

            Bitmap background = new Bitmap(contiene.Width, contiene.Height);
            Graphics g = Graphics.FromImage(background);

            g.Clear(Color.Black);
            g.SetClip(new Rectangle(offsetX, offsetY, AREA_W, AREA_H));

            Image baseBackground = Resources.GameBackgroundBase;

            if (baseBackground != null)
            {
                g.DrawImage(baseBackground, new Rectangle(offsetX, offsetY, AREA_W, AREA_H), new Rectangle(0, 0, baseBackground.Width, baseBackground.Height), GraphicsUnit.Pixel);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.DarkBlue), offsetX, offsetY, AREA_W, AREA_H);
            }

            if (playerShip.ShipTypeID == 1)
            {
                g.FillRectangle(new SolidBrush(Color.DarkGray), offsetX, offsetY + AREA_H - 50, AREA_W, 5);
            }

            contiene.Image = background;
            g.Dispose();

            Random r = new Random();
            int aletX_GameArea = r.Next(50, AREA_W - 50);
            int sale = r.Next(1, 2);

            PictureBox newNavex = new PictureBox();
            ShipFactory.CreateShip(newNavex, 0, playerShip.ShipTypeID, playerShip.BaseColor, playerShip.InitialHealth, 1);
            mainForm.Navex = newNavex;
            contiene.Controls.Add(mainForm.Navex);
            mainForm.Navex.BringToFront();

            PictureBox newNaveRival = new PictureBox();
            ShipFactory.CreateShip(newNaveRival, 180, sale, Color.DarkBlue, 100, 2);
            mainForm.NaveRival = newNaveRival;
            contiene.Controls.Add(mainForm.NaveRival);
            mainForm.NaveRival.BringToFront();

            int naveY_Player = AREA_H - mainForm.Navex.Height - 50;
            mainForm.Navex.Location = new Point(aletX_GameArea + offsetX, naveY_Player + offsetY);
            mainForm.NaveRival.Location = new Point(aletX_GameArea + offsetX, 50 + offsetY);

            int heartSize = 32;
            int heartSpacing = 5;
            int startX = 10;

            int startYPlayer = offsetY + AREA_H - heartSize - 10;
            int startYRival = offsetY + 10;

            for (int i = 0; i < 5; i++)
            {
                PictureBox heartP = new PictureBox
                {
                    Size = new Size(heartSize, heartSize),
                    Image = Resources.Heart_Full,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                heartP.Location = new Point(startX + (i * (heartSize + heartSpacing)) + offsetX, startYPlayer);
                mainForm.PlayerHearts[i] = heartP;
                contiene.Controls.Add(heartP);
                heartP.BringToFront();

                PictureBox heartR = new PictureBox
                {
                    Size = new Size(heartSize, heartSize),
                    Image = Resources.Heart_Full,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                heartR.Location = new Point(startX + (i * (heartSize + heartSpacing)) + offsetX, startYRival);
                mainForm.RivalHearts[i] = heartR;
                contiene.Controls.Add(heartR);
                heartR.BringToFront();
            }
        }

    }
}