using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    //internal SkinPlayerController skinPlayer;
    public bool godMode;
    // public GameObject impact;
    // public GameObject smoke;
    // public GameObject smokeEnd;
    public GameObject sprite;


    public GameObject circle;
    public float pushForce =1;
    public float circleRadio;
    public float scaleDelta = 0.001f;
    //inside class
    public Vector2 firstPressPos;
    public Vector2 secondPressPos;
    public Vector2 currentSwipe;
    Vector2 swipeTarget;
    public float distance;
    float radioDistance;
    public float maxRadioDistance = 10;
    public float maxScale = 10;
    Vector2 scale;
    public float force = 10;
    Vector3 lastGroundedPosition;
    float smokeTime;
    bool smoking;
    public bool reloading;
    public float reloadTime;
    public float reloadDuration = 1;
    public int ammo = 5;
    public int ammoAvailable = 5;
    public float distanceBetweenCircleAndSkull;
    Vector2 vel;
    Vector2 firstTouch;
    Rigidbody2D rigidbody2;
    Vector2 externForce;
    float forceDuration = 0.5f;
    float forceTime;
    bool applingForce;
    public bool enemyPushed;
    //public GameObject dieEffect;
    bool returnCircle;
    void AddForce(Vector2 force) {
        if (enemyPushed) {
            enemyPushed = false;
            externForce = force * 0.5f;
            applingForce = true;
            forceTime = Time.time + forceDuration * 0.5f;
            return;
        }
        externForce = force;
        applingForce = true;
        forceTime = Time.time + forceDuration;
    }
    public void SetAmmoAvailable(int x)
    {
        ammoAvailable = x;
        ammo = x;

    }
    public void StartGame() {

    }
    void Die() {

        GameObject prefab = Resources.Load("PlayerFragment") as GameObject;
        for (int i = 0; i < 10; i++)
        {
            GameObject g = Instantiate(prefab, this.transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1) + 0.1f, Random.Range(-1, 1) + 0.1f) * Random.Range(8, 12);

        }
        References.camera.DoAQuake();

        sprite.SetActive(false);
        rigidbody2.velocity = Vector2.zero;
        References.gameManager.GameOver();
        rigidbody2.velocity = Vector2.zero;
        Destroy(rigidbody2);
        this.transform.position = Vector2.zero;
        externForce = Vector2.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyPushed = true;
            collision.gameObject.SendMessage("AddForce", rigidbody2.velocity * pushForce);
        }

        if (collision.gameObject.CompareTag("Circle")&&!godMode)
        {
            Die();
        }

    }

    internal void RestartGame() {
        this.gameObject.AddComponent<Rigidbody2D>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.gravityScale = 0;

      
        sprite.SetActive(true);
      
        this.transform.position = Vector2.zero;
        rigidbody2.velocity = Vector2.zero;
        externForce = Vector2.zero;
        this.transform.rotation = Quaternion.identity;
        firstPressPos = Vector2.zero;
        secondPressPos = Vector2.zero;
        swipeTarget = Vector2.zero;
        returnCircle = false;
        reloading = true;
        reloadTime = Time.time + reloadDuration;
    }
    void Shoot() {
        if (!reloading)
        {
            float angle = Mathf.Atan2(currentSwipe.normalized.y, currentSwipe.normalized.x) * Mathf.Rad2Deg;

            this.transform.rotation = Quaternion.Euler(0,0,angle);
             rigidbody2.AddForce(-currentSwipe.normalized * distanceBetweenCircleAndSkull * force, ForceMode2D.Impulse);
            //this.GetComponent<Rigidbody2D>().AddForce(currentSwipe.normalized * distance * force, ForceMode2D.Impulse);
            if (this.GetComponent<Rigidbody2D>().velocity.y < 0)
               rigidbody2.velocity += -currentSwipe.normalized * distanceBetweenCircleAndSkull * force;
            else {
               rigidbody2.velocity = -currentSwipe.normalized * distanceBetweenCircleAndSkull * force;

            }

           // impact.transform.position = circle.transform.position;
            //impact.SetActive(true);
            //impact.transform.parent = null;
            circle.transform.parent = null;
            smoking = true;
            smokeTime = Time.time + 0.4f;
           // smoke.SetActive(true);
            ammo--;
            if (ammo <= 0)
            {
                ammo = ammoAvailable;
            }
            /*
            reloadTime = Time.time + reloadDuration;
            reloading = true;*/

           // References.gameManager.ReloadAmmoImages(ammo);
        }
    }
    public void Swipe()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
           
            firstPressPos =Camera.main.ScreenToWorldPoint( Input.mousePosition);
            firstPressPos = References.player.transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            circle.transform.position = (Vector2)References.player.transform.position + currentSwipe.normalized * radioDistance;
            distanceBetweenCircleAndSkull = Vector2.Distance(References.player.transform.position, circle.transform.position);

            //save ended touch 2d point
            secondPressPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            //secondPressPos =new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //create vector from the two points
            swipeTarget = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            // currentSwipe.Normalize();
        }
        else {
          
        }
        if (Input.GetMouseButtonUp(0))
        {
            returnCircle = true;
        }
        currentSwipe = Vector2.SmoothDamp(currentSwipe, swipeTarget, ref vel, 0.3f);

    }
  
    void Start()
    {
       // skinPlayer = GetComponent<SkinPlayerController>();
  
        circle.transform.parent = null;
        circle.transform.rotation = Quaternion.identity;
        rigidbody2 = GetComponent<Rigidbody2D>();
    }
    Vector2 velCircle;
    // Update is called once per frame
    void Update()
    {

        if (References.gameManager.gameState == GameState.InGame)
        {
            if (this.transform.position.magnitude > 9) {
                Die();
            }
            if (returnCircle) {
                circle.transform.position = Vector2.SmoothDamp(circle.transform.position, References.player.transform.position, ref velCircle, 0.04f);
                
                if (Vector2.Distance(circle.transform.position, this.transform.position) < 0.22f) {

                    Shoot();
                    //save began touch 2d point
                    firstPressPos = Vector2.zero;
                    secondPressPos = Vector2.zero;
                    swipeTarget = Vector2.zero;
                    returnCircle = false;
                }

            }
            rigidbody2.velocity = rigidbody2.velocity + externForce;
            if (applingForce && forceTime <Time.time) {
                applingForce = false;
                externForce = Vector2.zero;
            }
            if (smoking && Time.time > smokeTime)
            {
                // smoke.SetActive(false);
                smoking = false;
                // smokeEnd.SetActive(true);

            }


            Swipe();

            distance = Vector2.Distance(firstPressPos, secondPressPos);
            radioDistance = distance * circleRadio;
            if (radioDistance > maxRadioDistance)
            {
                radioDistance = maxRadioDistance;
            }
            /*scale.x = scale.y = distance * scaleDelta;
            if (scale.x > maxScale)
            {
                scale.x = scale.y = maxScale;
            }
            circle.transform.localScale = new Vector3(scale.x, scale.y, 0);
            */

            if (reloading && Time.time > reloadTime)
            {
                reloading = false;

            }
        }
    }
}
