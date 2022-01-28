using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy health")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 137;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots=0.2f;
    [SerializeField] float maxTimeBetweenShots=3f;
    [SerializeField] float projectileSpeed = 8f;

    [Header("Enemy sound and animation")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] float durationOfExplosion = 0.5f;
    float durationOfExplosion1 = 0.06f;
    [SerializeField] AudioClip DeathClip;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField]  float randomHitPosition = 0.65f;
    [SerializeField] AudioClip clip;
    [SerializeField] [Range(0, 1)] float shotSoundVolume = 0.65f;//ili shoot
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);   
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoots();
    }

    private void CountDownAndShoots()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, shotSoundVolume);

        GameObject laser = Instantiate(
                  projectile,
                  transform.position,
                  Quaternion.identity) as GameObject;//Quaternion.identity upotrijebi rotaciju koju vec imas
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        //yield return new WaitForSeconds(shotCounter);
    }

    private void OnTriggerEnter2D(Collider2D other)//kao drugi gameObjekt koji me udari
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        GameObject explosion1 = Instantiate(hitVFX,new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - randomHitPosition), transform.rotation);
        Destroy(explosion1, durationOfExplosion1);
        damageDealer.Hit();
        if (health <= 0)
        {
            Die(); 
        }
    }

    private void Die()
    {
       FindObjectOfType<GameSession>().AddToScore(scoreValue); ;
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(DeathClip, Camera.main.transform.position, deathSoundVolume);
    }
}
