using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] bulletObjects;
    public GameObject bulletSpawn;
    public GamePlay gameplay;
    public AudioClip HealthDamageSound;
    public HealthBar healthBar;

    private int bulletIndex = 0;
    private int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(
            this.bulletObjects[bulletIndex],
            this.bulletSpawn.transform.position,
            this.bulletObjects[bulletIndex].transform.rotation);

        //GameObject Blood = GameObject.Instantiate(GunSmoke, BulletSpawn.transform.position, BulletSpawn.transform.rotation.normalized) as GameObject;
        //Destroy(Blood, 2);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 7;


        // Destroy the bullet after 2 seconds
        Destroy(bullet, 6.0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Take Damage from Bullet
            UpdateHealth(-10);

            //Show blood after bullet colision
            //BloodlSplater(other);

            //Check bot life and destroy if less than 1
            if (life < 1)
            {
                //gamePlay.GameOver();
                //Sound.PlayOneShot(GameOverSound);
                //Destroy(gameObject);
                gameplay.GameOver();
            }
            //Destroy object after collide with player
            Destroy(other.gameObject); // destroy object 
        }

        /*if (other.tag == "BulletReload")
        {
            Sound.PlayOneShot(GunReloadSound);
        }
        if (other.tag == "Reward")
        {
            UpdateHealth(10);
        }*/

        
    }

    void UpdateHealth(int damage)
    {
        life += damage;
        if (life > 100)
        {
            life = 100;
        }
        //DamageCheck = true;
        healthBar.UpdateHealth(life);
        PlaySound(HealthDamageSound);
        gameplay.DisplayDamage();
    }

    void PlaySound(AudioClip SoundClip)
    {
        AudioSource Sound = GetComponent<AudioSource>();
        Sound.PlayOneShot(SoundClip);
    }
}
