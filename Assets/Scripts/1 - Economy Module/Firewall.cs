using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Firewall : Software
{

    //REFERENCES

    public Image healthBarImage;

    //AUDIO

    public AudioClip hitSound;

    //MAIN METHODS

    public override void OnStart()
    {
        UpdateHealthBar();
    }

    //ACTIONS
    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = (float)healthPoints / (float)maxHealthPoints;
    }

    public override void ReceiveDamage(int damage)
    {
        //Base
        base.ReceiveDamage(damage);

        //UpdateHealthBar
        UpdateHealthBar();

        //Play sound
        audioSource.PlayOneShot(hitSound);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Crawler"))
        {
            //Sets target as Crawler
            target = collider.gameObject.GetComponent<CrawlerUnit>();
            //If its enemy firewall
            if (target.currentTeamName != currentTeamName)
            {
                //Deals damage to target
                ReceiveDamage(target.damagePoints);
                //Dies
                target.Die();
            }
        }
    }
}