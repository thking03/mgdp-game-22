using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject sprite;
    public Vector3 unitX = new Vector3(1, 0, 0);
    public float gridSizeCoeff = 0.3f;
    //timer tracks number of frames to slow down movement
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //or "spawn" decided later?
        sprite.transform.position = new Vector3(0, 0, 0);
        sprite.GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {

        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        if(timer % 10 == 0)
        {
            //update the position
            sprite.transform.position = transform.position + new Vector3(horizontalInput * gridSizeCoeff, verticalInput * gridSizeCoeff, 0);

            Debug.Log(transform.position);
        }

        timer += 1;


    }
}
