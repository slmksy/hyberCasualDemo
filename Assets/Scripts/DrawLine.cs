using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DrawLine : MonoBehaviour
    {
        public GameObject linePrefab;
        public GameObject currentLine;

        public LineRenderer lineRenderer;
        public EdgeCollider2D edgeCollider;
        public List<Vector3> fingerPositions;

        private void Start()
        {
            
        }

        private bool isDownOnMine(Vector2 mousePosition) 
        {
            var hit = GetRaycastHit(mousePosition);
            if (hit.collider == null)
            {
                return false;
            }

            return hit.collider.gameObject.Equals(gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isDownOnMine(Input.mousePosition)) 
            {                             
                CreateLine();
                Debug.LogWarning("GetMouseButtonDown");
            }
            if (Input.GetMouseButton(0)&& isDownOnMine(Input.mousePosition)) 
            {                           
                var tempFingerPos = GetRaycastHit(Input.mousePosition).point;
                var distance = Vector3.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]);
    
                if (distance > 0.1f) 
                {
                    Debug.LogWarning("distance " + distance);
                    UpdateLine(tempFingerPos);
                }
            }
        }

        private RaycastHit GetRaycastHit(Vector3 mousePosition)
        {
            return RayFromCamera(mousePosition, 1000.0f);
        }

        private RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            Physics.Raycast(ray, out hit, rayLength);

            return hit;
        }

        private void UpdateLine(Vector3 newFingerPos) 
        {
            fingerPositions.Add(newFingerPos);
            lineRenderer.positionCount += 1;          
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos); ;
        }

        private void CreateLine() 
        {
            var vec = GetRaycastHit(Input.mousePosition).point;

            currentLine = Instantiate(linePrefab, vec, Quaternion.identity);
            lineRenderer = currentLine.GetComponent<LineRenderer>();
            edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
            fingerPositions.Clear();
    

            fingerPositions.Add(vec);
            fingerPositions.Add(vec);
            lineRenderer.SetPosition(0, fingerPositions[0]);
            lineRenderer.SetPosition(1, fingerPositions[1]);
        }
    }
}
