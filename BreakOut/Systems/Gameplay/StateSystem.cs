using BreakOut.Components;
using Leopotam.Ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private WorldComponent worldEntity;

        public void Initialize()
        {
            worldEntity = _world.CreateEntityWith<WorldComponent>();
            worldEntity.Width = 320;
            worldEntity.Height = 240;

            LoadMap(Resources.Birb);

            

            var ballEntity = MakeBall();
            
            BuildWalls();

        }

        private void LoadMap(Texture2D mapImage)
        {
            SpriteComponent sprite;
            TransformComponent transform;
            CollidableComponent collider;
            BlockComponent block;

            var pixels = new Color[10*20];
            mapImage.GetData(pixels);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    var pixel = pixels[x + y*20];
                    if (pixel.A < 255)
                        continue;

                    var blockEntity =_world.CreateEntityWith(out sprite, out transform, out collider);
                    block = _world.AddComponent<BlockComponent>(blockEntity);

                    sprite.Texture = Resources.Block;
                    sprite.Color = pixel;
                    transform.Position = new Vector2(x * Resources.Block.Width, y * Resources.Block.Height);

                    collider.CollisionBox = new Rectangle(0, 0, Resources.Block.Width, Resources.Block.Height);
                }
            }
        }

        private void BuildWalls()
        {
            var wallThickness = 32;

            // Top
            BuildWall(0, -wallThickness, worldEntity.Width, wallThickness);

            // Bottom
            BuildWall(0, worldEntity.Height, worldEntity.Width, wallThickness);

            // Left
            BuildWall(-wallThickness, 0, wallThickness, worldEntity.Height + wallThickness);

            // Right
            BuildWall(worldEntity.Width, 0, wallThickness, worldEntity.Height + wallThickness);
        }

        private int BuildWall(int x, int y, int width, int height)
        {
            var entity = _world.CreateEntityWith<TransformComponent, CollidableComponent>(out var transform, out var collider);

            transform.Position = new Vector2(x, y);

            collider.CollisionBox = new Rectangle(0, 0, width, height);

            return entity;
        }

        private int MakeBall()
        {
            TransformComponent transform;
            SpriteComponent sprite;
            BallComponent ball;

            var ballEntity = _world.CreateEntityWith(out transform, out sprite, out ball);

            var movement = _world.AddComponent<MovementComponent>(ballEntity);

            transform.Position = new Vector2(worldEntity.Width / 2f, worldEntity.Height - ball.Radius);

            ball.Radius = 4;

            movement.Velocity = 300f;
            movement.RotationalVelocity = -.2f;
            movement.Direction = Vector2.Normalize(new Vector2(1f, 1f));

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
