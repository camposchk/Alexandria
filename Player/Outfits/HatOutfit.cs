using System.Drawing;

public class HatOutfit : IPlayerOutfit
{
    public void Draw(Graphics g, Player player)
    {
        float hatHeight = player.Height * 0.55f; 
        float hatWidth = player.Width * 0.55f; 
        float hatDepth = player.Depth * 0.2f; 
        float hatX = player.X; 
        float hatY = player.Y; 
        float hatZ = player.Z - player.Depth * 1.1f; 

        PointF[][] hat = (hatX, hatY, hatZ, hatWidth, hatHeight, hatDepth)
            .Parallelepiped()
            .Isometric();

        Brush hatBrush = new SolidBrush(Color.Red); 
        foreach (var face in hat)
        {
            g.FillPolygon(hatBrush, face);
            hatBrush = player.GetDarkerBrush(hatBrush); 
        }
    }
}
