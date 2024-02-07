using System.Drawing;

public class ShirtOutfit : IPlayerOutfit
{
    public void Draw(Graphics g, Player player)
    {
        float shirtHeight = player.Height; 
        float shirtWidth = player.Width; 
        float shirtDepth = player.Depth * 0.4f; 
        float shirtX = player.X;
        float shirtY = player.Y; 
        float shirtZ = player.Z - player.Depth * 0.5f; 

        PointF[][] shirt = (shirtX, shirtY, shirtZ, shirtWidth, shirtHeight, shirtDepth)
            .Parallelepiped()
            .Isometric();

        Brush shirtBrush = new SolidBrush(Color.Blue); 
        foreach (var face in shirt)
        {
            g.FillPolygon(shirtBrush, face);
            shirtBrush = Colors.GetDarkerBrush(shirtBrush); 
        }
    }
}