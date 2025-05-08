using UnityEngine;

namespace Game.Dialogs
{
    [System.Serializable]
    public class DialogLine
    {
        public string characterName;
        [TextArea] public string message;
    }
}