using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public string thisSpeaker; // who this is assigned to
    public List<string> accesibleConvs = new List<string>(); // which convos can be accessed by this speaker
    public LoadScript.Conversation stagedConv = new LoadScript.Conversation(); // conversation currently playing
    public bool convlock = false;
    public int convnum = 0;

    public Dictionary<string,Sprite> speakerSprite;
    private int turn = -1;

    public Text characterName;
    public Text dialogue;
    public Image playerSprite;

    private string txt;

    public float typeSpeed;
    public bool isInteractable;

    void Start()
    {
        discoverConvos();
    }

    private void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.Space) && !convlock)
        {
            print(accesibleConvs.Count);
            getConvo(accesibleConvs[convnum]);
            convlock = true;
            nextTurn(); // run first part of convo
        }

        if (convlock && Input.GetKeyDown(KeyCode.Space))
        {
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
        txt = stagedConv.dialogueLines[turn].dialogueText;
        Debug.Log(txt);
        print(stagedConv.dialogueLines[turn].speaker);
        // characterName.text = stagedConv.dialogueLines[turn].speaker;
        // playerSprite.sprite = speakerSprite[stagedConv.dialogueLines[turn].speaker];
        StartCoroutine(type());
    }

    IEnumerator type()
    {
        dialogue.text = "";
        foreach (char letter in txt.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
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
        Debug.Log("Exited interactable area of Speaker");
    }
}
