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
    public class BrickBreakingSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<WorldComponent> _worldEntities;
        private EcsFilter<CollisionEventComponent> _collisionEvents;
        
        public void Run()
        {
            var worldComponenet = _worldEntities.Components1[0];

            foreach (var eventIndex in _collisionEvents)
            {
                var collisionEvent = _collisionEvents.Components1[eventIndex];
                var blockComponent = _world.GetComponent<BlockComponent>(collisionEvent.Collider);

                var transformComponenet = _world.GetComponent<TransformComponent>(collisionEvent.Collider);
                
                if (blockComponent != null)
                {
                    _world.RemoveEntity(collisionEvent.Collider);
                    Resources.Break.Play(0.5f, 0f, AudioUtil.GetPanningFromPosition(transformComponenet.Position.X, worldComponenet.Width));
                }
            }
        }
    }
}
