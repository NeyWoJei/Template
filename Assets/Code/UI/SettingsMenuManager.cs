// using System;
// using Cysharp.Threading.Tasks;
// using DG.Tweening;
// using Game.Core;
// using UnityEngine;
// using UnityEngine.UI;
// using VContainer;

// namespace Game.UI
// {
//     public class SettingsMenuManager : MonoBehaviour, IInitializable
//     {
//         [Inject] private MainMenuManager _mainMenuManager;

//         [Space(10)] [Header("МенюНастроекИгры")]
//         [SerializeField] private RectTransform settingsPanel;
//         [SerializeField] private Button settingsButton;
//         [SerializeField] private Button closeSettingsButton;
//         [SerializeField] private Slider volumeSlider;
//         [SerializeField] private Toggle fullscreenToggle;

//         public void Initialize()
//         {
//             volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
//             fullscreenToggle.onValueChanged.AddListener(OnFullScreenToggled);
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