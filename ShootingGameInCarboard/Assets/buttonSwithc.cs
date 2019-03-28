using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSwithc : MonoBehaviour
{
    public Material MaterialOn, MaterialOff;
    bool Switch = false;
    Renderer Rend;

    // Start is called before the first frame update
    void Start()
    {
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
        Rend.sharedMaterial = MaterialOff;
    }

   public void ButtonSwitch(bool GameStatus)
    {
        if (GameStatus)
        {
            Rend.sharedMaterial = MaterialOff;
        }
        else
        {
            Rend.sharedMaterial = MaterialOn;
        }
    }
}
