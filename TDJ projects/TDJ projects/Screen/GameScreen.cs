using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using TDJ_Project.Menu;

namespace TDJ_Project.Screen
{
    public class GameScreen
    {
        protected ContentManager content;
        protected GraphicsDeviceManager graphics;
        protected InputManager inputManager;
        protected List<List<string>> attributes;
        protected List<List<string>> contents;

        public virtual void Initialize() { }

        public virtual void LoadContent(ContentManager Content, InputManager inputManager, GraphicsDeviceManager graphics)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            this.graphics = graphics;
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            inputManager = new InputManager();
            this.inputManager = inputManager;
        }
        public virtual void UnloadContent()
        {
            content.Unload();
            inputManager = null;
            attributes.Clear();
            contents.Clear();
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
