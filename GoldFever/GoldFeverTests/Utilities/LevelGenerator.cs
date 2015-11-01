using GoldFever.Core;
using GoldFever.Core.Generic;
using GoldFever.Core.Level;
using GoldFever.Core.Model;
using GoldFever.Core.Track;
using System;

namespace GoldFeverTests.Utilities
{
    public class LevelGenerator
    {


        public static BaseLevel Generate(bool correct = true)
        {
            var game = new Game(new GameOptions()
            {
                ContentPath = "GoldFeverTests.Content"
            });

            var data = new LevelModel()
            {
                Port = new ShipPortModel()
                {
                    Index = 5,
                    Size = 10
                },
                Tracks = new TrackModel[]
                {
                    new TrackModel()
                    {
                        Type = TrackType.Start,
                        Direction = Direction.East,
                        Position = new Vector(0, 0)
                    },
                    new TrackModel()
                    {
                        Type = TrackType.Default,
                        Direction = Direction.East,
                        Position = new Vector((correct ? 1 : 2), 0)
                    }
                }
            };

            var manager = new LevelManager(game);
            return new BaseLevel(manager, data);
        }
    }
}
