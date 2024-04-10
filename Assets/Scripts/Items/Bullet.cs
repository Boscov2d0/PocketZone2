using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    private Vector2 _direction;
    private Vector3 _rayPosition;
    private RaycastHit2D _hit;
    private float _damage;
    private float _timer;
    private bool _isActive = false;

    public void Initialize(Vector2 direction, float distance, float damage)
    {
        _direction = direction;
        _damage = damage;

        _timer = distance;
        _isActive = true;
        _rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        if (!_isActive)
            return;

        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
            Destroy();

        _rayPosition = new Vector3(transform.position.x + 0.1f * _direction.x, transform.position.y, 0);

        _hit = Physics2D.Raycast(_rayPosition, _direction, 0.1f);
        if (_hit.collider != null)
        {
            if (_hit.collider.TryGetComponent(out NPCController npc))
                npc.GetDamage(_damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacles" || collision.TryGetComponent(out NPCController npc))
            Destroy();
    }
    private void Destroy()
    {
        _isActive = false;
        Destroy(gameObject);
    }
}