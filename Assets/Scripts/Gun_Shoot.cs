using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject cavityEffectPrefab;  
    public float bulletSpeed = 6f;
    public AudioClip shotSound;

    public Transform bulletSpawnPoint;  
    public Button shootButton;

    private AudioSource audioSource;

    void Start()
    {
        if (shootButton != null && bulletSpawnPoint != null)
        {
            shootButton.onClick.AddListener(ShootBullet);

            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Debug.LogError("Button component or BulletSpawnPoint not found on the GameObject.");
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);

                if (audioSource != null && shotSound != null)
                {
                    audioSource.PlayOneShot(shotSound);
                }

                Debug.Log("Bullet Spawn Direction: " + bulletSpawnPoint.forward);
                Debug.Log("Bullet Force Applied: " + bulletSpawnPoint.forward * bulletSpeed);
            }
            else
            {
                Debug.LogError("Bullet prefab is missing Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or bullet spawn point not assigned.");
        }
    }

    // Called when a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected!");
        // Check if the collided object has a cavity effect prefab
        if (cavityEffectPrefab != null)
        {
            // Instantiate the cavity effect at the collision point
            GameObject cavityEffect = Instantiate(cavityEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(cavityEffect, 3f);  // Destroy the cavity effect after 3 seconds
        }
    }
}
