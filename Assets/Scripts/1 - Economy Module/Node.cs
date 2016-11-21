﻿using UnityEngine;
using System.Collections;

namespace TowerDefense
{
    public class Node : Firewall
    {

        //References
        public AccessNode parentNode;

        // Use this for initialization
        public override void Start()
        {
            //dont install
        }

        public override void Die()
        {
            //Dont do base
            SwitchTeam();
            isInstalled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown("c")){
                SwitchTeam();
            }
        }

        public void SetTeam(string current, string enemy)
        {
            currentTeamName = current;
            enemyTeamName = enemy;
            SetTeamColor();
        }
        

        public void SwitchTeam()
        {
            parentNode.ChangeControllingTeam();
        }

    }
}