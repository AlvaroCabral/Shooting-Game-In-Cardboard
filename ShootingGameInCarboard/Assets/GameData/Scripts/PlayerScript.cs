using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public GameObject[] BulletObjects;
    public GameObject DamagePanel;
    public GameObject GunSmoke;
    public Image Damage;
    public healthBar healthBarScript;
    public AudioClip CollisionSound;
    public AudioClip ScoreSound, PerfectSound,GameOverSound,GunReloadSound;
    GamePlay gamePlay;
    GameObject BulletSpawn;
    AudioSource Sound;

    int life = 100;
    float DamagePanelTransparency = 0.7f;
    bool DamageCheck = false;
    string oldScore;
    TextMeshProUGUI TxtScore;
    Color OldColor;

    private bool gamePlayCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<AudioSource>();
        TxtScore = GameObject.Find("Score_Number").GetComponent<TextMeshProUGUI>();
        oldScore = TxtScore.text;
        OldColor = Damage.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (DamageCheck)
        {
            ShowDamage();
        }
        if(oldScore != TxtScore.text)
        {
            PlayScoreSound();
            oldScore = TxtScore.text;

        }
    }

    public void Fire()
    {
        if (gamePlayCheck)
        {
            BulletSpawn = GameObject.Find("BulletSpawn");
            // Create the Bullet from the Bullet Prefab
            int bulletIndex = Random.Range(0, 3);
            GameObject bullet = Instantiate(
                this.BulletObjects[bulletIndex],
                this.BulletSpawn.transform.position,
                this.BulletSpawn.transform.rotation);

            GameObject Blood = GameObject.Instantiate(GunSmoke, BulletSpawn.transform.position, BulletSpawn.transform.rotation.normalized) as GameObject;
            Destroy(Blood, 2);

            bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.transform.forward * 5;


            // Destroy the bullet after 2 seconds
            Destroy(bullet, 6.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Take Damage from Bullet
            UpdateHealth(-10);

            //Show blood after bullet colision
            //BloodlSplater(other);

            //Check bot life and destroy if less than 1
            if (life < 1)
            {
                gamePlay = GameObject.Find("GamePlay").GetComponent<GamePlay>();
                gamePlay.GameOver();
                Sound.PlayOneShot(GameOverSound);
                //Destroy(gameObject);
            }

            
        }
        if (other.tag == "BulletReload")
        {
            Sound.PlayOneShot(GunReloadSound);
        }
        if (other.tag == "Reward")
        {
            UpdateHealth(10);
        }
        //Destroy Bullet aftere colide
        Destroy(other.gameObject); // destroy object 
    }

    void UpdateHealth(int damage)
    {
        life += damage;
        if(life > 100)
        {
            life = 100;
        }
        DamageCheck = true;
        healthBarScript.SetHealth(life);
        Sound.PlayOneShot(CollisionSound);
    }

    void ShowDamage()
    {
        Debug.Log("Show damage");
        DamagePanel.SetActive(true);
        var tempColor = Damage.color;
        tempColor.a = DamagePanelTransparency; //1f makes it fully visible, 0f makes it fully transparent.
        Damage.color = tempColor;
        if(DamagePanelTransparency > 0)
        {
            DamagePanelTransparency -= 0.01f;
        }
        else
        {
            DamageCheck = false;
            DamagePanel.SetActive(false);
            Damage.color = OldColor;
            DamagePanelTransparency = 0.7f;
        }
    }

    void PlayScoreSound()
    {
        Sound.PlayOneShot(ScoreSound);
    }

    public void PlayPerfectSound()
    {
        Sound.PlayOneShot(PerfectSound);
    }

    public void ChangeGameStatus(bool status)
    {
        gamePlayCheck = status;
    }

}
