using UnityEngine;
using UnityEngine.UI;

public class ShootingButton : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public AudioClip shotSound;  // Add this variable for the gunshot sound

    private Button shootButton;
    private AudioSource audioSource;  // Add this variable for audio playback

    void Start()
    {
        // Ensure there's a Button component on the GameObject
        shootButton = GetComponent<Button>();

        // Check if the Button component is not null before trying to use it
        if (shootButton != null)
        {
            // Attach the ShootBullet method to the button's click event
            shootButton.onClick.AddListener(ShootBullet);

            // Get or add AudioSource component for audio playback
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }
    }

    void ShootBullet()
    {
        // Check if the bulletPrefab and bulletSpawnPoint are not null before shooting
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiate a new bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Get the Rigidbody component of the bullet
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Check if the Rigidbody component exists
            if (bulletRb != null)
            {
                // Apply force to the bullet in the forward direction
                bulletRb.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);

                // Play gunshot sound
                if (audioSource != null && shotSound != null)
                {
                    audioSource.PlayOneShot(shotSound);
                }

                // Debug log for direction and force (for troubleshooting)
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
}
