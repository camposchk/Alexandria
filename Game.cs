using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class Game : Form
{
    private PictureBox pb;
    private Timer tm;
    private Graphics g;
    private Player player;
    private Room room;
    private Menu menu;

    private Label ruby;
    // private WallDecoration wallDecoration;
    private FloorDecoration[] floorDecorations;
    private int index = 1;
    

    public Game()
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
        player.AddOutfit(new HatOutfit());
        player.AddOutfit(new ShirtOutfit());

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
            Bitmap bitmap = new(pb.Width, pb.Height);
            g = Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            tm.Start();
            menu.InitializeMenu();
            floorDecorations = FloorDecoration.GetFloorDecorations();
        };

        pb.Paint += (o, e) =>
        {
            menu.Draw(e.Graphics);
            if (menu.IsInventoryOpen) menu.OpenInventory(e.Graphics);
            if (menu.IsShopOpen) menu.OpenShop(e.Graphics);
            if (menu.IsCreatorOpen) menu.OpenCreator(e.Graphics);
            if (menu.IsOracleOpen) menu.OpenOracle(e.Graphics);

            foreach (var deco in floorDecorations)
            {
                deco.Draw(e.Graphics);
                // deco.DrawRec(e.Graphics);

            }

            player.Draw(e.Graphics, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, Color.Yellow);
            player.Draw(e.Graphics, 100, -2500, 0, 70, 70, 140, Color.Yellow);
            player.Move();
        };

        pb.MouseDown += (o, e) =>
        {
            bool clicked = false;
            bool isMoving = false;
            foreach (var deco in floorDecorations)
            {
                deco.OnFloorDecorationClick(e.Location);
                if (deco.Clicked)
                    clicked = true;
                if (deco.MoveOn)
                    isMoving = true;
            }

            if (!clicked && !isMoving)
                player.StartMove(room.NormalSelection);
                
            if (menu.IsActive)
                menu.SelectItem(e.Location);
        };

        pb.MouseMove += (o, e) =>
        {
            Rectangle screenBounds = Screen.FromControl(pb).Bounds;
            Rectangle triggerBounds;

            if (menu.IsActive)
                triggerBounds = new Rectangle(screenBounds.Right - 200, screenBounds.Top, 200, screenBounds.Height);
            else
                triggerBounds = new Rectangle(screenBounds.Right - 50, screenBounds.Top, 50, screenBounds.Height);

            bool isMouseOverTrigger = triggerBounds.Contains(Cursor.Position);

            if (isMouseOverTrigger && !menu.IsActive)
                menu.Toggle();
            if (!isMouseOverTrigger && menu.IsActive)
                menu.Toggle();

            foreach (var deco in floorDecorations)
                deco.Move(e.Location);
        };

        Controls.Add(ruby);
        Controls.Add(pb);
    }
}