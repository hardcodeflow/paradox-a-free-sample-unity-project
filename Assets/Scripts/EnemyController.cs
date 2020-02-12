using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject particles;
    private Transform playerTR;
   private Transform stageTR;
  //  public Transform tr;
    private Rigidbody2D rigidbody2;
    public SpriteRenderer sr_s;

    public float speed = 0.1f;
    float maxSpeed;

    private Renderer particles_s;
    public ParticleSystem ps;
    public ParticleSystem ps_s;
     public float  seed;
    public float pushForce = 3;
    ParticleSystem.MainModule main;
    Vector2 direction;

    Vector2 externForce;
    Vector2 exterTargetForce;
    float forceDuration = 1.2f;
    float forceTime;
    bool applingForce;

    float idleTime;
    float idleDuration;
    bool idle;
    Vector2 velForce;
    public float scaleFactor; 
    void AddForce(Vector2 force)
    {
        exterTargetForce = force;

        applingForce = true;
        forceTime = Time.time + forceDuration;
    }
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        playerTR = GameObject.Find("player").transform;
        stageTR = GameObject.Find("Stage").transform;
        //ColorController.instance.AddSprite(sr_s);
        maxSpeed = Random.Range(1.5f, 2.75f);
        int difficulty = References.gameManager.score;
        if (difficulty >= 0 && difficulty <= 100) {
            scaleFactor = Random.Range(0.3f,0.5f);
            pushForce = 0.05f;
            forceDuration = 1.2f;
        }
        if (difficulty > 100 && difficulty <= 300)
        {
            pushForce = 0.08f;
            forceDuration = 1f;
            scaleFactor = Random.Range(0.5f, 0.8f);
        }
        if (difficulty > 300 && difficulty <= 600)
        {
            forceDuration = 0.8f;
            pushForce = 0.1f;
            scaleFactor = Random.Range(0.8f, 1f);
        }
        this.transform.localScale = new Vector2(1,1)*scaleFactor;
    }

    void Update()
    {
        direction = playerTR.position - transform.position;
        direction.Normalize();
        if(!applingForce)
        transform.up = direction;
    }

    void FixedUpdate()
    {
        /* rb.AddForce(tr.up * speed * Time.deltaTime);
         if (Vector2.Distance(stageTR.position, tr.position) < 4.5f || Vector2.Distance(stageTR.position, tr.position) > 6f)
         {
             rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
         }
         else
         {
             rb.velocity = Vector2.ClampMagnitude(rb.velocity, 0.5f);
         }*/

        if (References.gameManager.gameState == GameState.InGame)
        {
            rigidbody2.velocity = direction * speed + externForce;

            if (applingForce && forceTime < Time.time)
            {
                applingForce = false;
                exterTargetForce = Vector2.zero;
            }
            externForce = Vector2.SmoothDamp(externForce, exterTargetForce, ref velForce, 0.1f);
        }
        }
    void Die() {
        // particles.transform.position = transform.position;
        // particles.transform.parent = transform.parent;

        seed = Random.Range(0, 1000);
        ps.randomSeed = (uint)seed;
        ps_s.randomSeed = (uint)seed;
        main = ps_s.main;
        Color32 c = ColorController.instance.currentColor;
        c.a = 255;
        main.startColor = new ParticleSystem.MinMaxGradient(c, c);
        // particles.SetActive(true);
        GameObject prefab = Resources.Load("Fragment") as GameObject;
        for (int i = 0; i < 10; i++)
        {
            GameObject g = Instantiate(prefab, this.transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1) + 0.1f, Random.Range(-1, 1) + 0.1f) * Random.Range(8, 12);

        }
        //AudioController.instance.PlayEnemyHit();
        References.soundManager.PlaySound(SoundId.EnemyHitSound);

        //Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("AddForce",rigidbody2.velocity*pushForce);
        }
            if (collision.gameObject.CompareTag("Bullet")|| collision.gameObject.CompareTag("Circle")&&applingForce)
        {
            References.camera.DoAQuake();

            References.gameManager.GiveAPoint();

            Die();
        }
    }
}
