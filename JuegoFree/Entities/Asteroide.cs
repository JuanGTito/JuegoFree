using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JuegoFree.Entities
{
    internal class Asteroide
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int Vida { get; set; }
        public int Daño { get; set; }
        public Point[] Poligono { get; set; }
        public string ColorHex { get; set; }
    }
}
