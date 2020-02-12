using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
    Vector3 vel;
    bool active;
    float activeTime;
    float activeDelay = 0.8f;
    void Start()
    {
        active = false;
        activeTime = Time.time + activeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active && Time.time > activeTime) {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.transform.position = Vector3.SmoothDamp(this.transform.position,Vector2.zero,ref vel,0.4f);
            if (Vector3.Distance(this.transform.position, Vector2.zero) < 0.2f) {
                Destroy(this.gameObject);
            }
        }
    }
}
