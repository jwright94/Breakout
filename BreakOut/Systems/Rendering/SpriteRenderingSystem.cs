using BreakOut.Components;
using Leopotam.Ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Systems.Rendering
{
    [EcsInject]
    public class SpriteRenderingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<SpriteComponent, TransformComponent> _entites;

        private GraphicsDeviceManager _graphicsDeviceManager;
        private GraphicsDevice _graphicsDevice;

        private SpriteBatch _spriteBatch;

        private RenderTarget2D _screen;

        public SpriteRenderingSystem(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
        {
            _graphicsDeviceManager = graphicsDeviceManager;
            _graphicsDevice = graphicsDevice;

            _spriteBatch = new SpriteBatch(_graphicsDevice);

            _screen = new RenderTarget2D(graphicsDevice, 320, 240, false, SurfaceFormat.ColorSRgb, DepthFormat.None);
            _graphicsDevice.SetRenderTarget(_screen);
            _graphicsDevice.Clear(Color.Transparent);

        }

        public void Run()
        {
            _graphicsDevice.SetRenderTarget(_screen);
            _graphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin(SpriteSortMode.Deferred);
            foreach(var spriteIndex in _entites)
            {
                var sprite = _entites.Components1[spriteIndex];
                var transform = _entites.Components2[spriteIndex];

                _spriteBatch.Draw(sprite.Texture, transform.Position, sprite.SourceRectangle, sprite.Color, transform.Rotation, sprite.Origin, sprite.Scale, SpriteEffects.None, sprite.Layer);
            }
            _spriteBatch.End();

            _graphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(SpriteSortMode.Immediate,
                BlendState.Opaque,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone);

            _spriteBatch.Draw(_screen, new Rectangle(0, 0, _graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight), Color.White);/*
            for (int i = 5; i > 0; i--)
            {
                _spriteBatch.Draw(_screen, new Rectangle(0, 0, _graphicsDeviceManager.PreferredBackBufferWidth / i, _graphicsDeviceManager.PreferredBackBufferHeight / i), Color.White);
            }*/
            _spriteBatch.End();
        }
    }
}
