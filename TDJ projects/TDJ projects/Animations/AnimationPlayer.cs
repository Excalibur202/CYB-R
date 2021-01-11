using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDJ_Project.Maps;
using TDJ_Project.Entities;

namespace TDJ_Project.Animations
{
    public class AnimationPlayer
    {
        public Rectangle source;
        public Player player;

        public int maxCols = 6;
        public int maxRows = 3;
        public int currentCol = 0;
        public int currentRow = 0;
        public int frameWidth = 100;
        public int frameHeight = 100;
        private float delta = 0;

        public AnimationPlayer(Player player)
        {
            this.player = player;
        }

        public void Anim(Vector2 pos, GameTime gameTime)
        {
            delta += (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.source = new Rectangle(currentCol * frameWidth, currentRow * frameHeight, frameWidth, frameHeight);

            if (player.ismoving)
            {
                if (player.backwards)
                    currentRow = 2;
                else
                    currentRow = 1;

                if (delta > 0.2)
                { 
                    currentCol++;
                    if (currentCol == maxCols)
                    {
                        currentCol = 0;
                    }
                    delta = 0;
                }
            }
            else
            {
                currentRow = 0;

                if (player.backwards)
                    currentCol = 1;
                else
                    currentCol = 0;                
            }
        }

        
        public void FX(Vector2 pos, GameTime gameTime)
        {
            this.source = new Rectangle(currentCol * frameWidth, currentRow * frameHeight, frameWidth, frameHeight);

            if (player.isattacking == true)
            {
                currentRow = 0;
                currentCol++;
                pos.X++;
            }
        }
    }
}
