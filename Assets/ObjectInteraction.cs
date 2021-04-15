using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    [Header("Interaction Type")]
    public bool pickup;
    public bool info;
    public string message;

    public bool KeyItem;
    public string KeyItemMessage;

    public PlayerInteraction Player;

    private Text infoText;

    public bool dialogue;
    public bool useBasicDialogue;
    [TextArea]
    public string[] FirstDialogue;
    [TextArea]
    public string[] SecondDialogue;
    [TextArea]
    public string[] ThirdDialogue;
    [TextArea]
    public string[] PartTwoDialogue;
    [TextArea]
    public string[] PartTwoRewardDialogue;

    [TextArea]
    public string[] BasicDialogue;

    public int QuestID;
    public bool twoPartDialogue;
    public int PickupQuestID;

    public float DespawnTimer = 2f;
    private float TimePassed = 0f;
    private bool InfoUp = false;
    public bool InitiatePickUpQuest = false;
    public bool EnableChest = false;
    public bool DisableChest = false;
    public bool EnableHeart = false;
    public bool DisableHeart = false;
    public bool Coin = false;

    public bool VictoryNPC;
    



    private void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
    }
    public void Interaction()
    {
        if (pickup)
        {
            Pickup();
        } else if (info)
        {
            Info();
            TimePassed = 0f;
        } else if (dialogue)
        {
            Dialogue();
        }

        

        if(Coin == true)
        {
            Player.AddCoin();
        }
        
    }

    private void Update()
    {
        if (InfoUp)
        {
            if(TimePassed >= DespawnTimer)
            {
                InfoUp = false;
                infoText.text = null;
            } else
            {
                TimePassed = TimePassed + Time.deltaTime;
            }
        }

        
    }

    private void Pickup()
    {
        
        if (KeyItem)
        {
            if (PickupQuestID == Player.CurrentPickupQuestStage)
            {
                Player.ProgressPickupQuest();
                infoText.text = KeyItemMessage;
                Player.InfoUp = true;
                this.gameObject.SetActive(false);
                if (EnableChest == true)
                {
                    Player.AddToInventory("Chest");
                }
                else if (EnableHeart == true)
                {
                    Player.AddToInventory("Heart");
                }
               
            } else
            {
                infoText.text = "I shouldn't take this right now.";
                InfoUp = true;
            }
        } else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    private void Info()
    {
        if (Player.CurrentQuestStage == QuestID)
        {
            if (KeyItem)
            {
                Player.ProgressMainQuest();
            }
            infoText.text = message;
            InfoUp = true;
        } else
        {
            infoText.text = message;
            InfoUp = true;
        }
    }

    public void EndInfo()
    {
        InfoUp = false;
        infoText.text = null;
    }

    private void Dialogue()
    {
        if(Player.CurrentQuestStage == QuestID)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(SecondDialogue);
            if (InitiatePickUpQuest == true)
            {
                Player.ProgressPickupQuest();
            }
            Player.ProgressMainQuest();
        } else if (Player.CurrentQuestStage < QuestID) {
            FindObjectOfType<DialogueManager>().StartDialogue(FirstDialogue); 
        }
        else if(twoPartDialogue == true)
        {
            if (Player.CurrentPickupQuestStage < PickupQuestID)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(PartTwoDialogue);
                
            } else if (Player.CurrentPickupQuestStage == PickupQuestID)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(PartTwoRewardDialogue);
                Player.ProgressPickupQuest();
                Player.ProgressMainQuest();
                 if (DisableChest == true)
                {
                    Player.RemoveFromInventory("Chest");
                }
                else if (DisableHeart == true)
                {
                    Player.RemoveFromInventory("Heart");
                }
                
            }
            else if (Player.CurrentPickupQuestStage > PickupQuestID)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(ThirdDialogue);
            }
        }
        else if(Player.CurrentQuestStage > QuestID) { 
            FindObjectOfType<DialogueManager>().StartDialogue(ThirdDialogue); 
        }
    }
}
