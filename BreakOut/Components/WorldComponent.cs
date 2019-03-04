using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Components
{
    public class WorldComponent
    {
        public int Width    { get; set; }
        public int Height   { get; set; }

        public Vector2 CameraShakeOffset    { get; set; }
        public Vector2 CameraShakeVelocity  { get; set; }
    }
}
