using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private SceenButtonsController _buttonsController;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _spawnPoint;

    public override void InstallBindings()
    {
        Container.Bind<SceenButtonsController>().FromInstance(_buttonsController).AsSingle().NonLazy();

        var playerInstance = Container.InstantiatePrefabForComponent<PlayerController>(_playerController, _spawnPoint.position, Quaternion.identity, null);

        Container.Bind<PlayerController>().FromInstance(playerInstance).AsSingle().NonLazy();
        
    }
}