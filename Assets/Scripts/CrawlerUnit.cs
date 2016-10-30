using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using System.Collections;
using System;

public class CrawlerUnit : Software
{
    
    //REFERENCES

    AICharacterControl aiCharacterControl;

    //MAIN VARIABLES

    public int walkingSpeed;
    public Transform targetTransform;
    public Image healthBarImage;

    //MAIN METHODS

    public override void OnStart()
    {
        UpdateHealthBar();
    }
    
    //ACTIONS

    public override void OnInstall()
    {
        //Sets walking target depending on team
        if (currentTeamName == economyGC.teamName_A) aiCharacterControl.target = economyGC.teamNode_B;
        if (currentTeamName == economyGC.teamName_B) aiCharacterControl.target = economyGC.teamNode_A;
    }

    //ANIMATION

    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthPoints / maxHealthPoints;
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
        //If its a firewall
        if (collision.gameObject.name.Contains("Firewall"))
        {
            //Sets target as firewall
            target = collision.gameObject.GetComponent<Firewall>();
            //Deals damage to target
            DealDamage(target);
            //Dies
            Die();
        }
    }

}
