﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Components
{
    public class BallComponent
    {
        public float RotationDirection { get; internal set; }
        public int Radius           { get; set; }
    }
}
