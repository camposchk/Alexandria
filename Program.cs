using System.Drawing;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Graphics g = null;
PointF cursor = PointF.Empty;

Player player = new Player();

PictureBox pb = new PictureBox()
{
    Dock = DockStyle.Fill
};

Timer tm = new Timer
{
    Interval = 10
};

Room room = new Room(pb);

tm.Tick += delegate
{
    room.DrawFloor(g);
    room.DrawWalls(g);
    player.Draw(g);
    pb.Refresh();
};

Form form = new Form()
{
    BackColor = Color.Black,
    WindowState = FormWindowState.Maximized,
    Controls = { pb }
};

form.Load += delegate
{
    Bitmap bitmap = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bitmap);
    tm.Start();
};

pb.Paint += (o, e) =>
{
    player.Draw(e.Graphics);
};

pb.MouseMove += (o, e) =>
{
    cursor = e.Location;
};

Application.Run(form);







