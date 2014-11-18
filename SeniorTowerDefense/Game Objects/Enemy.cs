using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;


namespace SeniorTowerDefense
{

    public class Enemy
    {
        private Vector2 position;
        private Vector2 destination;

        public Enemy(Vector2 p, Vector2 d)
        {
            position = p;
            destination = d;
        }

        public Vector2 Position() { return position; }

        public Vector2 Destination() { return destination; }

        public void moveTowardsDestination()
        {
            if (position.X < destination.X)
            {
                position.X++;
            }
            else if (position.X > destination.X)
            {
                position.X--;
            }
            if (position.Y < destination.Y)
            {
                position.Y++;
            }
            else if (position.Y > destination.Y)
            {
                position.Y--;
            }
        }
    }
}
