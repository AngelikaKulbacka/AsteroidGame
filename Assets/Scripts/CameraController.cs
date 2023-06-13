using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;

    private void Start()
    {
        MainCamera.cullingMask = 1 << LayerMask.NameToLayer("AsteroidLayer");
    }
}
