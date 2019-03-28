using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public GameObject GunHitPrefab;
    public GameObject BoxBlowPrefab;
    public GameObject[] Rewards;
    GamePlay gamePlay;
    float MoveSpeed;
    private GameObject Player;
    int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = Random.Range(1f, 2f);
        Player = GameObject.Find("Player");
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
            if (life > 1)
            {
                
                //Take Damage from Bullet
                damageHealth(Random.Range(25, 40));

                //Show blood after bullet colision
                //BloodlSplater(other);
                StartParticleSystem(other, GunHitPrefab);

                //Check bot life and destroy if less than 1
                if (life < 1)
                {
                    gamePlay = GameObject.Find("GamePlay").GetComponent<GamePlay>();
                    gamePlay.UpdateScore();
                    StartParticleSystem(other, BoxBlowPrefab);
                    Reward();
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
    }

    void Reward()
    {
        int reward = Random.Range(0, Rewards.Length);
        gamePlay.RespanwGameObjects(Rewards[reward], this.transform);
    }
}
