using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame
{
    public class Object : ICloneable
    {
        public Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 scale;      
        public Vector2 origin;
        public Vector2 direction;

        public bool hasJumped;
        public bool isRemoved = false;

        public float targetX = 128;
        public float rotation;
        public float rotationVelocity = 3f;
        public float linearVelocity = 4f;

        Rectangle rect;


        public Object(Texture2D newTexture, Vector2 newPos)
        {
            texture = newTexture;
            position = newPos;
            hasJumped = true;
        }

        public virtual void Update(GameTime gameTime)   // List<Object> objects
        {
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spB)
        {
            spB.Draw(texture, position, null, Color.White, rotation, origin, scale * 1.2f, SpriteEffects.None, 0f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
