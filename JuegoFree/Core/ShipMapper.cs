using System.Drawing;
using JuegoFree.Entities;

namespace JuegoFree.Core
{
    public static class ShipMapper
    {
        public static ShipConfiguration FromEntity(ShipEntity entity)
        {
            return new ShipConfiguration
            {
                Id = entity.Id,
                Name = entity.Name,
                InitialHealth = entity.BaseHealth,
                BaseDamage = entity.BaseDamage,
                Polygon = entity.Polygon,
                BaseColor = ColorTranslator.FromHtml(entity.ColorHex)
            };
        }
    }
}
