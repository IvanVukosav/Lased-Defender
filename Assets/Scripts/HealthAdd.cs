using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAdd : MonoBehaviour
{
    [Header("Sound and animation")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] float durationOfExplosion = 0.5f;
    float durationOfExplosion1 = 0.06f;
    [SerializeField] AudioClip DeathClip;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    [SerializeField] float health=200;
    [SerializeField] float randomHitPosition = 0.65f;
    Player player;
    // Update is called once per frame
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
      
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

        GameObject explosion1 = Instantiate(hitVFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - randomHitPosition), transform.rotation);
        Destroy(explosion1, durationOfExplosion1);
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(DeathClip, Camera.main.transform.position, deathSoundVolume);
        player.Health += 200;
    }
}
