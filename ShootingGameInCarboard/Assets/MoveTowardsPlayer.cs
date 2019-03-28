using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    private GameObject Player;
    private float MoveSpeed;
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
}
