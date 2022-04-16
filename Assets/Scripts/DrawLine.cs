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
        private GameObject currentLine;
        private LineRenderer lineRenderer;
        private EdgeCollider2D edgeCollider;
        private List<Vector3> fingerPositions;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && IsDownOnMine(Input.mousePosition)) 
            {                             
                CreateLine();
            }
            if (Input.GetMouseButton(0)&& IsDownOnMine(Input.mousePosition)) 
            {                           
                var tempPos = GetRaycastHit(Input.mousePosition).point;
                var distance = Vector3.Distance(tempPos, fingerPositions[fingerPositions.Count - 1]);
    
                if (distance > 0.1f) 
                {
                    UpdateLine(tempPos);
                }
            }
        }

        private bool IsDownOnMine(Vector2 mousePosition)
        {
            var hit = GetRaycastHit(mousePosition);
            if (hit.collider == null)
            {
                return false;
            }

            return hit.collider.gameObject.Equals(gameObject);
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
