using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDesactivate : MonoBehaviour
{
    public float timeOut;
    float timer;
    void Start()
    {
        timer = Time.time + timeOut;
    }
    private void OnEnable()
    {
        timer = Time.time + timeOut;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer) {
            this.gameObject.SetActive(false);
        }
    }
}
