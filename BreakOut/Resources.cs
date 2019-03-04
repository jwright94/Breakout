using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut
{
    public static class Resources
    {
        private const string contentFolder = "./Contents/";

        public static Texture2D Block;
        public static Texture2D Ball;
        public static Texture2D Pixel;

        public static Texture2D Birb { get; internal set; }

        public static void LoadContents(GraphicsDevice graphicsDevice)
        {
            Block = LoadTexture("block.png", graphicsDevice);
            Block = LoadTexture("block2.png", graphicsDevice);
            Ball = LoadTexture("ball.png", graphicsDevice);
            Birb = LoadTexture("levels/level.png", graphicsDevice);

            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Pixel.SetData<Color>(new[] { Color.White });
        }

        private static Texture2D LoadTexture(string filename, GraphicsDevice graphicsDevice)
        {
            var filestream = File.OpenRead(Path.Combine(contentFolder, filename));

            Texture2D texture2d;

            using (filestream)
            {
                texture2d = Texture2D.FromStream(graphicsDevice, filestream);
            }

            return texture2d;
        }
    }
}
