using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlay : MonoBehaviour
{
    public GameObject EnemyObject;
    public PlayerScript PlayerS;
    public float ratio = 5;
    public float maxRange;
    public float minRange;


    int Score = 0;

    float ScoreFontSize = 40;
    bool ScorePerfect = false;

    // Start is called before the first frame update
    void Start()
    {
        float spawnTime = 1;
        float spawnDelay = Random.Range(3, 7);
        InvokeRepeating("CreateNewEnemy", spawnTime, spawnDelay);

    }
    // Update is called once per frame
    void Update()
    {
        if (ScorePerfect)
        {
            AnimatePerfectScore();
        }

    }

    void CreateNewEnemy()
    {
        //Create New Position and instatiate new object(enemy)
        Vector3 position = new Vector3(randomOffSetRadius(), Random.Range(1, 7), randomOffSetRadius());
        Instantiate(EnemyObject, position, transform.rotation);
    }

    // Create a random point between a range and out of the ratio inside the range;
    float randomOffSetRadius()
    {
        float num = Random.Range(minRange, maxRange);
        if ((num > (0 - ratio)) && (num < ratio))
        {
            num = randomOffSetRadius();
        }
        return num;
    }

    public void UpdateScore()
    {
        Score += 1;
        TextMeshProUGUI TxtScore = GameObject.Find("Score_Number").GetComponent<TextMeshProUGUI>();
        TxtScore.text = string.Format("{0}", Score);

        float perfect = Score % 10;
        if(perfect == 0)
        {
            ScorePerfect = true;
            TxtScore.fontSize = 100;
            PlayerS.PlayPerfectSound();
        }
    }

    void AnimatePerfectScore()
    {

        TextMeshProUGUI TxtScore = GameObject.Find("Score_Number").GetComponent<TextMeshProUGUI>();
        
        if (TxtScore.fontSize > ScoreFontSize)
        {
            TxtScore.fontSize = TxtScore.fontSize - 1;
        }
        else
        {
            ScorePerfect = false;
        }
    }
}
