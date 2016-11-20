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
        public GameObject uninstallButton;
        public GameObject optionCanvas;


        //INSTALLATION
        public Transform installTransform;
        public bool isInstalled;
        public bool isInstalling, isUninstalling;
        public bool canUninstall;

        //ANIMATION

        public Animator anim;

        //MAIN VARIABLES

        public string currentTeamName, enemyTeamName; //What team they belong to

        //Theming
        public Color teamColor_A, teamColor_B;

        //MAIN

        void Start()
        {
            if (anim == null) anim = GetComponent<Animator>();

            //Themes
            SetTeamColor();
        }

        //ACTIONS

        public void Install(string installableName)
        {
            Debug.Log("Software is being INSTALLED");
            isInstalling = true;

            //Instantiates the prefab
            GameObject instance = Instantiate(Resources.Load(installableName + "_" + currentTeamName, typeof(GameObject)), installTransform.position, installTransform.rotation) as GameObject;
            installedSoftware = instance.GetComponent<Software>();
            installedSoftware.memorySlot = this;
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

        public void OnTapSelected()
        {
            ShowOptions();
        }

        public void OnTapDeselected()
        {
            HideOptions();
        }

        //UI

        //Show Option Canvas
        void ShowOptions()
        {
            anim.SetTrigger("show");

            //Only show if its not installing or uninstalling
            if(!isInstalling && !isUninstalling)
            {
                //Only show uninstall buttons if can uninstall
                if (canUninstall || !isInstalled)
                {
                    //Show proper buttons
                    ShowInstall(isInstalled);
                }
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

            optionCanvas.SetActive(true);
        }
        //Hide Option Canvas
        void HideOptions()
        {
            anim.SetTrigger("hide");
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

        public void SetTeamColor()
        {
            if(currentTeamName == "A")
            {
                GetComponent<Renderer>().material.color = teamColor_A;
            }
            if (currentTeamName == "B")
            {
                GetComponent<Renderer>().material.color = teamColor_B;
            }

        }
    }

}