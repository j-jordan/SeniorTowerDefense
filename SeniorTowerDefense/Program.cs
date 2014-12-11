using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeniorTowerDefense
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (TowerDefenseGame game = new TowerDefenseGame())
                game.Run();
        }
    }
#endif
}
