#!/usr/bin/env p

### Arguments should be inputted (txtfile, .json file name)
### Need to implement the lineorder part later on.

import json # Use to write to .json
import sys  # Use so that readtxt.py can be run on commandline with arguments

playerids = {
    "BENNY" : 0,
    "CHARLIE" : 1,
    "ALEX" : 2,
    "MAX" : 3,
    "TYLER" : 4,
    "TOMMY": 5,
    "THADDEUS": 6,
    "PLAYER" : 7,
    "PLAYER (thought)" : 8,
    "LOOKOUT" : 9,
    "SYSTEM" : 10
}

objectlist = []
idcounter = 0
inconvo = False
nextlinedialogue = False
linecounter = 0

with open(sys.argv[1]) as textfile:
    for line in textfile:
        line = line[:-1] # Drop the /n
        if "%" in line: # indicator of conversation
            if inconvo == True:
                objectlist.append(convobj)
            convobj = {} # Initialize the conversation object as a dict
            convobj["dialogueLines"] = [] # Initialize dialoguelines as a list
            convobj["conversation"] = line[2:] # drop the first two characters
            convobj["convID"] = idcounter
            idcounter += 1
            inconvo = True
            foundinit = False
        if line in playerids.keys(): # search for name in playerids
            print("Hit!")
            speaker = line
            if foundinit == False:
                convobj["initiator"] = line
                foundinit = True
            nextlinedialogue = True
            continue
        if nextlinedialogue == True:
            convobj["dialogueLines"].append({
                linecounter : line,
                "Speaker" : speaker
                })
            nextlinedialogue = False
            linecounter += 1
            print(linecounter)
        if "~" in line: # "~" indicate the end of the .txt file
            objectlist.append(convobj)
            print("Ended!")

# Write the .json file
with open(sys.argv[2], "w") as outfile:
    json.dump(objectlist, outfile)

"""
Eventually we want an object like 
{
    "conversation" : "Name"
    "convID" : #
    "initiator" : playerID
    "dialogueLines" : [
        {lineID : "line 1"
        Speaker : "Name"},
        {lineID : "line 2"
        Speaker : "Name"},
        ...
    ]
    "lineOrder" : [lineID, lineID, ...]
}
for every conversation in the script
"""