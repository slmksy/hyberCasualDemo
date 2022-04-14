using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandatoryHorizontalMoving : MonoBehaviour
{
    private const int maxPosZ = 12;
    private const int minPosZ = -12;
    private float speed = 3f;
    private int directionVal = 1;
    // Start is called before the first frame update
    void Start()
    {
        directionVal = (transform.position.z < 0) ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {       
        if (transform.position.z > maxPosZ) 
        {
            directionVal = -1;
        }
        if (transform.position.z < minPosZ)
        {
            directionVal = 1;
        }
        transform.position += new Vector3(0, 0, directionVal) * speed * Time.deltaTime;
    }
}
