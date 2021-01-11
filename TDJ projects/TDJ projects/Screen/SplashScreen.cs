using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using TDJ_Project.Animations;
using TDJ_Project.Menu;

namespace TDJ_Project.Screen
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<FadeAnimation> fade;
        List<Texture2D> images;

        FileManager fileManager;
        int imageNumber;

        public override void LoadContent(ContentManager Content, InputManager inputManager, GraphicsDeviceManager graphics)
        {
            base.LoadContent(Content, inputManager, graphics);

            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");

            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();

            fileManager.LoadContent("Load/Splash.txt", attributes, contents);

            for(int i = 0; i < attributes.Count; i++)
            {
                for(int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]));
                            fade.Add(new FadeAnimation());
                            break;
                        
                    }
                }
            }

            for(int i = 0; i < fade.Count; i++)
            {
                fade[i].LoadContent(content, images[i], "", new Vector2(ScreenManager.Instance.Dimensions.X/2, ScreenManager.Instance.Dimensions.Y / 2));
                //ImageWidth / 2 * scale - (imageWidth / 2)  ----- para redimensionar a imagem para o tamanho do ecrã
                //ImageHeight / 2 * scale - (imageHeight / 2) ----
                //fade[i].Scale = 4.3f;
                fade[i].IsActive = true;
            }
        }


        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            fade[imageNumber].Update(gameTime);

            if (fade[imageNumber].Alpha == 0.0f && imageNumber < images.Count-1 )
            {
                imageNumber++;
            }

            if (imageNumber == fade.Count - 1 && inputManager.KeyPressed(Keys.Enter, Keys.Space))
            {
                if (fade[imageNumber].Alpha != 1.0f)
                    ScreenManager.Instance.AddScreen(new MenuScreen(), inputManager, fade[imageNumber].Alpha);
                else
                    ScreenManager.Instance.AddScreen(new MenuScreen(), inputManager);
            }
            /*
            for(int i = 0; i < fade.Count; i++)
            {
                if (imageNumber == i)
                    fade[i].IsActive = true;
                else
                    fade[i].IsActive = false;

                fade[i].Update(gameTime);
            }
            */
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "SplashScreen", new Vector2(100, 100), Color.Black);

            fade[imageNumber].Draw(spriteBatch);

        }

    }
}
