using System;
using System.Collections.Generic;
using JuegoFree.Entities;

namespace JuegoFree.Core
{
    public static class ShipSelector
    {
        private static readonly Random rnd = new Random();

        public static ShipConfiguration GetRandomEnemyShip(
            List<ShipConfiguration> ships,
            ShipConfiguration playerShip)
        {
            if (ships == null || ships.Count == 0)
                return playerShip;

            // Evitar que sea la misma nave del jugador
            var candidates = ships.FindAll(s => s.Id != playerShip.Id);

            if (candidates.Count == 0)
                return playerShip;

            return candidates[rnd.Next(candidates.Count)];
        }
    }
}
