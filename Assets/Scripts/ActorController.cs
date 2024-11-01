using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class ActorController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterControllert;
    [SerializeField] private float speed;
    [SerializeField] private CamCrontroller refcam;
    private float currentV;
    private Vector3 lastv;
    //private Transform myt;

    public VisualEffect vf;
    public int spawnposid;
    
    //for fps count---------------------
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private float fps;
    //-----------------------------------
    private Vector2 mouseinput;
    private Vector2 mousepos;
    private Vector2 kbinput;
    public float movecirclerad;
    public float curang;

    //eat coin
    //[SerializeField] private int score;
    
    //public void addscore()
    //{
    //    score++;
    //}

    void Start()
    {
        characterControllert = GetComponent<CharacterController>();
        //myt = GetComponent<Transform>();
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        //score = 0;
        movecirclerad = 1.9f;
        spawnposid = Shader.PropertyToID("spawnpos");
    }

    public void mouseposcallback(InputAction.CallbackContext context)
    {
        mousepos = context.ReadValue<Vector2>();
        //Debug.Log(mousepos);
    }
    public void mousemovecallback(InputAction.CallbackContext context)
    {
        mouseinput = context.ReadValue<Vector2>();
        //Debug.Log(input);
        //characterControllert.Move(new Vector3(input.x, 0, input.y) * Time.deltaTime*0.1f);
        //transform.position = new Vector3(transform.position.x + input.x * Time.deltaTime/5f, transform.position.y, transform.position.z + input.y * Time.deltaTime / 5f);
        //fpstick();
    }
    public void keyboardwasdcallback(InputAction.CallbackContext context)
    {
        kbinput = (context.ReadValue<Vector2>())*2f;
    }

    private void movechar(Vector3 inp, Vector3 dir)
    {
        //Debug.Log(inp);
        //Debug.Log(dir);
        if (inp.x == 0 && inp.z == 0)
        {
            return;
        }
        lastv = Quaternion.FromToRotation(new Vector3(0,0,1),dir)*inp;
        float targetangle = Mathf.Atan2(lastv.x, lastv.z) * Mathf.Rad2Deg;
        float ntangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref currentV, 0.05f);
        transform.rotation = Quaternion.Euler(0, ntangle, 0);
        characterControllert.Move(lastv * Time.deltaTime);
    }

    private void circlemove(Vector2 inp)
    {
        curang = Mathf.Atan2(inp.y-1080/2, inp.x-1920/2);
        Vector3 newpos = new Vector3(Mathf.Cos(curang) * movecirclerad, Mathf.Sin(curang) * movecirclerad, transform.position.z);
        transform.position = newpos;
        vf.SetVector3(spawnposid, newpos);

    }

    private void fpstick()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //fpstick();
        //Debug.Log(fps);
        //Debug.Log(refcam.lookdir);
        //float xspeed = 0;
        //float yspeed = 0;
        //if (UnityEngine.Input.GetKey(KeyCode.D))
        //{
        //    xspeed += 1;
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.A))
        //{
        //    xspeed += -1;
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.W))
        //{
        //    yspeed += 1;
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.S))
        //{
        //    yspeed += -1;
        //}
        //if (xspeed != 0 || yspeed != 0)
        //{
        //    lasx = xspeed;
        //    lasy = yspeed;
        //}
        //with collide
        //characterControllert.Move(new Vector3 (xspeed, 0, yspeed) *Time.deltaTime);
        //nocollide
        //transform.position = new Vector3(transform.position.x + xspeed * Time.deltaTime, transform.position.y, transform.position.z + yspeed * Time.deltaTime);


        //movechar(new Vector3(kbinput.x, 0, kbinput.y), (new Vector3(refcam.lookdir.x, 0, refcam.lookdir.z)).normalized);

        circlemove(mousepos);

    }
}
