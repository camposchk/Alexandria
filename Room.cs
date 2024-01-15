using System;
using System.Drawing;
using System.Windows.Forms;

public class Room
{
    private PictureBox pictureBox;

    public Room(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;
        InitializeRoom();
        InitializePictureBox();
    }

    private void InitializeRoom()
    {
        // Initialize your room data here if needed
    }

    private void InitializePictureBox()
    {
        pictureBox.Paint += Room_Paint;

        pictureBox.Invalidate();
    }

    private void Room_Paint(object sender, PaintEventArgs e)
    {
        DrawFloor(e.Graphics);
        // DrawWalls(e.Graphics);
    }

    private void DrawFloor(Graphics g)
    {
        int outerRhombusWidth = 500;
        int outerRhombusHeight = 250;

        int outerRhombusX = (pictureBox.ClientSize.Width - outerRhombusWidth) / 2;
        int outerRhombusY = (pictureBox.ClientSize.Height - outerRhombusHeight) / 2;

        DrawRhombus(g, Color.DarkRed, outerRhombusX, outerRhombusY, outerRhombusWidth, outerRhombusHeight);

        int numRows = 5;
        int numCols = 5;

        int rhombusWidth = 100;
        int rhombusHeight = 50;

        int gap = 0;

        int innerRhombusGridX = outerRhombusX + (outerRhombusWidth - (numCols * (rhombusWidth + gap) - gap)) / 2;
        int innerRhombusGridY = outerRhombusY + (outerRhombusHeight - (numRows * (rhombusHeight + gap) - gap)) / 2;

        DrawRhombusGrid(g, innerRhombusGridX, innerRhombusGridY, numRows, numCols, rhombusWidth, rhombusHeight, gap);
        DrawSecondaryRhombusGrid(g, innerRhombusGridX, innerRhombusGridY, numRows, numCols, rhombusWidth, rhombusHeight, gap);
    }

    // private void DrawWalls(Graphics g)
    // {
    //     for (int row = 0; row < RoomSize; row++)
    //     {
    //         for (int col = 0; col < RoomSize; col++)
    //         {
    //             // Adjusted X and Y for isometric projection
    //             int x = (col - row) * TileSize / 2;
    //             int y = (col + row) * TileSize / 4;

    //             // Draw walls on the edges of the room
    //             if (row == 0 || col == 0 || row == RoomSize - 1 || col == RoomSize - 1)
    //             {
    //                 // Draw a rectangle to represent an isometric wall tile
    //                 g.FillRectangle(Brushes.Brown, x, y, TileSize, TileSize / 2);
    //             }
    //         }
    //     }
    // }

private static void DrawRhombusGrid(Graphics g, int x, int y, int numRows, int numCols, int rhombusWidth, int rhombusHeight, int gap)
{
    int outerRhombusWidth = 500;
    int outerRhombusHeight = 250;

    for (int i = 0; i < numRows; i++)
    {
        for (int j = 0; j < numCols; j++)
        {
            int rhombusX = x + j * (rhombusWidth + gap);
            int rhombusY = y + i * (rhombusHeight + gap);

            // if (IsInsideOuterRhombus(rhombusX, rhombusY, rhombusWidth, rhombusHeight, outerRhombusWidth, outerRhombusHeight))
                DrawRhombus(g, Color.DarkGoldenrod, rhombusX, rhombusY, rhombusWidth, rhombusHeight); 
        }
    }
}

private static void DrawSecondaryRhombusGrid(Graphics g, int x, int y, int numRows, int numCols, int rhombusWidth, int rhombusHeight, int gap)
{
    int outerRhombusWidth = 500;
    int outerRhombusHeight = 250;

    int verticalOffset = (int)(rhombusHeight * 0.5);
    int horizontalOffset = (int)(rhombusWidth * 0.5);

    for (int i = 0; i < numRows; i++)
    {
        for (int j = 0; j < numCols; j++)
        {
            int rhombusX = x + j * (rhombusWidth + gap);
            int rhombusY = y + i * (rhombusHeight + gap);

            rhombusY += verticalOffset;
            rhombusX += horizontalOffset;

            // if (IsInsideOuterRhombus(rhombusX, rhombusY, rhombusWidth, rhombusHeight, outerRhombusWidth, outerRhombusHeight))
                DrawRhombus(g, Color.DarkGreen, rhombusX, rhombusY, rhombusWidth, rhombusHeight);
        }
    }
}




private static bool IsInsideOuterRhombus(int x, int y, int width, int height, int outerWidth, int outerHeight)
{
    int centerX = x + width / 2;
    int centerY = y + height / 2;

    int outerCenterX = outerWidth / 2;
    int outerCenterY = outerHeight / 2;

    double distance = Math.Sqrt(Math.Pow(centerX - outerCenterX, 2) + Math.Pow(centerY - outerCenterY, 2));

    return distance <= outerWidth / 2;
}

    private static void DrawRhombus(Graphics g, Color color, int x, int y, int width, int height)
    {
        Point[] points =
        {
            new Point(x + width / 2, y),
            new Point(x + width, y + height / 2),
            new Point(x + width / 2, y + height),
            new Point(x, y + height / 2)
        };

        Brush brush = new SolidBrush(color);
        g.FillPolygon(brush, points);

        //Cont
        Pen pen = new Pen(Color.Black);
        g.DrawPolygon(pen, points);

        brush.Dispose();
        pen.Dispose();
    }
}
