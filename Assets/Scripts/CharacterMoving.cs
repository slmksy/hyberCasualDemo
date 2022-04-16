using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMoving : MonoBehaviour
{
    private Vector3 camInitialPos;
    private CharacterValues characterValues;


    // Start is called before the first frame update
    void Start()
    {
        characterValues = new CharacterValues();
        camInitialPos = Camera.main.transform.position;
        characterValues.charaterInitialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterValues.isFinished)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            return;
        }
     
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            Swerve(true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Swerve(false);
        }

        if (transform.position.z < CharacterValues.minPosZ ||
            transform.position.z > CharacterValues.maxPosZ) 
        {
            Restart();
        }


        UpdateMyRank();
        RunCharacter();
    }

    private void UpdateMyRank() 
    {
        var rank = BotsObjectModel.Instance.GetMyRank(gameObject);
        var txtRank = GameObject.Find("txtRank").GetComponent<Text>();
        txtRank.text = string.Concat("RANK : " + rank.ToString());
    }

    private void Restart() 
    {
        transform.position = characterValues.charaterInitialPos;
        Camera.main.transform.position = camInitialPos;
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
        Camera.main.transform.position += vec;
    }
}
