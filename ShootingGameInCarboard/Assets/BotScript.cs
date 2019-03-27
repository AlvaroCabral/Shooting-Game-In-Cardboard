using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotScript : MonoBehaviour
{

    public GameObject BloodPrefab;

    //Change Bot Material Variables
    public Material[] Materials;
    int MaterialIndex = 0;
    Renderer Rend;

    //Bot Life Variables
    int life = 100;
    private float maxHitpoint = 100;
   
    int currentWP = 0;
    float rotSpeed = 2.2f;
    float speed = 1.5f;
    float accuracyWP = 5.0f;
    float maxTimer;
    int i = 0;
    float timer;
    bool timerOn;

    // Start is called before the first frame update
    void Start()
    {
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
        Rend.sharedMaterial = Materials[MaterialIndex];
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            //Take Damage from Bullet
            damageHealth(10);

            //Show blood after bullet colision
            BloodlSplater(other);

            //Check bot life and destroy if less than 1
            if (life < 1)
            {
                Destroy(gameObject);
            }

            //Destroy Bullet aftere colide
            Destroy(other.gameObject); // destroy object 
        }
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

 

    private void BloodlSplater(Collider collider)
    {
        GameObject Blood = GameObject.Instantiate(BloodPrefab, collider.transform.position, collider.transform.rotation.normalized) as GameObject;
        Destroy(Blood, 2);
    }
    

    private void ChangeMaterial()
    {
        Debug.Log("ChangeMaterial");
        Rend.sharedMaterial = Materials[MaterialIndex + 1];
    }
}
