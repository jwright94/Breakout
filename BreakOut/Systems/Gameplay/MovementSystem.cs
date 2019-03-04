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
    public class MovementSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<MovementComponent, TransformComponent> _entites;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<WorldComponent> _worldInfo;

        public void Run()
        {
            var time = _timing.Components1[0];
            var world = _worldInfo.Components1[0];

            foreach (var index in _entites)
            {
                var movement = _entites.Components1[index];
                var transform = _entites.Components2[index];

                transform.Position += movement.Direction * movement.Velocity * time.DeltaTime;

                if(movement.RotationalVelocity > 0 || movement.RotationalVelocity < 0)
                { 
                    transform.Rotation += (float)(2.0 * Math.PI * time.DeltaTime * movement.RotationalVelocity);
                }
            }
        }
    }
}
