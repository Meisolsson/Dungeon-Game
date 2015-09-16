﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonGame
{
    class BlubaTower : Enemy
    {
        Texture2D balltexture;
        Texture2D underLayer;
        int timeBetweenAttack;
        bool isAttacking;
        int attackingTimer;
        float rotation;
        public BlubaTower(ContentManager Content, int seed, Vector2 position)
            : base(position, new Animation(Content, "tower", 100, 1, true), seed, 1.5F, 100, 3)
        {
            balltexture = Content.Load<Texture2D>("blubaball");
            underLayer = Content.Load<Texture2D>("towerUnder");
        }

        public override void Update(GameTime gameTime, Room room)
        {
            base.Update(gameTime, room);
            //changed hitbox because it must fit the enemy with it's rotation
            if (!isdead)
            {
                velocity *= 0.3f;
                if (isHurtTimer > 30)
                {
                    isHurtTimer = 0;
                    isHurt = false;
                }
                timeBetweenAttack++;
                if (timeBetweenAttack > rnd.Next(40, 70))
                {
                    timeBetweenAttack = 0;
                    isAttacking = true;
                }
                Vector2 ballVelocity = new Vector2();
                //calculate the distance between the two objects
                float XDistance = position.X - room.player.Position.X - 40;
                float YDistance = position.Y - room.player.Position.Y - 40;
                //sets the velocity to that with the right angle thanks to this function
                ballVelocity.X -= 5 * (float)Math.Cos(Math.Atan2(YDistance, XDistance));
                ballVelocity.Y -= 5 * (float)Math.Sin(Math.Atan2(YDistance, XDistance));
                rotation = (float)Math.Atan2(ballVelocity.Y, ballVelocity.X);
                if (isAttacking)
                {
                    attackingTimer++;
                    if (attackingTimer > 60)
                    {
                        attackingTimer = 0;
                        isAttacking = false;


                        room.gameObjects.Add(new Projectile(balltexture, position, ballVelocity));
                    }
                }
                walktimer++;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (isHurt)
                color = Color.Red;
            else
                color = Color.White;

            spriteBatch.Draw(underLayer, new Vector2(position.X - underLayer.Width / 2, position.Y - underLayer.Height / 2), Color.White);
            animation.Draw(spriteBatch, position, color, rotation);
        }
    }
}
