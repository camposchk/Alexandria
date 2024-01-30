using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Habbosch;

public partial class Game : Form
{
    private PictureBox pb;
    private Timer tm;
    private Graphics g;
    private Player player;
    private Room room;
    private Menu menu;

    private Label ruby;
    private TextBox speechTextBox;
    // private WallDecoration wallDecoration;
    private FloorDecoration[] floorDecorations;    

    private bool playerIsInFront = false;
    private Button speakButton;

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
            Height = 24, 
            BackColor = Color.White,
            Location = new Point(speechTextBox.Right + 10, this.ClientSize.Height - 100),
            Anchor = AnchorStyles.Bottom
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

        TestDecoration deco = new TestDecoration();
        pb.Paint += (o, e) =>
        {
            if (menu.IsInventoryOpen) menu.OpenInventory(e.Graphics);
            if (menu.IsShopOpen) menu.OpenShop(e.Graphics);
            if (menu.IsCreatorOpen) menu.OpenCreator(e.Graphics);
            if (menu.IsOracleOpen) menu.OpenOracle(e.Graphics);

            if(playerIsInFront)
            {
                foreach (var deco in floorDecorations)
                    deco.Draw(e.Graphics);

                player.Draw(e.Graphics, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, Color.Yellow);
                player.Draw(e.Graphics, 100, -2500, 0, 70, 70, 140, Color.Yellow);
                player.Move();
            }

            if(!playerIsInFront)
            {
                player.Draw(e.Graphics, player.X, player.Y, player.Z, player.Width, player.Height, player.Depth, Color.Yellow);
                player.Draw(e.Graphics, 100, -2500, 0, 70, 70, 140, Color.Yellow);
                player.Move();

                foreach (var deco in floorDecorations)
                    deco.Draw(e.Graphics);
            }

            menu.Draw(e.Graphics);
            
            deco.Draw(e.Graphics);
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
                if (deco.SpinOn)
                    deco.Spin();
            }

            if (!clicked && !isMoving)
                player.StartMove(room.NormalSelection);
                
            if (menu.IsActive)
                menu.SelectItem(e.Location);
        };

        pb.MouseMove += (o, e) =>
        {
            deco.X = e.Location.X;
            deco.Y = e.Location.Y;
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
        Controls.Add(speechTextBox);
        Controls.Add(speakButton);
        Controls.Add(pb);
    }
}