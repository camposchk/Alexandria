using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

public class Menu
{
    public FloorDecoration FloorDecorations { get; set; }
    public FloorDecoration[] FloorDecorationsItens { get; set; }
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

    public Image Logo { get; set; } = Image.FromFile("./Images/muse.png");

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

        FloorDecorations = new FloorDecoration();
        FloorDecorationsItens = FloorDecorations.GetFloorDecorations();
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
            Color color = Color.FromArgb(opacity, Color.DimGray);

            // Logo = new Bitmap(Logo, new Size(100, 100));

            using (SolidBrush brush = new(color))
                g.FillRectangle(brush, menu);
            
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

    public void OpenInventory(Graphics g)
    {
        Rectangle inventoryRect = new Rectangle(100, 100, pictureBox.ClientSize.Width / 2, pictureBox.ClientSize.Height / 2);
        g.DrawRectangle(Pens.Red, inventoryRect);

        int margin = 50;
        int spacingBetweenImages = 10;
        int maxColumns = 5; 
        int imageSize = 50; 

        int x = inventoryRect.X + margin;
        int y = inventoryRect.Y + margin;

        int column = 0; 

        foreach (var floorDecoration in FloorDecorationsItens)
        {
            foreach(var image in floorDecoration.Items)
            {
                if (column >= maxColumns)
                {
                    column = 0;
                    x = inventoryRect.X + margin;
                    y += imageSize + spacingBetweenImages;
                }

                g.DrawImage(image, x, y, imageSize, imageSize);

                x += imageSize + spacingBetweenImages;

                column++;
            }
        }
    }

    public void OpenShop(Graphics g)
    {
        g.DrawRectangle(Pens.Green, 100, 100, pictureBox.ClientSize.Width / 2, pictureBox.ClientSize.Height / 2);
    }

    public void OpenCreator(Graphics g)
    {
        g.DrawRectangle(Pens.Yellow, 100, 100, pictureBox.ClientSize.Width / 2, pictureBox.ClientSize.Height / 2);
    }

    public void OpenOracle(Graphics g)
    {
        g.DrawRectangle(Pens.Blue, 100, 100, pictureBox.ClientSize.Width / 2, pictureBox.ClientSize.Height / 2);
    }
}