using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    //REFERENCES
    WorldController worldController;
    public AICharacterControl aiCharacter;

    //PLAYER DATA
    public bool isMain;
    public string characterName;
    public Activity currentActivity;

    //PLAYER PREFERENCES
    public List<string> likedActivities, dislikedActivities;
    public Dictionary<string, string> likedDialogues, dislikedDialogues;

    //UI
    public Text mood;

    //START
    void Start()
    {
        //FETCH CONTROLLERS
        worldController = FindObjectOfType<WorldController>();
        aiCharacter = GetComponent<AICharacterControl>();

        //OPINIONS
        if (likedActivities == null)
            likedActivities = new List<string>();
        if (dislikedActivities == null)
            dislikedActivities = new List<string>();

        //DIALOGUES
        if (likedDialogues == null)
            likedDialogues = new Dictionary<string, string>();
        if (dislikedDialogues == null)
            dislikedDialogues = new Dictionary<string, string>();

        if (likedActivities.Count == 0 && dislikedActivities.Count == 0)
            DecideOpinions();
    }

    //UPDATE
    void Update()
    {
    }

    //MAIN ACTIONS
    public void CheckSpecificSchedule(string activityName)
    {
        currentActivity = worldController.GetActivityByName(activityName);
        MoveTowardsActivity(currentActivity.activityName);
    }

    public void CheckSchedule()
    {
        currentActivity = worldController.GetActivityByName(worldController.GetRandomActivity());
        MoveTowardsActivity(currentActivity.activityName);
        CheckOpinion(currentActivity.activityName);

    }

    void DecideOpinions()
    {
        int random;
        for (int i = 0; i < worldController.activities.Count; i++)
        {
            random = Random.Range(0, 2);
            if (random == 0)
            {
                likedActivities.Add(worldController.activities[i].activityName);
                likedDialogues.Add(worldController.activities[i].activityName, characterName + " thinks we should SAVE the " + worldController.activities[i].activityName + " module");

            }
            else
            {
                dislikedActivities.Add(worldController.activities[i].activityName);
                dislikedDialogues.Add(worldController.activities[i].activityName, characterName + " thinks we should DESTROY the " + worldController.activities[i].activityName + " module");

            }
        }
    }

    //OPINION AND DIALOGUE
    void CheckOpinion(string currentActivityName)
    {
        for (int i = 0; i < dislikedActivities.Count; i++)
        {
            if (dislikedActivities[i] == currentActivityName)
            {
                currentActivity.negativeVotes += 1;
                mood.text = "HATE";
                return;
            }
        }
        for (int i = 0; i < likedActivities.Count; i++)
        {
            if (likedActivities[i] == currentActivityName)
            {
                currentActivity.positiveVotes += 1;
                mood.text = "LOVE";
                return;
            }
        }
    }

    public string CheckDialogue()
    {
        string result = "";
    
        for(int i = 0; i < likedActivities.Count; i++)
        {
            if (likedActivities[i] == currentActivity.activityName)
            {
                
               likedDialogues.TryGetValue(currentActivity.activityName, out result);
                return result;
            }
        }
        for (int i = 0; i < dislikedActivities.Count; i++)
        {
            if (dislikedActivities[i] == currentActivity.activityName)
            {
                dislikedDialogues.TryGetValue(currentActivity.activityName, out result);
                return result;
            }
        }

        return result;

    }

    //MOVEMENT
    public void MoveTowardsActivity(string activityName)
    {
        if (activityName == "Base")
        {
            aiCharacter.target = worldController.baseActivity.GetAvailableSlot(this);
            mood.text = "";
        }
        else
        {
            for (int i = 0; i < worldController.activities.Count; i++)
            {
                if (activityName == worldController.activities[i].activityName)
                {
                    aiCharacter.target = worldController.activities[i].GetAvailableSlot(this);
                }
            }
        }
    }

    void MakeDecision()
    {

    }

    void ChooseNextAction()
    {

    }
}
