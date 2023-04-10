using Actions;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UnityEngine;
using Zenject;

namespace BasedStrategy.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LevelGridView _levelGridView;
        [SerializeField] private UnitActionSystem _unitActionSystem;
        [SerializeField] private GlobalActions _actions;
        public override void InstallBindings()
        {
           Container.Bind<LevelGridView>().FromInstance(_levelGridView);
           Container.Bind<GlobalActions>().FromInstance(_actions);
           Container.Bind<UnitActionSystem>().FromInstance(_unitActionSystem);
        }
    }
    
}