using UnityEngine;
using System.Collections;

public class CrawlerUnit : Software {

    //REFERENES
    EconomyGC economyGC;
    Firewall target;

    //MAIN VARIABLES

    //MOVEMENT
    public int walkingSpeed;


	// Use this for initialization
	void Start () {

        //Makes sure the CrawlerUnit has proper references
        if (economyGC == null) economyGC = FindObjectOfType<EconomyGC>();

        //Removes RAM from team who spawned it
        economyGC.RemoveRamFromPlayer(ramCost, currentTeamName);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //MAIN METHODS
    
    //Movement
    void Crawl()
    {

    }

    //Death
    public override void Die()
    {
        AnimateDeath();
        economyGC.AddRamToPlayer(ramCost, enemyTeamName);
    }

    //DAMAGE
    public override void DealDamage(Software targetObject)
    {
        base.DealDamage(targetObject);
        //extend functionality
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        //Extend functionality
    }

    //ANIMATION
    public override void AnimateSpawn()
    {

    }
    public override void AnimateDeath()
    {

    }

    //COLLISION

    void OnCollisionEnter(Collision collision)
    {
        //If its a firewall
        if(collision.gameObject.name.Contains("Firewall"))
        {
            target = collision.gameObject.GetComponent<Firewall>();
            DealDamage(target);
        }
    }

}
