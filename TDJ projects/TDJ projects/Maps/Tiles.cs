using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TDJ_Project.Maps
{
    public class Tiles
    {
        public Texture2D texture2d;

        public Rectangle Rectangle { get; set; }
        public static ContentManager Content { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2d, Rectangle, Color.White);
        }
    }

    public class NotCollisionTiles : Tiles
    {
        public NotCollisionTiles(string folder, int i, Rectangle newRectangle)
        {
            texture2d = Content.Load<Texture2D>(folder + i.ToString());
            Rectangle = newRectangle;
        }
    }

    public class CollisionTiles : Tiles
    {
        public CollisionTiles(string folder, int i, Rectangle newRectangle)
        {
            texture2d = Content.Load<Texture2D>(folder + i.ToString());
            Rectangle = newRectangle;
        }
    }
}
