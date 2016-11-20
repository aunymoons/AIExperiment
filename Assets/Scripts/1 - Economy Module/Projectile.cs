using UnityEngine;
using System.Collections;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {

        //REFERENCE
        public Tower owner;
        public CrawlerUnit target;

        //STATS
        public float lifetime;
        public float deathTime;

        //DAMAGE
        public int damagePoints;

        //PHYSICS
        public Rigidbody myRigidBody;
        public Collider myCollider;
        public float projectileSpeed;

        //EFFECTS
        public ParticleSystem[] particleSystems;

        //ANIMATIONS
        public Animator anim;

        //MAIN MEHTODS 

        void Start()
        {
            //Sets basic variables
            if (myRigidBody == null) myRigidBody = GetComponent<Rigidbody>();
            if (myCollider == null) myCollider = GetComponent<Collider>();
            if (anim == null) anim = GetComponent<Animator>();

            //Startup
            MoveForward();
            Invoke("Die", lifetime);
        }

        //ACTIONS

        void Die()
        {
            StartCoroutine(AnimateDeath());
        }

        //PHYSICS

        void MoveForward()
        {
            myRigidBody.AddForce(transform.forward * projectileSpeed);
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
                if (target.currentTeamName != owner.currentTeamName)
                {
                    //Delivers damage to the unit
                    target.ReceiveDamage(damagePoints);

                    //Destroy proyectile
                    Die();
                }
            }
        }

        //EFFECTS

        void DeathAnimation()
        {
            anim.SetTrigger("death");
        }

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
            TurnOffParticles();

            DeathAnimation();

            yield return new WaitForSeconds(deathTime);

            Destroy(gameObject);
        }

    }
}