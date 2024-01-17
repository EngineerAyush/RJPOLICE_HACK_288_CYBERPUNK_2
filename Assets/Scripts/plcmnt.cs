using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARCore;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class FurniturePlacementManager : MonoBehaviour
{
   public GameObject SpawanableFurniture;
   public  XROrigin xROrigin;
    public ARRaycastManager arRaycastManager;
    public ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); 
    private void Update() {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision=arRaycastManager.Raycast(Input.GetTouch(0).position,hits,TrackableType.PlaneWithinPolygon);
                if(collision && IsButtonPressed()==false)
                {
                    GameObject _object= Instantiate(SpawanableFurniture);
                    _object.transform.position = hits[0].pose.position;
                    _object.transform.rotation = hits[0].pose.rotation;
                }
                foreach (var plane in arPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                arPlaneManager.enabled = false;
            }
        }
    }
    public bool IsButtonPressed(){
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else{
            return true;
        }
    }
    public void SwitchFurniture(GameObject furniture)
    {
        SpawanableFurniture = furniture;
    }
}
