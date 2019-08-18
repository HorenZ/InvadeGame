using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InvadeGame.Helpers
{
    /// <summary>
    /// 虚拟基类
    /// </summary>
    public abstract class Alien
    {
        public UserControl alien;
        private bool isAlive;

        public Size size { get; set; }  //大小
        public Point Location { get; set; }   //位置
        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
                //如果当前对象死亡则将当前对象设置为“看不见”
                if (IsAlive == false)
                {
                    alien.Visible = false;
                }
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="canvasHost"></param>
        /// <param name="point"></param>
        public Alien(Form canvasHost,Point point)
        {
            IsAlive = true;
            alien = CreateAlien();
            Location = point;
            SetObjectLocation();
            canvasHost.Controls.Add(alien);  //添加控件到屏幕中
        }

        /// <summary>
        /// 虚方法
        /// </summary>
        /// <returns></returns>
        public abstract UserControl CreateAlien();

        /// <summary>
        /// 刷新位置
        /// </summary>
        protected void SetObjectLocation()
        {
            alien.Location = new Point(this.Location.X, this.Location.Y);
        }

        /// <summary>
        /// 创建一个矩形
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle CreateRect()
        {
            /*Rectangle r = new Rectangle(Location,size);
            return r;*/
            return new Rectangle(Location, size);
        }

        /// <summary>
        /// 判断是否相撞
        /// </summary>
        /// <param name="wraper"></param>
        /// <returns></returns>
        public bool CheckCollision(Alien wraper)
        {
            //获取当前对象的矩形区域
            Rectangle rect = CreateRect();
            //获取参对象的矩形区域
            Rectangle rectCheck = wraper.CreateRect();
            //检查两个矩形是否交叉,如果没有交叉则
            //rect的值是empty
            rect.Intersect(rectCheck);
            return (rect != Rectangle.Empty); //相交返回true——rect不为Empty
        }
    }
}
