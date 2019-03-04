using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Components
{
    public class MovementComponent
    {
        public Vector2 Direction    { get; set; }
        public float Velocity       { get; set; }
        public float RotationalVelocity       { get; set; }
    }
}
