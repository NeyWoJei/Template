using UnityEngine;

namespace Game.Dialogs
{
    [CreateAssetMenu(fileName = "New Dialog", menuName = "Game/Dialog")]
    public class DialogSO : ScriptableObject
    {
        public DialogLine[] lines;
    }
}