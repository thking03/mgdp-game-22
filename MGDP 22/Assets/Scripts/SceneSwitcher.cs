using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool first = CameraPanIntro.firstLit;
        bool second = CameraPanIntro.secondLit;
        bool third = CameraPanIntro.thirdLit;

        Scene currentScene = SceneManager.GetActiveScene();

        //populate w elif statements that correspond to each scene in the game
        if(currentScene.name == "watchtower intro")
        {
            if(player.transform.position.x > 28 && first && second && third)
            {
                SceneManager.LoadScene(sceneName: "npc town");
            }
        }
    }
}
