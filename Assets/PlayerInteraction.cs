using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject InteractObject = null;

    public GameObject SpeechBubble;

    public GameObject ChestImage;
    public GameObject HeartImage;

    private ObjectInteraction InteractScript = null;

    public int CurrentQuestStage = 0;
    public int CurrentPickupQuestStage = 0;
    public int QuestAmount;
    public bool InfoUp;
    private float TimePassed;
    private float DespawnTimer = 2f;
    public Text infoText;
    public Text CoinText;
    private int CoinNumber = 0;
    private bool Won;

    public GameObject VictoryObject;


    void Update()
    {
        if (InfoUp)
        {
            if (TimePassed >= DespawnTimer)
            {
                InfoUp = false;
                infoText.text = null;
                TimePassed = 0f;
            }
            else
            {
                TimePassed = TimePassed + Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Won != true)
            {
                if (InteractObject != null)
                {
                    InteractScript.Interaction();
                }
            } else
            {
                SceneManager.LoadScene(0);
            }
        }

        if(CurrentQuestStage >= QuestAmount)
        {
            Debug.Log("INSERT VICTORY TEXT HERE");
        }
    }

    public void AddCoin()
    {
        CoinNumber += 1;
        CoinText.text = " x" + CoinNumber;
    }

    public void Victory()
    {
        VictoryObject.SetActive(true);
        Won = true;
    }
    public void CheckDialogueCondition()
    {
        if(InteractScript.VictoryNPC == true)
        {
            if (CurrentQuestStage >= InteractObject.GetComponent<ObjectInteraction>().QuestID)
            {
                Victory();
            }
        }
    }
    public void AddToInventory(string name)
    {
        if(name == "Chest")
        {
            ChestImage.SetActive(true);
        } else if (name == "Heart")
        {
            HeartImage.SetActive(true);
        }
    }

    public void RemoveFromInventory(string name)
    {
        if (name == "Chest")
        {
            ChestImage.SetActive(false);
        }
        else if (name == "Heart")
        {
            HeartImage.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            InteractObject = other.gameObject;
            InteractScript = other.gameObject.GetComponent<ObjectInteraction>();
            SpeechBubble.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            InteractObject = collision.gameObject;
            InteractScript = collision.gameObject.GetComponent<ObjectInteraction>();
            SpeechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(InteractObject.GetComponent<ObjectInteraction>().info == true)
        {
            InteractObject.GetComponent<ObjectInteraction>().EndInfo();
        }
        InteractObject = null;
        InteractScript = null;
        SpeechBubble.SetActive(false);
    }

    public void ProgressMainQuest()
    {
        CurrentQuestStage += 1;
    }

    public void ProgressPickupQuest()
    {
        CurrentPickupQuestStage += 1;
    }
}
