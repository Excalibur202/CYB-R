using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using TDJ_Project.Maps;

namespace TDJ_Project.Entities
{
    public class Bullets
    {
        public static Texture2D bulletTexture;
        public Vector2 bulletPosition;
        public Vector2 direction;
        public float velocity;
        public bool die = false;
        Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>();

        public Bullets(Vector2 posIni, Vector2 posFin, float vel)
        {
            direction = posFin - posIni;
            direction.Normalize();
            velocity = vel;
            bulletPosition = posIni;
        }

        // novo constuctor de bullet com direcção definida por nós em vez de termos a 
        // posiçao inicial e final e calcularmos o vector de direcção. Assim podemos 
        // definir à partida qual é o movimento que queremos para a nossa bala
        // > Vector2(1, 0)
        // < Vector2(-1, 0)
        // ^ Vector2(0,-1)
        public Bullets(Vector2 posIni, string dir, float vel)
        {
            directions.Add("right", new Vector2(1, 0));
            directions.Add("left", new Vector2(-1, 0));
            directions.Add("up", new Vector2(0, -1));

            direction = directions[dir];
            velocity = vel;
            bulletPosition = posIni;
        }

        public void Update(GameTime gameTime)
        {
            bulletPosition += velocity * direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletTexture, bulletPosition, Color.White);
        }
        // 1 -> player colide true; 2 -> tile colide true; 0 -> no colide
        public int Collide(Rectangle rekt, List<CollisionTiles> collisionTiles)
        {
            Rectangle rectangle = new Rectangle((int)bulletPosition.X, (int)bulletPosition.Y, bulletTexture.Width, bulletTexture.Height);

            if (rectangle.Intersects(rekt))
            {
                return 1;  
            }

            foreach(CollisionTiles tile in collisionTiles)
            {
                if(rectangle.Intersects(tile.Rectangle)) return 2;
            }

            return 0;
        }
    }
}
