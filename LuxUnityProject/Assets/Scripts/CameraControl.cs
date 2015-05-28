/// Camera Control: Follows a GameObject, stays between four boundary objects * thresholds
/// By Saeed Afshari (2015)

using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public GameObject FollowObject = null;
    public GameObject LeftBoundary = null;
    public GameObject RightBoundary = null;
    public GameObject TopBoundary = null;
    public GameObject BottomBoundary = null;
    public float HorizontalThreshold = 0.95f;
    public float VerticalThreshold = 0.95f;

    public GameObject DebugText = null;
    new Camera camera;
	// Use this for initialization
	void Start () {
        camera = transform.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (FollowObject == null) return;
        Vector2 cameraSize = new Vector2(camera.pixelWidth * camera.orthographicSize / camera.pixelHeight, camera.orthographicSize);
        Vector3 followPos = FollowObject.transform.position; // - transform.position;
        float cameraLeft = transform.position.x - cameraSize.x * HorizontalThreshold;
        float cameraRight = transform.position.x + cameraSize.x * HorizontalThreshold;
        float cameraUp = transform.position.y + cameraSize.y * VerticalThreshold;
        float cameraDown = transform.position.y - cameraSize.y * VerticalThreshold;
        
        Vector3 newPos = transform.position;
        if (followPos.x < cameraLeft) newPos.x = FollowObject.transform.position.x + cameraSize.x * HorizontalThreshold;
        else if (followPos.x > cameraRight) newPos.x = FollowObject.transform.position.x - cameraSize.x * HorizontalThreshold;
        
        if (followPos.y > cameraUp) newPos.y = FollowObject.transform.position.y - cameraSize.y * VerticalThreshold;
        else if (followPos.y < cameraDown) newPos.y = FollowObject.transform.position.y + cameraSize.y * VerticalThreshold;

        if (RightBoundary != null && newPos.x + cameraSize.x > RightBoundary.transform.position.x)
            newPos.x = RightBoundary.transform.position.x - cameraSize.x;
        else if (LeftBoundary != null && newPos.x - cameraSize.x < LeftBoundary.transform.position.x)
            newPos.x = LeftBoundary.transform.position.x + cameraSize.x;
        if (TopBoundary != null && newPos.y + cameraSize.y > TopBoundary.transform.position.y)
            newPos.y = TopBoundary.transform.position.y - cameraSize.y;
        else if (BottomBoundary != null && newPos.y - cameraSize.y < BottomBoundary.transform.position.y)
            newPos.y = BottomBoundary.transform.position.y + cameraSize.y;

        camera.transform.position = newPos;

        //RightBoundary.transform.position = new Vector3(cameraRight, transform.position.y, RightBoundary.transform.position.z);
        //LeftBoundary.transform.position = new Vector3(cameraLeft, transform.position.y, LeftBoundary.transform.position.z);
        //TopBoundary.transform.position = new Vector3(transform.position.x, cameraUp, TopBoundary.transform.position.z);
        //BottomBoundary.transform.position = new Vector3(transform.position.x, cameraDown, BottomBoundary.transform.position.z);
        if (DebugText)
        {
            DebugText.GetComponent<UnityEngine.UI.Text>().text =
                cameraSize.ToString();
        }
    }
}
