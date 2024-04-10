using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [SerializeField] private GameMyManager _gameMyManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private CameraController _cameraPrefab;
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private Item _startWeaponPrefab;
    [SerializeField] private Item _patronsPrefab;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private NPCController _mutantPrefab;
    [SerializeField] private int _countOfMutant;

    private Transform _playerTransform;

    public override void InstallBindings()
    {
        Container.Bind<UIController>().AsSingle();

        BindManagers();
        CreatePlayer();

        Container.Bind<Transform>().FromInstance(_playerTransform);

        CreateCamera();
        CreateAndBindMutants();

        Container.Bind<Item>().AsTransient();

        Item startWeapon = Container.InstantiatePrefabForComponent<Item>(_startWeaponPrefab, _startPosition + new Vector3(2.5f, 0, 0), Quaternion.identity, null);
        Item patrons = Container.InstantiatePrefabForComponent<Item>(_patronsPrefab, _startPosition + new Vector3(3, -3, 0), Quaternion.identity, null);
    }
    private void BindManagers()
    {
        Container.BindInstance(_gameMyManager);
        Container.BindInstance(_uiManager);
        Container.BindInstance(_playerManager);
    }
    private void CreatePlayer()
    {
        PlayerController player = Container.InstantiatePrefabForComponent<PlayerController>(_playerPrefab, _startPosition, Quaternion.identity, null);
        Container.Bind<PlayerController>().FromInstance(player).AsTransient();
        _playerTransform = player.transform;
    }
    private void CreateCamera()
    {
        CameraController camera = Container.InstantiatePrefabForComponent<CameraController>(_cameraPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<CameraController>().FromInstance(camera).AsSingle();
    }
    private void CreateAndBindMutants()
    {
        for (int i = 0; i < _countOfMutant; i++)
        {
            bool isColliding = true;

            Vector3 randomPoint = new Vector3(Random.Range(-20, 23), 0f, Random.Range(-10f, 8));
            NPCController mutant = Container.InstantiatePrefabForComponent<NPCController>(_mutantPrefab, randomPoint, Quaternion.identity, null);
            Container.Bind<NPCController>().FromInstance(mutant).AsTransient();

            while (isColliding)
            {
                Collider[] colliders = Physics.OverlapBox(randomPoint, mutant.transform.localScale / 2, Quaternion.identity);

                isColliding = false;

                foreach (Collider collider in colliders)
                {
                    if (collider.tag == "Obstacles")
                    {
                        isColliding = true;
                        Destroy(mutant);
                        break;
                    }
                }
            }
        }
    }
}