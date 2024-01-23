using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

public class Menu
{
    private PictureBox pictureBox;
    public bool IsActive;

    public int Height { get; private set; }
    public int Width { get; private set; } = 200;

    public Label Inventario = new();
    public Label Mercado = new();
    public Label Leilao = new();
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

            g.DrawString(Mercado.ToString(), new Font("Mexcellent3D-Regular", 18, FontStyle.Regular), Brushes.Red, new PointF(100, 100));

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
        switch (index)
        {
            case 0:
                MessageBox.Show("Clicou no primeiro item!");
                break;
            case 1:
                MessageBox.Show("Clicou no segundo item!");
                break;
            case 2:
                MessageBox.Show("Clicou no terceiro item!");
                break;
            case 3:
                MessageBox.Show("Clicou no quarto item!");
                break;
        }
    }
    
}
