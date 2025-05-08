using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("МенюСтарта")]
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform menuPanel;
        [SerializeField] private Button quitButtonMainMenu;

        [Space(10)] [Header("МенюПаузы")]
        [SerializeField] private RectTransform pausePanel;
        [SerializeField] private Button resumeButton;

        [Space(10)] [Header("МенюВыхода")]
        [SerializeField] private Button quitButtonPause;

        [Space(10)] [Header("МенюЗагрузочногоЭкрана")]
        [SerializeField] private RectTransform loadingPanel;

        [Space(10)] [Header("МенюНастроекИгры")]
        [SerializeField] private RectTransform settingsPanel;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button closeSettingsButton;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle fullscreenToggle;

        [Space(10)] [Header("Диалоговое Окно")]
        [SerializeField] private RectTransform dialogPanel;
        [SerializeField] private TMP_Text dialogText;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private void Start()
        {
            ShowMainMenu();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            fullscreenToggle.onValueChanged.AddListener(OnFullScreenToggled);
        }

        // MAIN MENU

        public void ShowMainMenu()
        {
            menuPanel.gameObject.SetActive(true);
            menuPanel.localScale = Vector3.zero;
            menuPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }

        public async UniTask HideMainMenu()
        {
            await menuPanel.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            menuPanel.gameObject.SetActive(false);
        }

        // PAUSE MENU

        public void ShowPauseMenu()
        {
            pausePanel.gameObject.SetActive(true);
            pausePanel.localScale = Vector3.zero;
            pausePanel.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        }

        public async UniTask HidePauseMenu()
        {
            await pausePanel.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();

            pausePanel.gameObject.SetActive(false);
        }

        // LOADING SCREEN

        public void ShowLoadingScreen()
        {
            loadingPanel.gameObject.SetActive(true);
            loadingPanel.localScale = Vector3.zero;
            loadingPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }

        public void HideLoadingScreen()
        {
            loadingPanel.gameObject.SetActive(false);
        }

        // SETTINGS

        public void ShowSettingsMenu()
        {
            settingsPanel.gameObject.SetActive(true);
            settingsPanel.localScale = Vector3.zero;
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

        // DIALOG WINDOW

        public void ShowDialog(string message, Action onYes, Action onNo)
        {
            dialogText.text = message;
            dialogPanel.gameObject.SetActive(true);
            dialogPanel.localScale = Vector3.zero;
            dialogPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() => onYes());
            noButton.onClick.AddListener(() => onNo());
        }

        public async UniTask HideDialog()
        {
            await dialogPanel.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .AsyncWaitForCompletion();

            dialogPanel.gameObject.SetActive(false);
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

        public void SetYesButtonAction(Action onClick)
        {
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetNoButtonAction(Action onClick)
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}
