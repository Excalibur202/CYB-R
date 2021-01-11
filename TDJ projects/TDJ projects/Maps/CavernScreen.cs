using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using TDJ_Project.Screen;
using TDJ_Project.Entities;
using TDJ_Project.Menu;

namespace TDJ_Project.Maps
{
    public class CavernScreen : GameScreen
    {
        Map map = new Map("Cavern/");
        Texture2D backgroundTexture;
        Rectangle nextlevel;
        Texture2D nextLevel;
        Texture2D Controlls;
        Player player;
        Camera camera;
        HUD hud;
        List<EnemyFlying> fly = new List<EnemyFlying>();
        List<EnemyUnderground> under = new List<EnemyUnderground>();
        int maxSpritefly = 6;
        int maxUnder = 5;
        SpriteFont font;

        public override void LoadContent(ContentManager Content, InputManager inputManager, GraphicsDeviceManager graphics)
        {
            player = new Player(new Texture2D(graphics.GraphicsDevice, 70, 70), new Rectangle(100, 500, 70, 70), new Vector2(0, 3), new Texture2D(graphics.GraphicsDevice, 5, 5));

            for (int i = 0; i < maxSpritefly; i++)
                fly.Add(new EnemyFlying(new Texture2D(graphics.GraphicsDevice, 70, 70), new Rectangle(1000, 100, 70, 70), new Vector2(1, 1), new Texture2D(graphics.GraphicsDevice, 20, 20), 200));

            for (int j = 0; j < maxUnder; j++)
                under.Add(new EnemyUnderground(new Texture2D(graphics.GraphicsDevice, 70, 70), new Rectangle(250, 600, 70, 70), new Vector2(0, 3), 150, 200));

            nextlevel = new Rectangle(6060, 600, 100, 100);
            camera = new Camera(graphics.GraphicsDevice.Viewport);

            fly[1].DeadRectangle = new Rectangle(500, 100, 70, 70);
            fly[2].DeadRectangle = new Rectangle(1700, 90, 70, 70);
            fly[3].DeadRectangle = new Rectangle(2690, 100, 70, 70);
            fly[4].DeadRectangle = new Rectangle(3704, 200, 70, 70);

            under[1].DeadRectangle = new Rectangle(4000, 675, 70, 70);
            under[2].DeadRectangle = new Rectangle(5300, 675, 70, 70);
            under[3].DeadRectangle = new Rectangle(3900, 675, 70, 70);
            under[4].DeadRectangle = new Rectangle(3800, 675, 70, 70);


            player.Texture = Content.Load<Texture2D>("spriteTest");

            for (int j = 0; j < under.Count; j++)
                under[j].ImageUnder = Content.Load<Texture2D>("cs go");

            for (int i = 0; i < fly.Count; i++)
                fly[i].Image = Content.Load<Texture2D>("birb");

            backgroundTexture = Content.Load<Texture2D>("blueCavern");
            nextLevel = Content.Load<Texture2D>("Brutal");
            font = Content.Load<SpriteFont>("Font1");
            Controlls = Content.Load<Texture2D>("Controlls");

            Tiles.Content = Content;
            Bullets.bulletTexture = Content.Load<Texture2D>("bullet");

            map.Generate(new int[,]{
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,16,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,3,0,14,16,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,5,6,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,5,5,5,5,6,0,0,0,0,0,14,16,0,0,0,14,16,0,0,0,0,0,0,0,0,14,15,15,15,16,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,15,15,15,16,0,0,4,5,5,5,5,6,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,16,0,0,4,5,5,5,5,5,5,5,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,5,6,0,0,0,0,14,16,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,15,16,0,0,0,0,0,12,5,5,5,5,5,5,5,13,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,16,0,1,2,0,0,0,0,0,0,0,0,0,4,5,5,5,5,6,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,0,0,0,0,0,0,0,0,4,5,5,5,5,6,0,14,16,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,14,15,16,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,7,8,6,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,15,16,0,0,0,0,4,5,5,5,0,0,0,0,0,0,0,4,5,5,5,5,6,0,0,0,0,0,0,0,0},
                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,5,0,0,0,0,0,0,4,5,5,5,5,6,0,0,0,0,0,0,0,0},
                 {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0,0,1,2,2,2,2,2,2,2,2,2,2,2,2,3,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,11,2,2,2,2,0,2,0,2,0,2,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                 {4,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,6,0,0,4,5,5,5,5,5,5,5,5,5,5,5,5,6,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,0,5,0,5,0,5,5,5,5,5,5,5,5,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
             }, 65);

            base.LoadContent(Content, inputManager, graphics);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            player.Move(keyboardState, gameTime, new Vector2(player.Position.X + 200, player.Position.Y));

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Coll(tile.Rectangle, map.Width, map.Height);
                camera.Update(player.Position, map.Width, map.Height);
                /*
                foreach (EnemyFlying fl in fly)
                    fl.Collide(tile.Rectangle, map.Width, map.Height);

                foreach (EnemyUnderground un in under)
                    un.Collide(tile.Rectangle, map.Width, map.Height);
                */
            }


            player.Update(gameTime);

            foreach (EnemyFlying flyEnemy in fly)
            {
                flyEnemy.Update(gameTime, player.Position + new Vector2(player.Rectangle.Width / 2, player.Rectangle.Height / 2));

                foreach (Bullets bullet in flyEnemy.bullets)
                {
                    int collision = bullet.Collide(player.Rectangle, map.CollisionTiles);

                    if (collision == 1)
                    {
                        bullet.die = true;
                        player.HP -= 30;
                    }
                    else if (collision == 2)
                        bullet.die = true;
                }
                flyEnemy.bullets.RemoveAll(b => b.die);
            }

            foreach (EnemyFlying fl in fly)
            {
                foreach (Bullets bull in player.bullets)
                {
                    int collision = bull.Collide(fl.DeadRectangle, map.CollisionTiles);

                    if (collision == 1)
                    {
                        bull.die = true;
                        fl.EnemyHP -= 30;
                    }
                    else if (collision == 2)
                        bull.die = true;
                }

                player.bullets.RemoveAll(b => b.die);
            }

            foreach (EnemyUnderground un in under)
            {
                foreach (Bullets bull in player.bullets)
                {
                    int collision = bull.Collide(un.DeadRectangle, map.CollisionTiles);

                    if (collision == 1)
                    {
                        bull.die = true;
                        un.EnemyHP -= 30;
                    }
                    else if (collision == 2)
                        bull.die = true;
                }

                player.bullets.RemoveAll(b => b.die);
            }


            foreach (EnemyUnderground un in under)
                un.Update(gameTime, player);

            for (int i = 0; i < under.Count; i++)
                if (under[i].die)
                    under.RemoveAt(i);

            for (int i = 0; i < fly.Count; i++)
                if (fly[i].die)
                    fly.RemoveAt(i);

            if (player.die)
                ScreenManager.Instance.AddScreen(new MenuScreen(), inputManager, 1f);

            if (player.Position.X >= nextlevel.X && player.Position.Y >= nextlevel.Y || Keyboard.GetState().IsKeyDown(Keys.Z))
                ScreenManager.Instance.AddScreen(new MenuScreen(), inputManager, 1f);

            //System.Diagnostics.Debug.WriteLine("Player X:" + player.Position.X + " " + "PLayer Y:" + player.Position.Y);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            spriteBatch.Draw(backgroundTexture,
                           new Rectangle(0, 0, map.Width, map.Height),
                           null, Color.LightGoldenrodYellow, 0f, Vector2.Zero, SpriteEffects.None, 1);

            map.Draw(spriteBatch);

            player.Draw(spriteBatch);

            foreach (EnemyFlying flyEnemy in fly)
                flyEnemy.Draw(spriteBatch);

            foreach (EnemyUnderground un in under)
                un.Draw(spriteBatch);

            spriteBatch.Draw(nextLevel, nextlevel, Color.White);


            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(font, player.HP.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.Draw(Controlls, new Rectangle(10, 700, 300, 100), Color.White);

        }
    }
}

