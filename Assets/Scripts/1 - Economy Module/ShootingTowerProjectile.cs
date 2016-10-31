using UnityEngine;
using System.Collections;

public class ShootingTowerProjectile : MonoBehaviour {

    //REFERENCE
    public ShootingTower owner;
    public CrawlerUnit target;

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
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();
        MoveForward();
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
        rigidBody.AddForce(Vector3.forward * projectileSpeed);
    }

    //COLLISION

    void OnTriggerenter(Collider collider)
    {
        //If its a Crawler
        if (collider.gameObject.name.Contains("Crawler"))
        {
            //Sets target as Crawler
            target = collider.gameObject.GetComponent<CrawlerUnit>();
            
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
