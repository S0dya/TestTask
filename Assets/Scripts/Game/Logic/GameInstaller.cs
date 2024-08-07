using Zenject;
using UnityEngine;


public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private UIMain uiMain;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
        Container.Bind<UIMain>().FromInstance(uiMain).AsSingle().NonLazy();
    }
}
