using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public GameObject player;
    public float initialY;
    public float camY;

    // Start is called before the first frame update
    void Start()
    {

        initialY = player.transform.position.y + 3;
        //initialize camera position
        //might need to look at changing z depending how we want to design the game (might need to be somewhat player-dependent? idk though)
        Camera.main.transform.position = new Vector3(player.transform.position.x, initialY, -10);
        

    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y <= initialY)
        {
            camY = initialY;
        }
        else
        {
            camY = player.transform.position.y;
        }

        if(Camera.main.orthographic)
        {
            //vals for mathf.clamp determined by boundaries of scene, rn (8-11) set so that it's boundaries +- 9ish
            Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x,-6f,159f), camY, -10);
        }
        else
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x - (10 * Mathf.Tan(70.0f)), camY, player.transform.position.z -10);
        }
        
    }
}
