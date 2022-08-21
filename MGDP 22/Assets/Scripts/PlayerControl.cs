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
    [SerializeField]
    public static float minInteractDistance = 1.5f;

    private GameObject[] world;

    private GameObject closestInteract()
    {
        GameObject ret = null;
        float dist = minInteractDistance;

        foreach (GameObject obj in world)
        {
            GridControl grid = obj.GetComponent<GridControl>();
            if (grid.isInteractable)
            {
                grid.interactSymbol.SetActive(false);
            }
        }

        foreach (GameObject obj in world)
        {
            GridControl grid = obj.GetComponent<GridControl>();
            Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.y);
            Vector3 theyPos = new Vector3(obj.transform.position.x, 0, obj.transform.position.y);

            if(grid.isInteractable && Vector3.Distance(myPos, theyPos) < dist)
            {
                dist = Vector3.Distance(transform.position, obj.transform.position);
                ret = obj;
            } 
        }

        return ret;
    }

    private void Start()
    {
        world = GameObject.FindGameObjectsWithTag("Grid");
    }

    private void Update()
    {
        Debug.Log(OnGround());
        if (inTwoD)
        {
            //Interactability
            GameObject interact = closestInteract();
            if(interact != null)
            {
                interact.GetComponent<GridControl>().interactSymbol.SetActive(true);
                if(Input.GetButtonDown("Interact"))
                {
                    interact.GetComponent<GridControl>().InteractClick();
                }
            }


            //Movement
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
            if(Input.GetButtonDown("Down") && checkMoveable(new Vector3(0, 0, -1)))
            {
                transform.position += new Vector3(0, 0, -1);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
            if (Input.GetButtonDown("Up") && checkMoveable(new Vector3(0, 0, 1)))
            {
                transform.position += new Vector3(0, 0, 1);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
            if (Input.GetButtonDown("Right") && checkMoveable(new Vector3(1, 0, 0)))
            {
                transform.position += new Vector3(1, 0, 0);
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
            if (Input.GetButtonDown("Left") && checkMoveable(new Vector3(-1, 0, 0)))
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

        foreach (GameObject obj in world)
        {
            GridControl grid = obj.GetComponent<GridControl>();
            if (grid.isInteractable)
            {
                grid.interactSymbol.SetActive(false);
            }
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


            if (obj.GetComponent<GridControl>().position == loc + transform.position && obj.GetComponent<GridControl>().moveable == true)
            {
                if(obj.GetComponent<GridControl>().checkMoveable(loc, world) == false)
                {
                    return false;
                } else if(OnGround())
                {
                    if (obj.GetComponent<GridControl>().button == false)
                    {
                        obj.transform.position = obj.transform.position + loc;
                        obj.GetComponent<GridControl>().UpdatePosition();
                    } else
                    {
                        obj.GetComponent<GridControl>().ButtonClick();
                    }
                }
            }
        }

        if((transform.position + loc).z < -2 || (transform.position + loc).z > 25 || (transform.position + loc).x < -14)
        {
            return false;
        }

   

        return OnGround();
    }
}
