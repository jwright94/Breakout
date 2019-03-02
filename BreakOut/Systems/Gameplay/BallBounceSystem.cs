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
    public class BallBounceSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<BallComponent, TransformComponent> _balls;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<CollisionEventComponent> _collisionEvents;

        public void Run()
        {
            var time = _timing.Components1[0];

            foreach (var eventIndex in _collisionEvents)
            {
                var collisionEvent = _collisionEvents.Components1[eventIndex];

                var ball = _world.GetComponent<BallComponent>(collisionEvent.Ball);
                //var ballTransform = _world.GetComponent<TransformComponent>(collisionEvent.Ball);
                
                
                bool flipX = false;
                bool flipY = false;

                if (Math.Abs(collisionEvent.Direction.X) > Math.Abs(collisionEvent.Direction.Y))
                {
                    flipX = true;
                }
                else
                {
                    flipY = true;
                }
                
                ball.Direction = new Vector2(
                    flipX ? -ball.Direction.X : ball.Direction.X,
                    flipY ? -ball.Direction.Y : ball.Direction.Y);

                //ball.Velocity *= 1.25f;
                ball.RotationDirection *= -1f;
                _world.RemoveEntity(_collisionEvents.Entities[eventIndex]);
            }
        }
    }
}
