using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Transform _bulletSpawnPoint;
    protected PlayerManager _playerManager;
    protected WeaponManager _weaponManager;
    protected ItemManager _patrons;
    protected RaycastHit2D _hit;
    protected float _delayTimer;
    protected float _reloadTimer;
    protected bool _isDelay;
    protected bool _isReload;

    public void Initialize(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        _weaponManager = playerManager.CurrentWeapon;
    }
    public void Fire()
    {
        if (_isDelay)
            return;

        if (!_patrons && _weaponManager.CountPatronsInMagazine <= 0)
        {
            GetPatrons();
            return;
        }

        _weaponManager.CountPatronsInMagazine = _weaponManager.CountPatronsInMagazine - 1;
        GameObject bullet = GameObject.Instantiate(_weaponManager.BulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(_playerManager.LookDirection, _weaponManager.Distance, _weaponManager.Damage);
        _isDelay = true;
    }
    private void GetPatrons()
    {
        for (int i = 0; i < _playerManager.Items.Count; i++)
        {
            if (_playerManager.Items[i].Class == ItemClasses.Patrons)
                _patrons = _playerManager.Items[i];
        }

        if (_patrons)
            _isReload = true;
    }
    private void FixedUpdate()
    {
        if (_isDelay)
            Delay();

        if (_isReload)
            Reload();
    }
    private void Delay()
    {
        _delayTimer -= Time.fixedDeltaTime;
        if (_delayTimer <= 0)
        {
            _delayTimer = _weaponManager.DelayTime;
            _isDelay = false;
        }
    }
    private void Reload()
    {
        _reloadTimer -= Time.fixedDeltaTime;
        if (_reloadTimer <= 0)
        {
            int count = _patrons.Count - _weaponManager.MagazineSize;
            if (count > 0)
            {
                _weaponManager.CountPatronsInMagazine = _weaponManager.MagazineSize;
                _patrons.Count -= _weaponManager.MagazineSize;
            }
            else
            {
                _weaponManager.CountPatronsInMagazine = _patrons.Count;
                _playerManager.Items.Remove(_patrons);
                _patrons = null;
            }
            _reloadTimer = _weaponManager.ReloadTime;
            _isReload = false;
        }
    }
}