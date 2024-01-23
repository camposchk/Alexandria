using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Room
{
    public PointF NormalSelection { get; private set; }
    public float RoomWidth { get; private set; } = 750;
    public float RoomHeight { get; private set; } = 550;
    public float RoomDepth { get; private set; } = 20;
    
    private const int tileWidth = 50;
    private const int tileHeight = 50;
    private PictureBox pictureBox;

    bool IsTaken = true;
  
    private PointF cursor = PointF.Empty;
    public Room(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;

        InitializeRoom();
        InitializePictureBox();
    }

    private void InitializeRoom()
    {
        pictureBox.MouseMove += (o, e) =>
        {
            cursor = e.Location;
        };
    }

    private void InitializePictureBox()
    {
        pictureBox.Paint += Room_Paint;
        pictureBox.Invalidate();
    }

    private void Room_Paint(object sender, PaintEventArgs e)
    {
        DrawWalls(e.Graphics);
        DrawFloor(e.Graphics);
    }

    public void DrawFloor(Graphics g)
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
                    if (!IsTaken)
                    {
                        drawParallelepiped(g, 
                            x, y, RoomDepth + 2, 
                            tileWidth, tileHeight, 2,
                            GetDarkerColor(Color.Red)
                        );
                    }
                    else
                    {
                        drawParallelepiped(g, 
                            x, y, RoomDepth + 2, 
                            tileWidth, tileHeight, 2,
                            GetDarkerColor(tileColor)
                        );
                    }
                    this.NormalSelection = new PointF(x, y);
                }
            }
        }
    }


    public void DrawWalls(Graphics g)
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
            brush = GetDarkerBrush(brush);
        }
    }
    
    private void DrawRhombus(Graphics g, Color color, float x, float y, float width, float height, Color outlineColor = default)
    {
        PointF[] points = (x - (width / 2), y - (height / 2), width, height)
            .Rectangle()
            .Isometric(20);

        using (SolidBrush brush = new SolidBrush(color))
        using (Pen pen = new Pen(outlineColor))
        {
            g.FillPolygon(brush, points);
            g.DrawPolygon(pen, points);
        }
    }

    Brush GetDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.9f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        Color darkerColor = Color.FromArgb(red, green, blue);

        return new SolidBrush(darkerColor);
    }

    private Color GetDarkerColor(Color originalColor)
    {
        float factor = 0.8f;

        int red = (int)(originalColor.R * factor);
        int green = (int)(originalColor.G * factor);
        int blue = (int)(originalColor.B * factor);

        return Color.FromArgb(red, green, blue);
    }
}
