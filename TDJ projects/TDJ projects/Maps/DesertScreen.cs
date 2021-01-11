/*
author: Rui Olvieira
email: pedro.92@icloud.com
description: Desert Level
*/
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
    public class DesertScreen : GameScreen 
    {
        //Fetch Map resources (textures)
        public Map map = new Map("Desert/");
        //Background Texture for map
        Texture2D backgroundTexture;
        //Portal to the next Level
        Rectangle nextlevel;
        //Texture for portal
        Texture2D nextLevel;
        //Character controlls
        Texture2D Controlls;
        //Player
        Player player;
        //Camera
        Camera camera;
        //Enemies
        List<EnemyFlying> fly = new List<EnemyFlying>();
        List<EnemyUnderground> under = new List<EnemyUnderground>();
        //Number of enemies
        int maxSpritefly = 6;
        int maxUnder = 5;
        //Word Font
        SpriteFont font;

        public override void Initialize()
        {
            base.Initialize();
            
        }

        //Load and spawn resources
        public override void LoadContent(ContentManager Content, InputManager inputManager, GraphicsDeviceManager graphics)
        {
            //Spawn Player
            player = new Player(new Texture2D(graphics.GraphicsDevice, 70, 70), new Rectangle(100, 500, 70, 70), new Vector2(0, 3), new Texture2D(graphics.GraphicsDevice, 5, 5));

            //Spawn Enemies
            for (int i = 0; i < maxSpritefly; i++)
                fly.Add(new EnemyFlying(new Texture2D(graphics.GraphicsDevice, 70, 70), new Rectangle(1000, 100, 70, 70), new Vector2(1, 0), new Texture2D(graphics.GraphicsDevice, 20, 20), 200));

            for (int j = 0; j < maxUnder; j++)
                under.Add(new EnemyUnderground(new Texture2D(graphics.GraphicsDevice, 70, 70), new Rectangle(250, 600, 70, 70), new Vector2(0, 3), 150, 200));

            //Load Portal position
            nextlevel = new Rectangle(4000, 600, 100, 100);
            //Load Camera
            camera = new Camera(graphics.GraphicsDevice.Viewport);

            //Set enemies position
            fly[1].DeadRectangle = new Rectangle(350, 50, 70, 70);
            fly[2].DeadRectangle = new Rectangle(1700, 100, 70, 70);
            fly[3].DeadRectangle = new Rectangle(2500, 300, 70, 70);
            fly[4].DeadRectangle = new Rectangle(3704, 200, 70, 70);

            under[1].DeadRectangle = new Rectangle(1500, 675, 70, 70);
            under[2].DeadRectangle = new Rectangle(2300, 675, 70, 70);
            under[3].DeadRectangle = new Rectangle(3900, 675, 70, 70);
            under[4].DeadRectangle = new Rectangle(3500, 675, 70, 70);

            //Load Textures
            player.Texture = Content.Load<Texture2D>("spriteTest");
            font = Content.Load<SpriteFont>("Font1");
            Controlls = Content.Load<Texture2D>("Controlls");

            for (int j = 0; j < under.Count; j++)
                under[j].ImageUnder = Content.Load<Texture2D>("cs go");

            for (int i = 0; i < fly.Count; i++)
                fly[i].Image = Content.Load<Texture2D>("birb");

            backgroundTexture = Content.Load<Texture2D>("BG");
            nextLevel = Content.Load<Texture2D>("Brutal");

            Tiles.Content = Content;
            Bullets.bulletTexture = Content.Load<Texture2D>("bullet");

            //Generate Map
            map.Generate(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,22,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,22,22,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,16,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,16,0,4,5,5,13,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,14,15,15,16,0,0,0,0,0,12,9,13,0,0,0,0,0,0,0,0,0,0,0,30,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,26,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,21,1,8,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,19,0,0,1,2,2,2,3,0,0,0,0,0,0,0,0,0,17,0,0,28,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,8,5,6,0,0,0,0,0,0,19,0,0,0,0,0,0,0},
                {0,0,0,0,0,23,23,0,22,4,5,5,5,6,21,0,0,0,0,0,0,0,0,0,0,0,24,24,0,0,0,0,0,0,0,0,0,0,0,0,27,0,0,0,0,4,5,5,5,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {2,2,2,2,2,2,2,2,7,8,5,5,5,10,11,2,2,2,3,0,0,1,2,2,2,2,2,2,2,2,2,3,0,0,2,0,2,0,1,2,2,2,2,2,7,8,5,5,5,10,11,2,2,2,2,2,2,2,2,2,2,2,2,2},
                {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,6,0,0,4,5,5,5,5,5,5,5,5,5,6,0,0,5,0,5,0,4,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
            }, 65);

            base.LoadContent(Content, inputManager, graphics);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
         
        }

        //Game Update
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //Call player movement
            player.Move(keyboardState, gameTime, new Vector2(player.Position.X + 200, player.Position.Y));

            //Update collision with player and enemies
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Coll(tile.Rectangle, map.Width, map.Height);
                camera.Update(player.Position, map.Width, map.Height);
           
                foreach (EnemyUnderground un in under)
                    un.Collide(tile.Rectangle, map.Width, map.Height);
            }

            //Call player update
            player.Update(gameTime);

            //Update 
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
                ScreenManager.Instance.AddScreen(new CavernScreen(), inputManager, 1f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // base.Draw(spriteBatch);
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
