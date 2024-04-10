using UnityEngine;

public class PlayerMoveController : ObjectsDisposer
{
    private readonly PlayerManager _manager;
    private readonly Transform _body;
    private readonly Rigidbody2D _rigidbody;

    public PlayerMoveController(PlayerManager manager, Transform body, Rigidbody2D rigidbody) 
    {
        _manager = manager;
        _body = body;
        _rigidbody = rigidbody;

        _manager.Horizontal.SubscribeOnChange(Move);
        _manager.Vertical.SubscribeOnChange(Move);
    }
    protected override void OnDispose()
    {
        _manager.Horizontal.UnSubscribeOnChange(Move);
        _manager.Vertical.UnSubscribeOnChange(Move);

        base.OnDispose();
    }
    private void Move()
    {
        _rigidbody.velocity = new Vector2(_manager.Horizontal.Value * _manager.MoveSpeed, _manager.Vertical.Value * _manager.MoveSpeed);
        if (_manager.Horizontal.Value > 0)
        {
            _manager.LookDirection = Vector2.right;
            _body.rotation = new Quaternion(0, 0, 0, 1);
        }
        else if(_manager.Horizontal.Value < 0)
        {
            _manager.LookDirection = Vector2.left;
            _body.rotation = new Quaternion(0, 180, 0, 1);
        }
    }
}