﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject BulletObject;
    public GameObject DamagePanel;
    public Image Damage;
    public healthBar healthBarScript;
    GameObject BulletSpawn;

    int life = 100;
    float DamagePanelTransparency = 0.7f;
    bool DamageCheck = false;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (DamageCheck)
        {
            DamagePanel.SetActive(true);
            ShowDamage();
        }
    }

    public void Fire()
    {
        BulletSpawn = GameObject.Find("BulletSpawn");

        // Create the Bullet from the Bullet Prefab
        Debug.Log(BulletSpawn);
        GameObject bullet = Instantiate(
            this.BulletObject,
            this.BulletSpawn.transform.position,
            this.BulletSpawn.transform.rotation);

        bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.transform.forward * 5;


        // Destroy the bullet after 2 seconds
        Destroy(bullet, 6.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Take Damage from Bullet
            damageHealth(10);

            //Show blood after bullet colision
            //BloodlSplater(other);

            //Check bot life and destroy if less than 1
            if (life < 1)
            {
                
                //Destroy(gameObject);
            }

            Debug.Log("Enemy");

            //Destroy Bullet aftere colide
            Destroy(other.gameObject); // destroy object 
        }
    }

    void damageHealth(int damage)
    {
        life -= damage;
        DamageCheck = true;
        healthBarScript.SetHealth(life);
    }

    void ShowDamage()
    {
        var tempColor = Damage.color;
        tempColor.a = DamagePanelTransparency; //1f makes it fully visible, 0f makes it fully transparent.
        Damage.color = tempColor;
        if(DamagePanelTransparency > 0)
        {
            Debug.Log("changetransparebcy");
            DamagePanelTransparency -= 0.01f;
        }
        else
        {
            DamageCheck = false;
        }
    }

}