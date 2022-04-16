using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    private const int maxPosZ = 13;
    private const int minPosZ = -13;

    private Vector3 camInitialPos;
    private Vector3 charaterInitialPos;
    private bool isFinished;

    private float swerveSpeed = 3;
    private float runSpeed = 15f;
    public GameObject mainCam;
    // Start is called before the first frame update
    void Start()
    {
        camInitialPos = mainCam.transform.position;
        charaterInitialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            return;
        }

        RunCharacter();

        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            Swerve(true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Swerve(false);
        }

        if (transform.position.z < minPosZ ||
            transform.position.z > maxPosZ) 
        {
            Restart();
        }
    }

    private void Restart() 
    {
        transform.position = charaterInitialPos;
        mainCam.transform.position = camInitialPos;
    }

    void OnCollisionEnter(Collision other)
    {     
        if (other.gameObject.name.Contains("Obstacle"))
        {
            Restart();
        }       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Obstacle"))
        {           
            transform.position = charaterInitialPos;
            mainCam.transform.position = camInitialPos;
        }
        if (other.gameObject.name.Contains("EndLine"))
        {
            isFinished = true;
        }
    }

    private void Swerve(bool isLeft) 
    {
        var directionVal = isLeft ? 1 : -1;
        var vec = new Vector3(0, 0, directionVal) * swerveSpeed * Time.deltaTime;
        transform.position += vec;
    }

    private void RunCharacter() 
    {
        var vec = new Vector3(1, 0, 0) * runSpeed * Time.deltaTime;
        transform.position += vec;
        mainCam.transform.position += vec;
    }
}
