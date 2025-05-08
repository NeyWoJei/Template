using VContainer;
using VContainer.Unity;
using Game.Core;
using Game.States;
using Game.UI;
using Game.Systems;

namespace Game.DI 
{
    public class ProjectScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) {
            ConfigureCoreSystems(builder);
            ConfigureGameStatus(builder);
            ConfigureUIModule(builder);
            ConfigureAnimationSystems(builder);
            ConfigureEvents(builder);
            ConfigureInput(builder);
            ConfigureSaveLoad(builder);
        }
        private void ConfigureCoreSystems(IContainerBuilder builder) {
            builder.Register<IGameStateService, GameStateService>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<CheckPointSystem>(Lifetime.Singleton);
        }
        private void ConfigureGameStatus(IContainerBuilder builder) {
            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<LoadLevelState>(Lifetime.Singleton);
            builder.Register<MenuState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
            builder.Register<PauseState>(Lifetime.Singleton);
        }
        private void ConfigureUIModule(IContainerBuilder builder) {
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<UIManager>();
            builder.RegisterComponentInHierarchy<UIController>();
        }
        private void ConfigureAnimationSystems(IContainerBuilder builder) {
            builder.Register<AnimationSystem>(Lifetime.Singleton);
        }

        private void ConfigureEvents(IContainerBuilder builder) {
            builder.Register<EventBus>(Lifetime.Singleton);
        }
        private void ConfigureInput(IContainerBuilder builder) {
            builder.Register<InputSystem>(Lifetime.Singleton);
        }
        private void ConfigureSaveLoad(IContainerBuilder builder) {
            builder.Register<ISaveLoadService, SaveLoadSystem>(Lifetime.Singleton);
        }
    }
}
