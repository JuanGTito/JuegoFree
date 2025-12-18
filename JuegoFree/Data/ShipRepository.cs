using JuegoFree.Core;
using JuegoFree.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Drawing;

namespace JuegoFree.Data
{
    public static class ShipRepository
    {
        public static List<Avion> GetAllShips()
        {
            List<Avion> ships = new List<Avion>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM ships";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ships.Add(new Avion
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Vida = reader.GetInt32("vida"),
                            Daño = reader.GetInt32("daño"),
                            Poligono = Utils.PolygonParser.Parse(
                                reader.GetString("poligono_puntos")),
                            ColorHex = reader.GetString("color_hex"),
                            Tipo = reader.GetString("tipo")
                        });
                    }
                }
            }

            return ships;
        }
    }
}
