using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Room
{
    public Point IndexSelection { get; private set; }
    public PointF NormalSelection { get; private set; }
    public IDecoration[,] Decorations { get; private set; }

    public TestPlayer[,] Player { get; private set; }
    public float RoomWidth { get; private set; } = 750;
    public float RoomHeight { get; private set; } = 750;
    public float RoomDepth { get; private set; } = 20;
    
    private const int tileWidth = 50;
    private const int tileHeight = 50;
    private PictureBox pictureBox;
    private FloorTexture[] textures = FloorTexture.GetFloorTextures();
    bool IsTaken = false;
    private PointF cursor = PointF.Empty;
  
    public Room(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;

        InitializeRoom();
    }

    public void Draw(Graphics g)
    {
        drawWalls(g);
        drawFloor(g);

        drawDecorations(g);
        drawPlayer(g);
    }

    private void InitializeRoom()
    {
        pictureBox.MouseMove += (o, e) => cursor = e.Location;
        int rows = (int)(RoomHeight / tileHeight);
        int cols = (int)(RoomWidth / tileWidth);
        Decorations = new IDecoration[rows, cols];
        Player = new TestPlayer[rows, cols];
    }
    
    private void drawDecorations(Graphics g)
    {
        int rows = (int)(RoomHeight / tileHeight);
        int cols = (int)(RoomWidth / tileWidth);

        var center = new PointF(
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        ).Normal();

        for (int i = rows - 1; i >= 0; i--)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                var deco = Decorations[i, j];
                if (deco is null)
                    continue;
                
                float x = center.X - RoomWidth / 2 + j * tileWidth;
                float y = center.Y - RoomHeight / 2 + i * tileHeight;
                var screenPoint = new PointF(x, y).Isometric();
                deco.Draw(g, screenPoint.X, screenPoint.Y);
            }
        }
    }

    private void drawPlayer(Graphics g)
    {
        int rows = (int)(RoomHeight / tileHeight);
        int cols = (int)(RoomWidth / tileWidth);

        var center = new PointF(
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        ).Normal();

        for (int i = rows - 1; i >= 0; i--)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                TestPlayer player = Player[i, j];
                if (player is null)
                    continue;
                
                float x = center.X - RoomWidth / 2 + j * tileWidth;
                float y = center.Y - RoomHeight / 2 + i * tileHeight;
                var screenPoint = new PointF(x, y).Isometric(); 
                player.Draw(g, screenPoint.X, screenPoint.Y);
            }
        }
    }

    private void drawFloor(Graphics g)
    {        
        Color tileColor = Color.Pink;
        var center = new PointF(
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        ).Normal();

        drawParallelepiped(g, 
            center.X - (RoomWidth / 2), 
            center.Y - (RoomHeight / 2), 
            RoomDepth, RoomWidth,
            RoomHeight, RoomDepth,
            tileColor
        );

        int rows = (int)(RoomHeight / tileHeight);
        int cols = (int)(RoomWidth / tileWidth);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float x = center.X - RoomWidth / 2 + j * tileWidth;
                float y = center.Y - RoomHeight / 2 + i * tileHeight;

                var ncursor = cursor.Normal(20);

                if (new RectangleF(x, y, tileWidth, tileHeight).Contains(ncursor))
                {
                    if (IsTaken)
                    {
                        drawParallelepiped(g, 
                            x, y, RoomDepth + 2, 
                            tileWidth, tileHeight, 2,
                            getDarkerColor(Color.Red)
                        );
                    }
                    else
                    {
                        drawParallelepiped(g, 
                            x, y, RoomDepth + 2, 
                            tileWidth, tileHeight, 2,
                            getDarkerColor(tileColor)
                        );
                    }

                    this.IndexSelection = new Point(i, j);
                    this.NormalSelection = new PointF(x, y);
                }
            }
        }
    }

    private void drawWalls(Graphics g)
    {        
        var center = new PointF(
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        ).Normal();

        drawParallelepiped(g,
            center.X - (RoomWidth / 2), 
            center.Y + (RoomHeight / 2), 
            -230,
            RoomWidth + 20, 20, 270,
            Color.Purple
        );

        drawParallelepiped(g,
            center.X + (RoomWidth / 2), 
            center.Y - (RoomHeight / 2), 
            -230,
            20, RoomHeight, 270,
            Color.Purple
        );
    }

    private void drawParallelepiped(Graphics g, float x, float y, float z, float width, float height, float depth, Color baseColor)
    {
        PointF[][] chao = (x, y, z, width, height, depth)
            .Parallelepiped()
            .Isometric();

        Brush brush = new SolidBrush(baseColor);
        foreach (var face in chao)
        {
            g.FillPolygon(brush, face);
            brush = getDarkerBrush(brush);
        }
    }
    
    private Brush getDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.9f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }

    private Color getDarkerColor(Color originalColor)
    {
        float factor = 0.8f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        return Color.FromArgb(red, green, blue);
    }
}
