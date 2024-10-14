using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIscore_disp : MonoBehaviour
{
    public void on_eatupdate(int combo, int score)
    {
        //Debug.Log("Received score" + score.ToString());
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }
}
