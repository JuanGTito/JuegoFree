using JuegoFree.Entities;
using JuegoFree.Scenes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JuegoFree.Core.Utils;
using TimerForms = System.Windows.Forms.Timer;

namespace JuegoFree.Core
{
    internal class GameLoopManager
    {
        /***************DESTRUCTOR DEL MISIL Y LÓGICA DE JUEGO POR TICK********************/
        private static readonly Random random = new Random();
        private const int HORIZONTAL_SPEED = 1; // Velocidad X
        private const int RANDOM_CHANGE_FREQUENCY = 30; // Cambiar la dirección Y cada 30 ticks
        private static int verticalMoveTimer = 0;
        private static int currentYDirection = 0;
        private const string ASTEROIDE_NAME_PREFIX = "ASTEROIDE_";
        private const int ASTEROID_SPAWN_RATE = 100;
        private static int asteroidSpawnTimer = 0;

        private static int rivalBurstCounter = 0;       // Contador de misiles disparados en la ráfaga
        private static int rivalBurstTick = 0;
        private static int rivalCooldown = 0;           
        private const int RIVAL_BURST_MISSILES = 10;   // Total de misiles en la ráfaga
        private const int RIVAL_BURST_INTERVAL = 4;    // Cada 2 ticks disparar un misil (~40ms)
        private const int RIVAL_COOLDOWN_TICKS = 50;   // 1 segundo de espera entre ráfagas

        private static readonly SoundPlayer rivalShootSound = new SoundPlayer(
            @"C:\Users\juang\source\repos\JuanGTito\JuegoFree\JuegoFree\sond\disparo.wav"
        );

        public static void InitializeSounds()
        {
            rivalShootSound.Load(); // Carga el archivo en memoria
        }
        public static void HandleGameTick(
            PictureBox naveRival,
            PictureBox navex,
            PictureBox contiene,
            ref bool flag,
            TimerForms tiempo,
            ref float angulo,
            PictureBox[] playerHearts,
            PictureBox[] rivalHearts,
            Form1 mainForm)
        {

            InputManager.ProcessGameLogic(navex, contiene, tiempo, ref angulo);

            int X = naveRival.Location.X;
            int Y = naveRival.Location.Y;
            int W = naveRival.Width;
            int H = naveRival.Height;
            int PH = 10;
            int X2 = navex.Location.X;
            int Y2 = navex.Location.Y;
            int W2 = navex.Width;
            int H2 = navex.Height;
            int x = naveRival.Location.X;
            int y = naveRival.Location.Y;
            int verticalLimit = contiene.Height / 2;

            const int GAME_W = 600;
            const int GAME_H = 800;

            int offsetX = (contiene.Width - GAME_W) / 2;
            int offsetY = (contiene.Height - GAME_H) / 2;

            SpawnAsteroids(contiene, offsetX, GAME_W);

            // Si el rival está visible
            if (naveRival.Visible)
            {
                if (rivalBurstCounter > 0) // Estamos en medio de una ráfaga
                {
                    rivalBurstTick++;

                    if (rivalBurstTick >= RIVAL_BURST_INTERVAL)
                    {
                        rivalBurstTick = 0;

                        int xRival = naveRival.Location.X + (naveRival.Width / 2);
                        int yRival = naveRival.Location.Y + (naveRival.Height / 2);
                        Console.WriteLine("Disparo de misil rival"); // <-- prueba
                        MissileFactory.CreateMisil(contiene, 180, Color.OrangeRed, "Rival", xRival, yRival);

                        //rivalShootSound.Play();

                        rivalBurstCounter--;

                        if (rivalBurstCounter == 0)
                            rivalCooldown = RIVAL_COOLDOWN_TICKS; // Preparar cooldown
                    }
                }
                else
                {
                    // Si no estamos disparando, contar cooldown
                    if (rivalCooldown > 0)
                    {
                        rivalCooldown--;
                    }
                    else
                    {
                        // Iniciar nueva ráfaga
                        rivalBurstCounter = RIVAL_BURST_MISSILES;
                    }
                }
            }


            int minX_Boundary = offsetX;
            int maxX_Boundary = offsetX + GAME_W - naveRival.Width;

            if (flag == false)
            {
                if (x + HORIZONTAL_SPEED >= maxX_Boundary)
                    flag = true;

                x += HORIZONTAL_SPEED;
            }
            else
            {
                if (x - HORIZONTAL_SPEED <= minX_Boundary)
                    flag = false;

                x -= HORIZONTAL_SPEED;
            }
            x = Math.Max(minX_Boundary, Math.Min(x, maxX_Boundary));



            verticalMoveTimer++;

            if (verticalMoveTimer >= RANDOM_CHANGE_FREQUENCY)
            {
                currentYDirection = random.Next(-1, 2);
                verticalMoveTimer = 0;
            }

            int newY = y + currentYDirection;

            if (newY < offsetY + 10)
            {
                newY = offsetY + 10;
                currentYDirection = 1;
            }
            else if (newY > offsetY + (GAME_H / 2) - naveRival.Height)
            {
                newY = offsetY + (GAME_H / 2) - naveRival.Height;
                currentYDirection = -1;
            }

            y = newY;
            naveRival.Location = new Point(x, y);

            // ELIMINACION DEL MISIL Y DESCONTAR PUNTOS DE IMPACTO
            foreach (Control c in contiene.Controls.OfType<PictureBox>().ToList())
            {
                int X1 = c.Location.X;
                int Y1 = c.Location.Y;
                int W1 = c.Width;
                int H1 = c.Height;
                string nombre = c.Name;

                if (string.IsNullOrEmpty(nombre) && c.Tag != null)
                {
                    nombre = c.Tag.ToString(); // Si Tag es la vida, esto es problemático
                }

                // --- 1. LÓGICA DE MOVIMIENTO (CORREGIDA) ---
                if (nombre == "Misil") c.Top -= 12;
                else if (nombre == "Rival") c.Top += 8;
                else if (nombre != null && nombre.StartsWith(ASTEROIDE_NAME_PREFIX)) c.Top += 4;


                // ACTIVIDAD DE IMPACTO CON LA NAVE RIVAL (Misil del jugador golpea)
                if (X < X1 && X1 + W1 < X + W && Y < Y1 && Y1 + H1 < Y + H && nombre == "Misil")
                {
                    c.Dispose();
                    // Lógica de daño
                    if (naveRival.Tag is int vidaActualRival)
                    {
                        vidaActualRival -= mainForm.CurrentPlayerDamage;
                        naveRival.Tag = vidaActualRival;
                        HeartManager.UpdateHearts(rivalHearts, vidaActualRival);

                        if (vidaActualRival <= 0)
                        {
                            mainForm.IsGameActive = false;
                            WinScene.Show(contiene, mainForm);
                            return;
                        }
                    }
                }

                if (nombre != null && nombre.StartsWith("Asteroide"))
                {
                    // Chequeamos colisiones entre MISIL (Misil del Jugador) y este asteroide (c)
                    var misiles = contiene.Controls.OfType<PictureBox>()
                                          .Where(m => m.Tag?.ToString() == "Misil")
                                          .ToList();

                    foreach (var misil in misiles)
                    {
                        // Coordenadas del Misil
                        int M_X = misil.Location.X;
                        int M_Y = misil.Location.Y;
                        int M_W = misil.Width;
                        int M_H = misil.Height;

                        // Colisión Misil (M) vs Asteroide (c)
                        if (c.Bounds.IntersectsWith(misil.Bounds)) // Usamos Bounds para una colisión Bounding Box rápida
                        {
                            misil.Dispose(); // Destruir el misil

                            // Lógica de daño al asteroide
                            int vidaActualAsteroide = (int)c.Tag;
                            vidaActualAsteroide -= 25; // Daño fijo (ajustar según la potencia de tu misil)
                            c.Tag = vidaActualAsteroide;

                            // Verificar destrucción del asteroide
                            if (vidaActualAsteroide <= 0)
                            {
                                c.Dispose();
                                // Opcional: Crear una explosión o generar asteroides más pequeños aquí
                            }
                            break; // Salir del bucle de misiles después de un impacto
                        }
                    }
                }

                if (!mainForm.IsGameActive)
                    return;

                // Finalización del juego (Victoria del jugador)
                if(naveRival.Tag != null)
{
                    int vidaRival = (int)naveRival.Tag;

                    if (vidaRival <= 0)
                    {
                        mainForm.IsGameActive = false;
                        WinScene.Show(contiene, mainForm);
                        return;
                    }
                }

                // ACTIVIDAD DE IMPACTO CON MI NAVE (Misil del rival golpea)
                if (X2 < X1 + W1 && X1 + W1 < X2 + W2 && Y2 < Y1 + H1 && Y1 + H1 < Y2 + H2 && nombre == "Rival")
                {
                    c.Dispose();
                    // Lógica de daño
                    int daño = (X2 + PH < X1 + W1 && X1 + W1 < X2 + W2 - PH) ? 10 : 1;
                    int vidaActualJugador = (int)navex.Tag;
                    vidaActualJugador -= daño;
                    navex.Tag = vidaActualJugador;
                    HeartManager.UpdateHearts(playerHearts, vidaActualJugador);
                }

                // Finalización del juego (Derrota del jugador)
                if (navex.Tag != null)
                {
                    int vidaJugador = (int)navex.Tag;

                    if (vidaJugador <= 0)
                    {
                        mainForm.IsGameActive = false;
                        GameOverScene.Show(contiene, mainForm);
                        return;
                    }
                }

                if (nombre != null && nombre.StartsWith(ASTEROIDE_NAME_PREFIX))
                {
                    // Chequeamos colisiones entre MISIL (Misil del Jugador) y este asteroide (c)
                    // Buscamos misiles que estén cerca. Usamos c.Parent para evitar recorrer todo el contenedor si no es necesario.
                    var misiles = contiene.Controls.OfType<PictureBox>()
                                          .Where(m => m.Tag?.ToString() == "Misil" && c.Bounds.IntersectsWith(m.Bounds))
                                          .ToList();

                    foreach (var misil in misiles)
                    {
                        misil.Dispose(); // Destruir el misil

                        // Lógica de daño al asteroide: ¡Asumimos que la vida está en c.Tag!
                        if (c.Tag is int vidaActualAsteroide)
                        {
                            vidaActualAsteroide -= 25;
                            c.Tag = vidaActualAsteroide;

                            if (vidaActualAsteroide <= 0)
                            {
                                c.Dispose();
                            }
                        }
                        break;
                    }
                }

                // --- 3. COLISIÓN ASTEROIDE vs JUGADOR ---
                if (nombre != null && nombre.StartsWith(ASTEROIDE_NAME_PREFIX))
                {
                    // Asteroide (c) vs Nave Jugador (navex)
                    if (c.Bounds.IntersectsWith(navex.Bounds))
                    {
                        c.Dispose(); // Destruir asteroide

                        // Lógica de daño al jugador
                        int danoColision = 20;
                        if (navex.Tag is int vidaActualJugador)
                        {
                            vidaActualJugador -= danoColision;
                            navex.Tag = vidaActualJugador;
                            HeartManager.UpdateHearts(playerHearts, vidaActualJugador);

                            if (vidaActualJugador <= 0)
                            {
                                navex.Dispose();
                                // Si se rompe el bucle aquí, el Game Over se ejecuta abajo
                            }
                        }
                    }
                }

                if ((c.Location.Y <= 0 && nombre == "Misil") ||
                (c.Location.Y >= contiene.Height && (nombre == "Rival" || nombre.StartsWith(ASTEROIDE_NAME_PREFIX))))
                {
                    c.Dispose();
                }

                // Lógica de colisión directa entre naves
                const int PADDING = 8;
                bool navesColisionan =
                    // 1. El borde izquierdo del Rival (X) está a la izquierda del borde derecho del Jugador (X2 + W2) MENOS el PADDING
                    X + PADDING < X2 + W2 - PADDING &&
                    // 2. El borde derecho del Rival (X + W) MENOS el PADDING está a la derecha del borde izquierdo del Jugador (X2) MAS el PADDING
                    X + W - PADDING > X2 + PADDING &&
                    // 3. El borde superior del Rival (Y) MAS el PADDING está arriba del borde inferior del Jugador (Y2 + H2) MENOS el PADDING
                    Y + PADDING < Y2 + H2 - PADDING &&
                    // 4. El borde inferior del Rival (Y + H) MENOS el PADDING está abajo del borde superior del Jugador (Y2) MAS el PADDING
                    Y + H - PADDING > Y2 + PADDING;
                if (navesColisionan)
                {
                    mainForm.IsGameActive = false;
                    GameOverScene.Show(contiene, mainForm);
                    return;
                }
            }
        }
        // Dentro de la clase GameLoopManager
        private static void SpawnAsteroids(PictureBox contiene, int offsetX, int GAME_W)
        {
            const int GAME_H = 800;
            asteroidSpawnTimer++;

            if (asteroidSpawnTimer >= ASTEROID_SPAWN_RATE)
            {
                asteroidSpawnTimer = 0;

                int tipoAsteroide = random.Next(1, 4);
                int escala = random.Next(1, 3);

                PictureBox nuevoAsteroide = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Name = $"{ASTEROIDE_NAME_PREFIX}{tipoAsteroide}"
                };

                Color colorAsteroide;
                switch (random.Next(1, 4))
                {
                    case 1: colorAsteroide = Color.FromArgb(100, 100, 100); break;
                    case 2: colorAsteroide = Color.FromArgb(120, 120, 120); break;
                    default: colorAsteroide = Color.FromArgb(150, 100, 50); break;
                }

                AsteroidFactory.CreateAsteroid(nuevoAsteroide, tipoAsteroide, colorAsteroide, escala);

                int maxSpawnX = offsetX + GAME_W - nuevoAsteroide.Width;
                int spawnX = random.Next(offsetX, maxSpawnX);

                int offsetY = (contiene.Height - GAME_H) / 2;
                int spawnY = offsetY - nuevoAsteroide.Height;

                nuevoAsteroide.Location = new Point(spawnX, spawnY);

                contiene.Controls.Add(nuevoAsteroide);
                nuevoAsteroide.BringToFront();
            }
        }
    }
}