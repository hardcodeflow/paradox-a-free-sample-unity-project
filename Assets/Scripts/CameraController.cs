using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    float size1;
    float size2;
    float timer;
    bool goingUp = true;
    public Transform target;
    public bool goToTarget;
    Vector3 vel;
    Vector3 vel2;
    Vector3 quakeVel;
    Vector3 quakeTarget;
  
    float quakeTime;
    float quakeDuration = 0.4f;
    bool doingAQuake;
   internal  void DoAQuake() {
        quakeTime = Time.time + quakeDuration;
        doingAQuake = true;
    }
    void Start()
    {
        size1 = 10f;
        size2 = 11f;
    }
    internal void GivePointEffec() {
        GetComponent<Animator>().Play("bloom");
    }
    internal void GameOver()
    {
        GetComponent<Animator>().Play("gameover");
    }
    internal void RestartGame()
    {
        GetComponent<Animator>().Play("bloomidle");
    }
    internal void GoToTarget() {
        goToTarget = true;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (doingAQuake)
        {
            quakeTarget = Vector3.SmoothDamp(quakeTarget, new Vector3(Mathf.Sin(Time.time * 12f) * 2, Mathf.Sin(Time.time * 12f) * 2, 0), ref quakeVel, 0.4f);

        }
        if (goToTarget) {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, target.position + new Vector3(0, 0, -10) + quakeTarget, ref vel, 0.4f);

        }
        if (doingAQuake && Time.time > quakeTime)
        {
            doingAQuake = false;
            quakeTarget = Vector3.zero;

        }
        ///cam.orthographicSize = Mathf.Lerp(size1, size2, timer/10f);

        if (timer > 10f)
        {
            timer = 0f;
            goingUp = !goingUp;
            if (goingUp)
            {
                size1 = 10f;
                size2 = 11f;
            }
            else
            {
                size1 = 10f;
                size2 =11f;
            }
        }


    }
}
