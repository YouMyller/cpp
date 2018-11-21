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

        //double vi, t = 0;
        //double g = 9.8f;    


        public Bullet(Texture2D newTexture, Vector2 newPos) : base(newTexture, newPos)
        {
            scale = new Vector2(targetX / (float)texture.Width, targetX / (float)texture.Width);
            velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f + new Vector2(7f, 7f);
            lifeSpan = 2;
        }

        public override void Update(GameTime gameTime)  // List<Object> objects
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifeSpan)
            {
                isRemoved = true;
            }

            position += direction * 20f;

            

            //vi -= 100;
            //position.Y = (float)(vi * t - g * t * t / 2) + GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - texture.Height;
            //t = t - (gameTime.ElapsedGameTime.TotalSeconds / 4);
        }
    }
}
