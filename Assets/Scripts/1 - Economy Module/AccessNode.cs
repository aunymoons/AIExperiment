﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefense
{
    public class AccessNode : MemorySlot
    {

        public List<MemorySlot> influencedSlots;
        public Node childNode;
        
        void Start()
        {
            SetTeamColor();
            childNode.parentNode = this;
            childNode.SetTeam(currentTeamName, enemyTeamName);
            SetTeamChildren();
        }
        
        public void SetTeamChildren()
        {
            for(int i = 0; i < influencedSlots.Count; i++)
            {
                influencedSlots[i].SetTeam(currentTeamName, enemyTeamName);
            }
        }

        void Update()
        {
        }


        public void ChangeControllingTeam()
        {
            Software currentSoftware;
            for (int i = 0; i < influencedSlots.Count; i++)
            {
                currentSoftware = influencedSlots[i].installedSoftware;
                if(currentSoftware != null)
                {
                    currentSoftware.Die();
                }
                influencedSlots[i].ChangeTeam();
            }
            ChangeTeam();
            childNode.SetTeam(currentTeamName, enemyTeamName);
            childNode.SetTeamColor();
        }
        
    }
}