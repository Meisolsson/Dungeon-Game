﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Randomz
{
    class LockedDoor : Tile
    {
        public Animation animation;
        public Rectangle OwnHitBox { get { return new Rectangle((int)position.X, (int)position.Y-2, 63, 58); } }
        public int test;
        public float rotation;

        public LockedDoor(Vector2 position,ContentManager Content,sbyte type)
            : base(Content.Load<Texture2D>("LockedDoorRight"), position, type)
        {
            this.position = position;
        }
        internal override void Update(GameTime gameTime, Player player)
        {

            if (player.hitBox.Intersects(OwnHitBox))
                test++;
            else test = 0;
            if (test > 20 && player.numberOfKeys > 0)
            {
                isDeleted = true;
                player.numberOfKeys--;
            }
            base.Update(gameTime, player);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            float offsetY = 0;
            SpriteEffects eff = SpriteEffects.None;
            if (type == 0 || type == 3)
                eff = SpriteEffects.FlipHorizontally;
            
            if (type == 1 || type == 3) {
                rotation = -(float)Math.PI / 2;
                offsetY = 50;
            }

            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + offsetY), null, Color.White,rotation, new Vector2(), 1, eff, 1);
        }
    }
}
