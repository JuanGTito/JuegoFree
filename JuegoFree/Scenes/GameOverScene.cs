using System.Drawing;
using System.Windows.Forms;

namespace JuegoFree.Scenes
{
    public static class GameOverScene
    {
        public static void Show(PictureBox contiene, Form1 mainForm)
        {
            contiene.Controls.Clear();
            mainForm.IsGameActive = false;

            Bitmap bg = new Bitmap(contiene.Width, contiene.Height);
            Graphics g = Graphics.FromImage(bg);

            g.Clear(Color.Black);

            using (Font titleFont = new Font("Arial", 36, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.Red))
            {
                string text = "GAME OVER";
                SizeF size = g.MeasureString(text, titleFont);

                g.DrawString(
                    text,
                    titleFont,
                    brush,
                    (contiene.Width - size.Width) / 2,
                    120
                );
            }

            g.Dispose();
            contiene.Image = bg;

            Button btnRetry = new Button
            {
                Text = "Reintentar",
                Width = 200,
                Height = 40,
                Location = new Point(
                    (contiene.Width - 200) / 2,
                    250
                )
            };

            btnRetry.Click += (s, e) =>
            {
                mainForm.RestartGame();
            };

            contiene.Controls.Add(btnRetry);
        }
    }
}
