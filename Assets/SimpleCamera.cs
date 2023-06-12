using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;

    private void Start()
    {
        playerCamera.cullingMask = 1 << LayerMask.NameToLayer("AsteroidLayer");
    }
}
