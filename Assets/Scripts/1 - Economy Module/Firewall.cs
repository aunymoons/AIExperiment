using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace TowerDefense
{
    public class Firewall : Software
    {

        //REFERENCES
        public Collider myCollider;
        public Image healthBarImage;

        //AUDIO

        public AudioClip hitSound;

        //MAIN METHODS

        public override void OnStart()
        {
            base.OnStart();
            UpdateHealthBar();
        }

        public override void Die()
        {
            base.Die();
        }

        public override void OnInstall()
        {
            base.OnInstall();
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
        }
    }
}