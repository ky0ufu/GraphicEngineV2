using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursesSharp;

namespace Engine
{
    public class GameConsole : Game.Canvas
    {
        public GameConsole(int n, int m, Game game, Camera camera) : base(n, m, game, camera)
        {

        }

        public override void Draw()
        {          
            float deltaDist = camera.GetProperty("drawDistance") / charmap.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (distances[i, j] > camera.GetProperty("drawDistance") || distances[i, j] == float.PositiveInfinity)
                        Stdscr.Add(i, j, ' ');
                    else
                    {
                        char outChar = charmap[charmap.Length - 1 - (int)(distances[i, j] / deltaDist)];
                        Stdscr.Add(i, j,outChar);
                    }
                }
                Curses.NapMs(1);
            }

        }

        public override void Update()
        {
            Ray[,] rayMatrix = camera.GetRaysMatrix(n, m);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    float distValue = float.PositiveInfinity;
                    foreach (var obj in game.Entities.Entities)
                    {
                        float dist = obj.IntersectionDistance(rayMatrix[i, j]);
                        distValue = Math.Min(dist, distValue);
                    }
                    distances[i, j] = distValue;
                }
            }
        }

    }
}
