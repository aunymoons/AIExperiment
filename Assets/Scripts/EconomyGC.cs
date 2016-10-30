using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EconomyGC : GameController {

    //TEAM VALUES
    public string teamName_A, teamName_B;
    public int teamMembers_A, teamMembers_B;
    public int ram_A, ram_B;
    public Animator animator_A, animator_B;
    public Transform teamNode_A, teamNode_B;
    public Text ramTextMain_A, ramTextSecondary_A, ramTextMain_B, ramTextSecondary_B;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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

    //ANIMATIONS, EFFECTS AND UI

    public void AnimateRamPoints(string points, string team)
    {
        //Makes animation
        if (team == teamName_A)
        {
            ramTextSecondary_A.text = points;
            animator_A.SetTrigger("ramPoints");
        }
        if (team == teamName_B)
        {
            ramTextSecondary_B.text = points;
            animator_A.SetTrigger("ramPoints");
        }
        //Updates UI
        UpdateRamText();
    }

    public void UpdateRamText()
    {
        ramTextMain_A.text = ram_A.ToString();
        ramTextMain_B.text = ram_B.ToString();
    }

}
