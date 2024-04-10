using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;
    [SerializeField] private float _yMin;
    [SerializeField] private float _yMax;

    private Transform _player;

    [Inject]
    public void Construct(Transform player)
        =>_player = player;

    private Vector3 _offset;
    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;

    void FixedUpdate()
    {
        _desiredPosition = _player.position + _offset;
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);

        float clampedX = Mathf.Clamp(_smoothedPosition.x, _xMin, _xMax);
        float clampedY = Mathf.Clamp(_smoothedPosition.y, _yMin, _yMax);

        transform.position = new Vector3(clampedX, clampedY, -10);
    }
}