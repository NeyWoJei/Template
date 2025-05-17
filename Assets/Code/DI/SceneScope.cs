using UnityEngine;
using VContainer;
using Game.UI;
using Game.States;
using VContainer.Unity;
using System.Collections.Generic;

using CoreInitializable = Game.Core.IInitializable;

namespace Game.DI
{
    public class SceneScope : LifetimeScope
    {
        private static bool _isInitialized = false;

        protected override void Configure(IContainerBuilder builder)
        {
            if (_isInitialized)
            {
                Debug.LogWarning("SceneScope уже инициализирован, пропускаем повторный вызов");
                return;
            }
            _isInitialized = true;

            ConfigureUIModule(builder);
            ConfigureGameStatus(builder);
            AddStates(builder);
        }

        private void ConfigureUIModule(IContainerBuilder builder)
        {
            Debug.Log("Регистрация IUIService в SceneScope");
            builder.RegisterComponentInHierarchy<UIManager>().AsSelf();
            builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();
            builder.Register<UIController>(Lifetime.Singleton)
                .As<CoreInitializable>()
                .AsSelf();

        }

        private void ConfigureGameStatus(IContainerBuilder builder)
        {
            builder.Register<MenuState>(Lifetime.Singleton).As<IGameState>();
            builder.Register<PauseState>(Lifetime.Singleton).As<IGameState>();
            builder.Register<GameplayState>(Lifetime.Singleton).As<IGameState>();
            

        }
        
        private void AddStates(IContainerBuilder builder)
        {
            Debug.Log("Попытка добавления состояний в SceneScope");
            builder.RegisterBuildCallback(container =>
            {
                builder.RegisterInstance(container.Resolve<GameStateMachine>());

                try
                {
                    var uiController = container.Resolve<UIController>();
                    Debug.Log("UIController успешно разрешён: " + (uiController != null));
                    uiController.Initialize();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Ошибка при разрешении UIController: " + ex.Message);
                }
                
                var stateMachine = container.Resolve<IGameStateMachine>();
                var states = container.Resolve<IEnumerable<IGameState>>();

                foreach (var state in states)
                {
                    stateMachine.AddState(state);
                }   
            Debug.Log("Состояния добавлены");
            });
        }
    }
}