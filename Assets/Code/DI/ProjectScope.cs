using VContainer;
using VContainer.Unity;
using Game.Core;
using Game.States;
using Game.UI;
using Game.Systems;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using CoreInitializable = Game.Core.IInitializable;
using CoreTickable = Game.Core.ITickable;
using CoreFixedTickable = Game.Core.IFixedTickable;
using Game.Systems.Audio;

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
            ConfigureGameLoop(builder);
            ConfigureAudio(builder);

            RegisterBuildTick(builder); // регистрация тиков
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
            builder.Register<UIController>(Lifetime.Singleton)
                .As<CoreInitializable>();
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

        private void ConfigureGameLoop(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameLoop>()
                .AsSelf()
                .As<CoreInitializable, CoreTickable, CoreFixedTickable>();
        }    

        private void ConfigureAudio(IContainerBuilder builder) {
            builder.Register<IAudioSystem, AudioSystem>(Lifetime.Singleton);
        }

        private void RegisterBuildTick(IContainerBuilder builder) {
            builder.RegisterBuildCallback(container =>
            {
                var gameLoop = container.Resolve<GameLoop>();

                RegisterToLoop(container.Resolve<IEnumerable<CoreTickable>>(), gameLoop.RegisterTickable);
                RegisterToLoop(container.Resolve<IEnumerable<CoreInitializable>>(), gameLoop.RegisterInitializable);
                RegisterToLoop(container.Resolve<IEnumerable<CoreFixedTickable>>(), gameLoop.RegisterFixedTickable);

                gameLoop.Initialize();
            });
        }
        private void RegisterToLoop<T>(IEnumerable<T> list, Action<T> register) {
            foreach (var item in list) 
                register(item);
        }   
    }
}
