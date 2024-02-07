using System;
using System.Drawing;

public static class Colors
{
    private static Color[] colors = new Color[]{
        Color.FromArgb(255, 255, 223, 186), // Rosa Pastel
        Color.FromArgb(255, 173, 216, 230), // Azul Pastel
        Color.FromArgb(255, 152, 251, 152), // Verde Pastel
        Color.FromArgb(255, 221, 160, 221), // Roxo Pastel
        Color.FromArgb(255, 255, 255, 224), // Amarelo Pastel
        Color.FromArgb(255, 255, 228, 196), // Laranja Pastel
        Color.FromArgb(255, 230, 230, 250), // Lavanda Pastel
        Color.FromArgb(255, 189, 252, 201), // Menta Pastel
        Color.FromArgb(255, 255, 127, 80), // Coral Pastel
        Color.FromArgb(255, 173, 216, 230), // Turquesa Pastel
        Color.FromArgb(255, 200, 162, 200), // Lilás Pastel
        Color.FromArgb(255, 255, 218, 185), // Pêssego Pastel
        Color.FromArgb(255, 135, 206, 235), // Azul Céu Pastel
        Color.FromArgb(255, 214, 255, 178), // Limão Pastel
        Color.FromArgb(255, 255, 102, 204), // Rosa Claro
        Color.FromArgb(255, 255, 218, 185), // Pêssego Pastel Puff
        Color.FromArgb(255, 204, 204, 255), // Periwinkle Pastel
        Color.FromArgb(255, 255, 160, 122), // Salmão Pastel
        Color.FromArgb(255, 255, 240, 245), // Lavanda Pastel Blush
        Color.FromArgb(255, 152, 251, 152), // Verde Pálido
        Color.FromArgb(255, 224, 255, 255), // Ciano Claro
        Color.FromArgb(255, 250, 250, 210), // Amarelo Ouro Claro
        Color.FromArgb(255, 255, 182, 193), // Rosa Claro
        Color.FromArgb(255, 255, 160, 122), // Salmão Claro
        Color.FromArgb(255, 135, 206, 250), // Azul Céu Claro
        Color.FromArgb(255, 176, 196, 222), // Azul Aço Claro
        Color.FromArgb(255, 255, 255, 224), // Amarelo Claro
        Color.FromArgb(255, 255, 228, 225), // Rosa Névoa
        Color.FromArgb(255, 175, 238, 238), // Turquesa Pálida
        Color.FromArgb(255, 176, 224, 230), // Azul em Pó
        Color.FromArgb(255, 216, 191, 216), // Cardo Pastel
        Color.FromArgb(255, 196, 195, 221), // Cinza Lavanda
        Color.FromArgb(255, 219, 112, 147), // Vermelho Violeta Pálido
        Color.FromArgb(255, 238, 232, 170), // Ouro Pálido
        Color.FromArgb(255, 100, 149, 237), // Azul Centáurea
        Color.FromArgb(255, 127, 255, 212), // Aquamarine Pastel
        Color.FromArgb(255, 222, 184, 135), // BurlyWood Pastel
        Color.FromArgb(255, 95, 158, 160), // CadetBlue Pastel
        Color.FromArgb(255, 220, 20, 60), // Crimson Pastel
        Color.FromArgb(255, 143, 188, 139), // DarkSeaGreen Pastel
        Color.FromArgb(255, 255, 20, 147), // DeepPink Pastel
        Color.FromArgb(255, 211, 211, 211), // Cinza Claro
        Color.FromArgb(255, 186, 85, 211), // Orchid Médio Pastel
        Color.FromArgb(255, 152, 251, 152), // Verde Pálido
        Color.FromArgb(255, 135, 206, 250) // Azul Céu Claro
    };

    public static Color GetRandomColor()
    {
        Random random = new Random();
        return colors[random.Next(colors.Length)];
    }

    public static SolidBrush GetDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.9f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }

    public static Color GetDarkerColor(Color originalColor)
    {
        float factor = 0.8f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        return Color.FromArgb(red, green, blue);
    }
}