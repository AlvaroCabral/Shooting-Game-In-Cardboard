using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotScript : MonoBehaviour
{

    public GameObject BloodPrefab;
    GamePlay gamePlay;

    float MoveSpeed;
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
    private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = Random.Range(1f, 2f);

        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
        Rend.sharedMaterial = Materials[MaterialIndex];

        Player = GameObject.Find("Player");
        startPoint = this.transform.position;
        endPoint = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);

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
                gamePlay = GameObject.Find("GamePlay").GetComponent<GamePlay>();
                gamePlay.UpdateScore();
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
