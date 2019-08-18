using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer
{
    public class PictureSprite : Sprite
    {
        public Image image;
        public float width;
        public float height;

        public PictureSprite(Image img)
        {
            image = img;
            width = img.Width;
            height = img.Height;
        }

        public PictureSprite(Image img, int x, int y)
        {
            image = img;
            X = x;
            Y = y;
            width = img.Width;
            height = img.Height;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(image, 0, 0,width,height);
        }
    }
}