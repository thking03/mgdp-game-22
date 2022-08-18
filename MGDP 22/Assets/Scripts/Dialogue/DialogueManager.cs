using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public string[] sentences;
    public int[] speaker;
    public Sprite[] speakerSprite;
    public string[] speakerName;
    private int turn = -1;

    public Text characterName;
    public Text dialogue;
    public Image playerSprite;

    private string txt;

    public float typeSpeed;



    public void nextTurn()
    {
        turn++;
        txt = sentences[turn];
        characterName.text = speakerName[speaker[turn]];
        playerSprite.sprite = speakerSprite[speaker[turn]];
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
}
