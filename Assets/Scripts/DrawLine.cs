using System.Collections.Generic;
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
        private PaintedAreaCalculator paintedAreaCalculator;

        private void Start()
        {
            fingerPositions = new List<Vector3>();
            paintedAreaCalculator = new PaintedAreaCalculator();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && IsDownOnMine(Input.mousePosition)) 
            {                             
                CreateLine();
            }
            if (Input.GetMouseButton(0)&& IsDownOnMine(Input.mousePosition)) 
            {
                var newPoint = GetRaycastHit(Input.mousePosition).point;
                var lastPoint = fingerPositions[fingerPositions.Count - 1];
                var distance = Vector3.Distance(newPoint, lastPoint);

                if (distance > 0.1f)
                {                   
                    UpdateLine(newPoint);                  
                    paintedAreaCalculator.CalculatePaintedArea(newPoint, lastPoint, distance, lineRenderer.startWidth);                   
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
            lineRenderer.SetPosition(lineRenderer.positionCount-1, newFingerPos); 
                   
        }

        private void CreateLine() 
        {
            var vec = GetRaycastHit(Input.mousePosition).point;

            currentLine = Instantiate(linePrefab, vec, Quaternion.identity);
            paintedAreaCalculator.AddLine(currentLine);

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
