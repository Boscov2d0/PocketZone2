using UnityEngine;

public class PlayerDeathController : ObjectsDisposer
{
    private readonly GameMyManager _gameManager;
    private readonly PlayerManager _playerManager;

    public PlayerDeathController(GameMyManager gameManager, PlayerManager playerManager) 
    {
        _gameManager = gameManager;
        _playerManager = playerManager;
    }
    public void GetDamage(float value)
    {
        _playerManager.HP.Value -= value;
        if (_playerManager.HP.Value <= 0)
            Dead();
    }
    private void Dead() 
    {
        Time.timeScale = 0;
        _gameManager.GameStates.Value = UIStates.GameOver;
    }
}