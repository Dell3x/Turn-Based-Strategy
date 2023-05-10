using Actions;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace BasedStrategy.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_levelGridView")] [SerializeField] private LevelGridController levelGridController;
        [SerializeField] private UnitActionSystem _unitActionSystem;
        [SerializeField] private GlobalActions _actions;
        public override void InstallBindings()
        {
           Container.Bind<LevelGridController>().FromInstance(levelGridController);
           Container.Bind<GlobalActions>().FromInstance(_actions);
           Container.Bind<UnitActionSystem>().FromInstance(_unitActionSystem);
        }
    }
    
}