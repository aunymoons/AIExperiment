using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Activity : MonoBehaviour
{
    //REFERENCE
    public WorldController worldController;
    public string activityName;

    //STATES
    public bool isDone, activityVisited, hasPlayers, isCrucial, isMainActivity;

    //SLOTS
    public int capacity;
    public List<Transform> characterSlots;
    public List<Character> characterReferences;

    //VOTES
    public int positiveVotes, negativeVotes;

    //UI
    public Text resultText;

    /*---MAIN METHODS---*/

    //START
    void Start()
    {
        ResetSlots();
    }

    //UPDATE
    void Update()
    {

    }

    /*---STATES---*/

    //RESET ACTIVITY STATE
    public void ResetActivity()
    {
        ResetSlots();
        ResetVotes();
    }

    //RESET SLOTS
    void ResetSlots()
    {
        //Resets Capacity
        capacity = characterSlots.Count;

        hasPlayers = false;
    }

    //RESET VOTES
    void ResetVotes()
    {
        positiveVotes = 0;
        negativeVotes = 0;
    }

    /*---CALLED BY CHARACTERS---*/

    //RETURN AVAILABLE SLOT TRANSFORM
    public Transform GetAvailableSlot(Character character)
    {
        //If activity hasnt been visited, flag as visited
        if (!activityVisited) activityVisited = true;
        //If activity didnt have any players, flag as containing players
        if (!hasPlayers) hasPlayers = true;
        //Decrease Capacity
        capacity -= 1;

        //AddCharacter
        characterReferences.Add(character);

        //Return transform
        return characterSlots[capacity];
    }

    //PERFORM ACTIVITY
    public void PerformActivity(bool isPlayer)
    {
        //If activity has players
        if (hasPlayers)
        {
            //If main player is performing it
            if (isPlayer)
            {
                isDone = true;
                resultText.text = "DONE";
            }
            //if NPC is performing it
            else
            {
                if (GetProbability())
                {
                    isDone = true;
                    resultText.text = "DONE";
                }
            }

        }
    }

    //GET ACCORDING DIALOGUE
    public void GetDialogue()
    {
        string fullDialogue = "";

        for (int i = 0; i < characterReferences.Count; i++)
        {
            if (!characterReferences[i].isMain)
            {
                fullDialogue = fullDialogue + " " + characterReferences[i].CheckDialogue();
            }
            if (characterReferences[i].isMain)
            {
                isMainActivity = true;
            }
        }

        worldController.dialogueText.text = fullDialogue;
        
        //Resets players
        characterReferences.Clear();
    }

    //DETERMINE RESULT BASED ON PLAYERS
    bool GetProbability()
    {
        //Create random bet
        int randomBet = Random.Range(0, 2);
        //Set total amount of bets
        int totalBets = 4;
        //calculate probability
        int probabilityOfSuccess = positiveVotes + randomBet;

        //If probability of success is larger than half of the total bets return true (WIN)
        if (probabilityOfSuccess > totalBets / 2)
        {
            return true;
        }
        //If probability of success is equal to half of the total bets, toss a coin (TIE)
        else if (probabilityOfSuccess == totalBets / 2)
        {
            int coinToss = Random.Range(0, 2);
            if (coinToss == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //If the probability of success is less than half of the total bets, return false (LOOSE)
        else
        {
            return false;
        }

    }
}
