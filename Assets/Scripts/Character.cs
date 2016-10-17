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
    public string characterName;
    public Activity currentActivity;

    //PLAYER PREFERENCES
    public List<string> likedActivities, dislikedActivities;

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
            }
            else
            {
                dislikedActivities.Add(worldController.activities[i].activityName);
            }
        }
    }

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

    //MOVEMENT
    public void MoveTowardsActivity(string activityName)
    {
        if (activityName == "Base")
        {
            aiCharacter.target = worldController.baseActivity.GetAvailableSlot();
            mood.text = "";
        }
        else
        {
            for (int i = 0; i < worldController.activities.Count; i++)
            {
                if (activityName == worldController.activities[i].activityName)
                {
                    aiCharacter.target = worldController.activities[i].GetAvailableSlot();
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
