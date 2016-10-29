using UnityEngine;
using System.Collections;

public class EconomyGC : GameController {

    //TEAM VALUES
    public string teamName_A, teamName_B;
    public int ram_A, ram_B;
    public Animator animator_A, animator_B;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //MAIN
    void WinGame()
    {

    }
    void LooseGame()
    {

    }
    void PauseGame()
    {

    }

    //ACTIONS
    public void RemoveRamFromPlayer(int ram, string team)
    {
        if(team == teamName_A)
        {
            ram_A -= ram;
        }
        if (team == teamName_B)
        {
            ram_B -= ram;
        }
        if(team == teamName_A || team == teamName_B)
        {
            AnimateRamPoints("-" + ram, team);
        }
    }

    public void AddRamToPlayer(int ram, string team)
    {
        if (team == teamName_A)
        {
            ram_A += ram;
        }
        if (team == teamName_B)
        {
            ram_B += ram;
        }
        if (team == teamName_A || team == teamName_B)
        {
            AnimateRamPoints("+" + ram, team);
        }
    }

    //ANIMATIONS AND EFFECTS
    public void AnimateRamPoints(string points, string team)
    {
        if (team == teamName_A)
        {
            //animator_a.settrigger etc...
            Debug.Log("Team A " + points);
        }
        if (team == teamName_B)
        {
            //animator_b.settrigger etc...
            Debug.Log("Team B " + points);
        }
    }

}
