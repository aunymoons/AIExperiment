using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TowerDefense
{
    public class EconomyGC : GameController
    {

        //TEAM VALUES
        public string teamName_A, teamName_B;
        public int teamMembers_A, teamMembers_B;
        public int ram_A, ram_B;
        public Animator animator_A, animator_B, animatorGeneric;
        public Transform teamNode_A, teamNode_B;
        public Text ramTextMain_A, ramTextSecondary_A, ramTextMain_B, ramTextSecondary_B;
        public Image ramImage_A, ramImage_B;
        public int maxRamPerPlayer, totalMaxRam;

        //Theming
        public Color teamColor_A, teamColor_B;

        // Use this for initialization
        void Start()
        {
            maxRamPerPlayer = totalMaxRam / 2;
            ram_A = maxRamPerPlayer;
            ram_B = maxRamPerPlayer;

            UpdateRamText();
            UpdateRamUI();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //INHERITED
        public override void WinGame(string winTeam, string looseTeam)
        {
            if (!isGameOver){
                isGameOver = true;

                if(winTeam == teamName_A)
                {
                    Debug.Log("team A WON");
                    animatorGeneric.SetTrigger("ATeamWon");
                }
                if (winTeam == teamName_B)
                {
                    Debug.Log("team B WON");
                    animatorGeneric.SetTrigger("BTeamWon");
                }
            }
        }

        //ACTIONS

        public void RemoveRamFromPlayer(int ram, string team)
        {
            if (team == teamName_A)
            {
                ram_A -= ram;
            }
            if (team == teamName_B)
            {
                ram_B -= ram;
            }
            if (team == teamName_A || team == teamName_B)
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
            UpdateRamUI();
        }

        public void UpdateRamText()
        {
            ramTextMain_A.text = ram_A.ToString();
            ramTextMain_B.text = ram_B.ToString();
        }
        
        public void UpdateRamUI()
        {
            ramImage_A.fillAmount = (float)ram_A / totalMaxRam; 
            ramImage_B.fillAmount = (float)ram_B / totalMaxRam;
        }

    }
}