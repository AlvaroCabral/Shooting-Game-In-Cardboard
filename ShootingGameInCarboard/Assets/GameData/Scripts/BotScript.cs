using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotScript : MonoBehaviour
{

    public GameObject BloodPrefab;
    GamePlay gamePlay;
    
    //Change Bot Material Variables
    public Material[] Materials;
    int MaterialIndex = 0;
    Renderer Rend;

    //Bot Life Variables
    int life = 100;
    private float maxHitpoint = 100;

    //Bot Move Variables
    private Vector3 startPoint;
    private Vector3 endPoint;

    private float distance= 30f;
    
    // Start is called before the first frame update
    void Start()
    {
       

        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
        Rend.sharedMaterial = Materials[MaterialIndex];

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if(life > 1)
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
                StartParticleSystem(other, BloodPrefab);

                //Check bot life and destroy if less than 1
                if (life < 1)
                {
                    gamePlay = GameObject.Find("GamePlay").GetComponent<GamePlay>();
                    gamePlay.UpdateScore();
                    Destroy(gameObject);
                }

                //Destroy Bullet aftere colide

                Destroy(other.gameObject); // destroy object 
            }
            
        }
    }

    public void StartParticleSystem(Collider collider, GameObject PreFab)
    {
        GameObject Blow = GameObject.Instantiate(PreFab, collider.transform.position, collider.transform.rotation.normalized) as GameObject;
        Destroy(Blow, 2);
    }


    void damageHealth(int damage)
    {
        life -= damage;
        if (life == 70)
        {
            ChangeMaterial();
        }
        if (life == 30)
        {
            ChangeMaterial();
        }
    }
  
    private void ChangeMaterial()
    {
        Debug.Log("ChangeMaterial");
        Rend.sharedMaterial = Materials[MaterialIndex + 1];
    }
}
