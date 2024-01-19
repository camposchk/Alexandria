// RoomForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;


public partial class RoomForm : Form
{
    private PictureBox pb;
    private Timer tm;
    private Graphics g;
    private Player player;
    private Room room;

    public RoomForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {

        WindowState = FormWindowState.Maximized;
        BackColor = Color.Black;
        
        pb = new PictureBox
        {
            Dock = DockStyle.Fill
        };

        tm = new Timer
        {
            Interval = 10
        };

        player = new Player();
        room = new Room(pb);

        tm.Tick += (sender, e) =>
        {
            room.DrawFloor(g);
            room.DrawWalls(g);
            pb.Refresh();
        };

        Load += (sender, e) =>
        {
            Bitmap bitmap = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bitmap);
            tm.Start();
        };

        pb.Paint += (sender, e) =>
        {
            player.Draw(e.Graphics, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, Color.Yellow);
            player.Draw(e.Graphics, 500, -2100, -800, 70, 70, 140, Color.Yellow);
            player.Move();
        };

        pb.MouseClick += (sender, e) =>
        {
            player.StartMove(room.NormalSelection);

            Rectangle buttonBounds = new Rectangle(500, -2100, 70, 70);

            if (buttonBounds.Contains(e.Location))
            { 
                MessageBox.Show("Botão clicado!"); 
            }
        };

        Controls.Add(pb);

    }
}

