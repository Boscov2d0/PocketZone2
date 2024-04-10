using UnityEngine;

public class NPCMoveController : ObjectsDisposer
{
    private readonly Transform _transform;
    private readonly Transform _body;
    private readonly Transform _eye;
    private readonly Transform _target;
    private readonly SubscriptionProperty<NPCStates> _states;
    private readonly SubscriptionProperty<Vector2> _lookDirection;
    private readonly float _attackDistance;
    private readonly float _attackSpeed;

    private RaycastHit2D _hit;

    public NPCMoveController(Transform transform, Transform body, Transform eye, Transform target,
                             SubscriptionProperty<NPCStates> states, SubscriptionProperty<Vector2> lookDirection,
                             float attackDistance, float attackSpeed) 
    {
        _transform = transform;
        _body = body;
        _eye = eye;
        _target = target;
        _states = states;
        _lookDirection = lookDirection;
        _attackDistance = attackDistance;
        _attackSpeed = attackSpeed;
    }
    public void FixedExecute()
    {
        Vector3 directionToTarget = _target.position - _transform.position;
        if (directionToTarget.x > 0)
        {
            _body.rotation = new Quaternion(0, 0, 0, 1);
            _lookDirection.Value = Vector2.left;
        }
        else
        {
            _body.rotation = new Quaternion(0, 180, 0, 1);
            _lookDirection.Value = Vector2.right;
        }
        float test = _lookDirection.Value.x * 1.5f;
        _hit = Physics2D.Raycast(_eye.position, _lookDirection.Value, _attackDistance);
        if (_hit.collider != null)
        {
            if (_hit.collider.TryGetComponent(out PlayerController player))
            {
                _states.Value = NPCStates.Attack;
            }
        }

        _transform.position = Vector2.MoveTowards(_transform.position, new Vector3(_target.position.x + test, _target.position.y, 0), _attackSpeed);
    }
}