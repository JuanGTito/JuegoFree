using System.Windows.Forms;
using System.Drawing;

namespace JuegoFree.Scenes
{
    public static class MainMenuScene
    {
        // Recibe el Form1 para poder llamar a LoadGameScene()
        public static void Load(Control container, Form1 mainForm)
        {
            container.Controls.Clear();
            container.BackColor = Color.DimGray;

            Label title = new Label
            {
                Text = "JUEGO DE AVIONES",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            title.Location = new Point((container.Width - title.Width) / 2, 50);
            container.Controls.Add(title);

            int buttonWidth = 200;
            int buttonHeight = 50;
            int startY = container.Height / 2 - (buttonHeight * 2);
            int centerX = (container.Width - buttonWidth) / 2;

            // Botón INICIAR JUEGO
            Button startButton = CreateMenuButton("Iniciar Juego", buttonWidth, buttonHeight, centerX, startY);
            startButton.Click += (sender, e) =>
            {
                // ** LLAMADA AL NUEVO GESTOR DE ESCENAS EN FORM1 **
                mainForm.LoadGameScene();
            };
            container.Controls.Add(startButton);

            // Botón OPCIONES
            Button optionsButton = CreateMenuButton("Opciones", buttonWidth, buttonHeight, centerX, startY + buttonHeight + 10);
            optionsButton.Click += (sender, e) =>
            {
                MessageBox.Show("¡Opciones en construcción!", "Menú");
            };
            container.Controls.Add(optionsButton);

            // Botón SALIR
            Button exitButton = CreateMenuButton("Salir", buttonWidth, buttonHeight, centerX, startY + (buttonHeight + 10) * 2);
            exitButton.Click += (sender, e) =>
            {
                Application.Exit();
            };
            container.Controls.Add(exitButton);
        }

        // Función auxiliar para crear botones uniformes
        private static Button CreateMenuButton(string text, int width, int height, int x, int y)
        {
            return new Button
            {
                Text = text,
                Width = width,
                Height = height,
                Location = new Point(x, y),
                Font = new Font("Arial", 14, FontStyle.Regular),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }
    }
}