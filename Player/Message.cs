using System.Drawing;

public class Message
{
    const int start = 200;
    const int limit = 600;
    PointF position;
    string message;
    Color color;
    int depth;
    int time = 0;

    public bool IsActivated { get; set; } = true;
    public Message(PointF initial, string message, int depth)
    {
        this.position = initial;
        this.message = message;
        this.color = Colors.GetRandomColor();
        this.depth = depth;
    }

    public void Draw(Graphics g)
    {
        var font = new Font("Arial", 12);
        var size = (int)g.MeasureString(message, font).Width + 40;
        Rectangle rect = new Rectangle((int)position.X - size / 2, (int)position.Y - (depth + 40), size, 20);

        float a =
            time < start ? 1f :
            time > limit ? 0f :
            1f - (time - start) / (float)(limit - start);
        if (time > limit)
            IsActivated = false;

        SolidBrush ballonBrush = new SolidBrush(Color.FromArgb((int)(200 * a), 255, 255, 255));
        g.FillRectangle(ballonBrush, rect);

        StringFormat format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        Rectangle textArea = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);

        g.DrawString(this.message, font, new SolidBrush(Color.FromArgb((int)(255 * a), color.R, color.G, color.B)), textArea, format);

        position = new PointF(position.X, position.Y - 0.2f);
        time++;
    }
}