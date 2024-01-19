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

        WindowState = FormWindowState.Maximized;
        BackColor = Color.Black;
        // BackgroundImage = Image.FromFile("./Image/wallpaper.png");
        // BackgroundImageLayout = ImageLayout.Stretch;

        pictureBox = new PictureBox
        {
            Location = new Point(ClientSize.Width / 2 + 450, 80),
            Image = Image.FromFile("./Images/alexandriaBlack.png"),
            SizeMode = PictureBoxSizeMode.AutoSize
        };
        
        usernameTextBox = new TextBox
        {
            Location = new Point(ClientSize.Width / 2 + 725, pictureBox.Bottom + 80),
            Size = new Size(200, 25),
            PlaceholderText = "Username"
        };

        passwordTextBox = new TextBox
        {
            Location = new Point(ClientSize.Width / 2 + 725, usernameTextBox.Bottom + 10),
            Size = new Size(200, 25),
            PasswordChar = '*',
            PlaceholderText = "Password"
        };

        loginButton = new Button
        {
            Location = new Point(ClientSize.Width / 2 + 725, passwordTextBox.Bottom + 20),
            Size = new Size(200, 30),
            Text = "Login",
            BackColor = Color.White,
        };

        Controls.Add(usernameTextBox);
        Controls.Add(passwordTextBox);
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
