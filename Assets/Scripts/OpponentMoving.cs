using Assets.Scripts;
using System;
using UnityEngine;

public class OpponentMoving : MonoBehaviour
{
    private CharacterValues characterValues;
    private int randDirectionalVal;
    private DateTime lastGenerateRandTime;

    // Start is called before the first frame update
    void Start()
    {
        characterValues = new CharacterValues();
        characterValues.charaterInitialPos = gameObject.transform.position;
        randDirectionalVal = UnityEngine.Random.Range(0, 2);
        lastGenerateRandTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterValues.isFinished)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            return;
        }
           
        if(DateTime.Now.Subtract (lastGenerateRandTime).TotalSeconds > 1) 
        {
            lastGenerateRandTime = DateTime.Now;
            randDirectionalVal = UnityEngine.Random.Range(0, 2);
        }
      
        if (
            (transform.position.z < CharacterValues.minPosZ ||
            transform.position.z > CharacterValues.maxPosZ)) 
        {
            Restart();
        }

        RunCharacter();
        Swerve(randDirectionalVal == 1);
    }

    private void Restart() 
    {
        transform.position = characterValues.charaterInitialPos;
    }

    void OnCollisionEnter(Collision other)
    {     
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Obstacle"))
        {
            Restart();
        }
        if (other.gameObject.name.Equals("FinishLine"))
        {
            characterValues.isFinished = true;
        }
    }

    private void Swerve(bool isLeft) 
    {
        var directionVal = isLeft ? 1 : -1;
        var vec = new Vector3(0, 0, directionVal) * characterValues.swerveSpeed * Time.deltaTime;
        transform.position += vec;
    }

    private void RunCharacter() 
    {
        var vec = new Vector3(1, 0, 0) * characterValues.runSpeed * Time.deltaTime;
        transform.position += vec;
    }
}
