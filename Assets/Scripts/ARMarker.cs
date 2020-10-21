using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARMarker : MonoBehaviour
{
    [SerializeField]
    private GameObject marker;
    [SerializeField]
    private TextMeshProUGUI objectName;

    private GameObject createdObject;
    public GameObject CreatedObject
    {
        set { createdObject = value; }
    }

    private ARRaycastManager _arRaycastManager;

    private ARPlaneManager _arPlaneManager;
    public ARPlaneManager PlaneManager
    {
        get { return PlaneManager; }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 cameraCentre;
    private bool isPlaced = false;

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Start()
    {
        cameraCentre = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        if (isPlaced)
            return;

        if (_arRaycastManager.Raycast(cameraCentre, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpos = hits[0].pose;
            marker.SetActive(true);
            marker.transform.position = hitpos.position;
            marker.transform.rotation = hitpos.rotation;
        }
        else
        {
            marker.SetActive(false);
        }
    }

    public void EnableAllPlane()
    {
        foreach (var trackable in _arPlaneManager.trackables)
        {
            trackable.gameObject.SetActive(true);
        }
    }

    public void DisableAllPlane()
    {
        foreach (var trackable in _arPlaneManager.trackables)
        {
            trackable.gameObject.SetActive(false);
        }
    }

    public void PlaceObject()
    {
        isPlaced = true;
        objectName.text = createdObject.name;
        createdObject = Instantiate(createdObject, marker.transform.position, marker.transform.rotation);
        marker.SetActive(false);
        DisableAllPlane();
    }

    public void DestroyPlacedObject()
    {
        if (isPlaced)
        {
            Destroy(createdObject);
        }

        objectName.text = "";
        isPlaced = false;
    }

}
