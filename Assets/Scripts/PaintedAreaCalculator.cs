using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PaintedAreaCalculator
    {      
        private GameObject wallObj;
        private List<GameObject> lines = new List<GameObject>();
        private float wallArea;
        private float totalPaintedArea;

        public PaintedAreaCalculator()
        {
            wallObj = GameObject.Find("FinishWall");
            var wallScale = wallObj.transform.localScale;
            wallArea = wallScale.y * wallScale.z;
        }

        public void AddLine(GameObject line) 
        {
            lines.Add(line);
        }

   
        public void CalculatePaintedArea(Vector3 newPoint, Vector3 lastPoint, float distance, float width)
        {

            if (IsAlreadyPaintedArea(newPoint) || IsAlreadyPaintedArea(lastPoint)) 
            {
                return;
            }

            var paintedArea = distance * width;
           
            totalPaintedArea += paintedArea;

            var percentPainted = (totalPaintedArea * 100) / wallArea;
            var txtPaintedPercent = GameObject.Find("txtPaintedPercent").GetComponent<Text>();
            txtPaintedPercent.text = string.Concat("PAINTED % : " + percentPainted.ToString("n2"));
        }

        private bool IsAlreadyPaintedArea(Vector3 point)
        {
            for (int i = 0; i < lines.Count; ++i)
            {
                var lineRenderer = lines[i].GetComponent<LineRenderer>();
               
                for(int k = 0; k < lineRenderer.positionCount; ++k)                 
                {
                    if(i == lines.Count - 1 && k == lineRenderer.positionCount -5)                    
                    {
                        return false;
                    }
                    var distance = Vector3.Distance(point, lineRenderer.GetPosition(k));
                    if(distance < 0.7f) 
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }
    }
}
