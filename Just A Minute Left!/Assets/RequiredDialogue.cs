using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredDialogue : MonoBehaviour
{
    public List<DialogueText> PrimaryDialogue;
    public List<DialogueText> SecondaryDialogue;
    public List<DialogueText> RewardDialogue;
    public List<DialogueText> SecondaryRewardDialogue;

    public bool SpokenTo;
    public bool Rewarded;
    public string wantedItem;
    public string rewardedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
