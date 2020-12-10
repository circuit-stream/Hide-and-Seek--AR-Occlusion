using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{    
    public GameObject placedPrefab;
    public Camera arCamera;

    private GameObject placedObject;
    private Vector2 touchPosition;
    private Pose hitPose;
    private ARRaycastManager arRaycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    PlacedObject placementObject = hitObject.transform.GetComponent<PlacedObject>();
                    if (placementObject != null)
                    {
                        placementObject.Selected = true;
                        OcclusionUIManager.Instance.UpdateObjectCountText($"placedObject selected: {placementObject.Selected}");
                        StartCoroutine(TimedPosition(hitPose, placementObject));
                    }
                    else
                    {
                        OcclusionUIManager.Instance.UpdateObjectCountText($"placedObject selected: false");
                    }
                }


                if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    hitPose = hits[0].pose;
                    if (placedObject == null)
                    {
                        placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    }
                }
            }
        }
    }

    private IEnumerator TimedPosition(Pose hit, PlacedObject placementObject)
    {
        placementObject.Selected = false;
        OcclusionUIManager.Instance.UpdateObjectCountText($"placedObject selected: {placementObject.Selected}");
        yield return new WaitForSeconds(0.5f);
        placedObject.transform.position = new Vector3(Random.Range(-1, 1), hit.position.y, Random.Range(-1, 1));        
    }
}
