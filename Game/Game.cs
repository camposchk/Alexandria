using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class Game : Form
{
    private PictureBox pb;
    private Timer tm;
    private Graphics g;
    private TestPlayer player;
    private Room room;
    private Menu menu;
    private TextBox speechTextBox;
    private Color color;
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

        player = new TestPlayer();
        // player.AddOutfit(new HatOutfit());
        // player.AddOutfit(new ShirtOutfit());

        room = new Room(pb);
        room.Set(player, 3, 3);
        room.Set(new TestDecoration(), 5, 5);
        room.Set(new TestDecoration(), 8, 8);
        room.Set(new Lamp(), 10, 10);
                
        menu = new Menu(pb);

        speechTextBox = new TextBox
        {
            Width = 600, 
            Height = 20, 
            Location = new Point(this.ClientSize.Width / 2 - 300, this.ClientSize.Height - 100), 
            Anchor = AnchorStyles.Bottom,
            MaxLength = 21
        };

        speechTextBox.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(speechTextBox.Text))
                    return;

                player.Speak(speechTextBox.Text);
                speechTextBox.Clear();
            }
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

        speakButton.Click += (o, e) => 
        {
            if (string.IsNullOrEmpty(speechTextBox.Text))
                return;

            player.Speak(speechTextBox.Text);
            speechTextBox.Clear();
        };

        tm.Tick += (o, e) =>
        {
            g.Clear(BackColor);

            room.Draw(g);
            player.MovePlayer();

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
        };

        pb.MouseDown += (o, e) =>
        {
            room.Click(e.Location);
            player.ActivePlayer();
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
        };

        Controls.Add(speechTextBox);
        Controls.Add(speakButton);
        Controls.Add(pb);
    }
}