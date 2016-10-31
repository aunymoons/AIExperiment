using UnityEngine;
using System.Collections;

public abstract class Software : MonoBehaviour
{
    //REFERENCES

    protected EconomyGC economyGC;
    protected MemorySlot memorySlot;
    protected Animator animator;
    protected Software target;

    //MAIN VARIABLES

    public string currentTeamName, enemyTeamName; //What team they belong to
    public int ramCost; //Price for installing this software
    public float installTime, uninstallTime; //Installing/uninstalling times
    public int maxHealthPoints, healthPoints, damagePoints; //Stats

    //STATES

    public bool canUninstall, isInstalled, isDying;

    //AUDIO

    public AudioSource audioSource;
    public AudioClip installSound, uninstallSound, activateSound, deactivateSound, deathSound;

    //MAIN METHODS
    public void Start()
    {
        //Makes sure we have the proper references
        if (economyGC == null) economyGC = FindObjectOfType<EconomyGC>();
        if (animator == null) animator = GetComponent<Animator>();

        //Sets healthpoints
        maxHealthPoints = healthPoints;
       
        //Begins installation
        Install();

        //For inherited classes
        OnStart();
    }
    
    public virtual void OnStart() { }
    
    //ACTIONS
    
    public virtual void Install()
    {
        if (!isInstalled && !isDying)
        {
            //Starts Installing Animation
            StartCoroutine(AnimateInstall());
        }
    }

    public virtual void OnInstall() { }

    public virtual void Uninstall()
    {
        if (canUninstall && isInstalled && !isDying)
        {
            //Starts uninstalling animation
            StartCoroutine(AnimateUninstall());
        }
    }

    public virtual void OnUninstall() { }

    public virtual void Die()
    {
        //Ensures install/uninstall progress is stopped
        StopAllCoroutines();
        //checks if its not dying
        if (!isDying)
        {
            //flag as dying
            isDying = true;
            //Gives oponent the ram cost of this unit
            economyGC.AddRamToPlayer(ramCost, enemyTeamName);
            //Makes death animation
            StartCoroutine(AnimateDeath());
        }
    }

    //DAMAGE CONTROL

    public virtual void DealDamage(Software targetObject)
    {
        //Check if software is installed
        if (isInstalled && !isDying)
        {
            //Tell the other object to receive damage
            targetObject.ReceiveDamage(damagePoints);
        }
    }

    public virtual void ReceiveDamage(int damage)
    {
        //Check if software is installed
        if (isInstalled && !isDying)
        {
            //Removes healthpoints
            if (healthPoints >= 0)
            {
                healthPoints -= damage;
                if (healthPoints <= 0)
                {
                    //Makes sure healthpoints never become less than zero for animation purposes
                    healthPoints = 0;
                    Die();
                }
            }
        }
    }

    //ANIMATIONS
    /// <summary>
    /// -install: Show installation window
    /// -installPercentage: Move progress bar
    /// -activate: animate object installation
    /// -idle: idle state for static object or running anim for crawlers
    /// -deactivate: animate object uninstalling
    /// -uninstall
    /// -death
    /// </summary>
    /// <returns></returns>

    public virtual IEnumerator AnimateInstall()
    {
        //Removes RAM from team who spawned it
        economyGC.RemoveRamFromPlayer(ramCost, currentTeamName);

        //Begins install looping animation
        animator.SetTrigger("install");

        //Plays install sound
        audioSource.PlayOneShot(installSound);

        //Begins install percentage
        animator.SetFloat("installPercentage", 0f);

        //Starting variables
        float elapsedTime = 0;

        //as long as the elapsed time is less than the time i specified
        while (elapsedTime < installTime)
        {
            //Lerp
            animator.SetFloat("installPercentage", Mathf.Lerp(0f, 100f, (elapsedTime / installTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Ends install percentage
        animator.SetFloat("installPercentage", 100f);

        //Flags as installed
        isInstalled = true;

        //Calls on install
        OnInstall();

        //Animates installation end
        animator.SetTrigger("activate");

        //Plays activation sound
        audioSource.PlayOneShot(activateSound);


    }

    public virtual IEnumerator AnimateUninstall()
    {
        //Begins install looping animation
        animator.SetTrigger("uninstall");

        //Plays uninstall sound
        audioSource.PlayOneShot(uninstallSound);

        //Begins install percentage
        animator.SetFloat("installPercentage", 100f);

        //Starting variables
        float elapsedTime = 0;

        //as long as the elapsed time is less than the time i specified
        while (elapsedTime < uninstallTime)
        {
            //Lerp
            animator.SetFloat("installPercentage", Mathf.Lerp(100f, 0f, (elapsedTime / uninstallTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Ends install percentage
        animator.SetFloat("installPercentage", 0f);

        //Flags as installed
        isInstalled = false;

        //Calls on uninstall
        OnUninstall();

        //Animates uninstall end
        animator.SetTrigger("deactivate");

        //Plays deactivate sound
        audioSource.PlayOneShot(deactivateSound);

        //Removes gives RAM from team who spawned it
        economyGC.AddRamToPlayer(ramCost, currentTeamName);
    }

    public virtual IEnumerator AnimateDeath()
    {
        //Sets death animation trigger
        animator.SetTrigger("death");
        //Plays death sound
        audioSource.PlayOneShot(deathSound);
        //Waits for animation
        yield return new WaitForSeconds(5f);
        //gets destroyed
        Destroy(gameObject);
    }


}
