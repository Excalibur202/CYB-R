using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TDJ_Project.Maps
{
    public class Map
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        private List<NotCollisionTiles> notCollisionTiles = new List<NotCollisionTiles>();
        private string folder;

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        public List<NotCollisionTiles> NotCollisionTiles
        {
            get { return notCollisionTiles; }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public Map(string folder)
        {
            this.folder = folder;
        }

        public void Generate(int[,] map, int size)
        {
            for(int x = 0; x < map.GetLength(1); x++)
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0 && number <= 16 || number == 22)
                        collisionTiles.Add(new CollisionTiles(folder, number, new Rectangle(x * size, y * size, size, size)));
                    if (number >= 18 && number != 29 && number != 17 && number != 28 && number != 30 && number != 19)
                        notCollisionTiles.Add(new NotCollisionTiles(folder,number, new Rectangle(x * size, y * size, size, size)));
                    if (number == 30 || number == 28 || number == 19)
                        notCollisionTiles.Add(new NotCollisionTiles(folder,number, new Rectangle(x * size, y * size, size * 2, size * 2)));
                    if(number == 17)
                        notCollisionTiles.Add(new NotCollisionTiles(folder, number, new Rectangle(x * size, y * size, size * 3, size * 2)));
                    if(number == 30)
                        notCollisionTiles.Add(new NotCollisionTiles(folder, number, new Rectangle(x * size, y * size, size * 4, size * 4)));
                    if (number == 27)
                        notCollisionTiles.Add(new NotCollisionTiles(folder, number, new Rectangle(x * size, y * size, size * 2, size)));
                        


                    Width = (x + 1) * size;
                    Height = (y + 1) * size;
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(CollisionTiles tile in CollisionTiles)
            {
                tile.Draw(spriteBatch);
            }
            foreach(NotCollisionTiles tile in NotCollisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
