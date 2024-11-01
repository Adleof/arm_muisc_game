using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CamCrontroller : MonoBehaviour
{
    private float currentV;
    [SerializeField] private Transform target;
    private Vector2 mouseinput;
    private Vector3 reallocation;
    public Vector3 lookdir;
    public Vector3 pivot;
    private float yaw;
    private float pitch;
    private float viewdist;
    private float mouse_speed_mult;
    private Vector2 mousepos;
    public float cam_actor_dist;
    public float cam_lookat_scale;
    // Start is called before the first frame update
    void Start()
    {
        viewdist = 2.5f;
        reallocation = new Vector3(0, 0, 0);
        yaw = 0;
        pitch = 0;
        mouse_speed_mult = 0.5f;
        pivot = new Vector3(0, 0, 0.01f);
        cam_actor_dist = 3f;
        cam_lookat_scale = 0.2f;
    }

    public void mousemovecallback(InputAction.CallbackContext context)
    {
        mouseinput = context.ReadValue<Vector2>()*0.1f;
        yaw += -mouseinput.x* mouse_speed_mult;
        pitch = Mathf.Clamp(pitch + mouseinput.y* mouse_speed_mult, -70f,70f);
        //lookdir = Quaternion.AngleAxis(mouseinput.x,new Vector3(0,1,0)) * lookdir;
        //lookdir = Quaternion.AngleAxis(-mouseinput.y, new Vector3(1, 0, 0)) * lookdir;//change rotaton axis to local

    }
    public void mouseposcallback(InputAction.CallbackContext context)
    {
        mousepos = context.ReadValue<Vector2>();
        //Debug.Log(mousepos);
    }
    private void camrot_mousediff()
    {
        lookdir = new Vector3(Mathf.Cos(yaw*Mathf.Deg2Rad)*Mathf.Cos(pitch*Mathf.Deg2Rad), Mathf.Sin(pitch * Mathf.Deg2Rad), Mathf.Sin(yaw * Mathf.Deg2Rad) * Mathf.Cos(pitch * Mathf.Deg2Rad));
        lookdir *= viewdist;
        transform.rotation = Quaternion.LookRotation(lookdir, new Vector3(0, 1, 0));
    }
    private void camrot_mousepos(Vector2 inp)
    {
        float ang = Mathf.Atan2(inp.y, inp.x);
        //float mag = inp.magnitude/1000f;
        float mag = 1.0f;
        Vector3 lokatpos = new Vector3(Mathf.Cos(ang) * mag * cam_lookat_scale, Mathf.Sin(ang) * mag * cam_lookat_scale, 3);
        transform.position = lokatpos+(pivot-lokatpos)*cam_actor_dist/(cam_actor_dist - pivot.magnitude);
        transform.rotation = Quaternion.LookRotation(lokatpos - pivot, new Vector3(0, 1, 0));
    }
    // Update is called once per frame
    void Update()
    {
        camrot_mousepos(new Vector2(mousepos.x-1920/2f,mousepos.y-1080/2f));
        //camrot_mousediff();
        //Vector3 posdiff = target.transform.position - reallocation;
        //float movedist = posdiff.magnitude * 0.01f;
        //reallocation = reallocation + movedist * posdiff.normalized;
        //transform.position = reallocation - lookdir;
        //Debug.Log(target);
    }
}
