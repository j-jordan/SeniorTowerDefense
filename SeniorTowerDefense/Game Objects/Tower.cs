using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SeniorTowerDefense
{
    class Tower
    {
        private Vector2 gridPosition;
        private List<bullet> bullets;
        private static readonly TimeSpan reloadTime = TimeSpan.FromSeconds(2);
        private TimeSpan lastShootTime;

        public List<Vector2> getBulletPositions() 
        {
            List<Vector2> bulletPostitions = new List<Vector2>();

            bullets.ForEach(delegate(bullet b)
            {
                bulletPostitions.Add(b.position);
            });

            return bulletPostitions;
        }


        public Tower(Vector2 gP) 
        {
            bullets = new List<bullet>();
            gridPosition = gP;
        }
        struct bullet
        {
            public Vector2 position;
            public Vector2 velocity;
            public Vector2 destination; 
        }

        public void shootBullet(Vector2 destionation, GameTime gameTime)
        {
            if (lastShootTime + reloadTime < gameTime.TotalGameTime)
            {
                bullet b = new bullet();
                b.position = new Vector2(gridPosition.X * 45 + 6, gridPosition.Y * 45 + 110);
                b.velocity = new Vector2(4.2f, 4.2f);
                b.destination = destionation;

                bullets.Add(b);
                lastShootTime = gameTime.TotalGameTime;
            }
        }
        
        public void updateBullets()
        {
            bullets.ForEach(delegate(bullet b)
                {
                    if (b.position.X < b.destination.X)
                    {
                        b.position.X += b.velocity.X;
                    }
                    else if (b.position.X > b.destination.X)
                    {
                        b.position.X -= b.velocity.X;
                    }
                    if (b.position.Y < b.destination.Y)
                    {
                        b.position.Y += b.velocity.Y;
                    }
                    else if (b.position.Y > b.destination.Y)
                    {
                        b.position.Y -= b.velocity.Y;
                    }
                });
        }
    }
}
