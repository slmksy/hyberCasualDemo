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
           
            for (int i = 0; i < 10;++i) 
            {
                var initialPos = new Vector3(0, 0, UnityEngine.Random.Range(CharacterValues.minPosZ, CharacterValues.maxPosZ));
                var bot = Instantiate(botPrefab, initialPos, botPrefab.transform.rotation);
                BotsObjectModel.Instance.AddBot(bot);
            }
           
        }
    }
}
