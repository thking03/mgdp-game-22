using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLayerController : MonoBehaviour
{

    public bool isInteractable = false;

    public GameObject musicLayer;
    public AudioSource music;
    public int audionum;
    public List<int> DictLens = new List<int>();
    public int audioID; // this needs to be set manually inside the Unity Editor

    public Dictionary<int, AudioClip> TopAudios = new Dictionary<int, AudioClip>();
    public Dictionary<int, AudioClip> BottomAudios = new Dictionary<int, AudioClip>();
    public AudioClip Cdots, Cpad, Fdots, Fpad;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initialized");

        // Determine what layer should be affected & define music objects
        if (audioID == 1)
        {
            musicLayer = GameObject.Find("TopLayerMusic");
        }
        else if (audioID == 0)
        {
            musicLayer = GameObject.Find("BottomLayerMusic");
        }

        music = musicLayer.GetComponent<AudioSource>();

        // Create the dictionaries
        TopAudios.Add(0, Cdots);
        TopAudios.Add(1, Fdots);
        BottomAudios.Add(0, Cpad);
        BottomAudios.Add(1, Fpad);

        // Initialize numbers
        audionum = 0;
        DictLens.Add(BottomAudios.Count);
        DictLens.Add(TopAudios.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.Space))
        {
            if (music.isPlaying) // If music is currently waiting, wait for the clip to end.
            {
                MusicTransitioner(music);
            }

            if (audionum != DictLens[audioID])
            {
                if (audioID == 0)
                {
                    music.clip = BottomAudios[audionum];
                    Debug.Log("Audio switched to " + BottomAudios[audionum].ToString());
                }
                if (audioID == 1)
                {
                    music.clip = TopAudios[audionum];
                    Debug.Log("Audio switched to " + TopAudios[audionum].ToString());
                }
                music.Play();
            }
            else
            {
                music.Stop();
            }
            audionum = (audionum + 1) % (DictLens[audioID] + 1);
        }
    }

    // Adding in the Collider triggers
    private void OnTriggerEnter(Collider other)
    {
        // currently not detecting if the object entering the Collider is the player
        isInteractable = true;
        Debug.Log("Entered an interactable area of MusicLayerController");
    }
    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
        Debug.Log("Exited an interactable area of MusicLayerController");
    }

    // Adding in transition timers for elements
    public void MusicTransitioner(AudioSource source)
    {
        float audtime = source.time; // Get current position in the track
        float tracktime = source.clip.length; // Get the length of the track
        int sleeptime = (int)Mathf.Round(1000 * (tracktime - audtime));
        Debug.Log("Pausing " + sleeptime.ToString() + " milliseconds.");
        System.Threading.Thread.Sleep(sleeptime);
    }
}
