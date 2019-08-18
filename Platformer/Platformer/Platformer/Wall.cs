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
    public class Wall : PhysicsSprite
    {

        public Wall(int x, int y) : base(Properties.Resources.floor, x, y)
        {
            setMotionModel(0);
        }

        public override void paint(Graphics g)
        {
            base.paint(g);
        }
    }
}