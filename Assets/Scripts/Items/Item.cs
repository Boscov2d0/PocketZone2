using UnityEngine;
using Zenject;

public class Item : MonoBehaviour
{
    private PlayerController _player;
    [Inject]
    public void Construct(PlayerController player)
        => _player = player;
    private PlayerManager _playerManager;
    [Inject]
    public void Construct(PlayerManager playerManager)
        => _playerManager = playerManager;

    [SerializeField] private ItemManager _manager;
    [SerializeField] private TextMesh _countText;
    [SerializeField] private int _count;
    [SerializeField] private float _pickDistance;
    [SerializeField] private float _pickSpeed;
    private float _distanceToPlayer;

    public string Name { get { return _manager.Name; } private set { } }
    public ItemClasses Class { get { return _manager.Class; } private set { } }

    private void Start()
    {
        Name = _manager.Name;
        if (Class == ItemClasses.Patrons)
        {
            _count = Random.Range(0, _manager.MaxCount);
            _manager.Count = _count;
        }

        if (_count > 1)
            _countText.text = _count.ToString();
    }

    private void FixedUpdate()
    {
        _distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if (_distanceToPlayer <= _pickDistance)
        {
            if (_playerManager.Items.Count <= _playerManager.BagSize)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _pickSpeed);
                if (_distanceToPlayer <= 0.1f)
                {
                    _player.AddItem(_manager);
                    Destroy(gameObject);
                }
            }
        }
    }
}