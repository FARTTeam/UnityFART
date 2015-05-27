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

    new Camera camera;
	// Use this for initialization
	void Start () {
        camera = transform.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (FollowObject == null) return;
        Vector2 cameraSize = new Vector2(camera.pixelWidth * camera.orthographicSize / camera.pixelHeight, camera.orthographicSize);
        Vector3 followPos = FollowObject.transform.position - transform.position;
        float cameraLeft = transform.position.x - cameraSize.x * HorizontalThreshold;
        float cameraRight = transform.position.x + cameraSize.x * HorizontalThreshold;
        float cameraUp = transform.position.y + cameraSize.y * VerticalThreshold;
        float cameraDown = transform.position.y - cameraSize.y * VerticalThreshold;
    }
}
