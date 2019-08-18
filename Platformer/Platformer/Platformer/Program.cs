using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Platformer
{
    class Program : Engine
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PictureSprite background = new PictureSprite(Properties.Resources.Start,100,100);
            canvas.add(background);
            /*
            for (int i=0;i<13;i++)
            {
                for(int j=0;j<7; j++)
                {
                    PictureSprite background = new PictureSprite(Properties.Resources.wall2, i* 100, j * 100);
                    canvas.add(background);
                }
            }
            Goal goal = new Goal(1100, 500);
            canvas.add(goal);
            canvas.csAdd(character);
            for (int i = 0; i < 13; i++)
            {
                Wall wall = new Wall(i * 100, 0);
                canvas.csAdd(wall);
                wall = new Wall(i * 100, 600);
                canvas.csAdd(wall);
            }
            for (int i = 0; i < 7; i++)
            {
                Wall wall = new Wall(0, i * 100);
                canvas.csAdd(wall);
                wall = new Wall(1200, i * 100);
                canvas.csAdd(wall);
            }
            //canvas.csAdd(Zerg);
            enemyCount = 1;
            */
            Application.Run(new Program());
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */
        }
    }
}
