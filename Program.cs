using System;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

if (args.Length > 0)
    Code.Value = int.Parse(args[0]);

// LoginForm loginForm = new LoginForm();
// DialogResult loginResult = loginForm.ShowDialog();

// if (loginResult == DialogResult.OK)
// {
    Game game = new();
    Application.Run(game);
// }

public static class Code
{
    public static int Value { get; set; } = Random.Shared.Next();
}