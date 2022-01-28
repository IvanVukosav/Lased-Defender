using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding=0.5f;
    [SerializeField] int health = 1300;
    [SerializeField] AudioClip DeathClip;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed=20f;
    [SerializeField] float ProjectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip clip;
    [SerializeField] [Range(0, 1)] float shotSoundVolume = 0.25f;
   // [SerializeField] AudioSource source;
    Coroutine FiringCorouting;
    float xMin,xMax,yMin,yMax;


    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        
        SetUpMoveBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(DeathClip, Camera.main.transform.position, deathSoundVolume);
    }

    private void Fire()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
           FiringCorouting= StartCoroutine(FireContinuosly());
           
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(FiringCorouting);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, shotSoundVolume);
            GameObject laser = Instantiate(
                  laserPrefab,
                  transform.position,
                  Quaternion.identity) as GameObject;//Quaternion.identity upotrijebi rotaciju koju vec imas
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;// ili getkeydown.. ali ovo je jednostavnije, ovo mi daje promjenu, trenutne- promjene 
        var deltaY = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        return health;
    }
    public int Health
    {
        get { return health; }
        set { health=value; }
    }

}
