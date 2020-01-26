using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class BalloonProperties
    {
        public string MaterialName;
        public float SpeedMultiplier;
        public int Points;

        public BalloonProperties(string materialName, float speedMultiplier, int points)
        {
            MaterialName = materialName;
            SpeedMultiplier = speedMultiplier;
            Points = points;
        }
    }
}
