using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LoadScript : MonoBehaviour
{
    public string jsonFile;
    public Dictionary<string,Conversation> scenedialogue = new Dictionary<string, Conversation>();
    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        dynamic jsonObj = JsonConvert.DeserializeObject(jsonFile); // creates a loopable Json object
        foreach (var conv in jsonObj)
        {
            Conversation currentConv = new Conversation();
            string title = conv.conversation; // need var type declared specifically when adding to Dictionary
            currentConv.convTitle = title;
            currentConv.convID = conv.convID;
            currentConv.initiator = conv.initiator;
            foreach (var line in conv.dialogueLines)
            {
                if (line["lineID"] != null)
                {
                    int lID = line["lineID"]; // see above w/ adding to dict.
                    dialogueLine dLine = new dialogueLine();
                    dLine.dialogueText = line["Text"];
                    dLine.dialogueText.Replace("/Player/", playerName);
                        // replace /Player/ with the Player's name
                    dLine.speaker = line["Speaker"];
                    currentConv.dialogueLines.Add(lID, dLine);
                }
                else
                {
                    Debug.Log("Error finding lineID property. Make sure that JSON string is up to date.");
                }
            }
            // need to add lineorder loading
            lineOrders lOrders = new lineOrders();
            if (conv.lineorders.type == "default-dialogue") // this property isn't defined if it's not true
            {
                foreach (var entry in conv.lineorders)
                {
                    if (entry.Key != "type")
                    {
                        lOrders.villagerLines.Add(entry.Key, entry.Value);
                    }
                }
                
            }
            else
            {
                foreach (var entry in conv.lineorders.orders)
                {
                    List<int> order = new List<int>();
                    foreach (var num in entry)
                    {
                        int linenum = num;
                        order.Add(linenum);
                    }
                    lOrders.orderlist.Add(order);
                }

            }
            currentConv.lineorders = lOrders;
            scenedialogue.Add(title, currentConv); // add to scenedialogue Dictionary
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
        public lineOrders lineorders;
    }

    public class dialogueLine
    {
        public string dialogueText;
        public string speaker;
    }
    public class lineOrders
    {
        public bool isDefault; // set to True if the convo initiated by any townsperson

        // regular list of orderlines
        public List<List<int>> orderlist = new List<List<int>>();

        // special isDefault case
        public Dictionary<string, List<int>> villagerLines = new Dictionary<string, List<int>>();
    }
}
