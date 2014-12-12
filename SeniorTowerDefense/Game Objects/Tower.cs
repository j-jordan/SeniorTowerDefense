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
        private Vector2 towerPos;

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
                b.velocity = new Vector2(30f, 30f);
                towerPos = b.position = new Vector2(gP.X * 45 + 6, gP.Y * 45 + 110);

                bullets.Add(b);
            }

            gridPosition = gP;
        }

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

                //if our bullet hit his target, set his and all other bullets with the same destination to inactive
                if(checkForCollision(bulletToShoot))
                {
                    bulletToShoot.isActive = false;

                    bullets.ForEach(delegate(Bullet b)
                    {
                        if (b.destination == bulletToShoot.destination)
                        {
                            b.isActive = false;
                            b.position = towerPos;                            
                        }
                    });

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
                b.position.X -= b.velocity.X;
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
            if(Math.Abs(bullet.position.X - bullet.destination.X) <= 5 && Math.Abs(bullet.position.Y - bullet.destination.Y) <= 5 || 
                bullet.position.X - bullet.destination.X < 30 && bullet.position.Y - bullet.destination.Y < 30)
            {
                //DIRECT HIT
                return true;
            }
            return false;
        }
    }
}
