using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;
    public GameObject gunPrefab;  // Reference to your gun prefab
    public Transform cameraTransform;  // Reference to your camera's transform

    private GameObject gunInstance;

    void Start()
    {
        // Assuming all buttons are initially active
        firstButton.gameObject.SetActive(true);
        secondButton.gameObject.SetActive(false);
        thirdButton.gameObject.SetActive(false);

        // Hide the gun prefab initially
        gunPrefab.SetActive(false);

        // Add onClick listeners to buttons
        firstButton.onClick.AddListener(OnFirstButtonClick);
        secondButton.onClick.AddListener(OnSecondButtonClick);
        thirdButton.onClick.AddListener(OnThirdButtonClick);
    }

    void OnFirstButtonClick()
    {
        firstButton.gameObject.SetActive(false);
        // Hide the first button
        thirdButton.gameObject.SetActive(true);
        // Activate the gun prefab
        

        // Do any other logic you need for the first button click
    }

    void OnSecondButtonClick()
    {
        // Hide the second button
        secondButton.gameObject.SetActive(true);

        // Do any logic you need for the second button click
    }

    void OnThirdButtonClick()
    {
        // Hide the third button
        thirdButton.gameObject.SetActive(false);
        

        // Show the second button
        secondButton.gameObject.SetActive(true);
        gunInstance = Instantiate(gunPrefab, cameraTransform);
        gunInstance.transform.localPosition = Vector3.zero;
        gunPrefab.SetActive(true);
        // Do any logic you need for the third button click
    }
}
