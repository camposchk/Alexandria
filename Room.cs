using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Room
{
    public float RoomWidth { get; private set; } = 750;
    public float RoomHeight { get; private set; } = 350;
    public float RoomDepth { get; private set; } = 20;
    
    private const int tileWidth = 50;
    private const int tileHeight = 50;
    private PictureBox pictureBox;
  
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
        Color tileColor = Color.Brown;
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
                float x = center.X - RoomHeight / 2 + j * tileWidth + tileWidth / 2;
                float y = center.Y - RoomHeight / 2 + i * tileHeight + tileHeight / 2;

                var ncursor = cursor.Normal();

                if (new RectangleF(x, y, tileWidth, tileHeight).Contains(ncursor))
                    DrawRhombus(g, GetDarkerColor(tileColor), x, y, tileWidth, tileHeight);
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
            Color.Gray
        );

        drawParallelepiped(g,
            center.X + (RoomWidth / 2), 
            center.Y - (RoomHeight / 2), 
            -230,
            20, RoomHeight, 270,
            Color.Gray
        );


        // var center = new PointF(
        //     pictureBox.ClientSize.Width / 2,
        //     pictureBox.ClientSize.Height / 2
        // ).Normal();
        
        // var vertex = GetVertex(center.X, center.Y, RoomWidth, RoomHeight);

        // PointF rightVertex = vertex[1]; 
        // PointF topVertex = vertex[2];
        // PointF leftVertex = vertex[3]; 

        // PointF[] leftWall = new PointF[] 
        // {
        //     leftVertex,
        //     new PointF(leftVertex.X, leftVertex.Y - 250),
        //     new PointF(topVertex.X, topVertex.Y - 250),
        //     topVertex
        // };

        // PointF[] leftWall3D = new PointF[] 
        // {
        //     new PointF(leftVertex.X - 10, leftVertex.Y + 10),
        //     new PointF(leftVertex.X - 10, leftVertex.Y - 260),
        //     new PointF(topVertex.X, topVertex.Y - 265),
        //     new PointF(topVertex.X, topVertex.Y - 250),
        //     new PointF(leftVertex.X, leftVertex.Y - 250),
        //     new PointF(leftVertex.X, leftVertex.Y + 15)
        // };

        // PointF[] rightWall = new PointF[] 
        // {
        //     rightVertex,
        //     new PointF(rightVertex.X, rightVertex.Y - 250),
        //     new PointF(topVertex.X, topVertex.Y - 250),
        //     topVertex
        // };

        // PointF[] rightWall3D = new PointF[] 
        // {
        //     new PointF(rightVertex.X + 10, rightVertex.Y + 10),
        //     new PointF(rightVertex.X + 10, rightVertex.Y - 260),
        //     new PointF(topVertex.X, topVertex.Y - 265),
        //     new PointF(topVertex.X, topVertex.Y - 250),
        //     new PointF(rightVertex.X, rightVertex.Y - 250),
        //     new PointF(rightVertex.X, rightVertex.Y + 15)
        // };

        // Brush LeftWallColor = new SolidBrush(Color.Gray);
        // Brush RightWallColor = new SolidBrush(Color.DarkGray);

        // g.FillPolygon(LeftWallColor, leftWall);
        // g.FillPolygon(GetDarkerBrush(LeftWallColor), leftWall3D);
        // g.FillPolygon(RightWallColor, rightWall);
        // g.FillPolygon(GetDarkerBrush(RightWallColor), rightWall3D);
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

    private PointF[] GetVertex(float x, float y, float width, float height)
    {
        PointF[] points = (x - (width / 2), y - (height / 2), width, height)
            .Rectangle()
            .Isometric();

        return points;
    }

    Brush GetDarkerBrush(Brush originalBrush)
    {
        Color originalColor = ((SolidBrush)originalBrush).Color;

        float factor = 0.8f;

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
