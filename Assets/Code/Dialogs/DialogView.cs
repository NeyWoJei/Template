using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Dialogs
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text characterNameText;
        [SerializeField] private TMP_Text messageText;

        public void Show(string characterName, string message) {
            characterNameText.text = characterName;
            messageText.text = "";
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;

            DOTween.Kill(messageText);
            // messageText.DOText(message, 1.5f).SetEase(Ease.Linear);
        }

        public void Hide() {
            DOTween.Kill(messageText);
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}