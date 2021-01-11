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

namespace TDJ_Project.Menu
{
    public class MenuScreen : GameScreen
    {
        MenuManager menu;
        SpriteFont font;

        public override void LoadContent(ContentManager Content, InputManager inputManager, GraphicsDeviceManager graphics)
        {
            base.LoadContent(Content, inputManager, graphics);

            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");

            menu = new MenuManager();
            menu.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            menu.Update(gameTime, inputManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }

    }
}

