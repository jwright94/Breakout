using BreakOut.Components;
using BreakOut.Utils;
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
    public class CollisionSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<BallComponent, TransformComponent> _balls;
        private EcsFilter<CollidableComponent, TransformComponent> _collidables;

        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<WorldComponent> _worldInfo;

        public void Run()
        {
            var time = _timing.Components1[0];
            var world = _worldInfo.Components1[0];

            foreach (var ballIndex in _balls)
            {
                var ball = _balls.Components1[ballIndex];
                var ballTransform = _balls.Components2[ballIndex];
                
                var ballCollisionRectangle = new Rectangle(
                    (int)(ballTransform.Position.X - ball.Radius),
                    (int)(ballTransform.Position.Y - ball.Radius),
                    ball.Radius*2, 
                    ball.Radius*2);

                foreach(var collidableIndex in _collidables)
                {
                    CheckCollision(ballIndex, collidableIndex, ballCollisionRectangle);
                }
            }
        }

        public void CheckCollision(int ballIndex, int collidableIndex, Rectangle ballCollisionRectangle)
        {
            var collider = _collidables.Components1[collidableIndex];
            var colliderTransform = _collidables.Components2[collidableIndex];

            var collidableRectangle = new Rectangle(
                    (int)(colliderTransform.Position.X + collider.CollisionBox.X),
                    (int)(colliderTransform.Position.Y + collider.CollisionBox.Y),
                    collider.CollisionBox.Width,
                    collider.CollisionBox.Height);

            ballCollisionRectangle.Intersects(ref collidableRectangle, out var wasCollision);

            if(wasCollision)
            {
                ResolveCollision(ballIndex, collidableIndex, ballCollisionRectangle, collidableRectangle);
            }
        }

        private void ResolveCollision(int ballIndex, int collidableIndex, Rectangle ballCollisionRectangle, Rectangle collidableRectangle)
        {
            Console.WriteLine("BallCollided: ");
            Console.WriteLine(ballCollisionRectangle);
            Console.WriteLine(collidableRectangle);

            if (ballCollisionRectangle.CollidesWith(collidableRectangle))
            {
                var intersect = GetShallowIntersect(ballCollisionRectangle.GetIntersectionDepth(collidableRectangle));

                var ballTransform = _balls.Components2[ballIndex];
                
                ballTransform.Position += intersect;

                DispatchCollisionEvent(_balls.Entities[ballIndex], _collidables.Entities[collidableIndex], intersect);
            }
        }

        private Vector2 GetShallowIntersect(Vector2 intersect)
        {
            if(Math.Abs(intersect.X) < Math.Abs(intersect.Y))
            {
                return new Vector2(intersect.X, 0);
            }

            return new Vector2(0, intersect.Y);
        }

        private void DispatchCollisionEvent(int ballId, int collidableId, Vector2 intersect)
        {
            var entity = _world.CreateEntityWith<CollisionEventComponent>(out var collisionEvent);

            collisionEvent.Ball = ballId;
            collisionEvent.Collider = collidableId;
            collisionEvent.Direction = Vector2.Normalize(intersect);
        }
    }
}
