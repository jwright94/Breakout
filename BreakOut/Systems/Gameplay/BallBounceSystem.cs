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
    public class BallBounceSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<BallComponent, MovementComponent> _balls;
        private EcsFilter<CollisionEventComponent> _collisionEvents;
        private EcsFilter<WorldComponent> _worldEntities;

        private Random random = new Random();


        private int pitchIndex = 0;
        private float[] pitches = new[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };

        public void Run()
        {
            var worldComponent = _worldEntities.Components1[0];

            foreach (var eventIndex in _collisionEvents)
            {

                var collisionEvent = _collisionEvents.Components1[eventIndex];

                var movement = _world.GetComponent<MovementComponent>(collisionEvent.Ball);
                var ball = _world.GetComponent<BallComponent>(collisionEvent.Ball);
                var ballTransform = _world.GetComponent<TransformComponent>(collisionEvent.Ball);

                Resources.Boop.Play(0.7f, GetNextPitch(), AudioUtil.GetPanningFromPosition(ballTransform.Position.X, worldComponent.Width));

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
        }


        private float GetNextPitch()
        {
            var pitch = pitches[pitchIndex];
            pitchIndex = ++pitchIndex % pitches.Length;
            return pitch;
        }
    }
}
