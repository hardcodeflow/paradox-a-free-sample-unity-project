using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestroyer : MonoBehaviour
{
    float timer;
    public bool onlyDesactivate;
    void Update()
    {
        timer += Time.deltaTime;

        if(!onlyDesactivate&&timer >= 1)
        {
            Destroy(gameObject);
        }
    }
}
