using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public string thisSpeaker; // who this is assigned to
    public List<string> accesibleConvs = new List<string>(); // which convos can be accessed by this speaker
    public LoadScript.Conversation stagedConv = new LoadScript.Conversation(); // conversation currently playing
    public bool convlock = false;
    public bool isTyping = false;
    public int convnum = 0;

    public Dictionary<string,Sprite> speakerSprite;
    private int turn = -1;

    public GameObject textDisplay;
    public GameObject nameDisplay;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogue;
    public List<int> order;
    public int whichOrder = 0; // can be set to not 0 if other conditions are met

    private string txt;

    public float typeSpeed;
    public bool isInteractable;

    void Start()
    {
        discoverConvos();

        // initialize the text display
        textDisplay = GameObject.Find("TextDisplay"); // get the display canvas gameobject
        GameObject dialogueDisplay = textDisplay.transform.GetChild(0).gameObject;
        dialogue = dialogueDisplay.GetComponent<TextMeshProUGUI>();

        // initialize the name display
        GameObject nameDisplay = textDisplay.transform.GetChild(1).gameObject;
        characterName = nameDisplay.GetComponent<TextMeshProUGUI>();

        txt = "";
        characterName.text = "";
        StartCoroutine(type());

    }
    private void Update()
    {
        GC.KeepAlive(dialogue);

        if (isInteractable && Input.GetKeyDown(KeyCode.Space) && !convlock)
        {
            print(accesibleConvs.Count);
            getConvo(accesibleConvs[convnum]);
            order = stagedConv.lineorders.orderlist[whichOrder];
            convlock = true;
            nextTurn(); // run first part of convo
        }
        
        if (convlock && Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            // reinitialize display ??
            textDisplay = GameObject.Find("TextDisplay"); // get the display canvas gameobject
            GameObject dialogueDisplay = textDisplay.transform.GetChild(0).gameObject;
            dialogue = dialogueDisplay.GetComponent<TextMeshProUGUI>();
            nextTurn(); // run next part of convo
        }
    }

    public void discoverConvos()
    {
        foreach (KeyValuePair<string, LoadScript.Conversation> conv in LoadScript.scenedialogue)
        {
            // go through all conversations and see if the speaker matches this speaker
            if (conv.Value.initiator == thisSpeaker)
            {
                accesibleConvs.Add(conv.Value.convTitle);
            }
        }
    }
    public void getConvo(string input)
    {
        foreach (var title in accesibleConvs)
        {
            // go through all conversations and see if this is the right one
            if (title == input)
            {
                stagedConv = LoadScript.scenedialogue[title]; // stage it
            }
        }
    }    
    public void nextTurn()
    {
        turn++;
       try
        {
            int thisturn = order[turn];
            txt = stagedConv.dialogueLines[thisturn].dialogueText;
            Debug.Log(txt);
            characterName.text = stagedConv.dialogueLines[thisturn].speaker;
            StartCoroutine(type());
        }
        catch (KeyNotFoundException) // these exceptions get called when the conversation "completes"
        {
            resetConv();
        }
        catch (ArgumentOutOfRangeException) // these exceptions get called when the conversation "completes" (this one called first)
        {
            resetConv();
        }
    }

    public void resetConv() // call this to reset the conversation
    {
        txt = "";
        characterName.text = "";
        StartCoroutine(type());
        turn = -1;
    }

    IEnumerator type()
    {
        isTyping = true;
        dialogue.text = "";
        foreach (char letter in txt.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // currently not detecting if the object entering the Collider is the player
        isInteractable = true;
        Debug.Log("Entered interactable area of Speaker");
    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
        resetConv();
        Debug.Log("Exited interactable area of Speaker & conversation reset.");
    }
}
