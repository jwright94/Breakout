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
    class PaddleMovementSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<PaddleComponent, MovementComponent> _entites;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<WorldComponent> _worldInfo;
        private EcsFilter<PlayerInputComponent> _playerInput;

        public void Run()
        {
            var time = _timing.Components1[0];
            var world = _worldInfo.Components1[0];
            var playerInput = _playerInput.Components1[0];

            foreach (var index in _entites)
            {
                var paddle = _entites.Components1[index];
                var movement = _entites.Components2[index];
                
                if(playerInput.PressingLeft)
                {
                    movement.Direction = new Vector2(-1f, 0);
                }

                if (playerInput.PressingRight)
                {
                    movement.Direction = new Vector2(1f, 0);
                }

                if(!playerInput.PressingRight && !playerInput.PressingLeft)
                {
                    movement.Velocity = 0f;
                    movement.Direction = Vector2.Zero;
                }
                else
                {
                    movement.Velocity = 350f;
                }
            }
        }
    }
}
