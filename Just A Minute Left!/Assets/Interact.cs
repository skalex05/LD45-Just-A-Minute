using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interact : MonoBehaviour
{
    public Transform InteractDetection;
    public GameObject DialogueBox;
    public Dialogue dialogueIN;
    public Vector2 DrEvilsBase;

    public TextMeshProUGUI InteractionPrompt;
    public List<string> items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool interacting;
    public bool end;

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            
            if (GetComponent<Timer>().timeLeft < 999)
            {
                int xx = Mathf.CeilToInt((999f/7) * Time.unscaledDeltaTime);
                GetComponent<Timer>().timeLeft += xx;
                if (GetComponent<Timer>().timeLeft > 999)
                {
                    GetComponent<Timer>().timeLeft = 999;
                }
            }
        }
        Collider2D col = Physics2D.OverlapCircle(InteractDetection.position, 0.5f, 1 << 10);
        InteractionPrompt.text = "Press E to interact";
        if (col)
        {
            if (col.gameObject.name == "Georgie" || col.gameObject.name == "The Guards" || col.gameObject.name == "The Thugs" || col.gameObject.name == "The Balloon Guy" || col.gameObject.name == "The Clown" || col.gameObject.name == "The Bartender" || col.gameObject.name == "Scar" || col.gameObject.name == "Crazy Jack")
            {
                InteractionPrompt.text = "Press E to Talk to " + col.gameObject.name;
            }
            else if (col.gameObject.name == "Hobo 2")
            {
                InteractionPrompt.text = "Press E to Talk to Crazy Jack's Friend";
            }
            else if (col.gameObject.name == "Clown 2")
            {
                InteractionPrompt.text = "Press E to Talk to The Clown";
            }
            else if (col.gameObject.name == "Money")
            {
                InteractionPrompt.text = "Press E to Pick Up Some Free Money";
            }
            else if (col.gameObject.name == "Tv")
            {
                InteractionPrompt.text = "Press E to Watch TV";
            }
        }
        col = Physics2D.OverlapCircle(InteractDetection.position, 0.5f, 1 << 11);
        if (col)
        {
            if (col.gameObject.name == "Alley" || col.gameObject.name == "Alley1")
            {
                InteractionPrompt.text = "Press E to Go Down The Alley";
            }
            else if (col.gameObject.name == "Outside1" || col.gameObject.name == "Outside2" || col.gameObject.name == "The Special Pipe")
            {
                InteractionPrompt.text = "Press E to Go Back Out";
            }
            else if (col.gameObject.name == "The Bar")
            {
                InteractionPrompt.text = "Press E to Go In The Bar";
            }
        }
        if (!Input.GetButtonDown("Interact") || interacting)
        {
            return;
        }
        col = Physics2D.OverlapCircle(InteractDetection.position, 0.5f, 1 << 12);
        if (col)
        {
            InteractionPrompt.text = "Press E to Unplug THE BUTTON";
        }
        col = Physics2D.OverlapCircle(InteractDetection.position, 0.5f, 1 << 10);//npc
        if (col)
        {
            if (col.transform.childCount > 0)
            {
                if (col.transform.GetChild(0).tag == "Exclamation")
                {
                    col.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            if (col.gameObject.GetComponent<OnInteractDialogue>())
            {
                GetComponent<Movement>().enabled = false;
                if (col.gameObject.GetComponent<BasicMovement>())
                {
                    col.gameObject.GetComponent<BasicMovement>().enabled = false;
                    col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
                dialogueIN.NPC = col.gameObject;
                if (col.gameObject.GetComponent<OnInteractDialogue>().SpokenTo)
                {
                    dialogueIN.texts = col.gameObject.GetComponent<OnInteractDialogue>().SecondaryDialogue;
                }
                else
                {
                    dialogueIN.texts = col.gameObject.GetComponent<OnInteractDialogue>().PrimaryDialogue;
                }
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                DialogueBox.SetActive(true);
            }
            else if (col.gameObject.GetComponent<RequiredDialogue>())
            {
                GetComponent<Movement>().enabled = false;
                if (col.gameObject.GetComponent<BasicMovement>())
                {
                    col.gameObject.GetComponent<BasicMovement>().enabled = false;
                    col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
                dialogueIN.NPC = col.gameObject;
                if (!col.gameObject.GetComponent<RequiredDialogue>().SpokenTo)
                {
                    dialogueIN.texts = col.gameObject.GetComponent<RequiredDialogue>().PrimaryDialogue;
                }
                else
                {
                    if (col.gameObject.GetComponent<RequiredDialogue>().Rewarded)
                    {
                        dialogueIN.texts = col.gameObject.GetComponent<RequiredDialogue>().SecondaryRewardDialogue;
                    }
                    else if (items.Contains(col.gameObject.GetComponent<RequiredDialogue>().wantedItem) || col.gameObject.GetComponent<RequiredDialogue>().wantedItem == "")
                    {
                        dialogueIN.texts = col.gameObject.GetComponent<RequiredDialogue>().RewardDialogue;
                        items.Remove(col.gameObject.GetComponent<RequiredDialogue>().wantedItem);
                        if (col.gameObject.GetComponent<RequiredDialogue>().rewardedItem == "Dr Evil Base")
                        {
                            transform.position = DrEvilsBase;
                            GameObject.Find("Main Camera").transform.position = new Vector3(0, DrEvilsBase.y + 2.5f, -10);
                        }
                        else if (col.gameObject.GetComponent<RequiredDialogue>().rewardedItem == "ElevatorAccess")
                        {
                            col.isTrigger = true;
                        }
                        else if (col.gameObject.GetComponent<RequiredDialogue>().rewardedItem != "")
                        {
                            items.Add(col.gameObject.GetComponent<RequiredDialogue>().rewardedItem);
                        }
                        col.gameObject.GetComponent<RequiredDialogue>().Rewarded = true;
                    }
                    else
                    {
                        dialogueIN.texts = col.gameObject.GetComponent<RequiredDialogue>().SecondaryDialogue;
                    }
                }
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                DialogueBox.SetActive(true);
            }
        }
        col = Physics2D.OverlapCircle(InteractDetection.position, 0.5f, 1 << 11);//place
        if (col)
        {
            transform.position = col.GetComponent<TpLocation>().Location;
            GameObject.Find("Main Camera").transform.position = new Vector3(0, col.GetComponent<TpLocation>().Location.y + col.GetComponent<TpLocation>().CameraOffset, -10);
        }
        col = Physics2D.OverlapCircle(InteractDetection.position, 0.5f, 1 << 12);//Plug Socket (End of the game)
        if (col)
        {
            Time.timeScale = 0;
            end = true;
            GetComponent<Timer>().paused = true;
            GameObject.Find("Win Screen").GetComponent<Animator>().SetTrigger("Win");
        }
    }
}
