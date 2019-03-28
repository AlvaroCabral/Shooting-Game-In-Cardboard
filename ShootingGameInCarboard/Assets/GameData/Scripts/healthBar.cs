using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthBar : MonoBehaviour
{
    public Image ImgHealthBar;
    public TextMeshProUGUI TxtHealthBar;
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
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(100);
    }
}
