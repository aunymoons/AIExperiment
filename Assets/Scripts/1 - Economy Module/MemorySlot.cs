using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;

namespace TowerDefense
{

    public class MemorySlot : MonoBehaviour
    {

        //REFERENCES
        public EconomyGC economyGC;
        public AccessNode accessNode;
        public Software installedSoftware;
        public List<GameObject> installablePrefabs_A, installablePrefabs_B;
        public List<GameObject> installableButtons;
        public List<GameObject> coloredGameObjects;
        public GameObject uninstallButton;
        public GameObject optionCanvas;
        public CanvasGroup optionCanvasGroup;
        PayButton currentButton;


        //INSTALLATION
        public Transform installTransform;
        public bool isInstalled;
        public bool isInstalling, isUninstalling;
        public bool canUninstall;

        //ANIMATION

        public Animator anim;

        //MAIN VARIABLES

        public string currentTeamName, enemyTeamName; //What team they belong to



        //MAIN

        void Start()
        {
            //Verifies References
            if (economyGC == null) economyGC = FindObjectOfType<EconomyGC>();
            if (anim == null) anim = GetComponent<Animator>();
            if (optionCanvasGroup == null) optionCanvasGroup = optionCanvas.GetComponent<CanvasGroup>();

            //Themes
            SetTeamColor();
        }

        //ACTIONS

        public bool CheckPrice(string installName)
        {
            bool result = false;
            switch (installName)
            {
                case "CrawlerSmall":
                    if (currentTeamName == "A" && economyGC.ram_A >= 200)
                    {
                        result = true;
                    }
                    else if (currentTeamName == "B" && economyGC.ram_B >= 200)
                    {
                        result = true;
                    }
                    break;
                case "CrawlerBig":
                    if (currentTeamName == "A" && economyGC.ram_A >= 400)
                    {
                        result = true;
                    }
                    else if (currentTeamName == "B" && economyGC.ram_B >= 400)
                    {
                        result = true;
                    }
                    break;
                case "ShootingTower":
                    if (currentTeamName == "A" && economyGC.ram_A >= 200)
                    {
                        result = true;
                    }
                    else if (currentTeamName == "B" && economyGC.ram_B >= 200)
                    {
                        result = true;
                    }
                    break;
                case "AreaTower":
                    if (currentTeamName == "A" && economyGC.ram_A >= 400)
                    {
                        result = true;
                    }
                    else if (currentTeamName == "B" && economyGC.ram_B >= 400)
                    {
                        result = true;
                    }
                    break;
                case "Firewall":
                    if (currentTeamName == "A" && economyGC.ram_A >= 500)
                    {
                        result = true;
                    }
                    else if (currentTeamName == "B" && economyGC.ram_B >= 500)
                    {
                        result = true;
                    }
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public void Install(string installableName)
        {
            //Hides UI
            HideOptions();

            if (CheckPrice(installableName))
            {
                //Sets flag
                isInstalling = true;

                //Instantiates the prefab
                GameObject instance = Instantiate(Resources.Load(installableName + "_" + currentTeamName, typeof(GameObject)), installTransform.position, installTransform.rotation) as GameObject;
                installedSoftware = instance.GetComponent<Software>();
                installedSoftware.memorySlot = this;
            }
            else
            {
                ShowOptions();
            }
        }

        public void FinishInstall()
        {
            Debug.Log("Software was INSTALLED");
            
            isInstalling = false;
            isInstalled = true;
        }

        public void Uninstall()
        {
            Debug.Log("Software is being UNINSTALLED");
            isUninstalling = true;

            installedSoftware.Uninstall();
        }

        public void FinishUninstall()
        {
            Debug.Log("Software was UNINSTALLED");
            isUninstalling = false;
            isInstalled = false;
        }
        //INTERACTION

        public void OnTapSelected(bool isUI)
        {
            if(!isUI)
            ShowOptions();

            if (isUI)
            {
                Debug.Log("is clicking on UI");
            }
            else
            {
                Debug.Log("is clicking OUTSIDE UI");
            }
            
        }

        public void OnTapDeselected(bool isUI)
        {
            
            HideOptions();
        }

        //UI

        //Show Option Canvas
        void ShowOptions()
        {
            
            
            //Only show if its not installing or uninstalling
            if (!isInstalling && !isUninstalling)
            {
                //optionCanvasGroup.alpha = 1;
                //optionCanvasGroup.interactable = true;
                optionCanvas.SetActive(true);

                anim.SetTrigger("show");

                
                //Only show uninstall buttons if can uninstall
                //if (canUninstall || !isInstalled)
                //{
                //Show proper buttons
                ShowInstall(isInstalled);
                //}
                //updates buttons
                UpdateButtons();
            }
            
        }

        //Update buttons
        void UpdateButtons()
        {
            for (int i = 0; i < installableButtons.Count; i++)
            {
                currentButton = installableButtons[i].GetComponent<PayButton>();
                currentButton.currentTeamName = currentTeamName;
                currentButton.enemyTeamName = enemyTeamName;
                currentButton.UpdateButton();
            }
        }

        //Show install/uninstall buttons
        void ShowInstall(bool installed)
        {
            uninstallButton.SetActive(installed);
            for (int i = 0; i < installableButtons.Count; i++)
            {
                installableButtons[i].SetActive(!installed);
            }
            
        }
        //Hide Option Canvas
        void HideOptions()
        {
            //MISSING "IF ITS HIDDEN"
            anim.SetTrigger("hide");
            //optionCanvasGroup.alpha = 0;
            //optionCanvasGroup.interactable = false;
            StartCoroutine(tempHide());
        }

        IEnumerator tempHide()
        {
            yield return new WaitForSeconds(0.1f);
            optionCanvas.SetActive(false);
        }

        //EVENTS

        public void ChangeTeam()
        {
            string tempTeamName = currentTeamName;

            currentTeamName = enemyTeamName;

            enemyTeamName = tempTeamName;

            HideOptions();

            SetTeamColor();
        }

        public void SetTeam(string current, string enemy)
        {
            currentTeamName = current;
            enemyTeamName = enemy;
            SetTeamColor();
        }

        public void SetTeamColor()
        {
            if (currentTeamName == "A")
            {
                for (int i = 0; i < coloredGameObjects.Count; i++)
                {
                    coloredGameObjects[i].GetComponent<Renderer>().material.color = economyGC.teamColor_A;
                }

            }
            if (currentTeamName == "B")
            {
                for (int i = 0; i < coloredGameObjects.Count; i++)
                {
                    coloredGameObjects[i].GetComponent<Renderer>().material.color = economyGC.teamColor_B;
                }
            }

        }
    }

}