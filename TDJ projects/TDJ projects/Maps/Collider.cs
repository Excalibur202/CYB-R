using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJ_Project.Maps
{
    public static class Collider
    {
        //public static bool CollisionCheck(this Rectangle rec1, Rectangle rec2)
        //{
        //    if (rec1.Intersects(rec2)) { return true; }
        //    else return false;
        //}

        //public static bool SidesCheck(this Rectangle rec1, Rectangle rec2)
        //{
        //    if (rec1.Right >= rec2.Left - 1 ||
        //        rec1.Left <= rec2.Right + 1 ||
        //        rec1.Bottom >= rec2.Top - 1 || 
        //        rec1.Top <= rec2.Bottom + 1)
        //        return true;
        //    else return false;
        //}

        //public static int SidesColl(this Rectangle rec1, Rectangle rec2)
        //{
        //    if (rec1.Right >= rec2.Left - 1) return rec1.X = rec2.X - 1 -rec1.Width;
        //    if (rec1.Left <= rec2.Right) return rec1.X = rec2.X + rec2.Width+1;
        //    if (rec1.Bottom >= rec2.Top) return rec1.Y = rec2.Y - 1;
        //    if (rec1.Top <= rec2.Bottom) return rec1.Y = rec2.Y + rec2.Height + 1;
        //    else return 0;


        //}

        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
                    r1.Top >= r2.Bottom - 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 2));
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Left - 3 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                    r1.Left <= r2.Right + 3 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }
    }
}
