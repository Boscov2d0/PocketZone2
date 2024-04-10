using UnityEngine;
using Zenject;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameMyManager _gameMyManager;
    [SerializeField] private Transform _eye;
    [SerializeField] private Transform _body;
    [SerializeField] private int _hp;
    [SerializeField] private float _moveSpeed;    
    [SerializeField] private float _patrolDistance;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _rayDistance;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _damage;
    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _hpBar;

    private SubscriptionProperty<NPCStates> _states = new SubscriptionProperty<NPCStates>();
    private SubscriptionProperty<Vector2> _lookDirection = new SubscriptionProperty<Vector2>();

    private Transform _player; 
    [Inject]
    public void Construct(Transform player)
        => _player = player;
    private DiContainer _container;

    [Inject]
    public DiContainer Container
    {
        get => _container;
        set => _container = value;
    }

    private NPCPatrolController _patrolController;
    private NPCMoveController _moveController;
    private NPCAttackController _attackController;
    private NPCDeathController _deathController;

    public int ID { get; private set; }

    private void Start()
    {
        ID = Random.Range(-1000,1000);
        _states.SubscribeOnChange(OnStateChange);
        _states.Value = NPCStates.Patroling;

        _attackController = new NPCAttackController(_eye, _lookDirection,
                                            _reloadTime, _attackDistance, _damage);
        _deathController = new NPCDeathController(_container, _gameMyManager, gameObject, _hp, _hpBar);
    }
    private void OnDestroy()
    {
        _states.UnSubscribeOnChange(OnStateChange);
        ControllersDisposer();
        _attackController?.Dispose();
        _deathController?.Dispose();
    }
    private void OnStateChange() 
    {
        ControllersDisposer();

        switch (_states.Value) 
        {
            case NPCStates.Patroling:
                _patrolController = new NPCPatrolController(transform, _body, _eye, _player,
                                                            _states, _lookDirection, 
                                                            _patrolDistance, _patrolSpeed, _rayDistance, ID);
                break;
            case NPCStates.Follow:
                _moveController = new NPCMoveController(transform, _body, _eye, _player,
                                                        _states, _lookDirection, _attackDistance, _patrolSpeed);
                break;
        }
    }
    private void ControllersDisposer() 
    {
        _patrolController?.Dispose();
        _moveController?.Dispose();
    }
    private void FixedUpdate()
    {
        _patrolController?.FixedExecute();
        _moveController?.FixedExecute();
        _attackController?.FixedExecute();
    }
    public void GetDamage(float value) => _deathController.GetDamage(value);
}