using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueText
{
    public float readSpeed = 20;
    public string text;
    public float readWait = 3;
    public string speaking;
}


public class Dialogue : MonoBehaviour
{

    GameObject timeText;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;
    public List<DialogueText> texts = new List<DialogueText>();
    public GameObject NPC;

    public int textIndex;
    public int characterIndex;

    void OnEnable()
    {
        GameObject.Find("Player").GetComponent<Timer>().paused = true;
        timeText = GameObject.Find("Time");
        if (timeText != null)
        {
            timeText.SetActive(false);
        }
        GameObject.Find("Player").GetComponent<Interact>().interacting = true;
        textIndex = 0;
        characterIndex = 0;
        ctext = "";
    }

    string ctext = "";
    float textwait = 0;
    float characterWait = 0;
    void Update()
    {

        if (textwait > 0)
        {
            textwait -= Time.deltaTime;
            return;
        }
        if (characterWait > 0)
        {
            characterWait -= Time.deltaTime;
            return;
        }
        if (textIndex >= texts.ToArray().Length)
        {
            dialogueBox.SetActive(false);
            textBox.text = "";
            if (NPC)
            {
                if (NPC.GetComponent<BasicMovement>())
                {
                    NPC.GetComponent<BasicMovement>().enabled = true;
                }
                if (NPC.GetComponent<OnInteractDialogue>())
                {
                    NPC.GetComponent<OnInteractDialogue>().SpokenTo = true;
                }
                else if (NPC.GetComponent<RequiredDialogue>())
                {
                    NPC.GetComponent<RequiredDialogue>().SpokenTo = true;
                }
            }
            if (timeText != null)
            {
                timeText.SetActive(true);
            }
            GameObject.Find("Player").GetComponent<Interact>().interacting = false;
            GameObject.Find("Player").GetComponent<Movement>().enabled = true;
            GameObject.Find("Player").GetComponent<Timer>().tf -= 3;
            GameObject.Find("Player").GetComponent<Timer>().paused = false;
            return;
        }
        DialogueText textObj = texts[textIndex];
        
        ctext += textObj.text[characterIndex];
        if (textObj.speaking != "")
        {
            textBox.text = textObj.speaking + ": " + ctext;
        }
        else
        {
            textBox.text = ctext;
        }
        characterIndex++;
        characterWait = 1 / textObj.readSpeed;
        if (characterIndex >= textObj.text.Length)
        {
            textIndex++;
            ctext = "";
            characterIndex = 0;
            textwait = textObj.readWait;
        }
    }
}
