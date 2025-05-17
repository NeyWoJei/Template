// using System;
// using Cysharp.Threading.Tasks;
// using DG.Tweening;
// using Game.Core;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.UI;
// using VContainer;

// namespace Game.UI
// {
//     public class PauseMenuManager : MonoBehaviour, IInitializable
//     {
//         [Inject] private IUIService _uiService;
        
//         [Space(10)] [Header("МенюПаузы")]
//         [SerializeField] private RectTransform pausePanel;
//         [SerializeField] private Button resumeButton;
//         [SerializeField] private Button settingsButtonPause;
//         [SerializeField] private Button quitButtonPause;

//         [Space(10)] [Header("МенюЗагрузочногоЭкрана")]
//         [SerializeField] private Image loadingImage;


//         public void Initialize()
//         {

//         }

//         // PAUSE MENU

//         public void ShowPauseMenu()
//         {
//             pausePanel.gameObject.SetActive(true);
//             pausePanel.localScale = Vector3.zero;
//             pausePanel.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
//         }

//         public async UniTask HidePauseMenu()
//         {
//             await pausePanel.DOScale(Vector3.zero, 0.2f)
//                 .SetEase(Ease.InBack)
//                 .AsyncWaitForCompletion();

//             pausePanel.gameObject.SetActive(false);
//         }

//         public void HideLoadingScreen()
//         {
//             if (gameObject == null)
//             {
//                 Debug.Log("UIManager уничтожен.");
//                 return;
//             }
//             loadingImage.gameObject.SetActive(false);
//         }

//         // SETTINGS

//         public void ShowSettingsMenu()
//         {
//             settingsPanel.gameObject.SetActive(true);
//             settingsPanel.localScale = Vector3.zero;
//             settingsPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
//         }

//         public async UniTask HideSettingsMenu()
//         {
//             await settingsPanel.DOScale(Vector3.zero, 0.2f)
//                 .SetEase(Ease.InBack)
//                 .AsyncWaitForCompletion();

//             settingsPanel.gameObject.SetActive(false);
//         }

//         private void OnVolumeChanged(float value)
//         {
//             AudioListener.volume = value;
//         }

//         private void OnFullScreenToggled(bool isFullscreen)
//         {
//             Screen.fullScreen = isFullscreen;
//         }

//         // BUTTON ACTIONS

//         public void SetStartButtonAction(Action onClick)
//         {
//             startButton.onClick.RemoveAllListeners();
//             startButton.onClick.AddListener(() => onClick?.Invoke());
//         }

//         public void SetResumeButtonAction(Action onClick)
//         {
//             resumeButton.onClick.RemoveAllListeners();
//             resumeButton.onClick.AddListener(() => onClick?.Invoke());
//         }

//         public void SetQuitButtonMainMenuAction(Action onClick)
//         {
//             quitButtonMainMenu.onClick.RemoveAllListeners();
//             quitButtonMainMenu.onClick.AddListener(() => onClick?.Invoke());
//         }

//         public void SetQuitButtonPauseAction(Action onClick)
//         {
//             quitButtonPause.onClick.RemoveAllListeners();
//             quitButtonPause.onClick.AddListener(() => onClick?.Invoke());
//         }

//         public void SetSettingsButtonAction(Action onClick)
//         {
//             settingsButton.onClick.RemoveAllListeners();
//             settingsButton.onClick.AddListener(() => onClick?.Invoke());
//         }

//         public void SetCloseSettingsButtonAction(Action onClick)
//         {
//             closeSettingsButton.onClick.RemoveAllListeners();
//             closeSettingsButton.onClick.AddListener(() => onClick?.Invoke());
//         }
//     }
// }