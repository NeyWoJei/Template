using VContainer;
using VContainer.Unity;
using Game.Core;
using Game.States;
using Game.UI;
using Game.Systems;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Systems.Audio;

using CoreInitializable = Game.Core.IInitializable;
using CoreTickable = Game.Core.ITickable;
using CoreFixedTickable = Game.Core.IFixedTickable;

namespace Game.DI
{
    public class ProjectScope : LifetimeScope
    {
        private ProjectScope _instance;
        private static bool _isInitialized = false;

        private new void Awake()
        {
            if (_instance != null && _instance != this) {
                Debug.LogWarning("Обнаружен второй экземпялр ProjectScope, уничтожаем его.");
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            base.Awake();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            if (_isInitialized)
            {
                Debug.LogWarning("ProjectScope уже инициализирован, пропускаем повторный вызов");
                return;
            }
            _isInitialized = true;

            try
            {
                ConfigureCoreSystems(builder);      // 1. Core системы
                ConfigureGameStatus(builder);       // 2. Состояния игры
                ConfigureAudio(builder);            // 3. Аудио системы
                ConfigureUIModule(builder);         // 4. UI
                ConfigureAnimationSystems(builder); // 5. Анимация
                ConfigureEvents(builder);           // 6. События
                ConfigureInput(builder);            // 7. Ввод
                ConfigureSaveLoad(builder);         // 8. Сохранение и загрузка
                ConfigureGameLoop(builder);         // 9. Игровой цикл
                RegisterBuildTick(builder);         // 10. Регистрация в игровом цикле

                Debug.Log(builder.ToString());
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка в ProjectScope конфигурации: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void ConfigureCoreSystems(IContainerBuilder builder)
        {
            builder.Register<IGameStateService, GameStateService>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<CheckPointSystem>(Lifetime.Singleton);
            builder.Register<PoolSystem>(Lifetime.Singleton).As<CoreInitializable>();
        }

        private void ConfigureGameStatus(IContainerBuilder builder)
        {
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadLevelState>(Lifetime.Singleton).As<IGameState>();
            builder.Register<BootstrapState>(Lifetime.Singleton)
                .AsSelf()
                .As<IGameState, CoreInitializable>();
            builder.Register<MenuState>(Lifetime.Singleton).As<IGameState>().AsSelf();
            builder.Register<PauseState>(Lifetime.Singleton).As<IGameState>();
            builder.Register<GameplayState>(Lifetime.Singleton).As<IGameState>();
        }

        private void ConfigureAnimationSystems(IContainerBuilder builder)
        {
            builder.Register<AnimationSystem>(Lifetime.Singleton);
        }

        private void ConfigureEvents(IContainerBuilder builder)
        {
            builder.Register<EventBus>(Lifetime.Singleton);
        }
        private void ConfigureUIModule(IContainerBuilder builder)
        {
            Debug.Log("Регистрация IUIService в ProjectScope");
            builder.RegisterComponentInHierarchy<UIManager>().AsSelf();
            builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();
            builder.Register<UIController>(Lifetime.Singleton)
                .As<CoreInitializable>()
                .AsSelf();
        }

        private void ConfigureInput(IContainerBuilder builder)
        {
            builder.Register<InputSystem>(Lifetime.Singleton).As<CoreInitializable>();
        }

        private void ConfigureSaveLoad(IContainerBuilder builder)
        {
            builder.Register<ISaveLoadService, SaveLoadSystem>(Lifetime.Singleton);
        }

        private void ConfigureGameLoop(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameLoop>()
                .AsSelf()
                .As<CoreInitializable, CoreTickable, CoreFixedTickable>();
        }

        private void ConfigureAudio(IContainerBuilder builder)
        {
            builder.Register<IAudioSystem, AudioSystem>(Lifetime.Singleton).As<CoreInitializable>();
        }

        private void RegisterBuildTick(IContainerBuilder builder)
        {
            Debug.Log("BuildTick");
            builder.RegisterBuildCallback(container =>
            {
                Debug.Log("RegisterBuildTick вызван");
                try
                {
                    var gameLoop = container.Resolve<GameLoop>();
                    var stateMachine = container.Resolve<IGameStateMachine>();

                    var states = container.Resolve<IEnumerable<IGameState>>();
                    foreach (var state in states)
                    {
                        stateMachine.AddState(state);
                    }

                    var bootstrapState = container.Resolve<BootstrapState>();
                    bootstrapState.SetStateMachine(stateMachine);

                    RegisterToLoop(container.Resolve<IEnumerable<CoreTickable>>(), gameLoop.RegisterTickable, "CoreTickable");
                    RegisterToLoop(container.Resolve<IEnumerable<CoreInitializable>>(), gameLoop.RegisterInitializable, "CoreInitializable");
                    RegisterToLoop(container.Resolve<IEnumerable<CoreFixedTickable>>(), gameLoop.RegisterFixedTickable, "CoreFixedTickable");

                    gameLoop.Initialize();
                    Debug.Log("Build tick завершил регистрацию");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Ошибки в RegisterBuildTick: {ex.Message}\n{ex.StackTrace}");
                }
            });
        }

        private void RegisterToLoop<T>(IEnumerable<T> list, Action<T> register, string typeName)
        {
            Debug.Log($"Регистрируем следующие: {typeName}");
            if (list == null)
            {
                Debug.LogError($"Список с {typeName} пуст!");
                return;
            }
            foreach (var item in list)
            {
                Debug.Log($"Регистрация {typeName}: {item?.GetType().Name ?? "пусто"}");
                if (item != null) register(item);
                else Debug.LogWarning($"Обнаружен пустой {typeName} в списке.");
            }
        }
    }
}
