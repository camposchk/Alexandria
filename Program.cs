using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Graphics g = null;

Player player = new Player();

PictureBox pb = new PictureBox()
{
    Dock = DockStyle.Fill
};

Form form = new Form()
{
    BackColor = Color.Black,
    WindowState = FormWindowState.Maximized,
    Controls = { pb }
};

pb.Paint += (o , e) =>
{
    player.Draw(e.Graphics);
};

Room room = new Room(pb);

Application.Run(form);







