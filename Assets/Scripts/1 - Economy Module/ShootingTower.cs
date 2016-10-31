using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingTower : Software
{

    //REFERENCE
    public GameObject gun;
    public GameObject projectilePrefab;
    public Transform gunBarrel;
    public Transform targetTransform;
    public GameObject lastShotBullet;

    //MAIN
    public float reloadTime;
    public float aimSpeed;

    //ENEMIES
    public List<Transform> enemiesInRange;
    float currentDistance, shortestDistance;

    //AUDIO
    public AudioClip shootSound;


    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    public override void OnStart()
    {
        enemiesInRange = new List<Transform>();

        InvokeRepeating("Shoot", 0, reloadTime);
    }

    public void ChooseTarget()
    {
        //Create a possible target transform
        Transform possibleTarget = null;

        //Sets shortest distance to infinity
        shortestDistance = Mathf.Infinity;

        //For each enemy in range
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            //Get the distance between us and them
            currentDistance = Vector3.Distance(transform.position, enemiesInRange[i].position);

            //if its shorter than the shortest
            if (currentDistance < shortestDistance)
            {
                //Make possible target the current enemy
                possibleTarget = enemiesInRange[i];
                //Make the shortest distance this distance
                shortestDistance = currentDistance;
            }
        }

        //Return possible target
        targetTransform = possibleTarget;
    }

    void Aim()
    {
        //Look at target
        if (targetTransform != null)
        {
            gun.transform.LookAt(targetTransform);
        }
    }

    void Shoot()
    {
        //if there is something to shoot at
        if (targetTransform != null)
        {
            //Make shooting animation
            ShootingAnimation();

            //Play sound
            audioSource.PlayOneShot(shootSound);

            //Instantiate projectile prefab
            lastShotBullet =  Instantiate(projectilePrefab, gunBarrel.position, gunBarrel.rotation) as GameObject;

            //Assign Owner
            lastShotBullet.GetComponent<ShootingTowerProjectile>().owner = this;
            lastShotBullet = null;

        }
    }

    void ShootingAnimation()
    {
        animator.SetTrigger("shoot");
    }

    //COLLISION

    void OnTriggerEnter(Collider other)
    {
        //If is a crawler unit
        if (other.name.Contains("Crawler"))
        {
            //Get the crawler unit
            CrawlerUnit currentCrawler = other.gameObject.GetComponent<CrawlerUnit>();

            //If the enemies in range doesnt contain this transform
            if (!enemiesInRange.Contains(other.transform))
            {
                //Add this transform
                enemiesInRange.Add(other.transform);

                //If its the first enemy
                if (enemiesInRange.Count == 1)
                {
                    //Choose a different target
                    ChooseTarget();
                }

                //if current crawler doesnt contain this tower, add it
                if (!currentCrawler.shootingTowers.Contains(this))
                {
                    currentCrawler.shootingTowers.Add(this);
                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //If is a crawler unit
        if (other.name.Contains("Crawler"))
        {
            //Get the crawler unit
            CrawlerUnit currentCrawler = other.gameObject.GetComponent<CrawlerUnit>();

            //If the enemies in range contains this transform
            if (enemiesInRange.Contains(other.transform))
            {
                //Remove it
                enemiesInRange.Remove(other.transform);

                //Choose a different target
                ChooseTarget();

                //Tell the tower to aim elsewhere
                if (currentCrawler.shootingTowers.Contains(this))
                {
                    currentCrawler.shootingTowers.Remove(this);
                }

            }
        }


    }
}
