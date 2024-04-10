using UnityEngine;
using Zenject;

public class NPCDeathController : ObjectsDisposer
{
    private readonly DiContainer _container;
    private readonly GameMyManager _gameMyManager;
    private GameObject _gameObject;
    private Transform _hpBar;
    private float _hp;
    private float _maxHP;

    public NPCDeathController(DiContainer container, GameMyManager gameMyManager, GameObject gameObject, float hp, Transform hpBar)
    {
        _container= container;
        _gameMyManager = gameMyManager;
        _gameObject = gameObject;
        _maxHP = _hp = hp;
        _hpBar = hpBar;
    }
    public void GetDamage(float value)
    {
        _hp -= value;
        float x = _hp / _maxHP;
        _hpBar.localScale = new Vector3(x, 1,1); 
        OnHealthChange();
    }
    private void OnHealthChange()
    {
        if (_hp <= 0)
            Dead();
    }
    private void Dead() 
    {
        GameObject pregab = _gameMyManager.AllItemsList[Random.Range(0, _gameMyManager.AllItemsList.Count)].gameObject;
        var item = _container.InstantiatePrefabForComponent<Item>(pregab);
        item.transform.position = _gameObject.transform.position;
        GameObject.Destroy(_gameObject);
    }
}