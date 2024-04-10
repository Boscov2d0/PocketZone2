using UnityEngine;

public class NPCAttackController : ObjectsDisposer
{
    private readonly Transform _eye;
    private readonly SubscriptionProperty<Vector2> _lookDirection;
    private readonly float _reloadTime;
    private readonly float _attackDistance;
    private readonly float _damage;

    private RaycastHit2D _hit;
    private float _timer;
    private bool _isReloaded;

    public NPCAttackController(Transform eye, SubscriptionProperty<Vector2> lookDirection,
                               float reloadTime, float attackDistance, float damage)
    {
        _eye = eye;
        _reloadTime = reloadTime;
        _lookDirection = lookDirection;
        _attackDistance = attackDistance;
        _damage = damage;
        _timer = _reloadTime;
    }
    public void FixedExecute()
    {
        if (!_isReloaded)
            Reload();

        _hit = Physics2D.Raycast(_eye.position, _lookDirection.Value * -1, _attackDistance);
        if (_hit.collider != null)
        {
            if (_hit.collider.TryGetComponent(out PlayerController player))
            {
                if (_isReloaded)
                    Attack(player);
            }
        }
    }
    private void Attack(PlayerController player)
    {
        player.GetDamage(_damage);
        _timer = _reloadTime;
        _isReloaded = false;
    }
    private void Reload()
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
            _isReloaded = true;
    }
}