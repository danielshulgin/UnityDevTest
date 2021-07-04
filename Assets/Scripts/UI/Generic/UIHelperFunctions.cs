using UnityEngine;

namespace UI.Generic
{
    public static class UIHelperFunctions
    {
        public static void SetActiveCanvasGroup(CanvasGroup canvasGroup, bool active)
        {
            if (active)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
                return;
            }
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}