using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Components
{
    public class TransformComponent
    {
        public Vector2 Position     { get; set; }
        public float Rotation       { get; set; }
        public float Scale          { get; set; }
    }
}
