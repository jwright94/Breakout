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
    public class StateSystem : IEcsRunSystem, IEcsInitSystem
    {
        public EcsWorld _world;
        
        public void Initialize()
        {
            SpriteComponent sprite;
            TransformComponent transform;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    _world.CreateEntityWith(out sprite, out transform);
                    sprite.Texture = Resources.Block;
                    transform.Position = new Vector2(x*Resources.Block.Width, y*Resources.Block.Height);
                }
            }


        }

        public void Run()
        {

        }

        public void Destroy()
        {

        }
    }
}
