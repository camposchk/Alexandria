using System.Drawing;

public class Player 
{
    public int X { get; set; }
    public int Y { get; set; }
    public float Width { get; set; } = 25;
    public float Height { get; set; } = 40;

    public void Draw(Graphics g)
    {
        RectangleF rectBar = new RectangleF(200, 200, this.Width, this.Height);
        
        g.FillRectangle(Brushes.PapayaWhip, rectBar);

        //Contorno
        g.DrawRectangle(Pens.DarkRed, rectBar);
    }
}