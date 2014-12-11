using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SeniorTowerDefense
{
    class Tower
    {
        private Vector2 gridPosition;
        private List<Bullet> bullets;
        private static readonly TimeSpan reloadTime = TimeSpan.FromSeconds(.1);
        private TimeSpan lastShootTime;
        Bullet bulletToShoot;

        public List<Vector2> getBulletPositions() 
        {
            List<Vector2> bulletPostitions = new List<Vector2>();

            bullets.ForEach(delegate(Bullet b)
            {
                if(b.isActive)
                    bulletPostitions.Add(b.position);
            });

            return bulletPostitions;
        }

        public Tower(Vector2 gP) 
        {
            bullets = new List<Bullet>(10);
            bulletToShoot = new Bullet();

            for (int i = 0; i < bullets.Capacity; i++ )
            {
                Bullet b = new Bullet();
                b.isActive = false;
                b.velocity = new Vector2(10f, 10f);
                b.position = new Vector2(gP.X * 45 + 6, gP.Y * 45 + 110);

                bullets.Add(b);
            }

            gridPosition = gP;
        }

        /*struct bullet
        {
            public bool isActive;

            public Vector2 position;
            public Vector2 velocity;
            public Vector2 destination; 
        }*/

        public Vector2 shootBullet(Vector2 destination, GameTime gameTime)
        {            
            if (lastShootTime + reloadTime < gameTime.TotalGameTime)
            {
                //use our global bullet if we're still shooting
                if(!bulletToShoot.isActive)
                {
                    //load a bullet from the chamber
                    bullets.ForEach(delegate(Bullet b)
                    {
                        if (!b.isActive)
                        {
                            bulletToShoot = b;
                        }
                    });

                    bulletToShoot.isActive = true;
                    bulletToShoot.destination = destination;
                }

                //Reload
                lastShootTime = gameTime.TotalGameTime;

                //Make sure to update the bullet so it knows where to go
                bulletToShoot.destination = destination;
                updateBullet(bulletToShoot);

                //if our bullet hit his target
                if(checkForCollision(bulletToShoot))
                {
                    bullets.Remove(bulletToShoot);
                    return destination;
                }
            }
            return new Vector2(0, 0);
        }
        
        public void updateBullet(Bullet b)
        {
            //X Values
            if (b.position.X < b.destination.X)
            {
                b.position.X += b.velocity.X;
            }
            else if (b.position.X > b.destination.X)
            {
                b.position.X -= 30;
            }
                    
            //Y Values
            if (b.position.Y < b.destination.Y)
            {
                b.position.Y += b.velocity.Y;
            }
            else if (b.position.Y > b.destination.Y)
            {
                b.position.Y -= b.velocity.Y;
            }
        }

        public bool checkForCollision(Bullet bullet)
        {
            if(bullet.position == bullet.destination)
            {
                //DIRECT HIT
                return true;
            }
            return false;
        }
    }
}
