using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public UnityEvent<int,int> updateUI;

    public UnityEvent<float,float,float> updateang;
    private float previousang;
    public float paddleangle;
    public float scale_rad;
    public float roc;//radius of curvature
    public float movespeed;

    public GameObject coinPrefab;
    //public AudioSource coinsong;
    public AudioClip coinsong;
    public float movecirclerad;
    private double[] song;
    public bool start;
    //public float coinspeed;
    //public float cointime;
    private float starttime;
    private int curindex;
    private float ang;
    private float timeoffset;
    public float device_delay;
    private float lasttime;
    public int combo;
    public int score;


    public void onEatcoin()
    {
        combo++;
        score += Mathf.Clamp(combo,1,10);
        Debug.Log("eaten");
        updateUI.Invoke(combo,score);
    }

    public void onMiss()
    {
        Debug.Log("miss");
        combo = 0;
        updateUI.Invoke(combo, score);
    }

    // Start is called before the first frame update
    void Start()
    {
        start = false;
       // song = new double[] {5.34590149,  6.01444483,  6.6246829 ,  7.21238947,  7.80988955,
       // 8.35102582,  8.9306097 ,  9.58159471, 10.15284634, 10.74267721,
       //11.34490204, 11.97191834, 12.59629297, 13.18738747, 13.80214214,
       //14.38213921, 14.65637016, 14.97449327, 15.28083849, 15.55544996,
       //15.82557082, 16.16553617, 16.42271662, 16.75739312, 17.05223083,
       //17.35202932, 17.65572572, 17.95523381, 18.25701833, 18.56719804,
       //18.8264029 , 19.13851118, 19.45877147, 19.7644403 , 20.02922201,
       //20.31844854, 20.62206364, 20.9693594 , 21.26433349, 21.53062105,
       //21.84566331, 22.18105054, 22.44831896, 22.75942063, 23.05139303,
       //23.38200879, 23.9859848 , 24.53244996, 25.7826345 , 26.93287182,
       //28.14316773, 29.38056469, 29.97518682, 30.57791948, 31.16803932,
       //31.77691054, 32.37710452, 32.97114754, 33.54153109, 34.7722311 ,
       //35.96660471, 37.1781168 , 38.36492014, 39.58584404, 40.16662169,
       //40.73342371, 41.93740988, 42.53190088, 43.13017797, 44.8024559 ,
       //45.58010626, 46.75942802, 47.88318872, 48.79918981, 49.4929812 ,
       //50.06332994, 50.69323874, 51.29623079, 51.86196542, 52.46612263,
       //53.04688549, 53.61833262, 54.2573514 , 54.90130472, 55.47234225,
       //56.09587502, 56.66973758, 57.24076271, 57.88851404, 58.50619006,
       //59.11338735, 59.65954065, 60.23599148, 60.8229115 , 61.36522436,
       //61.97934866, 62.64518595, 63.2796371 , 63.85058355, 64.46796393,
       //65.12964344, 65.67750144, 66.27658558, 66.84516811, 67.43270731,
       //68.08512974, 68.71540046, 69.30615973, 69.8934257 };
        song = new double[] {5.4,  6 ,  6.6,  7.2,  7.8,  8.4,  9 ,  9.6, 10.2, 10.8, 11.4,
       12 , 12.6, 13.2, 13.8, 14.4, 14.7, 15 , 15.3, 15.6, 15.9, 16.2,
       16.5, 16.8, 17.1, 17.4, 17.7, 18 , 18.3, 18.6, 18.9, 19.2, 19.5,
       19.8, 20.1, 20.4, 20.7, 21 , 21.3, 21.6, 21.9, 22.2, 22.5, 22.8,
       23.1, 23.4, 24 , 24.6, 25.8, 27 , 28.2, 29.4, 30 , 30.6, 31.2,
       31.8, 32.4, 33 , 33.6, 34.8, 36 , 37.2, 38.4, 39.6, 40.2, 40.8,
       42 , 42.6, 43.2, 44.7, 45.6, 46.8, 48 , 48.9, 49.5, 50.1, 50.7,
       51.3, 51.9, 52.5, 53.1, 53.7, 54.3, 54.9, 55.5, 56.1, 56.7, 57.3,
       57.9, 58.5, 59.1, 59.7, 60.3, 60.9, 61.5, 62.1, 62.7, 63.3, 63.9,
       64.5, 65.1, 65.7, 66.3, 66.9, 67.5, 68.1, 68.7, 69.3, 69.9};
        curindex = 0;
        movecirclerad = 1.9f;
        ang = Random.value * Mathf.PI * 2;
        timeoffset = (25f - 3f) / 10f +0.3f + device_delay;
        combo = 0;
        score = 0;

        paddleangle = 90f;
        previousang = 90f;
        roc = 3.8f;//1.9, scale0.5
        scale_rad = 0.3f;
        movespeed = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (paddleangle != previousang)
        {
            previousang = paddleangle;
            updateang.Invoke(paddleangle,roc,scale_rad);
        }
        if (!start && Input.GetMouseButton(0))
        {
            start = true;
            starttime = Time.time;
            //coinsong.PlayOneShot(coinsong.clip, 0.5f);
            AudioSource.PlayClipAtPoint(coinsong,transform.position);
        }
        //Debug.Log(Time.time - starttime);
        if(start && curindex < song.Length && (Time.time - starttime) > (song[curindex] - timeoffset))
        {
            float td = (float)song[curindex] - lasttime;
            lasttime = (float)song[curindex];
            curindex++;
            ang += (Random.value-0.5f)* Mathf.PI*td;
            Instantiate(coinPrefab, new Vector3(Mathf.Cos(ang) * movecirclerad, Mathf.Sin(ang) * movecirclerad, 25), Quaternion.Euler(0, 0, ang*Mathf.Rad2Deg));
            //Debug.Log("coin created");
        }
    }
}
