using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Utils
{
    public class AudioUtil
    {
        public static float GetPanningFromPosition(float position, float worldWidth)
        {
            return (position - (worldWidth / 2f)) / (worldWidth / 2f) * 0.7f;
        }
    }
}
