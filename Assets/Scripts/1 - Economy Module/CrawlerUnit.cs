using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CrawlerUnit : Software
{
    
    //REFERENCES

    public AICharacterControl aiCharacterControl;
    public List<ShootingTower> shootingTowers;
    

    //MAIN VARIABLES

    public Transform targetTransform;
    public Image healthBarImage;

    //MAIN METHODS

    public override void OnStart()
    {
        //Verifies aiCharacterControll is set
        if (aiCharacterControl == null) aiCharacterControl = GetComponent<AICharacterControl>();

        //Sets the health bar for the first time
        UpdateHealthBar();
    }
    
    //ACTIONS

    public override void OnInstall()
    {
        //Sets walking target depending on team
        if (currentTeamName == economyGC.teamName_A) aiCharacterControl.target = economyGC.teamNode_B;
        if (currentTeamName == economyGC.teamName_B) aiCharacterControl.target = economyGC.teamNode_A;
    }

    public override void Die()
    {

        //Stop moving
        aiCharacterControl.target = transform;
       
        //Base death
        base.Die();

        //Removes itself from all shooting towers
        for( int i = 0; i < shootingTowers.Count; i++)
        {
            shootingTowers[i].enemiesInRange.Remove(transform);

            //If it was shooting me directly
            if(shootingTowers[i].targetTransform == transform)
            {
                //tell it to look for a new target
                shootingTowers[i].ChooseTarget();
            }
        }

        //Clear the towers
        shootingTowers.Clear();

    }

    //ANIMATION

    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = (float)healthPoints / (float)maxHealthPoints;
    }

    //DAMAGE

    public override void ReceiveDamage(int damage)
    {
        //Base
        base.ReceiveDamage(damage);
        //UpdateHealthBar
        UpdateHealthBar();
    }

    //COLLISION

    void OnCollisionEnter(Collision collision)
    {
        /*
        //If its a firewall
        if (collision.gameObject.name.Contains("Firewall"))
        {
            //Sets target as firewall
            target = collision.gameObject.GetComponent<Firewall>();
            //If its enemy firewall
            if(target.currentTeamName != currentTeamName)
            {
                //Deals damage to target
                DealDamage(target);
                //Dies
                Die();
            }
        }
        */
    }

}
