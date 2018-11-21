using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame
{
    public class Player : Object
    {

        public Player(Texture2D newTexture, Vector2 newPos) : base(newTexture, newPos)
        {
            scale = new Vector2(targetX / (float)texture.Width, targetX / (float)texture.Width);
        }

        public override void Update(GameTime gameTime)  // List<Object> objects
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
                velocity.Y += 0.15f;
            }

            if (position.Y + scale.Y >= 890)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
        }
    }
}
