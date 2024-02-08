using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Menu
{
    private PictureBox pictureBox;
    public Player player { get; set; }
    public bool IsInventoryOpen { get; private set; } = false;
    public bool IsShopOpen { get; private set; } = false;
    public bool IsCreatorOpen { get; private set; } = false;
    public bool IsOracleOpen { get; private set; } = false;
    public bool IsActive;

    public int Height { get; private set; }
    public int Width { get; private set; } = 200;

    private Rectangle menu { get; set; }
    private List<(RectangleF, IDecoration)> shopItems { get; set; } = new();
    private Rectangle[] items = new Rectangle[4];
    private Rectangle rec;
    private Rectangle close;

    private SolidBrush color1 = new SolidBrush(Colors.GetRandomColor());
    private SolidBrush color2 = new SolidBrush(Colors.GetRandomColor());
    private SolidBrush color3 = new SolidBrush(Colors.GetRandomColor());
    private SolidBrush color4 = new SolidBrush(Colors.GetRandomColor());

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

        this.rec = new Rectangle(
            pictureBox.ClientSize.Width / 4,
            pictureBox.ClientSize.Height / 4,
            pictureBox.ClientSize.Width / 2,
            pictureBox.ClientSize.Height / 2
        );

        this.close = new Rectangle(rec.Right - 30, rec.Y - 30, 30, 30);
        player = new Player();
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

            using (SolidBrush brush = new(color))
                g.FillRectangle(brush, menu);

            g.DrawRectangle(Pens.Gray, menu);

            g.DrawString($"{player.Ruby}", new Font("Arial", 12), Brushes.DarkRed, menu.X + 20, menu.Y + 10);
            g.FillEllipse(new SolidBrush(Colors.GetRandomColor()), new Rectangle(menu.X + 55, menu.Y + 9, 20, 20));

            g.FillEllipse(IsInventoryOpen ? new SolidBrush(Colors.GetRandomColor()) : color1, items[0]);
            g.DrawEllipse(Pens.White, items[0]);

            g.FillEllipse(IsShopOpen ? new SolidBrush(Colors.GetRandomColor()) : color2, items[1]);
            g.DrawEllipse(Pens.White, items[1]);

            g.FillEllipse(IsCreatorOpen ? new SolidBrush(Colors.GetRandomColor()) : color3, items[2]);
            g.DrawEllipse(Pens.White, items[2]);

            g.FillEllipse(IsOracleOpen ? new SolidBrush(Colors.GetRandomColor()) : color4, items[3]);
            g.DrawEllipse(Pens.White, items[3]);
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
            if (shopItems[i].Item1.Contains(mouseLocation))
            {
                player.Buy(shopItems[i].Item2);
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
        Color color = Color.FromArgb(200, menuColor.R, menuColor.G, menuColor.B); ;
        SolidBrush brush = new SolidBrush(color);

        g.FillRectangle(brush, rec);
        g.DrawRectangle(Pens.Gray, rec);

        g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.DarkRed)), close);
        g.DrawRectangle(Pens.Gray, close);

        for (int i = 0; i < n; i++)
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
        int itemWidth = 50;
        int itemHeight = 50;

        int itemsPerRow = (inventoryRect.Width - margin * 2 + spacingBetweenImages) / (itemWidth + spacingBetweenImages);

        List<dynamic> items = new List<dynamic> { puff, lamp, couch };
        int x = inventoryRect.X + margin;
        int y = inventoryRect.Y + margin;

        for (int i = 0; i < player.purchasedDecorations.Count; i++)
        {

            int row = i / itemsPerRow;
            int col = i % itemsPerRow;
            int itemX = x + (col * (itemWidth + spacingBetweenImages)) + 20;
            int itemY = y + (row * (itemHeight + spacingBetweenImages)) + 40;

            player.purchasedDecorations[i].Draw(g, itemX, itemY);
        }
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
        int itemWidth = 50;
        int itemHeight = 50;

        int itemsPerRow = (inventoryRect.Width - margin * 2 + spacingBetweenImages) / (itemWidth + spacingBetweenImages);

        List<dynamic> items = new List<dynamic> { puff, lamp, couch };
        int x = inventoryRect.X + margin;
        int y = inventoryRect.Y + margin;

        for (int i = 0; i < items.Count; i++)
        {

            int row = i / itemsPerRow;
            int col = i % itemsPerRow;
            int itemX = x + (col * (itemWidth + spacingBetweenImages)) + 20;
            int itemY = y + (row * (itemHeight + spacingBetweenImages)) + 40;

            if (items[i].Quantity > 0)
            {
                shopItems.Add((new RectangleF(itemX - 15, itemY - 30, 100, 100), items[i]));
                items[i].Draw(g, itemX, itemY);
            }


        }
        g.DrawString($"{player.Ruby}", new Font("Arial", 12), Brushes.Black, close.X - 80, close.Y + 10);
        g.FillEllipse(new SolidBrush(Colors.GetRandomColor()), new Rectangle(close.X - 45, close.Y + 9, 20, 20));
    }

    Puff puff = new Puff()
    {
        SizeFactor = 0.6f
    };

    Lamp lamp = new Lamp()
    {
        SizeFactor = 0.5f
    };

    Couch couch = new Couch()
    {
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