using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Windows.Forms;

public class Room
{
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
        DrawFloor(e.Graphics);
        DrawWalls(e.Graphics);
    }

    public void DrawFloor(Graphics g)
    {
        int outerRhombusWidth = 500;
        int outerRhombusHeight = 500;
        
        var center = new PointF(
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        ).Normal();

        int innerRhombusWidth = 40;
        int innerRhombusHeight = 40;

        DrawRhombus(g, Color.Black, center.X, center.Y, outerRhombusWidth, outerRhombusHeight);

        int rows = outerRhombusHeight / innerRhombusHeight;
        int cols = outerRhombusWidth / innerRhombusWidth;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float x = center.X - outerRhombusHeight / 2 + j * innerRhombusWidth + innerRhombusWidth / 2;
                float y = center.Y - outerRhombusHeight / 2 + i * innerRhombusHeight + innerRhombusHeight / 2;

                var ncursor = cursor.Normal();

                if (new RectangleF(x, y, innerRhombusWidth, innerRhombusHeight).Contains(ncursor))
                    DrawRhombus(g, Color.Red, x, y, innerRhombusWidth, innerRhombusHeight);
                else 
                    DrawRhombus(g, Color.DarkRed, x, y, innerRhombusWidth, innerRhombusHeight);
            }
        }
    }
    private void DrawRhombus(Graphics g, Color color, float x, float y, float width, float height)
    {
        PointF[] points = (x - (width / 2), y - (height / 2), width, height)
            .Rectangle()
            .Isometric();

        using (SolidBrush brush = new SolidBrush(color))
        {
            g.FillPolygon(brush, points);
        }
    }

    private void DrawWalls(Graphics g)
    {
        int outerRhombusWidth = 500;
        int outerRhombusHeight = 500;

        var center = new PointF(
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        );

        float startXLeft = center.X - outerRhombusWidth / 1.5f;
        float startYLeft = center.Y;
        float endXLeft = center.X - outerRhombusWidth / 1.5f;
        float endYLeft = center.Y - 150;

        // Linha superior
        g.DrawLine(Pens.Blue, startXLeft, startYLeft, endXLeft, endYLeft);

        // Linha superior direita
        float startXUp = center.X;
        float startYUp = center.Y - 175;
        float endXUp = center.X;
        float endYUp = center.Y - 325;
        g.DrawLine(Pens.Blue, startXUp, startYUp, endXUp, endYUp);

        float startXRight = center.X + outerRhombusWidth / 1.5f;
        float startYRight = center.Y;
        float endXRight = center.X + outerRhombusWidth / 1.5f;
        float endYRight = center.Y - 150;
        g.DrawLine(Pens.Blue, startXRight, startYRight, endXRight, endYRight);

        g.DrawLine(Pens.Blue, endXLeft, endYLeft, endXUp, endYUp);
        g.DrawLine(Pens.Blue, endXUp, endYUp, endXRight, endYRight);
    }
}
