using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GamePlay : MonoBehaviour
{
    public GameObject MenuObject;
    public GameObject[] EnemyObjects;
    public GameObject ResumeButton;
    public GameObject closeButton;
    public GameObject DamagePanel;
    public Image DamagePanelImage;
    public AudioClip GameOverSound,PerfectSound,PointSound;
    private bool gamePlayStatus;

    public float ratio = 5;
    public float maxRange;
    public float minRange;

    float spawnTime = 1;
    float spawnDelayEnemy = 4f, spawnDelayReward = 20;

    public int menuSceneIndex = 0;
    public int gameSceneIndex = 1;

    int totalPoints = 0;
    private bool pointsPerfect;
    float pointsFontSize = 50;

    float DamagePanelTransparency = 0.7f;
    bool DamageCheck = false;
    Color OldColor;

    //AudioSource Sound;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Game");
        Debug.Log(totalPoints);
        Time.timeScale = 1f;
        MenuObject.SetActive(false);
        InvokeRepeating("CreateNewEnemy", spawnTime, spawnDelayEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsPerfect)
        {
            AnimatePerfectScore();
        }
        if (DamageCheck)
        {
            ShowDamage();
        }

    }

    void CreateNewEnemy()
    {
        int num = Random.Range(0, EnemyObjects.Length);
        RespanwGameObjects(EnemyObjects[num], false , getRandomPosition());
    }

    Transform getRandomPosition()
    {
        Transform newRandomPosition = transform;
        newRandomPosition.position = new Vector3(randomOffSetRadius(), Random.Range(1, 7), randomOffSetRadius());
        newRandomPosition.rotation = transform.rotation;

        return newRandomPosition;
    }

    float randomOffSetRadius()
    {
        float num = Random.Range(minRange, maxRange);
        if ((num > (0 - ratio)) && (num < ratio))
        {
            num = randomOffSetRadius();
        }
        return num;
    }

    public void RespanwGameObjects(GameObject gameObject, bool destroy,Transform SpawnPosition = null)
    {
        if (SpawnPosition == null)
        {
            SpawnPosition = transform;
        }
        GameObject Blow = GameObject.Instantiate(gameObject, SpawnPosition.transform.position, gameObject.transform.rotation.normalized) as GameObject;
        if (destroy)
        {
            Destroy(Blow, 2);
        }
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MenuObject.SetActive(true);
        closeButton.SetActive(false);
        ResumeButton.SetActive(true);
        gamePlayStatus = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        MenuObject.SetActive(false);
        closeButton.SetActive(true);
        gamePlayStatus = true;
    }

    public void GameOver()
    {
        PauseGame();
        ResumeButton.SetActive(false);
        PlaySound(GameOverSound);

    }

    public  void ExitGame()
    {
        LoadScene(menuSceneIndex);
    }

    public void RestartGame()
    {
        LoadScene(gameSceneIndex);
    }

    void PlaySound(AudioClip SoundClip)
    {
        AudioSource Sound = GetComponent<AudioSource>();
        Sound.PlayOneShot(SoundClip);
    }

    //Load Menu
    void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void UpdateScore()
    {
        totalPoints += 1;
        TextMeshProUGUI PointsText = GameObject.Find("Points_Number").GetComponent<TextMeshProUGUI>();
        PointsText.text = string.Format("{0}", totalPoints);

        PlaySound(PointSound);
        Debug.Log(totalPoints);
        float perfect = totalPoints % 10;
        if (perfect == 0)
        {
            pointsPerfect = true;
            PointsText.fontSize = 100;
            PlaySound(PerfectSound);
        }
    }

    void AnimatePerfectScore()
    {
        TextMeshProUGUI PointsText = GameObject.Find("Points_Number").GetComponent<TextMeshProUGUI>();
        if (PointsText.fontSize > pointsFontSize)
        {
            PointsText.fontSize = PointsText.fontSize - 1;
        }
        else
        {
            pointsPerfect = false;
        }
    }

    public void DisplayDamage()
    {
        DamageCheck = true;
    }
    void ShowDamage()
    {
        Debug.Log("Show damage");
        DamagePanel.SetActive(true);
        OldColor = DamagePanelImage.color;
        var tempColor = DamagePanelImage.color;
        tempColor.a = DamagePanelTransparency; //1f makes it fully visible, 0f makes it fully transparent.
        DamagePanelImage.color = tempColor;
        if (DamagePanelTransparency > 0)
        {
            DamagePanelTransparency -= 0.01f;
        }
        else
        {
            DamageCheck = false;
            DamagePanel.SetActive(false);
            DamagePanelImage.color = OldColor;
            DamagePanelTransparency = 0.7f;
        }
    }

}
