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
    "LOOKOUT" : 8,
    "SYSTEM" : 9
}

objectlist = []
idcounter = 0
inconvo = False
nextlinedialogue = False
convdetect = False
linecounter = 0

if sys.argv[3]:
    with open(sys.argv[3]) as f:
        orderdict = json.load(f)
    lineorders = True

print(orderdict)

with open(sys.argv[1]) as textfile:
    for line in textfile:
        line = line[:-1] # Drop the /n
        if "-- -- --" in line: # Conversation titles are indicated by "-- -- --" (opened and closed by them)
            convdetect = not convdetect # flips the bool
            continue
        if convdetect: # indicator of conversation
            if inconvo == True:
                if lineorders:
                    convobj["lineorders"] = orderdict[convobj["conversation"]]
                objectlist.append(convobj)
            convobj = {} # Initialize the conversation object as a dict
            convobj["dialogueLines"] = [] # Initialize dialoguelines as a list
            convobj["conversation"] = line # drop the first two characters
            convobj["convID"] = idcounter
            idcounter += 1
            inconvo = True
            foundinit = False
        if line in playerids.keys(): # allows "stage directions" to be ignored & keeps subconvos in one object
            print("Hit!")
            speaker = line
            if foundinit == False:
                convobj["initiator"] = line
                foundinit = True
            nextlinedialogue = True
            continue # proceeds to the next step of the for loop & skips everything below
        if nextlinedialogue == True:
            convobj["dialogueLines"].append({
                linecounter : line,
                "Speaker" : speaker
                })
            nextlinedialogue = False
            linecounter += 1
        if "~" in line: # "~" indicate the end of the .txt file
            if lineorders:
                convobj["lineorders"] = orderdict[convobj["conversation"]]
            objectlist.append(convobj)
            print("Ended!")

### Code to get lineorders from a different file
# Find convo name & then have the different lineorders
# Add the different lineorders into the dictionary as a dictionary
# Keep the names of the lineorders.

# Write the .json file
with open(sys.argv[2], "w") as outfile:
    json.dump(objectlist, outfile)