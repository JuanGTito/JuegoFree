using System.Drawing;
using JuegoFree.Entities;

namespace JuegoFree.Core
{
    public static class ShipMapper
    {
        public static ShipConfiguration FromEntity(Avion entity)
        {
            return new ShipConfiguration
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Vida = entity.Vida,
                Daño = entity.Daño,
                Poligono = entity.Poligono,
                BaseColor = ColorTranslator.FromHtml(entity.ColorHex),
                Tipo = entity.Tipo
            };
        }
    }
}
