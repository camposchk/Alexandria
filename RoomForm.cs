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
    private Menu menu;

    private Label ruby;

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
            Dock = DockStyle.Fill,
        };

        tm = new Timer
        {
            Interval = 10
        };


        player = new Player();
        room = new Room(pb);
        menu = new Menu(pb);

        ruby = new Label()
        {
            Text = player.Ruby.ToString(),
            ForeColor = Color.DarkRed,
            Font = new Font("Mexcellent3D-Regular", 18, FontStyle.Regular),
            Location = new Point(1800,100)
        };

        tm.Tick += (o, e) =>
        {
            pb.Refresh();
        };

        Load += (o, e) =>
        {
            Bitmap bitmap = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bitmap);
            tm.Start();
        };

        pb.Paint += (o, e) =>
        {
            player.Draw(e.Graphics, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, Color.Yellow);
            player.Draw(e.Graphics, 100, -2500, 0, 70, 70, 140, Color.Yellow);
            player.Move();

            menu.Draw(e.Graphics);
        };

        pb.MouseDown += (o, e) =>
        {
            player.StartMove(room.NormalSelection);
            
            // Rectangle buttonBounds = new Rectangle(1755, 800, 200, 200);
            // g.FillRectangle(Brushes.Red, buttonBounds);

            // if (buttonBounds.Contains(e.Location))
            //     menu.Toggle();

        };

        pb.MouseMove += (o, e) =>
        {
            Rectangle screenBounds = Screen.FromControl(pb).Bounds;
            Rectangle triggerBounds = new Rectangle(screenBounds.Right - 50, screenBounds.Top, 50, screenBounds.Height);

            bool isMouseOverTrigger = triggerBounds.Contains(Cursor.Position);

            if (isMouseOverTrigger && !menu.IsActive)
                menu.Toggle();
                
            if (!isMouseOverTrigger && menu.IsActive)
                menu.Toggle();
        };

        Controls.Add(ruby);
        Controls.Add(pb);
    }
}

