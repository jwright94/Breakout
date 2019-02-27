﻿using BreakOut.Components;
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
    public class BallSystem : IEcsRunSystem
    {
        public EcsWorld _world;
        private EcsFilter<BallComponent, TransformComponent> _entites;
        private EcsFilter<TimeInfoComponent> _timing;
        private EcsFilter<WorldComponent> _worldInfo;

        public void Run()
        {
            var time = _timing.Components1[0];
            var world = _worldInfo.Components1[0];

            foreach (var index in _entites)
            {
                var ball = _entites.Components1[index];
                var ballTransform = _entites.Components2[index];

                ballTransform.Position += ball.Direction * ball.Velocity * time.DeltaTime;
                ballTransform.Rotation += (float)Math.PI * time.DeltaTime * ball.RotationDirection;

                bool flipX = false;
                bool flipY = false;

                if (ballTransform.Position.X < 0 || ballTransform.Position.X > world.Width)
                {
                    flipX = true;
                }

                if (ballTransform.Position.Y < 0 || ballTransform.Position.Y > world.Height)
                {
                    flipY = true;
                }


                if (flipX || flipY)
                {
                    ball.Direction = new Vector2(
                        flipX ? -ball.Direction.X : ball.Direction.X,
                        flipY ? -ball.Direction.Y : ball.Direction.Y);

                    ballTransform.Position = new Vector2(
                        MathHelper.Clamp(ballTransform.Position.X, 0, world.Width),
                        MathHelper.Clamp(ballTransform.Position.Y, 0, world.Height));

                    //ball.Velocity *= 1.25f;
                    ball.RotationDirection *= -1f;
                }
            }
        }
    }
}