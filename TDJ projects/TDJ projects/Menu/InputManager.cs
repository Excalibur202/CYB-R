using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace TDJ_Project.Menu
{
    public class InputManager
    {
        KeyboardState prevKeystate, keystate;

        public KeyboardState PrevKeyState
        {
            get { return prevKeystate; }
            set { prevKeystate = value; }
        }


        public KeyboardState KeyState
        {
            get { return keystate; }
            set { keystate = value; }
        }

        public void Update()
        {
            prevKeystate = keystate;
            keystate = Keyboard.GetState();
        }

        //Normal
        public bool KeyPressed(Keys key)
        {
            if (keystate.IsKeyDown(key) && prevKeystate.IsKeyUp(key))
                return true;

            return false;
        }

        //Multiplas Teclas a serem premidas uma vez
        public bool KeyPressed(params Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if (keystate.IsKeyDown(key) && prevKeystate.IsKeyUp(key))
                    return true;

          
            }

            return false;
        }

        //Uma tecla que mantem ser premida depois de clicado uma vez
        public bool KeyReleased(Keys key)
        {
            if (keystate.IsKeyUp(key) && prevKeystate.IsKeyDown(key))
                return true;

            return false;
        }

        //Multiplas teclas que mantem serem premidas depois de serem clicadas uma vez
        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keystate.IsKeyUp(key) && prevKeystate.IsKeyDown(key))
                    return true;


            }

            return false;
        }

        //Manter tecla premida
        public bool KeyDown(Keys key)
        {
            if (keystate.IsKeyDown(key))
                return true;

            return false;
        }

        //Manter multiplas teclas premidas
        public bool KeyDown(params Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if (keystate.IsKeyDown(key))
                    return true;
            }

            return false;
        }
    }
}
