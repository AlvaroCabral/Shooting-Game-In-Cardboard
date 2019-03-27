using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public GameObject EnemyObject;
    public int max;

    float ratio = 5;
    float ratioHeight = 3;
    

    // Start is called before the first frame update
    void Start()
    {   
        //Get Position Values
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        Vector3 position = new Vector3(Random.Range(x + ratio, x + ratio + max), 1, 0);
        Instantiate(EnemyObject, position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
