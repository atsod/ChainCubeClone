using UnityEngine;

public class CubeBounds : MonoBehaviour
{
    [SerializeField] private MeshRenderer _boundsRenderer;

    private Vector3 _boundsSize;
    private Bounds _bounds;

    private Transform _transform;

    private void Awake()
    {
        _boundsSize = _boundsRenderer.bounds.size;
        _bounds = new Bounds(_boundsRenderer.bounds.center, _boundsSize);

        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        CheckBounds();
    }

    private void CheckBounds()
    {
        Vector3 cubePosition = _transform.position;

        cubePosition.x = Mathf.Clamp(cubePosition.x, _bounds.min.x, _bounds.max.x);
        cubePosition.y = Mathf.Clamp(cubePosition.y, _bounds.min.y, _bounds.max.y);
        cubePosition.z = Mathf.Clamp(cubePosition.z, _bounds.min.z, _bounds.max.z);
        
        _transform.position = cubePosition;
    }
}
