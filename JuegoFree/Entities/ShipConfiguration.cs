using System.Drawing;

public class ShipConfiguration
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public int Vida { get; set; }
    public int Daño { get; set; }

    public Point[] Poligono { get; set; }

    public Color BaseColor { get; set; }

    public string Tipo { get; set; }
}
