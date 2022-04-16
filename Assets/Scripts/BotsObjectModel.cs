using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BotsObjectModel
    {
        private static BotsObjectModel instance = null;
        private List<GameObject> botObjects = new List<GameObject>();
        private BotsObjectModel() 
        {
        }

        public static BotsObjectModel Instance 
        {
            get            
            {
                if(instance == null)
                {
                    instance = new BotsObjectModel();
                }

                return instance;
            }
        }

        public void AddBot(GameObject gameObject) 
        {
            botObjects.Add(gameObject);
        }

        public int GetMyRank(GameObject playerObj) 
        {
            int rank = botObjects.Count + 1; ;

            foreach(var bot in botObjects) 
            {
                if (playerObj.transform.position.x > bot.transform.position.x) 
                {
                    rank -= 1;
                }
            }

            return rank;
        }
    }

   
}
