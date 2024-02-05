using System.Drawing;
using System.Windows.Forms;

public partial class Game : Form
{
    private PictureBox pb;
    private Timer tm;
    private Graphics g;
    private Player player;
    private Room room;
    private Menu menu;
    private TextBox speechTextBox;
    private Color color;

    // private WallDecoration wallDecoration;
    private FloorDecoration[] floorDecorations;    
    private Button speakButton;

    public Game()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {

        WindowState = FormWindowState.Maximized;
        BackColor = Colors.GetRandomColor();
        color = Colors.GetRandomColor();
        
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

        room.Set(new TestDecoration(), 5, 5);
        room.Set(new TestDecoration(), 8, 8);
        room.Set(new Lamp(), 10, 10);
                
        menu = new Menu(pb);

        speechTextBox = new TextBox
        {
            Width = 600, 
            Height = 20, 
            Location = new Point(this.ClientSize.Width / 2 - 300, this.ClientSize.Height - 100), 
            Anchor = AnchorStyles.Bottom
        };

        speakButton = new Button
        {
            Text = "Falar",
            Width = 50, 
            Height = 25, 
            BackColor = Color.FromArgb(200, 0, 0, 0),
            ForeColor = Color.White,
            Location = new Point(speechTextBox.Right + 10, this.ClientSize.Height - 101),
            Anchor = AnchorStyles.Bottom
        };

        tm.Tick += (o, e) =>
        {
            g.Clear(BackColor);

            room.Draw(g);

            player.Draw(g, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, color);
            player.Move();

            menu.Draw(g);

            if (menu.IsInventoryOpen) menu.OpenInventory(g);
            if (menu.IsShopOpen) menu.OpenShop(g);
            if (menu.IsCreatorOpen) menu.OpenCreator(g);
            if (menu.IsOracleOpen) menu.OpenOracle(g);

            pb.Refresh();
        };

        Load += (o, e) =>
        {
            Bitmap bitmap = new(pb.Width, pb.Height);

            g = Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            this.pb.Image = bitmap;

            tm.Start();

            menu.InitializeMenu();

            floorDecorations = FloorDecoration.GetFloorDecorations();
        };

        pb.MouseDown += (o, e) =>
        {
            bool clicked = false;
            bool isMoving = false;
            
            room.Click(e.Location);

            if (!clicked && !isMoving)
                player.StartMove(room.NormalSelection);

            menu.SelectItem(e.Location);
        };

        pb.MouseMove += (o, e) =>
        {               
            room.Move(e.Location);
            
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
                deco.TryMove(e.Location);
        };

        Controls.Add(speechTextBox);
        Controls.Add(speakButton);
        Controls.Add(pb);
    }
}