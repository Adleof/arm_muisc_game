using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIcombo_disp : MonoBehaviour
{
    public void on_eatupdate(int combo, int score)
    {
        //Debug.Log("Received combo" + combo.ToString());
        if (combo == 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Combo: " + combo.ToString();
        }
        
    }
}
