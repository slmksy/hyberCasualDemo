using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    private float speed = 12f;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*  var pos = transform.position;
          var camPos = camera.transform.position;
          transform.position = new Vector3(pos.x + speed * Time.deltaTime, pos.y, pos.z);
          camera.transform.position = new Vector3(camPos.x + speed * Time.deltaTime, camPos.y, camPos.z);*/
        //  transform.tra(targetPos, Vector3.up, degrees * Time.deltaTime);

        transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
        camera.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
    }
}
