using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoFree.Core
{
    public static class GameSettings
    {
        // 1. RESOLUCIÓN BASE DE DISEÑO
        public const int GAME_WIDTH = 600;
        public const int GAME_HEIGHT = 800;

        public const int STARS_SPEED_GAME = 5;

        // 2. ESCALAMIENTO Y PANTALLA ACTUAL
        public static int CurrentScreenWidth { get; set; }
        public static int CurrentScreenHeight { get; set; }

        // 3. CONSTANTES DE JUGABILIDAD

        // Vida
        public const int PLAYER_MAX_HEALTH = 100; // Vida máxima para el jugador y rival
        public const int HEART_VALUE = 20;        // Valor de cada segmento del corazón (100 / 5)

        // Velocidad
        public const int PLAYER_SPEED = 10;       // Velocidad de movimiento por tick (usado en InputManager)
        public const int MISSILE_SPEED = 10;      // Velocidad del misil del jugador

        // Cadencia de Fuego (Cooldown)
        // 100ms = 10 disparos/segundo (usado con el Stopwatch en InputManager)
        public const long PLAYER_FIRE_RATE_MS = 100;

        // Comportamiento del Rival
        // El umbral que Dispara debe alcanzar (50 frames * 20ms/frame = 1 segundo)
        public const int RIVAL_FIRE_THRESHOLD = 50;

        // 4. CONSTANTES DE HUD Y VISUALES
        public const int HUD_MARGIN = 10; // Margen para los elementos del HUD (corazones)
        public const int SHIP_WIDTH = 50; // Ancho base de la nave (usado en ShipFactory)
        public const int SHIP_HEIGHT = 50; // Alto base de la nave
    }
}
