using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testgame
{
    class Character
    {
        Texture2D texture;

        Vector2 position;
        Vector2 velocity;

        bool hasJumped;


        public Character(Texture2D newTexture, Vector2 newPos)  //Vector2 newPos
        {
            texture = newTexture;
            //position = newPos;
            hasJumped = true;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 3f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -3f;
            }
            else
            {
                velocity.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;    //might want to remove i since it doesn't do anything???
            }

            if (position.Y + texture.Height >= 450)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
        }

        public void Draw(SpriteBatch spB)
        {
            spB.Draw(texture, position, Color.White);
        }
    }
}
