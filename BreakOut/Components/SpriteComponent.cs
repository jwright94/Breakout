using Leopotam.Ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Components
{
    public class SpriteComponent
    {
        [EcsIgnoreNullCheck]
        public Texture2D Texture;

        public Vector2 Scale { get; set; } = Vector2.One;
        public Color Color { get; set; } = Color.White;

        public Rectangle? SourceRectangle { get; set; }
        public Vector2 Origin { get; set; } = Vector2.Zero;

        public float Layer { get; set; }
    }
}
