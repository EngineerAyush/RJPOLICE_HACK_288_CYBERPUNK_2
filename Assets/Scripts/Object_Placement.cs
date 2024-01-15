using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARObjectPlacement : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject targetPrefab;
    public GameObject gunPrefab;

    private bool objectsPlaced = false;

    void Update()
    {
        if (!objectsPlaced)
        {
            // Check for a touch on the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Raycast to detect planes
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Place the target objects randomly
                    int numberOfTargets = 3; // You can ask the user for this value
                    StartCoroutine(PlaceObjects(hits[0].pose.position, numberOfTargets));

                    // Display the gun after placing objects
                    StartCoroutine(DisplayGun());
                }
            }
        }
    }

    IEnumerator PlaceObjects(Vector3 center, int numberOfTargets)
    {
        objectsPlaced = true;

        for (int i = 0; i < numberOfTargets; i++)
        {
            // Randomly place targets around the center
            Vector3 randomPosition = center + Random.onUnitSphere * 0.5f;
            Instantiate(targetPrefab, randomPosition, Quaternion.identity);
            yield return null; // Wait for one frame before placing the next object
        }
    }

    IEnumerator DisplayGun()
    {
        yield return new WaitForSeconds(1f);
         GameObject gun = Instantiate(gunPrefab, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, Camera.main.transform.rotation);

    // Make the gun a child of the camera
    gun.transform.parent = Camera.main.transform; 
    }
}
