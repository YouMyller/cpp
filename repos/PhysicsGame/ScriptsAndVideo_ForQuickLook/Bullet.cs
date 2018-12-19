using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame.Objects
{
    public class Bullet : Object
    {
        float timer;
        public float lifeSpan;
        public int changed = 0;

        public Bullet(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects, Vector2 scaleBase) 
            : base(newTexture, newPos, collisionObjects, scaleBase)
        {
            scale = new Vector2(texture.Width / 20, texture.Height / 20);   
            lifeSpan = 6;
            velocity = new Vector2(14, 14);
            weight = .1f;
            collisionObjects.Add(this);
            scaleBase = new Vector2(140, 110);
            scaleRect = scale;
        }

        public override void Update(GameTime gameTime, List<Object> collisionObjects)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifeSpan)
            {
                isRemoved = true;
            }

            position += direction * velocity;

            position.Y += weight;

            direction.Y += .01f;
        }

        public override void Collision(List<Object> returnObjects)
        {
            //Olin kehitellyt enemmänkin kollisiohihin liittyviä juttuja bulleteille (esim. seinistä kimpoilu), mutta niihin ei loppujen lopuksi
            //riittänyt aika

            foreach (var obj in returnObjects)
            {
                if (obj == this || obj is Bullet || obj is Player)
                {
                    continue;
                }

                if (this.rect.Intersects(obj.rect))
                {
                    timer = 5.99f;
                }
            }
        }
    }
}
