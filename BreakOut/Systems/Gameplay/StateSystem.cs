using BreakOut.Components;
using Leopotam.Ecs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Systems.Gameplay
{
    [EcsInject]
    public class StateSystem : IEcsRunSystem, IEcsInitSystem
    {
        public EcsWorld _world;
        
        public void Initialize()
        {
            SpriteComponent sprite;
            TransformComponent transform;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    _world.CreateEntityWith(out sprite, out transform);
                    sprite.Texture = Resources.Block;
                    transform.Position = new Vector2(x*Resources.Block.Width, y*Resources.Block.Height);


                }
            }

            var ballEntity = MakeBall();

            var world = _world.CreateEntityWith<WorldComponent>();
            world.Width = 320;
            world.Height = 240;

        }

        private int MakeBall()
        {
            TransformComponent transform;
            SpriteComponent sprite;
            BallComponent ball;

            var ballEntity = _world.CreateEntityWith(out transform, out sprite, out ball);

            transform.Position = new Vector2(200, 200);

            ball.Velocity = 500f;
            ball.RotationDirection = -1f;
            ball.Direction = Vector2.Normalize(new Vector2(1f, 1f));

            sprite.Texture = Resources.Ball;
            sprite.Origin = new Vector2(sprite.Texture.Width, sprite.Texture.Height) * 0.5f;
            return ballEntity;
        }

        public void Run()
        {

        }

        public void Destroy()
        {

        }
    }
}
