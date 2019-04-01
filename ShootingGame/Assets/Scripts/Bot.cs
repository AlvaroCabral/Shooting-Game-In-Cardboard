using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject BloodPrefab;
    public GameObject BotSmokeRespawn;
    public GameObject BotBlowSmokeParticleSystem;
    GamePlay gameplay;
    public AudioClip BotDeadSound;
    public int botPoints = 1;
    //Bot Life Variables
    int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameplay = GameObject.Find("GamePlay").GetComponent<GamePlay>();
        gameplay.RespanwGameObjects( BotSmokeRespawn, true, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (life > 1)
            {
                int BulletDamage = 10;
                if (other.name == "MagicBullet(Clone)")
                {
                    BulletDamage = 20;
                }
                if (other.name == "SpecialBullet(Clone)")
                {
                    BulletDamage = 30;
                }
                //Take Damage from Bullet
                damageHealth(BulletDamage);

                //Show blood after bullet colision
                gameplay.RespanwGameObjects( BloodPrefab, true, other.transform);

                //Check bot life and destroy if less than 1
                if (life < 1)
                {
                    //gamePlay = GameObject.Find("GamePlay").GetComponent<GamePlay>();
                    //gamePlay.UpdateScore();
                    PlaySound(BotDeadSound);
                    gameplay.RespanwGameObjects(BotBlowSmokeParticleSystem, true, other.transform);
                    gameplay.UpdateScore();
                    Destroy(gameObject);
                }

                //Destroy Bullet aftere colide

                Destroy(other.gameObject); // destroy object 
            }

        }
    }

    void damageHealth(int damage)
    {
        life -= damage;
        /*if (life == 70)
        {
            ChangeMaterial();
        }
        if (life == 30)
        {
            ChangeMaterial();
        }*/
    }

    void PlaySound(AudioClip SoundClip)
    {
        AudioSource Sound = GetComponent<AudioSource>();
        Sound.PlayOneShot(SoundClip);
    }

}
