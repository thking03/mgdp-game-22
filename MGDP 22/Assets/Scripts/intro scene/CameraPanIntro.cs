using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanIntro : MonoBehaviour
{
    public GameObject player;
    public GameObject towerBlock;
    public GameObject tower2Block;
    public GameObject tower3Block;
    public float initialY;
    public float camY;

    public static bool firstLit = false;
    public static bool secondLit = false;
    public static bool thirdLit = false;
    //public static int numLit = 0;

    // Start is called before the first frame update
    void Start()
    {

        
        Camera.main.orthographicSize = 4f;
        initialY = player.transform.position.y + 2;
        //initialize camera position
        //might need to look at changing z depending how we want to design the game (might need to be somewhat player-dependent? idk though)
        Camera.main.transform.position = new Vector3(player.transform.position.x, initialY, -10);
        

    }

    // Update is called once per frame
    void Update()
    {
        float currentY = Camera.main.transform.position.y;

        if(player.transform.position.y <= initialY + 1f)
        {
            camY = currentY;
        }
        else
        {
            camY = player.transform.position.y;
        }

        if(Camera.main.orthographic)
        {
            //vals for mathf.clamp determined by boundaries of scene
            Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x,-1f,14f), camY, -10);
        }
        else
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x - (10 * Mathf.Tan(70.0f)), camY, player.transform.position.z -10);
        }

        if ((Vector3.Distance(player.transform.position, towerBlock.transform.position) <= 2) && !firstLit)
        {
            firstLit = true;
            if(Camera.main.orthographic)
            {
                while(Camera.main.orthographicSize < 8)
                {
                    Camera.main.orthographicSize += 0.00001f;
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 0.00001f, Camera.main.transform.position.z);
                }
            }


        }

        if ((Vector3.Distance(player.transform.position, tower2Block.transform.position) <= 2) && !secondLit)
        {
            secondLit = true;
        }

        if ((Vector3.Distance(player.transform.position, tower3Block.transform.position) <= 2) && !thirdLit)
        {
            thirdLit = true;
        }

    }
}
