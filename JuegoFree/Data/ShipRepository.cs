using JuegoFree.Core;
using JuegoFree.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Drawing;

namespace JuegoFree.Data
{
    public static class ShipRepository
    {
        public static List<ShipEntity> GetAllShips()
        {
            List<ShipEntity> ships = new List<ShipEntity>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM ships";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ships.Add(new ShipEntity
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            BaseHealth = reader.GetInt32("base_health"),
                            BaseDamage = reader.GetInt32("base_damage"),
                            Polygon = Utils.PolygonParser.Parse(
                                reader.GetString("polygon_points")),
                            ColorHex = reader.GetString("color_hex")
                        });
                    }
                }
            }

            return ships;
        }
    }
}
