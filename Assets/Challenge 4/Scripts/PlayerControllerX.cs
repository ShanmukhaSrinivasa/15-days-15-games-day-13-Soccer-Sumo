using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    [Header("Boost Elements")]
    private bool canBoost = true;
    private float boostStrength = 20.0f;
    private float boostCoolDown = 2.0f;
    [SerializeField] private ParticleSystem boostParticle;

    [Header("SFX")]
    [SerializeField] private AudioClip ballHitFX;
    [SerializeField] private AudioClip powerUpFX;

    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        if (GameManager.instance.isGameActive == true)
        {
            // Add force to player in direction of the focal point (and camera)
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && canBoost)
            {
                canBoost = false;

                boostParticle.Play();

                playerRb.AddForce(focalPoint.transform.forward * boostStrength, ForceMode.Impulse);

                StartCoroutine(BoostCoolDown());
            }

            // Set powerup indicator position to beneath player
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

            if (transform.position.y < -10)
            {
                Destroy(gameObject);
                GameManager.instance.GameOver();
            }
        }

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCooldown());
            powerupIndicator.SetActive(true);
            playerAudio.PlayOneShot(powerUpFX);
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    IEnumerator BoostCoolDown()
    {
        yield return new WaitForSeconds(boostCoolDown);
        canBoost = true;
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
            playerAudio.PlayOneShot(ballHitFX);

        }
    }



}
