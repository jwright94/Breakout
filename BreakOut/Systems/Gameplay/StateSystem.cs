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
            CollidableComponent collider;

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    _world.CreateEntityWith(out sprite, out transform, out collider);
                    sprite.Texture = Resources.Block;
                    transform.Position = new Vector2(x*Resources.Block.Width, y*Resources.Block.Height) * 3f;

                    collider.CollisionBox = new Rectangle(0, 0, Resources.Block.Width, Resources.Block.Height);
                    //break;
                }
                //break;
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

            ball.Radius = 4;

            ball.Velocity = 300f;
            ball.RotationDirection = -.2f;
            ball.Direction = Vector2.Normalize(new Vector2(1f, 1f));

            sprite.Texture = Resources.Ball;
            sprite.Origin = new Vector2(sprite.Texture.Width, sprite.Texture.Height) * 0.5f;

            sprite.Scale = new Vector2(2f * ball.Radius / sprite.Texture.Width);
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
