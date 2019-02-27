using BreakOut.Components;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Systems.Gameplay
{
    [EcsInject]
    public class BallSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<BallComponent, TransformComponent> _entites;
        private EcsFilter<TimeInfoComponent> _timing;

        public void Run()
        {
            var time = _timing.Components1[0];

            foreach(var index in _entites)
            {
                var ball = _entites.Components1[index];
                var ballTransform = _entites.Components2[index];

                ballTransform.Position += ball.Direction * ball.Velocity * time.DeltaTime;
            }
        }
    }
}
