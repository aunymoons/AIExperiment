using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TowerDefense
{
    public class PayButton : MonoBehaviour
    {
        //References
        public EconomyGC economyGC;
        public Button installButton;

        public int price;
        public string currentTeamName, enemyTeamName;

        void Start()
        {
            economyGC = FindObjectOfType<EconomyGC>();
            installButton = GetComponent<Button>();
        }
        
        public void UpdateButton()
        {
            economyGC = FindObjectOfType<EconomyGC>();
            if (currentTeamName == "A")
            {
                if(price <= economyGC.ram_A)
                {
                    installButton.interactable = true;
                }
                if (price > economyGC.ram_A)
                {
                    installButton.interactable = false;
                }

            }
            if (currentTeamName == "B")
            {
                if (price <= economyGC.ram_B)
                {
                    installButton.interactable = true;
                }
                if (price > economyGC.ram_B)
                {
                    installButton.interactable = false;
                }
            }
        }
    }
}