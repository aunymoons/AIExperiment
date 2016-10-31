using UnityEngine;
using System.Collections;

public class ShootingTowerProjectile : MonoBehaviour {

    //REFERENCE
    public ShootingTower owner;
    public CrawlerUnit target;

    //STATS
    public float lifetime;

    //DAMAGE
    public int damagePoints;

    //PHYSICS
    public Rigidbody rigidBody;
    public float projectileSpeed;

    //EFFECTS
    public ParticleSystem[] particleSystems;

    //MAIN MEHTODS 

    void Start()
    {
        //Sets basic variables
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();

        //Startup
        MoveForward();
        Invoke("Die", lifetime);
    }

    //ACTIONS

    void Die()
    {
        TurnOffParticles();
        StartCoroutine(AnimateDeath());
    }

    //PHYSICS

    void MoveForward()
    {
        rigidBody.AddForce(transform.forward * projectileSpeed);
    }

    //COLLISION

    void OnTriggerEnter(Collider other)
    {
        //If its a Crawler
        if (other.gameObject.name.Contains("Crawler"))
        {
            
            //Sets target as Crawler
            target = other.gameObject.GetComponent<CrawlerUnit>();
            
            //if crawler is from opposite team
            if(target.currentTeamName != owner.currentTeamName)
            {
                //Delivers damage to the unit
                target.ReceiveDamage(damagePoints);

                //Destroy proyectile
                Die();
            }
        }
    }

    //EFFECTS

    void TurnOffParticles()
    {
        //Creates current emission module
        ParticleSystem.EmissionModule currentEmissionModule;

        //sets all particle systems rate to zero
        for (int i = 0; i < particleSystems.Length; i++)
        {
            currentEmissionModule = particleSystems[i].emission;
            currentEmissionModule.rate = new ParticleSystem.MinMaxCurve(0);
        }

    }
    
    IEnumerator AnimateDeath()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

}
