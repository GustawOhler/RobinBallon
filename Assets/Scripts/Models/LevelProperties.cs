using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class LevelProperties
    {
        public float BalloonTimeInterval;
        public int SecondsForGame;
        public string Name;

        public LevelProperties(float balloonTimeInterval, int secondsForGame, string name)
        {
            BalloonTimeInterval = balloonTimeInterval;
            SecondsForGame = secondsForGame;
            Name = name;
        }
    }
}
