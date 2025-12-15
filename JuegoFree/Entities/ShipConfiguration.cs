using System.Drawing;

public class ShipConfiguration
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int InitialHealth { get; set; }
    public int BaseDamage { get; set; }

    public Point[] Polygon { get; set; }

    public Color BaseColor { get; set; }
}
