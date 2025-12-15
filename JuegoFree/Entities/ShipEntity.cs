using System.Drawing;

namespace JuegoFree.Entities
{
    public class ShipEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseHealth { get; set; }
        public int BaseDamage { get; set; }
        public Point[] Polygon { get; set; }
    }
}
