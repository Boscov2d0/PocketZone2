using UnityEngine;

public class NPCPatrolController : ObjectsDisposer
{
    private readonly Transform _transform;
    private readonly Transform _body;
    private readonly Transform _eye;
    private readonly Transform _player;
    private readonly SubscriptionProperty<NPCStates> _states;
    private readonly float _patrolDistance;
    private readonly float _patrolSpeed;
    private readonly float _rayDistance;
    private readonly int _id;

    private RaycastHit2D _hit;
    private Vector2 _targetPoint;
    private SubscriptionProperty<Vector2> _lookDirectionVector;
    private int _lookDirection;
    private float _distanceToPoint;
    private float _distanceToPlayer;

    public NPCPatrolController(Transform transform, Transform body, Transform eye, Transform player,
                               SubscriptionProperty<NPCStates> states, SubscriptionProperty<Vector2> lookDirectionVector,
                               float patrolDistance, float patrolSpeed, float rayDistance, int id)
    {
        _transform = transform;
        _body = body;
        _eye = eye;
        _player = player;
        _states = states;
        _lookDirectionVector = lookDirectionVector;
        _lookDirectionVector.Value = Vector2.right;
        _patrolDistance = patrolDistance;
        _patrolSpeed = patrolSpeed;
        _rayDistance = rayDistance;
        _id = id;
        _lookDirection = 1;
        ChangeTarget();

    }
    public void FixedExecute()
    {
        if (_states.Value != NPCStates.Patroling)
            return;

        _transform.position = Vector2.MoveTowards(_transform.position, _targetPoint, _patrolSpeed);
        OnTargetPointPosCheck();
        OnPlayerPosCheck();
    }
    private void OnTargetPointPosCheck()
    {
        _hit = Physics2D.Raycast(_eye.position, _lookDirectionVector.Value, _rayDistance);
        if (_hit.collider != null)
        {
            if (_hit.collider.tag == "Obstacles")
                ChangeTarget();
            if (_hit.collider.TryGetComponent(out NPCController npc))
                if(npc.ID != _id)
                ChangeTarget();
        }

        _distanceToPoint = Vector2.Distance(_transform.position, _targetPoint);
        if (_distanceToPoint <= 0)
            ChangeTarget();
    }
    private void OnPlayerPosCheck()
    {        
        _distanceToPlayer = Vector2.Distance(_transform.position, _player.position);
        if (_distanceToPlayer <= 4)
            _states.Value = NPCStates.Follow;
    }
    private void ChangeTarget()
    {
        _lookDirection *= -1;
        _targetPoint = new Vector2(_transform.position.x + _patrolDistance * _lookDirection, _transform.position.y);
        Rotate();

    }
    private void Rotate() 
    {
        if (_targetPoint.x > _transform.position.x)
        {
            _body.rotation = new Quaternion(0, 0, 0, 1);
            _lookDirectionVector.Value = Vector2.right;
        }
        else
        {
            _body.rotation = new Quaternion(0, 180, 0, 1);
            _lookDirectionVector.Value = Vector2.left;
        }
    }
}