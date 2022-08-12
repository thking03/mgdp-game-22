using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public bool isInteractable = false;

    public GameObject backMusic;
    public AudioSource music;
    public int audionum;

    public Dictionary<int, AudioClip> audios = new Dictionary<int, AudioClip>();
    public AudioClip Cdots, Cpad, Fdots, Fpad, Improv;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initialized");
        backMusic = GameObject.Find("BackgroundMusic");
        music = backMusic.GetComponent<AudioSource>();
        audionum = 0;
        audios.Add(0, Improv);
        audios.Add(1, Cdots);
        audios.Add(2, Cpad);
        audios.Add(3, Fdots);
        audios.Add(4, Fpad);
    }

    // Update is called once per frame
    void Update()
    {
        if(isInteractable && Input.GetKeyDown(KeyCode.Space))
        {
            // This method works at switching inside the dictionary but sound currently isn't playing.
            audionum = (audionum + 1) % 5;
            music.clip = audios[audionum];
            Debug.Log("Audio switched to"+ audios[audionum].ToString());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            isInteractable = true;
            Debug.Log("Entered interactable area");
    }

    private void OnTriggerExit(Collider other)
    {
            isInteractable = false;
            Debug.Log("Exited interactable area");
    }
}

