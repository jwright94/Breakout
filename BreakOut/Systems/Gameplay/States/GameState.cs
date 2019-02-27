using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Systems.Gameplay.States
{
    public abstract class GameState
    {
        protected StateSystem GameStateSystem { get; }

        public GameState(StateSystem gameStateSystem)
        {
            GameStateSystem = gameStateSystem;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
