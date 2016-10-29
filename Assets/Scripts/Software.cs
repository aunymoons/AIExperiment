using UnityEngine;
using System.Collections;

public abstract class Software : MonoBehaviour
{

    //What team they belong to
    public string currentTeamName, enemyTeamName;
    //Price for installing this software
    public int ramCost;
    //Installing/uninstalling times
    public float installTime, uninstallTime;
    //Stats
    public int healthPoints, damagePoints;

    //MAIN METHODS
    //Actions
    public abstract void Die();

    //Related to damage

    public virtual void DealDamage(Software targetObject)
    {
        //Tell the other object to receive damage
        targetObject.ReceiveDamage(damagePoints);
    }

    public virtual void ReceiveDamage(int damage)
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

    //ANIMATIONS

    public abstract void AnimateSpawn();

    public abstract void AnimateDeath();

    //TODO: HP bar animation.

}
