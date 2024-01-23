using System.Windows.Forms;

ApplicationConfiguration.Initialize();

// LoginForm loginForm = new LoginForm();
// DialogResult loginResult = loginForm.ShowDialog();

// if (loginResult == DialogResult.OK)
// {
    RoomForm roomForm = new();
    Application.Run(roomForm);
// }