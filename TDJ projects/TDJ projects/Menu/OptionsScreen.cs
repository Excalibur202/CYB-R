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
using TDJ_Project.Screen;

namespace TDJ_Project.Menu
{
    public class OptionsScreen : GameScreen
    {
        List<string> menuItems;
        List<string> animationTypes;
        List<List<Animation>> animation;
        List<Texture2D> menuImages;
        List<Animation> tempAnimation;
        List<string> linkType;
        List<string> linkID;


        Vector2 position;
        FileManager fileManager;
        int axis;
        int itemNumber;
        string align;

        Rectangle source;
        SpriteFont font;

        private void SetMenuItems()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuImages.Count == i)
                    menuImages.Add(ScreenManager.Instance.NullImage);
            }
            for (int i = 0; i < menuImages.Count; i++)
            {
                if (menuItems.Count == i)
                    menuItems.Add("");
            }
        }

        private void SetAnimations()
        {
            Vector2 dimensions = Vector2.Zero;
            Vector2 posNew = Vector2.Zero;

            for (int i = 0; i < menuItems.Count; i++)
            {
              dimensions.X += font.MeasureString(menuItems[i]).X + menuImages[i].Width;
              dimensions.Y += font.MeasureString(menuItems[i]).Y + menuImages[i].Height;
            }
            if (axis == 1)
            {
              posNew.X = (ScreenManager.Instance.Dimensions.X - dimensions.X) / 2;
            }
            else if (axis == 2)
            {
              posNew.Y = (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2;
            }
            else
            {
                posNew = position;
            }

            tempAnimation = new List<Animation>();

            for (int i = 0; i < menuImages.Count; i++)
            {
                dimensions = new Vector2(font.MeasureString(menuItems[i]).X + menuImages[i].Width, font.MeasureString(menuItems[i]).Y + menuImages[i].Height);

                if (axis == 1)
                    posNew.Y = (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2;
                else
                    posNew.X = (ScreenManager.Instance.Dimensions.X - dimensions.X) / 2;

                for (int j = 0; j < animationTypes.Count; j++)
                {
                    switch (animationTypes[j])
                    {
                        case "Fade":
                            tempAnimation.Add(new FadeAnimation());
                            tempAnimation[tempAnimation.Count - 1].LoadContent(content, menuImages[i], menuItems[i], posNew);
                            //tempAnimation[tempAnimation.Count - 1].Font = font;
                            break;
                    }

                }

                if (tempAnimation.Count > 0)
                    animation.Add(tempAnimation);

                tempAnimation = new List<Animation>();

                if (axis == 1)
                {
                    posNew.X += dimensions.X;
                }
                else
                {
                    posNew.Y += dimensions.Y;
                }

            }
        }

        public override void LoadContent(ContentManager Content, InputManager inputManager, GraphicsDeviceManager graphics)
        {
            base.LoadContent(Content, inputManager, graphics);

            menuItems = new List<string>();
            animationTypes = new List<string>();
            menuImages = new List<Texture2D>();
            animation = new List<List<Animation>>();
            position = Vector2.Zero;
            itemNumber = 0;
            fileManager = new FileManager();
            linkID = new List<string>();
            linkType = new List<string>();
            this.inputManager = new InputManager();

            fileManager.LoadContent("Load/Options.txt", attributes, contents);

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Font":
                            font = this.content.Load<SpriteFont>(contents[i][j]);
                            break;
                        case "Item":
                            menuItems.Add(contents[i][j]);
                            break;
                        case "Image":
                            menuImages.Add(this.content.Load<Texture2D>(contents[i][j]));
                            break;
                        case "Axis":
                            axis = int.Parse(contents[i][j]);
                            break;
                        case "Position":
                            string[] temp = contents[i][j].Split(' ');
                            position = new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                            break;
                        case "Source":
                            temp = contents[i][j].Split(' ');
                            source = new Rectangle(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2]), int.Parse(temp[3]));
                            break;
                        case "Animation":
                            animationTypes.Add(contents[i][j]);
                            break;
                        case "Align":
                            align = contents[i][j];
                            break;
                        case "LinkType":
                            linkType.Add(contents[i][j]);
                            break;
                        case "LinkID":
                            linkID.Add(contents[i][j]);
                            break;
                    }
                }
            }

            SetMenuItems();
            SetAnimations();

            //System.Diagnostics.Debug.WriteLine("Atributos: " + attributes.Count);
            //System.Diagnostics.Debug.WriteLine("Conteudos: " + contents.Count);
            //System.Diagnostics.Debug.WriteLine("Menu Items: " + menuItems.Count);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            position = Vector2.Zero;
            animation.Clear();
            menuImages.Clear();
            menuItems.Clear();
            animationTypes.Clear();
            inputManager = null;
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (axis == 1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    itemNumber++;
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    itemNumber--;
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    itemNumber++;
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    itemNumber--;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (linkType[itemNumber] == "Screen")
                {
                    ScreenManager.Instance.AddScreen(new MenuScreen(), inputManager);
                }

                if (linkType[itemNumber] == "Size")
                {

                }

                if(linkType[itemNumber] == "Texture")
                {

                }
            }

            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > menuItems.Count - 1)
                itemNumber = menuItems.Count - 1;

            for (int i = 0; i < animation.Count; i++)
            {
                for (int j = 0; j < animation[i].Count; j++)
                {
                    if (itemNumber == i)
                        animation[i][j].IsActive = true;
                    else
                        animation[i][j].IsActive = false;

                    animation[i][j].Update(gameTime);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < animation.Count; i++)
            {
                for (int j = 0; j < animation[i].Count; j++)
                {
                    animation[i][j].Draw(spriteBatch);
                }
            }

            base.Draw(spriteBatch);
        }
    }
}
