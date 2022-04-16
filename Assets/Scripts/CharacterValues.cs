using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterValues
    {
        public const float maxPosZ = 12.5f;
        public const float minPosZ = -12.5f;

        public Vector3 charaterInitialPos;
        public bool isFinished;

        public float swerveSpeed = 3;
        public float runSpeed = 15f;

        public CharacterValues() 
        {

        }
    }
}
