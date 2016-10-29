using UnityEngine;
using System.Collections;

public class CrawlerUnit : Software {

    //REFERENES
    EconomyGC economyGC;
    Software target;

    //MAIN VARIABLES
    public int HealthPoints, DamagePoints;

    //MOVEMENT
    public int walkingSpeed;


	// Use this for initialization
	void Start () {

        //Makes sure the CrawlerUnit has proper references
        if (economyGC == null) economyGC = FindObjectOfType<EconomyGC>();

        //Removes RAM from team who spawned it
        economyGC.RemoveRamFromPlayer(ramCost, team);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //MAIN METHODS

    void Crawl()
    {

    }

    void Die()
    {

    }

    public void DealDamage()
    {

    }
    public void ReceiveDamage()
    {

    }

    //COLLISION

    void OnCollisionEnter(Collision collision)
    {
        //If its a firewall
        if(collision.gameObject.name == "Firewall")
        {
            target = collision.gameObject.GetComponent<Firewall>();
        }
    }

}
