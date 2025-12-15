using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoFree.Core
{
    internal class Utils
    {
        public static Image RotateImage(Image img, float angle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.TranslateTransform(img.Width / 2f, img.Height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-img.Width / 2f, -img.Height / 2f);

                g.DrawImage(img, Point.Empty);
            }

            return bmp;
        }


        public static class PolygonParser
        {
            public static Point[] Parse(string data)
            {
                if (string.IsNullOrWhiteSpace(data))
                    return new Point[0];

                string[] pairs = data.Split(';');
                List<Point> points = new List<Point>();

                foreach (string pair in pairs)
                {
                    if (string.IsNullOrWhiteSpace(pair))
                        continue;

                    string[] xy = pair.Split(',');

                    if (xy.Length != 2)
                        continue;

                    if (int.TryParse(xy[0], out int x) &&
                        int.TryParse(xy[1], out int y))
                    {
                        points.Add(new Point(x, y));
                    }
                }

                return points.ToArray();
            }
        }


    }
}
