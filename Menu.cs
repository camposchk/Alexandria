using System;
using System.Drawing;
using System.Windows.Forms;

public class Menu
{
    private PictureBox pictureBox;
    public bool IsActive;
    private TabControl tabControl;

    private TabPage tabPage1;
    private TabPage tabPage2;
    private TabPage tabPage3;

    public int Height { get; private set; } = 600;
    public int Width { get; private set; } = 1000;

    public Menu(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        IsActive = false;

        tabPage1 = new TabPage();
        tabPage2 = new TabPage();
        tabPage3 = new TabPage();

        tabControl = new TabControl()
        {

            Size = new Size(Width, Height),
            Location = new Point((pictureBox.ClientSize.Width - Width) / 2, (pictureBox.ClientSize.Height - Height) / 2),
            TabPages = { tabPage1, tabPage2, tabPage3 }
        };
    }

    public void Toggle()
    {
        IsActive = !IsActive;
    }

public void Draw(Graphics g)
{
    if (IsActive)
    {
        int menuWidth = 200; 
        int opacity = 200;  

        Color color = Color.FromArgb(opacity, Color.DimGray);

        using (SolidBrush brush = new SolidBrush(color))
            g.FillRectangle(brush, new Rectangle(pictureBox.ClientSize.Width - menuWidth, 0, menuWidth, pictureBox.ClientSize.Height));
    }
}
}
