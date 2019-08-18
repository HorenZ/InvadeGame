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
    class GreenSprite:Sprite
    {
        public GreenSprite(Form canvasHost,Point point)
            : base(canvasHost, point)
        {
            size = new Size(100, 61);
        }

        public override UserControl CreateAlien()
        {
            return new ucGreenSprite();
        }

        public override void Rotate()
        {
            base.Rotate();
        }
    }
}
