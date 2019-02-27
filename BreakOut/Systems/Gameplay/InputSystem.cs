using BreakOut.Components;
using Leopotam.Ecs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Systems.Gameplay
{
    [EcsInject]
    public class InputSystem : IEcsRunSystem, IEcsInitSystem
    {
        public EcsWorld _world;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<PlayerInputComponent> _playerInput;
        
        public void Initialize()
        {

        }

        public void Destroy()
        {

        }

        public void Run()
        {
            KeyboardState state = Keyboard.GetState();
            
            foreach (var index in _playerInput)
            {
                var playerInput = _playerInput.Components1[index];

                playerInput.PressingLeft = state.IsKeyDown(Keys.Left);
                playerInput.PressingRight = state.IsKeyDown(Keys.Right);
                playerInput.PressingPrimary = state.IsKeyDown(Keys.Z);
                playerInput.PressingSecondary = state.IsKeyDown(Keys.X);
            }
        }
    }
}
