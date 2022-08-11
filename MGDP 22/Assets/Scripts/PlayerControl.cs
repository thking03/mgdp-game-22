using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speedTwoD;
    public bool inTwoD = true;
    public Rigidbody rb;
    public float jumpForce;
    public GameObject raycastSpawn;

    private GameObject[] world;

    private void Start()
    {
        world = GameObject.FindGameObjectsWithTag("Grid");
    }

    private void Update()
    {

        if (inTwoD)
        {
            Vector3 dir = new Vector3(0, 0 , 0);

            if (Input.GetButton("Right") && !Input.GetButton("Left"))
            {
                dir.x = speedTwoD * Time.deltaTime;
            } else if(Input.GetButton("Left") && !Input.GetButton("Right"))
            {
                dir.x = -1 * speedTwoD * Time.deltaTime;
            }

            if(Input.GetButtonDown("Up") && OnGround())
            {
                rb.AddForce(new Vector3(0, jumpForce, 0));
            }

            transform.position += dir;
        } else
        {
            if(Input.GetButtonDown("Right") && checkMoveable(new Vector3(0, 0, -1)))
            {
                transform.position += new Vector3(0, 0, -1);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
            if (Input.GetButtonDown("Left") && checkMoveable(new Vector3(0, 0, 1)))
            {
                transform.position += new Vector3(0, 0, 1);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
            if (Input.GetButtonDown("Up") && checkMoveable(new Vector3(1, 0, 0)))
            {
                transform.position += new Vector3(1, 0, 0);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
            if (Input.GetButtonDown("Down") && checkMoveable(new Vector3(-1, 0, 0)))
            {
                transform.position += new Vector3(-1, 0, 0);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
        }

        //don't rotate!!
        if (((transform.rotation.eulerAngles.x < 358) && (transform.rotation.eulerAngles.x > 2)) ||
     ((transform.rotation.eulerAngles.z < 358) && (transform.rotation.eulerAngles.z > 2)) || ((transform.rotation.eulerAngles.y < 358) && (transform.rotation.eulerAngles.y > 2)))
        {
            // put character to default rotation
            transform.rotation = Quaternion.identity;
        }
    }

    public void SwitchDimensions()
    {
        if(inTwoD)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        }


        inTwoD = !inTwoD;
    }

    public bool OnGround()
    {
        //ADDED IN BUFFER 8-11 (the +- 0.05 thing is what to change if you don't like it)
        return (Mathf.RoundToInt(transform.position.y) <= transform.position.y + 0.05 && Mathf.RoundToInt(transform.position.y) >= transform.position.y - 0.05);

    }

    private bool checkMoveable(Vector3 loc)
    {
        
        foreach (GameObject obj in world)
        {
            if (obj.GetComponent<GridControl>().position == loc + transform.position && obj.GetComponent<GridControl>().moveable == false)
            {
                return false;
            }

            

        }

   

        return OnGround();
    }
}
