using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDJ_Project.Maps;
using TDJ_Project.Animations;


namespace TDJ_Project.Entities
{
    public class Player
    {
        // acessors
        public Texture2D Texture { get; set; }
        //public Rectangle Rectangle { get; set; }
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 velocity;
        public int HP = 500;
        public bool ismoving = false, isattacking = false, backwards = false, die = false, hasJumped = false, jumping = false;
        public AnimationPlayer animation;
        const float TIMER = 1f;
        float timer = 0f;
        public List<Bullets> bullets = new List<Bullets>();
        Texture2D perBullet;
        KeyboardState previousState, currentState = Keyboard.GetState();

        public Vector2 Position
        {
            get { return position; }
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }
        public Texture2D PerBullet
        {
            get { return perBullet; }
            set { perBullet = value; }
        }

        public Player(Texture2D texture,Rectangle rec, Vector2 vel, Texture2D newBulletTexture)
        {
            Texture = texture;
            rectangle = rec;
            //position = pos;
            velocity = vel;
            PerBullet = newBulletTexture;
            animation = new AnimationPlayer(this);
        }

        public void Update(GameTime gametime)
        {
            // atualizar com hitboxes e animaçao
            //gravidade -- mudar para colidir com as plataformas
            animation.FX(this.Position, gametime);
            animation.Anim(this.Position, gametime);
            if (HP <= 0) die = true;


            foreach (Bullets bull in bullets)
                bull.Update(gametime);
        }

        /*public void Coll(Rectangle newtiles)
        {
           
        }*/

        public void Coll(Rectangle newtiles, int xOffset, int yOffset)
        {
            //if (this.Rectangle.CollisionCheck(newtiles)==true)
            //{
            //    Rectangle = new Rectangle(Rectangle.X, newtiles.Y - newtiles.Height - 1, Rectangle.Width, Rectangle.Height);
            //    velocity.Y = 0;
            //    hasJumped = false;
            //}

            //if (this.Rectangle.SidesCheck(newtiles) == true)
            //{
            //    this.Rectangle.SidesColl(newtiles);
            //    velocity.X = 0;
            //}

            if (Rectangle.TouchTopOf(newtiles))
            {

                rectangle.Y = newtiles.Y - Rectangle.Height - 1;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newtiles))
                rectangle.X = newtiles.X - Rectangle.Width - 5;

            if (rectangle.TouchRightOf(newtiles))
                rectangle.X = newtiles.X + newtiles.Width + 5;

            if (rectangle.TouchBottomOf(newtiles))
                velocity.Y = 3f;


            if (position.X < 0) rectangle.X = 0;
            if (position.X > xOffset - Rectangle.Width) rectangle.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - Rectangle.Height) die = true;
        }


       /* public float Pause(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds;
            timer -= elapsed;
            if(timer <= 0)
            {
                timer = TIMER;
            }

           return timer;
        }*/


        public void Move(KeyboardState keyboardState, GameTime gameTime, Vector2 enemyPos)
        {
            //inicializar posição
            //position = Vector2.Zero;
            position = new Vector2(Rectangle.X, Rectangle.Y);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentState = Keyboard.GetState();

            //alterar posiçao com input
            // force a timer on space so u can't spam jump
            // create timer class for jumps/attacks
            if (currentState.IsKeyDown(Keys.Space) && hasJumped == false && jumping == false)
            {
                // Pause(gameTime);
                position.Y -= 5f;
                hasJumped = true;
                ismoving = false;
                timer = 0;
                jumping = true;
            }

            if (currentState.IsKeyDown(Keys.A))
            {
                velocity.X = -2f;
                ismoving = true;
                backwards = true;
            }
            else if (currentState.IsKeyDown(Keys.D))
            {
                velocity.X = 2f;
                ismoving = true;
                backwards = false;
            }
            else
            {
                velocity.X = 0f;
                ismoving = false;
            }

            if (jumping)
            {
                velocity.Y = -3f;
                if (timer >= TIMER)
                {
                    jumping = false;
                    timer = 0;
                }
            }
            else
            {
                velocity.Y = 3f;
            }
            //if (currentState.IsKeyUp(Keys.Space))
            //{

            //    //position.Y = 3f;
            //    velocity.Y = 1f;
            //    ismoving = false;
            //}

            if (currentState.IsKeyUp(Keys.J) && previousState.IsKeyDown(Keys.J))
            {
                string direction = backwards ? "left" : "right";
                ShootBullets(direction);
            }

            if (currentState.IsKeyUp(Keys.E) && previousState.IsKeyDown(Keys.E))
            {
                ShootBullets("up");
            }

            rectangle = new Rectangle(
               (int)(Position.X + velocity.X),
               (int)(Position.Y + velocity.Y),
               Rectangle.Width, Rectangle.Height);

            previousState = currentState;
        }

        public void ShootBullets(string direction)
        {
            
            bullets.Add(new Bullets(new Vector2(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2), direction, 5));
           // bullets.Add(new Bullets(new Vector2(deadRectangle.X + deadRectangle.Width / 2, deadRectangle.Y + deadRectangle.Y / 2), playerPos, 3));
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, this.Rectangle, animation.source,  Color.White);

            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch);
        }
    }
}
