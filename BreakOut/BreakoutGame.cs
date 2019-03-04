using BreakOut.Systems.Gameplay;
using BreakOut.Systems.Rendering;
using Leopotam.Ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut
{
    public class BreakoutGame : Game
    {
        GraphicsDeviceManager graphics;

        EcsWorld _world;
        EcsSystems _systems;

        EcsSystems _renderingSystems;
        EcsSystems _gameplaySystems;

        TimingSystem _timingSystem;

        public BreakoutGame()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {

            // Initialized Graphics Window
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            base.Initialize();
        }

        private void InitializeECS()
        {
            // Initialize ECS
            _world = new EcsWorld();

            _systems = new EcsSystems(_world);

            _renderingSystems = new EcsSystems(_world, "Rendering Systems");
            _renderingSystems.Add(new SpriteRenderingSystem(graphics, GraphicsDevice));


            _gameplaySystems = new EcsSystems(_world, "Gameplay Systems");
            _gameplaySystems.Add(_timingSystem = new TimingSystem());
            _gameplaySystems.Add(new StateSystem());
            _gameplaySystems.Add(new InputSystem());
            _gameplaySystems.Add(new PaddleMovementSystem());
            _gameplaySystems.Add(new MovementSystem());
            _gameplaySystems.Add(new CollisionSystem());
            _gameplaySystems.Add(new BrickBreakingSystem());
            _gameplaySystems.Add(new BallBounceSystem());

            _systems.Add(_renderingSystems);
            _systems.Add(_gameplaySystems);

            _systems.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            _systems.Dispose();
            _world.Dispose();
            base.Dispose(disposing);
        }

        protected override void LoadContent()
        {
            Resources.LoadContents(GraphicsDevice);

            InitializeECS();
        }

        protected override void Update(GameTime gameTime)
        {
            _timingSystem.SetGameTime(gameTime);

            _gameplaySystems.Run();
            _world.RemoveOneFrameComponents();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _renderingSystems.Run();
            base.Draw(gameTime);
        }
    }
}
