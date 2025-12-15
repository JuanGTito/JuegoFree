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
        public static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

                gfx.DrawImage(img, new Point(0, 0));
            }
            return bmp;
        }

        public static class PolygonParser
        {
            public static Point[] Parse(string data)
            {
                string[] pairs = data.Split(';');
                Point[] points = new Point[pairs.Length];

                for (int i = 0; i < pairs.Length; i++)
                {
                    string[] xy = pairs[i].Split(',');
                    int x = int.Parse(xy[0]);
                    int y = int.Parse(xy[1]);
                    points[i] = new Point(x, y);
                }

                return points;
            }
        }

    }
}
