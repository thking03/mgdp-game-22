using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanChapters : MonoBehaviour
{
    public GameObject player;
    public float initialY;
    public float camY;
    public float startLevelX;

    public float camInSpeed = .2f;
    public bool hasPanned = false;

    public bool readyToStart = false;

    // Start is called before the first frame update
    void Start()
    {
        startLevelX = 186;
        initialY = player.transform.position.y + 3;

        Camera.main.transform.position = new Vector3(startLevelX, initialY + 3, -10);
        Camera.main.orthographicSize = 8;
        

        
        //initialize camera position
        //might need to look at changing z depending how we want to design the game (might need to be somewhat player-dependent? idk though)
        //Camera.main.transform.position = new Vector3(player.transform.position.x, initialY, -10);
        

    }

    IEnumerator panning(float time)
    {

        while (Camera.main.transform.position.x > player.transform.position.x)
        {
            Camera.main.transform.position += Vector3.left * camInSpeed;
            if(Camera.main.orthographicSize >= 5)
            {
                Camera.main.orthographicSize -= 0.01f;
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 0.01f, Camera.main.transform.position.z);
            }
            yield return new WaitForSeconds(time);

        }

        readyToStart = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        if(!hasPanned)
        {
            StartCoroutine(panning(0.000000000001f));
            hasPanned = true;
        }

        if(readyToStart)
        {
            if (player.transform.position.y <= initialY)
            {
                camY = initialY;
            }
            else
            {
                camY = player.transform.position.y;
            }

            if (Camera.main.orthographic)
            {
                //vals for mathf.clamp determined by boundaries of scene, rn (8-11) set so that it's boundaries +- 9ish
                Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, 4.5f, 190f), camY, -10);
            }
            else
            {
                Camera.main.transform.position = new Vector3(player.transform.position.x - (10 * Mathf.Tan(70.0f)), camY, player.transform.position.z - 10);
            }
        }

        
        
    }
}
