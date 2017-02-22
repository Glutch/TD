using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool cameraControlsActive = true;

    public float panSpeed = 30f;
    public float panBorderThickness = 20f;
    public float scrollSpeed = 2f;
    public float minY = 10f;
    public float maxY = 50f;
    public float minZ = -90f;
    public float maxZ = -4f;
    public float minX = 0f;
    public float maxX = 82f;

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("x")) {
            cameraControlsActive = !cameraControlsActive;
        }

        if (!cameraControlsActive)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("mouse 2")){
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;

    }
}
