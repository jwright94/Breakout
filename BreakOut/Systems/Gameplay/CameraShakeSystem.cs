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
    public class CameraShakeSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<CollisionEventComponent> _collisionEvents;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<WorldComponent> _worldInfo;

        public void Run()
        {
            var time = _timing.Components1[0];
            var world = _worldInfo.Components1[0];

            Vector2 totalCameraPush = Vector2.Zero;

            foreach (var eventIndex in _collisionEvents)
            {
                totalCameraPush -= _collisionEvents.Components1[0].Direction;
            }

            world.CameraShakeVelocity += totalCameraPush;

            if (world.CameraShakeVelocity.LengthSquared() > 0.01f)
            {
                world.CameraShakeOffset += world.CameraShakeVelocity;
                // Pull the velocity closer to 0
                world.CameraShakeVelocity = (-world.CameraShakeOffset) * 2f * time.DeltaTime;
                //world.CameraShakeVelocity -= 2f * world.CameraShakeVelocity * time.DeltaTime;
            }
            else
            {
                world.CameraShakeOffset = Vector2.Zero;
                world.CameraShakeVelocity = Vector2.Zero;
            }
        }
    }
}
