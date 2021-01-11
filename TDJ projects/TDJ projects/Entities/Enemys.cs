using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using TDJ_Project.Maps;

namespace TDJ_Project.Entities
{
    public class EnemyUnderground : Entity
    {
        //Collision collision = new Collision();

        public EnemyUnderground(Texture2D texture, Rectangle newPosition, Vector2 velocity, float newDistance, int hp)
        {
            ImageUnder = texture;
            DeadRectangle = newPosition;
            distance = newDistance;
            EnemyHP = hp;
            DeadVelocity = velocity;

            oldDistance = distance;
        }
       

        public void Update(GameTime gameTime, Player player)
        {
            deadRectangle.X += (int)deadVelocity.X;
            deadRectangle.Y += (int)deadVelocity.Y;

            origin = new Vector2(ImageUnder.Width / 2, ImageUnder.Height / 2);

            if (distance <= 0)
            {
                right = true;
                deadVelocity.X = 1f;
            }
            else if (distance >= oldDistance)
            {
                right = false;
                deadVelocity.X = -1f;
            }

            if (right)
                distance += 1;
            else
                distance -= 1;

            playerDistance = player.Position.X - DeadRectangle.X;

            if (playerDistance >= -300 && playerDistance <= 300)
            {
                if (playerDistance < -1)
                {
                    deadVelocity.X = -1f;
                }
                else if (playerDistance > 1)
                {
                    deadVelocity.X = 1f;
                }
                else if (playerDistance == 0)
                {
                    deadVelocity.X = 0f;
                }
            }

            if (EnemyHP <= 0) die = true;
        }

      

        public void Draw(SpriteBatch spriteBatch)
        {
            if (deadVelocity.X > 0)
                spriteBatch.Draw(ImageUnder, position, DeadRectangle, Color.White, rotation, origin, scale, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(ImageUnder, position, DeadRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
    public class EnemyFlying : Entity
    {
        public List<Bullets> bullets = new List<Bullets>();
        Texture2D perBullet;
        float radius = 400f;
        float shoot = 0;

        public Texture2D PerBullet
        {
            get { return perBullet; }
            set { perBullet = value; }
        }

        public EnemyFlying(Texture2D texture, Rectangle rekt, Vector2 velocity, Texture2D newBulletTexture, int hp)
        {
            Image = texture;
            DeadRectangle = rekt;
            DeadVelocity = velocity;
            perBullet = newBulletTexture;
            EnemyHP = hp;
        }
        
        public void ShootBullets(Vector2 playerPos)
        {
            bullets.Add(new Bullets(new Vector2(deadRectangle.X + deadRectangle.Width /2 ,deadRectangle.Y + deadRectangle.Y / 2), playerPos, 3));
        }
        
        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            MoveEnemy(gameTime);

            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(Vector2.Distance(playerPos, new Vector2(deadRectangle.X + deadRectangle.Width / 2, deadRectangle.Y + deadRectangle.Y / 2)) < radius)
            {
                if (shoot > 1)
                {
                    shoot = 0;
                    ShootBullets(playerPos);

                }
            }
            foreach(Bullets bullet in bullets)
            {
                bullet.Update(gameTime);
            }

            if (EnemyHP <= 0) die = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(DeadDirection.X == -1)
                spriteBatch.Draw(Image, DeadRectangle, null, Color.White,0,new Vector2(0,0),SpriteEffects.FlipHorizontally,0);
            else
                spriteBatch.Draw(Image, DeadRectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);

            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch);
        }
    }
}
