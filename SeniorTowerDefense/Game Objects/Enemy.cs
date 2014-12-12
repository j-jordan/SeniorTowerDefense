using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;


namespace SeniorTowerDefense
{

    public class Enemy
    {
        Random randy = new Random();

        private Vector2 position;
        private Vector2 destination;

        private int velocity;
        public int health;

        public bool isTargetable;

        public Enemy()
        {
            velocity = randy.Next(5) + 1;
            health = 200;

            isTargetable = false;
        }

        public Enemy(Vector2 p, Vector2 d)
        {
            velocity = randy.Next(5) + 1;
            health = 100;

            isTargetable = true;

            position = p;
            destination = d;
        }

        public Vector2 Position() { return position; }

        public Vector2 Destination() { return destination; }

        public bool reachedDestination()
        {
            if (Position().X >= Destination().X)
                return true;
            return false;
        }

        public void moveTowardsDestination()
        {
            if (position.X < destination.X)
            {
                position.X+= velocity;
            }
            else if (position.X > destination.X)
            {
                position.X-= velocity;
            }
            if (position.Y < destination.Y)
            {
                position.Y+= velocity;
            }
            else if (position.Y > destination.Y)
            {
                position.Y-= velocity;
            }
        }

        public void hitEnemy()
        {
            health -= 50;
        }
    }
}
