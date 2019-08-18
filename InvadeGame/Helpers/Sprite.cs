using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvadeGame.Helpers
{
    public  class Sprite : Alien
    {
        /// <summary>
        /// 位置偏移速度
        /// </summary>
        public int locationSpeed = 1;
        /// <summary>
        /// 旋转速度
        /// </summary>
        public double rotateSpeed = 1;
        /// <summary>
        /// 下落速度
        /// </summary>
        public int VSpeed = 1;

        /// <summary>
        /// 角度
        /// </summary>
        public string Angle { get; set; }

        public Sprite(Form canvasHost, Point point)
            :base(canvasHost, point)
        {
        }//运行构造函数时  运行基类的构造函数

        public override UserControl CreateAlien()
        {
            return null;
        }
        /// <summary>
        /// 下落
        /// </summary>
        public void Down()
        {
            this.Location = new Point(this.Location.X, this.Location.Y + VSpeed);
            SetObjectLocation();
        }
        /// <summary>
        /// 左右移动
        /// </summary>
        public void Move()
        {
            this.Location = new Point(this.Location.X + locationSpeed, this.Location.Y);
            SetObjectLocation();
        }

        public virtual void Rotate()
        {
            this.Angle += rotateSpeed;
        }
    }
}
