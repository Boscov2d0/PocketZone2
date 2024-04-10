using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameMyManager _gameMyManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Transform _body;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer _helmetSprite;
    [SerializeField] private SpriteRenderer _jacketSprite;
    [SerializeField] private SpriteRenderer _leftPantsSprite;
    [SerializeField] private SpriteRenderer _rigthPantsSprite;
    [SerializeField] private Transform _pistolPosition;
    [SerializeField] private Transform _automatPosition;

    private PlayerMoveController _playerMoveController;
    private PlayerDeathController _playerDeathController;

    private IWeapon _weapon = null;
    private GameObject _createdWeapon;

    private void Start()
    {
        _playerManager.HP.Value = _playerManager.HealthPoints;

        _playerMoveController = new PlayerMoveController(_playerManager, _body, _rigidbody);
        _playerDeathController = new PlayerDeathController(_gameMyManager, _playerManager);

        _playerManager.States.SubscribeOnChange(OnStateChange);
        _playerManager.PutOnItem += PutClothesOn;
        _playerManager.PutOnItem += PutWeaponsOn;

        PutClothesOn();
        PutWeaponsOn();
    }
    private void OnDestroy()
    {
        _playerMoveController?.Dispose();
        _playerDeathController?.Dispose();

        _playerManager.States.UnSubscribeOnChange(OnStateChange);
        _playerManager.PutOnItem -= PutClothesOn;
        _playerManager.PutOnItem -= PutWeaponsOn;

        _playerManager.CurrentWeapon = null;
        _playerManager.CurrentHelmet = null;
        _playerManager.CurrentJacket = null;
        _playerManager.CurrentPants = null;
        _playerManager.Items.Clear();
    }
    private void OnStateChange()
    {
        switch (_playerManager.States.Value)
        {
            case PlayerStates.Attack:
                _weapon?.Fire();
                break;
        }
    }
    public void PutClothesOn()
    {
        if (_playerManager.CurrentHelmet)
            _helmetSprite.sprite = _playerManager.CurrentHelmet.IconSprite;
        else
        {
            _helmetSprite.sprite = null;
            _helmetSprite.enabled = false;
        }
        if (_playerManager.CurrentJacket)
            _jacketSprite.sprite = _playerManager.CurrentJacket.IconSprite;
        else
        {
            _helmetSprite.sprite = null;
            _helmetSprite.enabled = false;
        }
        if (_playerManager.CurrentPants)
            _leftPantsSprite.sprite = _playerManager.CurrentPants.IconSprite;
        else
        {
            _helmetSprite.sprite = null;
            _helmetSprite.enabled = false;
        }
        if (_playerManager.CurrentPants)
            _rigthPantsSprite.sprite = _playerManager.CurrentPants.IconSprite;
        else
        {
            _helmetSprite.sprite = null;
            _helmetSprite.enabled = false;
        }
    }
    public void PutWeaponsOn()
    {
        if (_playerManager.CurrentWeapon)
        {
            if (_createdWeapon)
                Destroy(_createdWeapon);

            if (_playerManager.CurrentWeapon.Class == ItemClasses.Pistol)
            {
                _createdWeapon = Instantiate(_playerManager.CurrentWeapon.Prefab, _pistolPosition.position, Quaternion.identity);
                _createdWeapon.transform.SetParent(_pistolPosition.transform);
                _weapon = _createdWeapon.GetComponent<Pistol>();
            }
            else if (_playerManager.CurrentWeapon.Class == ItemClasses.Automat)
            {
                _createdWeapon = Instantiate(_playerManager.CurrentWeapon.Prefab, _automatPosition.position, Quaternion.identity);
                _createdWeapon.transform.SetParent(_automatPosition.transform);
                _weapon = _createdWeapon.GetComponent<Automat>();
            }

            _weapon.Initialize(_playerManager);
        }
        else
        {
            Destroy(_createdWeapon);
        }
    }
    public void GetDamage(float value) =>
        _playerDeathController?.GetDamage(value);

    public bool AddItem(ItemManager item)
    {
        if (_playerManager.Items.Count >= _playerManager.BagSize)
            return false;

        if (_playerManager.Items.Count == 0)
        {
            _playerManager.Items.Add(item);
            return true;
        }

        for (int i = 0; i < _playerManager.Items.Count; i++)
        {
            if (_playerManager.Items[i].Name == item.Name)
            {
                int count = _playerManager.Items[i].Count + item.Count;
                if (count > _playerManager.Items[i].MaxCount)
                {
                    _playerManager.Items[i].Count = _playerManager.Items[i].MaxCount;
                    item.Count = count - _playerManager.Items[i].MaxCount;
                    _playerManager.Items.Add(item);
                }
                else
                    _playerManager.Items[i].Count += item.Count;

                return true;
            }
        }

        _playerManager.Items.Add(item);

        return true;
    }
}