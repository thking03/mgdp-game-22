using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LoadScript : MonoBehaviour
{
    public string jsonFile;
    public Dictionary<string,Conversation> scenedialogue = new Dictionary<string, Conversation>();

    // Start is called before the first frame update
    void Start()
    {
        dynamic jsonObj = JsonConvert.DeserializeObject(jsonFile); // creates a loopable Json object
        foreach (var conv in jsonObj)
        {
            Debug.Log(conv.conversation);
            Conversation currentConv = new Conversation();
            currentConv.convTitle = conv.conversation;
            currentConv.convID = conv.convID;
            currentConv.initiator = conv.initiator;
            foreach (var line in conv.dialogueLines)
            {
                int lID = line.lineID;
                dialogueLine dLine = new dialogueLine();
                dLine.dialogueText = line.Text;
                dLine.speaker = line.Speaker;
                currentConv.dialogueLines.Add(
                    lID, dLine);
            }
            // need to add lineorder loading

            scenedialogue.Add(conv.conversation, currentConv); // add to scenedialogue Dictionary
            Debug.Log(currentConv);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing because there's no need to re-load dialogue
    }

    public class Conversation
    {
        public Dictionary<int,dialogueLine> dialogueLines = new Dictionary<int, dialogueLine>();
        public string convTitle;
        public int convID;
        public string initiator;
        public dynamic lineorders;
    }

    public class dialogueLine
    {
        public string dialogueText;
        public string speaker;
    }
    public class lineorders
    {
        public bool isDefault; // set to True if the convo initiated by any townsperson
        public List<List<int>> orderlist;
        public List<int> order;
    }
}
