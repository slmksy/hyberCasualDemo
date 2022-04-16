using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GenerateBots : MonoBehaviour
    {
        public GameObject botPrefab;
        void Start()
        { 
            var initialPos = new Vector3(0, 0, UnityEngine.Random.Range(CharacterValues.minPosZ, CharacterValues.maxPosZ));
            Instantiate(botPrefab, initialPos, botPrefab.transform.rotation);
        }
    }
}
