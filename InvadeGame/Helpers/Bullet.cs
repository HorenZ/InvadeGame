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
    public  class Bullet:Alien
    {
        private int Speed=5;

        public Bullet(Form canvasHost,Point point)
            : base(canvasHost, point)
        {
            Location = point;
            size = new Size(18,40);
        }

        public override UserControl CreateAlien()
        {
            return new ucBullet();
        }

        public void GoUp()
        {
            if (this.Location.Y < 0)
            {
                IsAlive = false;
                this.alien.Visible = false;
                return;
            }
            if (this.Location.Y <= 0)
            {
                this.IsAlive = false;
            }
            alien.Location = new Point(this.Location.X, this.Location.Y - Speed);  //子弹上移
            Location = new Point(this.Location.X, this.Location.Y - Speed);  //更新飞机位置
            
        }

       
    }
}
