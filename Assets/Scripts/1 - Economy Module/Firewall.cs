using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Firewall : Software {

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
        healthBarImage.fillAmount = healthPoints / maxHealthPoints;
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



}