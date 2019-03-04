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
        private EcsFilter<BallComponent, MovementComponent> _balls;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<CollisionEventComponent> _collisionEvents;

        public void Run()
        {
            var time = _timing.Components1[0];

            foreach (var eventIndex in _collisionEvents)
            {
                var collisionEvent = _collisionEvents.Components1[eventIndex];

                var movement = _world.GetComponent<MovementComponent>(collisionEvent.Ball);
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

                movement.Direction = new Vector2(
                    flipX ? -movement.Direction.X : movement.Direction.X,
                    flipY ? -movement.Direction.Y : movement.Direction.Y);

                //ball.Velocity *= 1.25f;
                ball.RotationDirection *= -1f;
                _world.RemoveEntity(_collisionEvents.Entities[eventIndex]);
            }
            /*
            if (ballTransform.Position.X - ball.Radius < 0 || ballTransform.Position.X + ball.Radius > world.Width)
            {
                flipX = true;
            }

            if (ballTransform.Position.Y - ball.Radius < 0 || ballTransform.Position.Y + ball.Radius > world.Height)
            {
                flipY = true;
            }


            if (flipX || flipY)
            {
                ball.Direction = new Vector2(
                    flipX ? -ball.Direction.X : ball.Direction.X,
                    flipY ? -ball.Direction.Y : ball.Direction.Y);

                ballTransform.Position = new Vector2(
                    MathHelper.Clamp(ballTransform.Position.X, ball.Radius, world.Width - ball.Radius),
                    MathHelper.Clamp(ballTransform.Position.Y, ball.Radius, world.Height - ball.Radius));

                //ball.Velocity *= 1.25f;
                ball.RotationDirection *= -1f;
                */
            }
    }
}
