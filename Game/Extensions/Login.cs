using System;
using System.Drawing;
using System.Windows.Forms;

public partial class LoginForm : Form
{
    private TextBox usernameTextBox;
    private TextBox passwordTextBox;
    private Button loginButton;
    private PictureBox pictureBox;

    public LoginForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Image backgroundImage = Image.FromFile("./Images/darkroom.png");
        double imageAspectRatio = (double)backgroundImage.Width / backgroundImage.Height;

        Rectangle screenRectangle = Screen.PrimaryScreen.Bounds;
        int screenHeight = screenRectangle.Height;
        int screenWidth = screenRectangle.Width;
        double screenAspectRatio = (double)screenWidth / screenHeight;

        int formWidth, formHeight;
        if (screenAspectRatio > imageAspectRatio)
        {
            formHeight = screenHeight;
            formWidth = (int)(formHeight * imageAspectRatio);
        }
        else
        {
            formWidth = screenWidth;
            formHeight = (int)(formWidth / imageAspectRatio);
        }

        Size = new Size(formWidth, formHeight);
        StartPosition = FormStartPosition.CenterScreen;
        BackgroundImage = backgroundImage;
        BackgroundImageLayout = ImageLayout.Stretch;

        pictureBox = new PictureBox
        {
            Location = new Point((Width - ClientSize.Width) / 2 + 550, 80),
            Image = Image.FromFile("./Images/Logo/alexandriaBlack.png"),
            SizeMode = PictureBoxSizeMode.AutoSize
        };
        
        usernameTextBox = new TextBox
        {
            Location = new Point((Width - ClientSize.Width) / 2, pictureBox.Bottom + 80),
            Size = new Size(200, 25),
            PlaceholderText = "Username"
        };

        passwordTextBox = new TextBox
        {
            Location = new Point((Width - ClientSize.Width) / 2, usernameTextBox.Bottom + 10),
            Size = new Size(200, 25),
            PasswordChar = '*',
            PlaceholderText = "Password"
        };

        loginButton = new Button
        {
            Location = new Point((Width - ClientSize.Width) / 2 + 850, passwordTextBox.Bottom + 20),
            Size = new Size(200, 80),
            Text = "Entrar",
            BackColor = Colors.GetRandomColor(),
        };

        // Controls.Add(usernameTextBox);
        // Controls.Add(passwordTextBox);
        Controls.Add(loginButton);
        Controls.Add(pictureBox);

        loginButton.Click += (sender, e) =>
        {
            if (IsValidLogin(usernameTextBox.Text, passwordTextBox.Text))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
    }

    private bool IsValidLogin(string username, string password)
    {
        return true;
    }
    
}
