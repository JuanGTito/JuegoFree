using System.Drawing;

namespace JuegoFree.Entities
{
    public class Avion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Vida { get; set; }
        public int Daño { get; set; }
        public Point[] Poligono { get; set; }
        public string ColorHex { get; set; }

        public string Tipo { get; set; }
    }
}
