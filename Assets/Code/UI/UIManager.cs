using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIManager : MonoBehaviour, IInitializable
    {
        [Space(10)] [Header("Главное")]
        [SerializeField] private CanvasGroup menuCanvasGroup;

        [Header("МенюСтарта")]
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform menuPanel;
        [SerializeField] private Button quitButtonMainMenu;

        [Space(10)] [Header("МенюПаузы")]
        [SerializeField] private CanvasGroup pauseCanvasGroup;
        [SerializeField] private RectTransform pausePanel;
        [SerializeField] private Button resumeButton;

        [Space(10)] [Header("МенюВыхода")]
        [SerializeField] private Button quitButtonPause;

        [Space(10)] [Header("МенюЗагрузочногоЭкрана")]
        [SerializeField] private Image loadingImage;

        [Space(10)] [Header("МенюНастроекИгры")]
        [SerializeField] private CanvasGroup settingsCanvasGroup;
        [SerializeField] private RectTransform settingsPanel;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button closeSettingsButton;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle fullscreenToggle;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        public void Initialize()
        {
            ShowMainMenu();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            fullscreenToggle.onValueChanged.AddListener(OnFullScreenToggled);
        }

        // MAIN MENU

        public void ShowMainMenu()
        {
            menuCanvasGroup.DOFade(1, 0.3f);
            menuPanel.gameObject.SetActive(true);
        }

        public async UniTask HideMainMenu()
        {
            await menuCanvasGroup.DOFade(0, 0.3f).AsyncWaitForCompletion();

            if (gameObject == null)
            {
                Debug.Log("UIManager уничтожен.");
                return;
            }
            menuPanel.gameObject.SetActive(false);

        }

        // PAUSE MENU

        public void ShowPauseMenu()
        {
            pausePanel.gameObject.SetActive(true);
            pauseCanvasGroup.alpha = 0;
            pauseCanvasGroup.DOFade(1, 0.3f);
        }

        public async UniTask HidePauseMenu()
        {
            pauseCanvasGroup.DOFade(0, 0.3f);
            await UniTask.Delay(300);
            pausePanel.gameObject.SetActive(false);
        }

        // LOADING SCREEN

        public void ShowLoadingScreen()
        {
            loadingImage.gameObject.SetActive(true);
            loadingImage.color = new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, 0);
            loadingImage.DOFade(1, 0.7f);
        }

        public async UniTask HideLoadingScreen()
        {
            loadingImage.gameObject.SetActive(true);
            loadingImage.color = new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, 1);
            loadingImage.DOFade(0, 0.7f);
            
            await UniTask.Delay(700);

            loadingImage.gameObject.SetActive(false);
        }

        // SETTINGS

        public void ShowSettingsMenu()
        {
            settingsPanel.gameObject.SetActive(true);
            settingsPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }

        public async UniTask HideSettingsMenu()
        {
            await settingsPanel.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();

            settingsPanel.gameObject.SetActive(false);
        }

        private void OnVolumeChanged(float value)
        {
            AudioListener.volume = value;
        }

        private void OnFullScreenToggled(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        // BUTTON ACTIONS

        public void SetStartButtonAction(Action onClick)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetResumeButtonAction(Action onClick)
        {
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetQuitButtonMainMenuAction(Action onClick)
        {
            quitButtonMainMenu.onClick.RemoveAllListeners();
            quitButtonMainMenu.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetQuitButtonPauseAction(Action onClick)
        {
            quitButtonPause.onClick.RemoveAllListeners();
            quitButtonPause.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetSettingsButtonAction(Action onClick)
        {
            settingsButton.onClick.RemoveAllListeners();
            settingsButton.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetCloseSettingsButtonAction(Action onClick)
        {
            closeSettingsButton.onClick.RemoveAllListeners();
            closeSettingsButton.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}