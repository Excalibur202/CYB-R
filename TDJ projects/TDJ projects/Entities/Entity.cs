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
    public class Entity
    {

        #region Variaveis 
        protected int health;
        protected Vector2 position;
        //protected Texture2D imageUnder;
        public bool right;
        public bool die = false;
        public float rotation = 0f;
        public float deltaTime = 0f;
        public float newDistance;
        public float oldDistance;
        public float distance;
        protected Rectangle deadRectangle;
        protected Vector2 deadDirection;
        protected Vector2 deadVelocity;
        protected Vector2 origin;
        protected Vector2 scale;
        protected float playerDistance;
        public Player player;
        public int enemyHP;
        #endregion


        #region Propiedades
        public Texture2D Image { get; set; }
        public Texture2D ImageUnder { get; set; }
        public Rectangle DeadRectangle
        {
            set { deadRectangle = value; }
            get { return deadRectangle; }
        }
        public Vector2 DeadDirection
        {
            set { deadDirection = value; }
            get { return deadDirection; }
        }
        public Vector2 DeadVelocity
        {
            set { deadVelocity = value; }
            get { return deadVelocity; }
        }
        public int EnemyHP
        {
            set { enemyHP = value; }
            get { return enemyHP; }
        }
        #endregion


        public void MoveEnemy(GameTime gameTime)
        {
            GameTime game = gameTime;

            deltaTime += (float)game.ElapsedGameTime.TotalSeconds;

            if (deltaTime < 5)
            {
                deadDirection.X = 1;
            }
            if (deltaTime > 5)
            {
                deadDirection.X = -1;
                if (deltaTime > 10)
                {
                    deltaTime = 0f;
                }
            }

            deadRectangle = new Rectangle(deadRectangle.X + (int)(deadDirection.X * deadVelocity.X), deadRectangle.Y + (int)(deadDirection.Y * deadVelocity.Y), deadRectangle.Width, deadRectangle.Height);
        }
        
        public void Collide(Rectangle newTiles, int xOffset, int yOffset)
        {
            
            if (deadRectangle.TouchTopOf(newTiles))
            {
                deadRectangle.Y = newTiles.Y - deadRectangle.Height - 1;
            }

            if (deadRectangle.TouchLeftOf(newTiles))
                deadRectangle.X = newTiles.X - deadRectangle.Width - 3;

            if (deadRectangle.TouchRightOf(newTiles))
                deadRectangle.X = newTiles.X + newTiles.Width + 3;

            if (deadRectangle.TouchBottomOf(newTiles))
                deadVelocity.Y = -3f;
        }
    }

}



