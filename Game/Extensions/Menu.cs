using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Menu
{
    private PictureBox pictureBox;
    public FloorDecoration[] FloorDecorations { get; set; }
    public Player player { get; set; }
    public bool IsInventoryOpen { get; private set; } = false;
    public bool IsShopOpen { get; private set; } = false;
    public bool IsCreatorOpen { get; private set; } = false;
    public bool IsOracleOpen { get; private set; } = false;
    public bool IsActive;

    public int Height { get; private set; }
    public int Width { get; private set; } = 200;

    public Label Inventario = new();
    public Label Shop = new();
    public Label Creator = new();
    public Label Oraculo = new();

    private Rectangle menu { get; set; }
    private List<RectangleF> shopItems { get; set; } = new();
    private Rectangle[] items = new Rectangle[4];
    private Rectangle rec;
    private Rectangle close;
    private Image ruby = Image.FromFile("./Images/coin2.png");
    private Image chest1 = Image.FromFile("./Images/vault.png");
    private Image chest2 = Image.FromFile("./Images/chest2.png");
    private Image shop1 = Image.FromFile("./Images/shop1.png");
    private Image shop2 = Image.FromFile("./Images/shop2.png");
    private Image ball1 = Image.FromFile("./Images/ball1.png");
    private Image ball2 = Image.FromFile("./Images/ball2.png");

    Color menuColor = Colors.GetRandomColor();

    public Menu(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;
    }

    public void InitializeMenu()
    {
        IsActive = false;
        
        menu = new Rectangle(pictureBox.ClientSize.Width - Width, 0, Width, pictureBox.ClientSize.Height);
        
        items[0] = new(menu.Left + 50, menu.Top + 200, 100, 100);
        items[1] = new(menu.Left + 50, items[0].Bottom + 100, 100, 100);
        items[2] = new(menu.Left + 50, items[1].Bottom + 100, 100, 100);
        items[3] = new(menu.Left + 50, items[2].Bottom + 100, 100, 100);

        FloorDecorations = FloorDecoration.GetFloorDecorations();
        player = new Player();

        this.rec = new Rectangle(
            pictureBox.ClientSize.Width / 4, 
            pictureBox.ClientSize.Height / 4, 
            pictureBox.ClientSize.Width / 2, 
            pictureBox.ClientSize.Height / 2
        );

        this.close = new Rectangle(rec.Right - 30, rec.Y - 30, 30, 30);
    }

    public void Toggle()
    {
        IsActive = !IsActive;
    }

    public void Draw(Graphics g)
    {
        if (IsActive)
        {
            int opacity = 200;  
            Color color = Color.FromArgb(opacity, menuColor);

            // Logo = new Bitmap(Logo, new Size(100, 100));

            using (SolidBrush brush = new(color))
                g.FillRectangle(brush, menu);

            g.DrawRectangle(Pens.Gray, menu);

            g.DrawString($"{player.Ruby}", new Font("Arial", 12), Brushes.DarkRed, menu.X + 20, menu.Y + 10);
            g.DrawImage(ruby, new Rectangle(menu.X + 55, menu.Y + 9, 20, 20));
            
            // if (!IsInventoryOpen)
            //     g.DrawImage(chest1, items[0]);
            // else 
            //     g.DrawImage(chest2, items[0]);

            // if (!IsShopOpen)
            //     g.DrawImage(shop1, items[1]);
            // else
            //     g.DrawImage(shop2, items[1]);

            g.FillEllipse(new SolidBrush(Colors.GetRandomColor()), items[0]);
            g.DrawEllipse(new Pen(Colors.GetRandomColor()), items[0]);
            
            g.FillEllipse(new SolidBrush(Colors.GetRandomColor()), items[1]);
            g.DrawEllipse(new Pen(Colors.GetRandomColor()), items[1]);

            g.FillEllipse(new SolidBrush(Colors.GetRandomColor()), items[2]);
            g.DrawEllipse(new Pen(Colors.GetRandomColor()), items[2]);

            g.FillEllipse(new SolidBrush(Colors.GetRandomColor()), items[3]);
            g.DrawEllipse(new Pen(Colors.GetRandomColor()), items[3]);

            // if(!IsOracleOpen)
            //     g.DrawImage(ball1, items[3]);
            // else
            //     g.DrawImage(ball2, items[3]);
        }
    }

    public void SelectItem(Point mouseLocation)
    {
        if (IsActive)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Contains(mouseLocation))
                {
                    OnItemClick(i);
                    break;
                }
            }
        }

        if (close.Contains(mouseLocation))
            CloseAllMenus();


        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i].Contains(mouseLocation))
            {
                player.Buy(FloorDecorations[i]);
                break;
            }
        }
    }

    protected virtual void OnItemClick(int index)
    {
        CloseAllMenus();

        switch (index)
        {
            case 0:
                IsInventoryOpen = !IsInventoryOpen;
                break;
            case 1:
                IsShopOpen = !IsShopOpen;
                break;
            case 2:
                IsCreatorOpen = !IsCreatorOpen;
                break;
            case 3:
                IsOracleOpen = !IsOracleOpen;
                break;
        }
    }

    private void CloseAllMenus()
    {
        IsInventoryOpen = false;
        IsShopOpen = false;
        IsCreatorOpen = false;
        IsOracleOpen = false;
    }

    public void MenuLayout(Graphics g, int n)
    {
        Color color = Color.FromArgb(200, menuColor.R, menuColor.G, menuColor.B);;
        SolidBrush brush = new SolidBrush(color);

        g.FillRectangle(brush, rec);
        g.DrawRectangle(Pens.Gray, rec);
        
        g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.DarkRed)), close);
        g.DrawRectangle(Pens.Gray, close);
            
        for(int i = 0; i < n; i++)
        {
            g.FillRectangle(brush, rec.X + 100 * i, rec.Y - 30, 100, 30);
            g.DrawRectangle(Pens.Gray, rec.X + 100 * i, rec.Y - 30, 100, 30);

            brush = Colors.GetDarkerBrush(brush);
        }
    }

    public void OpenInventory(Graphics g)
    {
        MenuLayout(g, 4);

        Rectangle inventoryRect = new Rectangle(
            pictureBox.ClientSize.Width / 4, 
            pictureBox.ClientSize.Height / 4, 
            pictureBox.ClientSize.Width / 2 - 100, 
            pictureBox.ClientSize.Height / 2 - 100
        );
        
        int margin = 20;
        int spacingBetweenImages = 10;
        int maxColumns = (inventoryRect.Width - 2 * margin) / (50 + spacingBetweenImages); 
        int imageSize = 50; 

        int x = inventoryRect.X + margin;
        int y = inventoryRect.Y + margin;

        int column = 0;
    }

public void OpenShop(Graphics g)
{
    MenuLayout(g, 4);

    Rectangle inventoryRect = new Rectangle(
        pictureBox.ClientSize.Width / 4, 
        pictureBox.ClientSize.Height / 4, 
        pictureBox.ClientSize.Width / 2 - 100, 
        pictureBox.ClientSize.Height / 2 - 100
    );

    int margin = 20;
    int spacingBetweenImages = 10;
    int maxColumns = (inventoryRect.Width - 2 * margin) / (50 + spacingBetweenImages);
    int imageSize = 50;

    int x = inventoryRect.X + margin;
    int y = inventoryRect.Y + margin;

    int column = 0;

    g.DrawString($"{player.Ruby}", new Font("Arial", 12), Brushes.DarkRed, close.X - 80, close.Y + 10);

    for (int i = 0; i < shopItems.Count; i++)
    {
        if (column >= maxColumns)
        {
            column = 0;
            x = inventoryRect.X + margin;
            y += imageSize + spacingBetweenImages;
        }

        if (testDecoration.Quantity > 0)
        {
            testDecoration.Draw(g, x, y);
            g.DrawString(testDecoration.Quantity.ToString(), new Font("Arial", 12), Brushes.DarkRed, x, y);
        }

        shopItems[i] = new RectangleF(x, y, imageSize, imageSize);

        x += imageSize + spacingBetweenImages;
        column++;
    }
}

    TestDecoration testDecoration = new TestDecoration() {
        SizeFactor = 0.4f
    };

    public void OpenCreator(Graphics g)
    {
        MenuLayout(g, 3);
    }

    public void OpenOracle(Graphics g)
    {
        MenuLayout(g, 1);
    }

}