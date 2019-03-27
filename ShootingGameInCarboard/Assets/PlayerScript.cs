using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject BulletObject;
    GameObject BulletSpawn;

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
        bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.transform.forward * 2;


        // Destroy the bullet after 2 seconds
        Destroy(bullet, 10.0f);
    }
}
