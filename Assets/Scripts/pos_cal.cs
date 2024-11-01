using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pos_cal : MonoBehaviour
{
    public Vector3 str_pos; 
    public Vector3 cur_pos;
    public Transform str_frame;
    public Vector3 uptest;
    public Vector3 righttest;

    public Vector2 calculate_pos()
    {
        Vector3 posdiff = cur_pos - str_pos;
        Vector3 xdir = str_frame.right;
        Vector3 ydir = str_frame.up;
        float xpos = Vector3.Dot(posdiff, xdir);
        float ypos = Vector3.Dot(posdiff, ydir);
        return new Vector2(xpos, ypos);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uptest = transform.up;
        righttest = transform.right;
    }
}
