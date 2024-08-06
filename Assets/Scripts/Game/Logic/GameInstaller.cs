using Zenject;
using UnityEngine;


public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
        Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle().NonLazy();
        Container.Bind<PlayerCombat>().FromInstance(playerCombat).AsSingle().NonLazy();
    }
}
