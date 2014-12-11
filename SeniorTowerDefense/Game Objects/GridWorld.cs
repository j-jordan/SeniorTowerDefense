using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SeniorTowerDefense
{
    class GridWorld
    {
        public enum MapEntity
        {
            Obstical,
            Tower,
            Enemy,
            Source,
            Destination,
            Empty
        }

        private Vector2 startRegion;
        private Vector2 endRegion;

        private MapEntity[,]map;

        public MapEntity getMapEntity(int indexX, int indexY)
        {
            return map[indexX,indexY];
        }

        public int getMapHeight()
        {
            return map.GetUpperBound(1);
        }

        public int getMapGirth()
        {
            return map.GetUpperBound(0);
        }

        public Vector2 getMapSource()
        {
            int bound0 = map.GetUpperBound(0);
            int bound1 = map.GetUpperBound(1);
 
            for (int i = 0; i <= bound0; i++)
            {
                for (int j = 0; j <= bound1; j++)
                {
                    if(map[i,j].Equals(MapEntity.Source))
                    {
                        return new Vector2(i,j);
                    }
                }
            }

            return new Vector2(0,0);
        }

        public Vector2 getMapDestination()
        {
            int bound0 = map.GetUpperBound(0);
            int bound1 = map.GetUpperBound(1);

            for (int i = 0; i <= bound0; i++)
            {
                for (int j = 0; j <= bound1; j++)
                {
                    if (map[i, j].Equals(MapEntity.Destination))
                    {
                        return new Vector2(i, j);
                    }
                }
            }

            return new Vector2(0, 0);
        }

        public bool setTowerPosition(int x, int y)
        {
            if (x < map.GetUpperBound(0) && x > 0 && y < map.GetUpperBound(1) && y > 0)
            {
                map[x, y] = MapEntity.Tower;
                return true;
            }
            return false;
        }

        public GridWorld()
        {
            //Our map space
            map = new MapEntity[28,13];

            int bound0 = map.GetUpperBound(0);
            int bound1 = map.GetUpperBound(1);
            //Create an empty map
            for(int i = 0; i <= bound0; i++)
            {
                for(int j = 0; j <= bound1; j++)
                {
                    map[i,j] = MapEntity.Empty;
                }
            }

            //Setup start and end zones
            map[0,6] = MapEntity.Source;
            map[27,6] = MapEntity.Destination;
        }
    }
}
