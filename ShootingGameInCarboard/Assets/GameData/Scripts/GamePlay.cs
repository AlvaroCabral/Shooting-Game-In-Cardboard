using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlay : MonoBehaviour
{
    public GameObject EnemyObject;
    public GameObject MenuObject;
    public GameObject RewardBoxObject;
    public PlayerScript PlayerS;
    public GameObject ButtonSwitch;
    public float ratio = 5;
    public float maxRange;
    public float minRange;
    public GameObject ResumeButton;

    public bool gamePlayCheck = true;
    int Score = 0;

    float ScoreFontSize = 40;
    bool ScorePerfect = false;
    float spawnTime = 1;
    float spawnDelayEnemy = 0.3f, spawnDelayReward = 20;
    // Start is called before the first frame update
    void Start()
    {
        spawnDelayEnemy = Random.Range(2, 5);
        spawnDelayReward = Random.Range(10, 25);
        InvokeRepeating("CreateNewEnemy", spawnTime, spawnDelayEnemy);
        InvokeRepeating("CreateNewReward", spawnTime, spawnDelayReward);
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
        RespanwGameObjects(EnemyObject);
    }
    void CreateNewReward()
    {
        RespanwGameObjects(RewardBoxObject);
    }

    //Respawn object in a position, if transform atributes are not passed a new positin is calculated.
    public void RespanwGameObjects(GameObject gameObject, Transform objectPosition = null)
    {
        Transform newTransform = gameObject.transform;
        if (objectPosition == null)
        {
            //Create new random Position and instatiate new object
            newTransform.position = new Vector3(randomOffSetRadius(), Random.Range(1, 7), randomOffSetRadius());
            newTransform.rotation = transform.rotation;
        }
        else
        {
            newTransform.position = objectPosition.position;
            newTransform.rotation = objectPosition.rotation;
        }

        Instantiate(gameObject, newTransform.position, newTransform.rotation);
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

    //Update Canvas with Score
    public void UpdateScore()
    {
        Score += 1;
        TextMeshProUGUI TxtScore = GameObject.Find("Score_Number").GetComponent<TextMeshProUGUI>();
        TxtScore.text = string.Format("{0}", Score);

        float perfect = Score % 10;
        if (perfect == 0)
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MenuObject.SetActive(true);
        gamePlayCheck = false;
        PlayerS.ChangeGameStatus(gamePlayCheck);
        ResumeButton.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        MenuObject.SetActive(false);
        gamePlayCheck = true;
        PlayerS.ChangeGameStatus(gamePlayCheck);
    }

    public void GameOver()
    {
        PauseGame();
        ResumeButton.SetActive(false);
        ButtonSwitch.SetActive(false);
    }

    //Function for intercative gameObject Button in the Scene
    public void PauseResume()
    {
        if (gamePlayCheck)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

}
