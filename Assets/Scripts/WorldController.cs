using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    //REFERENCES
    public Character mainCharacter;
    public Activity baseActivity;
    public List<Activity> activities;
    public List<string> activitiesNames;
    public string lastSelectedActivity, playerSelectedActivity;
    public List<Character> characters;

    //UI
    public GameObject optionCanvas, dialogueCanvas, activityCanvas;
    public Text dialogueText;
    public List<Button> optionButtons;
    public Button baseButton;

    //COUNTERS
    public int turns, turnCounter;

    //STATES
    public bool gameIsOver;

    //SCORE
    public int finalPositivePoints, finalNegativePoints;

    /*---MAIN METHODS---*/

    //START
    void Start()
    {
        //Sets Available activities for the first time
        ResetActivities();
        //Displays dialogue of available options
        ShowOptions();
    }

    //UPDATE
    void Update()
    {
        //Basic inputs
        HandleUserInput();

        //Quit Game
        QuitGame();
    }


    /*---GAMEPLAY METHODS---*/

    //KEYBOARD INPUT
    void HandleUserInput()
    {
        if (Input.GetKeyDown("1"))
        {
            SelectAvailableActivity(0);
        }
        if (Input.GetKeyDown("2"))
        {
            SelectAvailableActivity(1);
        }
        if (Input.GetKeyDown("3"))
        {
            SelectAvailableActivity(2);
        }
        if (Input.GetKeyDown("4"))
        {
            SelectAvailableActivity(3);
        }
        if (Input.GetKeyDown("5"))
        {
            SelectAvailableActivity(4);
        }
        if (Input.GetKeyDown("6"))
        {
            SelectAvailableActivity(5);
        }
        if (Input.GetKeyDown("7"))
        {
            SelectAvailableActivity(6);
        }
        if (Input.GetKeyDown("8"))
        {
            SelectAvailableActivity(7);
        }
        if (Input.GetKeyDown("9"))
        {
            SelectAvailableActivity(8);
        }
        if (Input.GetKeyDown("0"))
        {
            SelectAvailableActivity(9);
        }
    }

    //RESETS ALL ACTIVITIES
    public void ResetActivities()
    {
        //Clear the list of available activities
        activitiesNames.Clear();
        //Reset the base activity
        baseActivity.ResetActivity();
        //Loop through all activities
        for (int i = 0; i < activities.Count; i++)
        {
            //Reset Each activity
            activities[i].ResetActivity();
            //if the activity hasn't been visited
            if (!activities[i].activityVisited)
            {
                //For every available spot of the activity
                for (int x = 0; x < activities[i].capacity; x++)
                {
                    //Add activity name to the list of available activities
                    activitiesNames.Add(activities[i].activityName);
                }
            }
        }
    }

    //SHOW AVAILABLE ACTIVITIES
    public void ShowOptions()
    {
        //For every activity
        for (int i = 0; i < activities.Count; i++)
        {
            //Turn off all buttons
            optionButtons[i].interactable = false;
            //For every button of an activity that has been visited
            if (!activities[i].activityVisited)
            {
                //Disable button
                optionButtons[i].interactable = true;
            }
        }
        //if no activites are available
        if(activitiesNames.Count == 0)
        {
            baseButton.interactable = true;
        }
        else
        {
            baseButton.interactable = false;
        }

        optionCanvas.SetActive(true);
    }

    //SELECT AVAILABLE ACTIVITY
    public void SelectAvailableActivity(int selection)
    {
        //Hide UI
        HideOptions();
        HideDialogueCanvas();
        
        //If game is not over yet
        if (!gameIsOver)
        {
            //if there are no activities left
            if (activitiesNames.Count == 0 || turns == turnCounter)
            {
                //Tell main character AI to go to the base
                mainCharacter.CheckSpecificSchedule("Base");
                //Tell NPC AIs to go to the base
                AssignActivities();
                //Flag the game as over
                gameIsOver = true;
            }
            //If there are any available activities
            else
            {
                //Make Player selection
                SelectActivityPlayer(selection);

                //Make AI selection
                SelectActivityAI();

                //Gets the dialogue
                GetActivityByName(playerSelectedActivity).GetDialogue();

                //passes turn
                turnCounter++;
            }

            //Resets Available activities
            ResetActivities();

            //Shows remaining options
            ShowOptions();

            //Show dialogue canvas
            ShowDialogueCanvas();
        }
        //If game is over show results
        else
        {
            GetResults();
        }
    }

    //PLAYER SELECTS ACTIVITY
    public void SelectActivityPlayer(int option)
    {
        //For every listed activity
        for (int i = 0; i < activitiesNames.Count; i++)
        {
            //Check if the activity selected by the user is available in the list of available activities
            if (activities[option].activityName == activitiesNames[i])
            {
                //Tell player AI to select specific activity
                mainCharacter.CheckSpecificSchedule(GetSpecificActivity(i));
                //Perform activity 
                activities[option].PerformActivity(true);
                //Sets variable
                playerSelectedActivity = activities[option].activityName;
                //Ends this loop
                break;
            }
        }
    }

    //AI's SELEC ACTIVITIES
    public void SelectActivityAI()
    {
        //Tell NPC AIs to select and perform the remaining activities
        AssignActivities();
        PerformAllActivities();
    }

    //TELLS CHARACTERS TO FETCH ACTIVITIES TO DO
    public void AssignActivities()
    {
        ShuffleCharacters();

        for (int i = 0; i < characters.Count; i++)
        {
            if (activitiesNames.Count != 0)
            {
                characters[i].CheckSchedule();
            }
            else
            {
                characters[i].CheckSpecificSchedule("Base");
            }
        }
    }

    //PERFORM ALL ACTIVITIES
    public void PerformAllActivities()
    {
        //For every listed activity
        for (int x = 0; x < activities.Count; x++)
        {
            //Perform such activity as an NPC
            activities[x].PerformActivity(false);
        }
    }

    

    //HIDE AVAILABLE ACTIVITIES
    public void HideOptions()
    {
        optionCanvas.SetActive(false);
    }

    //GET GAME RESULTS
    public void GetResults()
    {
        finalPositivePoints = 0;
        finalNegativePoints = 0;
        for (int i = 0; i < activities.Count; i++)
        {
            if (activities[i].isDone)
            {
                if (activities[i].isCrucial)
                {
                    finalPositivePoints++;
                }
                else
                {
                    finalNegativePoints++;
                }
                
            }
        }
        if (finalPositivePoints > finalNegativePoints)
        {
            WinGame();
        }
        else
        {
            LooseGame();
        }
    }


    /*---UI---*/

    //SHOW DIALOGUE CANVAS
    public void ShowDialogueCanvas()
    {
        dialogueCanvas.SetActive(true);
    }

    //HIDE DIALOGUE CANVAS
    public void HideDialogueCanvas()
    {
        dialogueCanvas.SetActive(false);
    }

    /*---UTILITIES---*/

    //WIN
    void WinGame()
    {
        Debug.LogWarning("YOU WIN");
    }

    //LOOSE
    void LooseGame()
    {
        Debug.LogWarning("YOU LOOSE");
    }

    //QUIT
    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //SHUFFLES CHARACTERS
    void ShuffleCharacters()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Character temp = characters[i];
            int randomIndex = Random.Range(i, characters.Count);
            characters[i] = characters[randomIndex];
            characters[randomIndex] = temp;
        }
    }

    //GENERATES RANDOM TRUE/FALSE BOOL
    bool RandomResult()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    //GETS AN AVAILABLE SPECIFIC ACTIVITY
    public string GetSpecificActivity(int option)
    {
        string result;
        if (activitiesNames.Count != 0)
        {
            result = activitiesNames[option];
            activitiesNames.RemoveAt(option);

            lastSelectedActivity = result;
        }
        else
        {
            result = "Base";
        }
        return result;
    }

    //GETS AN AVAILABLE RANDOM ACTIVITY
    public string GetRandomActivity()
    {
        string result;
        if (activitiesNames.Count != 0)
        {
            
            for (int i = 0; i < activitiesNames.Count; i++)
            {
                if(lastSelectedActivity == activitiesNames[i])
                {
                    result = activitiesNames[i];
                    activitiesNames.RemoveAt(i);

                    lastSelectedActivity = result;
                    return result;
                }
            }
            
            int random = Random.Range(0, activitiesNames.Count);
            result = activitiesNames[random];
            activitiesNames.RemoveAt(random);

            lastSelectedActivity = result;
        }
        else
        {
            result = "Base";
        }
        return result;
    }
 
    //GET ACTIVITY REFERENCE BY NAME
    public Activity GetActivityByName(string name)
    {
        if (name != "Base")
        {
            for (int i = 0; i < activities.Count; i++)
            {
                if (activities[i].activityName == name)
                {
                    return activities[i];
                }
            }
        }
        return baseActivity;
    }

    /* -- LEGACY
     //PLAYER SELECTION
    public void SelectActivity(int option)
    {
        //Checks what activities are available
        ResetActivities();

        //If game is not over yet
        if (!gameIsOver)
        {
            //If there are any available activities
            if (activitiesNames.Count != 0)
            {
                //For every listed activity
                for (int i = 0; i < activitiesNames.Count; i++)
                {
                    //Check if the activity selected by the user is available in the list of available activities
                    if (activities[option].activityName == activitiesNames[i])
                    {
                        //Tell player AI to select specific activity
                        mainCharacter.CheckSpecificSchedule(GetSpecificActivity(i));
                        //Perform activity 
                        activities[option].PerformActivity(true);

                        //Tell NPC AIs to select and perform the remaining activities
                        AssignActivities();
                        PerformAllActivities();

                        //Displays dialogue of available options
                        ShowOptions();

                        //Add a turn counter
                        turnCounter++;

                        //Make sure it selects only one activity
                        break;
                    }
                }

            }
            //If there arent any other available activities or the turns are over
            if (activitiesNames.Count == 0 || turns == turnCounter)
            {
                //Tell main character AI to go to the base
                mainCharacter.CheckSpecificSchedule("Base");
                //Tell NPC AIs to go to the base
                AssignActivities();
                //Flag the game as over
                gameIsOver = true;
            }
        }
        else
        {
            //Show the results of the game
            GetResults();
        }
    }
     */
}
