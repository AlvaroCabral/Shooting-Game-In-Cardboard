using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image ImgHealthBar;

    public Text TxtHealthBar;

    public int Min, Max;

    private int CurrentValue;

    private float CurrentPercentage;

    public void SetHealth(int health)
    {
        if(health != CurrentValue)
        {
            if(Max - Min == 0)
            {
                CurrentValue = 0;
                CurrentPercentage = 0;
            }
            else
            {
                CurrentValue = health;
                CurrentPercentage = (float)CurrentValue / (float)(Max - Min);
            }

            TxtHealthBar.text = string.Format("{0} %", Mathf.RoundToInt(CurrentPercentage * 100));

            ImgHealthBar.fillAmount = CurrentPercentage;
        }
    }

    public float GetCurrentValue
    {
        get { return CurrentValue; }
    }

    public float GetCurrentPercentage
    {
        get { return CurrentPercentage;  }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
