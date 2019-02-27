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
    public class TimingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private GameTime _gameTime;
        private EcsFilter<TimeInfoComponent> _timeInfo;

        public void Initialize()
        {
            _world.CreateEntityWith<TimeInfoComponent>();
        }

        public void Run()
        {
            foreach (var timerIndex in _timeInfo)
            {
                _timeInfo.Components1[timerIndex].DeltaTime = (float)(_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);
            }
        }

        public void Destroy()
        {

        }

        public void SetGameTime(GameTime gameTime)
        {
            _gameTime = gameTime;
        }
    }
}
