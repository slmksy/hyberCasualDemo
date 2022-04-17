using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterValues
    {
        public const float maxPosZ = 7.5f;
        public const float minPosZ = -7.5f;

        public Vector3 charaterInitialPos;
        public bool isFinished;

        public float swerveSpeed = 3;
        public float runSpeed = 15f;

        public CharacterValues() 
        {

        }
    }
}
