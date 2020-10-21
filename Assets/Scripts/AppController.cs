using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField]
	private GameObject selectionMenu;
    [SerializeField]
    private GameObject placeMenu;

    private ARMarker marker;

    void Start()
    {
        marker = FindObjectOfType<ARMarker>();
    }

    public void OpenPlaceMenu(GameObject selectedObject)
    {
        marker.CreatedObject = selectedObject;
        marker.EnableAllPlane();

        selectionMenu.SetActive(false);
        placeMenu.SetActive(true);
    }

    public void BackToSelection()
    {
        marker.DestroyPlacedObject();

        placeMenu.SetActive(false);
        selectionMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
