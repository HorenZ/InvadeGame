using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InvadeGame.Controll;

namespace InvadeGame.Helpers
{
    class Ship:Alien
    {
        private int Speed = 10;

        public Ship(Form canvasHost,Point point)
            : base(canvasHost, point)
        {
            Location = point;
            size = new Size(89, 79);
        }

        public override UserControl CreateAlien()
        {
            return new ucShip();
        }

        public void GoUp()
        {
            Location = new Point(Location.X, Location.Y - Speed);
            this.alien.Location = Location;
        }

        public void GoDown()
        {
            Location = new Point(Location.X, Location.Y + Speed);
            this.alien.Location = Location;
        }

        public void GoLeft()
        {
            Location = new Point(Location.X - Speed, Location.Y);
            this.alien.Location = Location;
        }

        public void GoRight()
        {
            Location = new Point(Location.X + Speed, Location.Y);
            this.alien.Location = Location;
        }
    }
}
