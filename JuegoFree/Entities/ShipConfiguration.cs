using System.Drawing;

namespace JuegoFree.Entities
{
    public class ShipConfiguration
    {
        public string Name { get; set; }
        public int ShipTypeID { get; set; }
        public int InitialHealth { get; set; }

        public int BaseDamage { get; set; }
        public float DamageMultiplier { get; set; }

        public Color BaseColor { get; set; }
        public Color MissileColor { get; set; }

        public int GetFinalDamage()
        {
            return (int)(BaseDamage * DamageMultiplier);
        }
    }


    public static class ShipCatalog
    {
        public static ShipConfiguration GetShip(int shipID)
        {
            switch (shipID)
            {
                case 1:
                    return new ShipConfiguration
                    {
                        Name = "Fighter (Modelo T-1)",
                        ShipTypeID = 1,
                        InitialHealth = 100,
                        BaseDamage = 10,
                        DamageMultiplier = 1.0f,
                        BaseColor = Color.Blue,
                        MissileColor = Color.LightBlue
                    };

                case 2:
                    return new ShipConfiguration
                    {
                        Name = "Cruiser (Modelo T-2)",
                        ShipTypeID = 2,
                        InitialHealth = 150,
                        BaseDamage = 8,
                        DamageMultiplier = 0.8f,
                        BaseColor = Color.Green,
                        MissileColor = Color.LimeGreen
                    };

                case 3:
                    return new ShipConfiguration
                    {
                        Name = "Scout (Modelo T-3)",
                        ShipTypeID = 3,
                        InitialHealth = 75,
                        BaseDamage = 12,
                        DamageMultiplier = 1.3f,
                        BaseColor = Color.Red,
                        MissileColor = Color.OrangeRed
                    };

                default:
                    return GetShip(1);
            }
        }
    }

}
