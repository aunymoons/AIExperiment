using UnityEngine;
using System.Collections;

public class ShootingTower : Software {

    //MAIN
    public float reloadTime;

    //AUDIO
    public AudioClip shootSound;
	
	// Update is called once per frame
	void Update () {
	
	}

    void Aim()
    {

    }

    void Shoot()
    {
        ShootingAnimation();
        audioSource.PlayOneShot(shootSound);
    }

    void ShootingAnimation()
    {
        animator.SetTrigger("shoot");
    }
}
