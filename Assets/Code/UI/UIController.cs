using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using VContainer;
using Game.States;
using Game.Systems;
using Code.Systems.Events;
using R3;
using System;

namespace Game.UI
{
    public class UIController : IInitializable, IDisposable
    {
        [Inject] private UIManager _uiManager;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private EventBus _eventBus;
        private CompositeDisposable _disposables = new CompositeDisposable();

        private bool isPaused = false;
        public UIController()
        {
            Debug.Log("UIController запущен.");
        }
        public void SetUIManager(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        public void Initialize()
        {
            isPaused = false;

            Debug.Log("UIController инициализирован.");
            _uiManager.SetStartButtonAction(OnStartGame);
            _uiManager.SetQuitButtonMainMenuAction(OnQuitMainMenu);
            _uiManager.SetResumeButtonAction(OnResumeGame);
            _uiManager.SetQuitButtonPauseAction(OnQuitPauseMenu);
            _uiManager.SetSettingsButtonAction(OnOpenSettings);
            _uiManager.SetCloseSettingsButtonAction(OnCloseSettings);

            _eventBus.OnTriggerEvent
                .Where(id => id == InputEvents.ESC)
                .Subscribe(_ => UOnPauseGame().Forget())
                .AddTo(_disposables);
        }

        private async void OnStartGame()
        {
            Debug.Log("OnStartGame вызван.");
            _uiManager.ShowLoadingScreen();
            await UniTask.Delay(1000);

            await _uiManager.HideMainMenu();
            await _gameStateMachine.Enter<GameplayState>();
        }

        private void OnQuitMainMenu()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void OnResumeGame()
        {
            _uiManager.HidePauseMenu().Forget();
            Time.timeScale = 1;
        }

        private void OnQuitPauseMenu()
        {
            _uiManager.HidePauseMenu().Forget();
            Time.timeScale = 1;
        }

        private void OnOpenSettings()
        {
            _uiManager.ShowSettingsMenu();
        }

        private void OnCloseSettings()
        {
            _uiManager.HideSettingsMenu().Forget();
        }
        private async UniTaskVoid UOnPauseGame()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                _uiManager.ShowPauseMenu();
            }
            else
            {
                _uiManager.HidePauseMenu().Forget();
            }
                await UniTask.Yield();
        }
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
