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
    player.Draw(g, player.X, player.Y, player.Z, player.Height, player.Width, player.Depth, Color.Yellow);
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
    player.Draw(e.Graphics, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, Color.Yellow);
    player.Draw(e.Graphics, 1350, -1200, 1150, 70, 70, 140, Color.Yellow);
    player.Move();
};

pb.MouseMove += (o, e) =>
{
    cursor = e.Location;
};

pb.MouseDown += (o, e) =>
{
    player.StartMove(room.NormalSelection);
};

Application.Run(form);







