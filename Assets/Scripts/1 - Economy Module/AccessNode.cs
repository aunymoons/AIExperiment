using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefense
{
    public class AccessNode : MemorySlot
    {

        public List<MemorySlot> influencedSlots;
        
        

        void Update()
        {
            if(Input.GetKeyDown("c")){
                ChangeControllingTeam();
            }
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
        }


        void OnTriggerEnter(Collider collider)
        {
            /* MAKE THE EQUIVALENT OF THIS FOR THE ACCESSNODE
            if (collider.gameObject.name.Contains("Crawler"))
            {
                //Sets target as Crawler
                target = collider.gameObject.GetComponent<CrawlerUnit>();
                //If its enemy firewall
                if (target.currentTeamName != currentTeamName)
                {
                    //Checks if this firewall is done installing
                    if (isInstalled && !isDying)
                    {
                        //Deals damage to target
                        ReceiveDamage(target.damagePoints);
                        //Dies
                        target.Die();
                    }
                }
            }
            */
        }

    }
}