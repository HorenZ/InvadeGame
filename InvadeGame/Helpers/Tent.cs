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
    class Tent : Alien
    {
        public int hietedCount = 0;

        public Tent(Form canvasHost, Point point) 
            : base(canvasHost, point)
        {
            size = new Size(122, 69);
        }

        public override UserControl CreateAlien()
        {
            return new ucTent();
        }

        public void Hited()
        {
            hietedCount++;
            if (hietedCount >= 3)
            {
                this.alien.Visible = true;
                IsAlive = false;
            }
        }
    }
}
