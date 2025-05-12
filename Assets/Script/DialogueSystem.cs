using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public List<string> dialogueList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        GetDialogueOptions();
    }

    public void GetDialogueOptions()
    {
        dialogueList.AddRange(new string[]
        {
            "Oh, a flower, that's pretty",
            "WHY DO YOU HAVE THAT, GET AWAY FROM ME",
            "I want to hold that tungsten cube... actually on second thought I don't",
            "Hehe... FISH!",
            "Oh hey it's that thing from that game, I WANT IT!",
            "Alright fine, I'll hold it"
        }   
        );


    }
}
