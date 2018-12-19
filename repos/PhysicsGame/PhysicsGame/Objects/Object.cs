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
    public class Object
    {
        protected Texture2D texture;

        public Vector2 position;
        protected Vector2 velocity;
        public Vector2 scale;      
        protected Vector2 origin;
        public Vector2 direction;

        protected bool hasJumped;
        public bool isRemoved = false;

        protected float accelerationX;
        float targetX = 128;
        public float rotation;
        protected float rotationVelocity = 3f;

        public Vector2 scaleRect;

        protected float weight;

        public Object(Texture2D newTexture, Vector2 newPos, List<Object> objects, Vector2 scaleBase)
        {
            texture = newTexture;
            position = newPos;
            hasJumped = true;
            scaleRect = scaleBase;
        }

        public virtual void Update(GameTime gameTime, List<Object> collisionObjects)
        {
            position += velocity;
        }

        public virtual void Collision(List<Object> returnObjects) { }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)scaleRect.X, (int)scaleRect.Y);
            }
        }

        public virtual void Draw(SpriteBatch spB)
        {
            spB.Draw(texture, position, null, Color.White, rotation, origin, scale * 1.2f, SpriteEffects.None, 0f);
        }

        //Collision
        protected bool IsTouchingLeft(Object obj)
        {
            return this.rect.Right + this.velocity.X > obj.rect.Left &&
                this.rect.Left < obj.rect.Left &&
                this.rect.Bottom > obj.rect.Top &&
                this.rect.Top < obj.rect.Bottom;
        }

        protected bool IsTouchingRight(Object obj)
        {
            return this.rect.Left + this.velocity.X < obj.rect.Right &&
                this.rect.Right > obj.rect.Right &&
                this.rect.Bottom > obj.rect.Top &&
                this.rect.Top < obj.rect.Bottom;
        }

        protected bool IsTouchingTop(Object obj)
        {
            return this.rect.Bottom + this.velocity.Y > obj.rect.Top &&
                this.rect.Top < obj.rect.Top &&
                this.rect.Right > obj.rect.Left &&
                this.rect.Left < obj.rect.Right;
        }

        protected bool IsTouchingBottom(Object obj)
        {
            return this.rect.Top + this.velocity.Y < obj.rect.Bottom &&
                this.rect.Bottom > obj.rect.Bottom &&
                this.rect.Right > obj.rect.Left &&
                this.rect.Left < obj.rect.Right;
        }
    }
}
