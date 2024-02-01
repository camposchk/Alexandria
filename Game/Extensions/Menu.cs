using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

public class Menu
{
    public FloorDecoration[] FloorDecorations { get; set; }
    public bool IsInventoryOpen { get; private set; } = false;
    public bool IsShopOpen { get; private set; } = false;
    public bool IsCreatorOpen { get; private set; } = false;
    public bool IsOracleOpen { get; private set; } = false;
    private PictureBox pictureBox;
    public bool IsActive;

    public int Height { get; private set; }
    public int Width { get; private set; } = 200;

    public Label Inventario = new();
    public Label Shop = new();
    public Label Creator = new();
    public Label Oraculo = new();

    private Rectangle menu { get; set; }

    private Rectangle[] items = new Rectangle[4];

    // public Image Logo { get; set; } = Image.FromFile("./Images/muse.png");

    private Rectangle rec;
    private Rectangle close;

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
            Color color = Color.FromArgb(opacity, Color.Black);

            // Logo = new Bitmap(Logo, new Size(100, 100));

            using (SolidBrush brush = new(color))
                g.FillRectangle(brush, menu);

            g.DrawRectangle(Pens.Gray, menu);
            
            g.FillRectangle(Brushes.Red, items[0]);
            g.FillRectangle(Brushes.Blue, items[1]);
            g.FillRectangle(Brushes.Yellow, items[2]);
            g.FillRectangle(Brushes.Green, items[3]);

            // g.DrawString(Shop.ToString(), new Font("Mexcellent3D-Regular", 18, FontStyle.Regular), Brushes.Red, new PointF(100, 100));

            // g.DrawImage(Logo, new PointF(pictureBox.ClientSize.Width - 125, 20));
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
    }

    protected virtual void OnItemClick(int index)
    {
        CloseAllMenus();

        switch (index)
        {
            case 0:
                IsInventoryOpen = true;
                break;
            case 1:
                IsShopOpen = true;
                break;
            case 2:
                IsCreatorOpen = true;
                break;
            case 3:
                IsOracleOpen = true;
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
        Color color = Color.FromArgb(200, 0, 0, 0);
        SolidBrush brush = new SolidBrush(color);

        g.FillRectangle(brush, rec);
        g.DrawRectangle(Pens.Gray, rec);
        
        g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.DarkRed)), close);
        g.DrawRectangle(Pens.Gray, close);
            
        for(int i = 0; i < n; i++)
        {
            g.FillRectangle(brush, rec.X + 100 * i, rec.Y - 30, 100, 30);
            g.DrawRectangle(Pens.Gray, rec.X + 100 * i, rec.Y - 30, 100, 30);
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

        foreach (var floorDecoration in FloorDecorations)
        {
            foreach(var image in floorDecoration.Items)
            {
                if (column >= maxColumns)
                {
                    column = 0;
                    x = inventoryRect.X + margin;
                    y += imageSize + spacingBetweenImages;
                }

                float scaleWidth = (float)imageSize / image.Width;
                float scaleHeight = (float)imageSize / image.Height;

                float scale = Math.Min(scaleWidth, scaleHeight);

                int newWidth = (int)(image.Width * scale);
                int newHeight = (int)(image.Height * scale);

                int centerX = x + (imageSize - newWidth) / 2;
                int centerY = y + (imageSize - newHeight) / 2;

                g.DrawImage(image, new Rectangle(centerX, centerY, newWidth, newHeight));

                x += imageSize + spacingBetweenImages;

                column++;
            }
        }
    }

    public void OpenShop(Graphics g)
    {
        MenuLayout(g, 2);
    }

    public void OpenCreator(Graphics g)
    {
        MenuLayout(g, 3);
    }

    public void OpenOracle(Graphics g)
    {
        MenuLayout(g, 1);
    }
}