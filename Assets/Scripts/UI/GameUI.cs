using Data;
using TMPro;
using UI.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        
        [SerializeField] private CanvasGroup pausePanelCanvasGroup;

        private PlayerData _playerData;
        
        
        [Inject]
        private void Resolve(PlayerData playerData)
        {
            _playerData = playerData;
            _playerData.OnLevelChanged += HandleLevelChanged;
            HandleLevelChanged(_playerData.CurrentLevel);
        }

        private void OnDestroy()
        {
            _playerData.OnLevelChanged -= HandleLevelChanged;
        }

        public void PauseButtonClick()
        {
            Time.timeScale = 0f;
            UIHelperFunctions.SetActiveCanvasGroup(pausePanelCanvasGroup, true);
        }
        
        public void ContinueButtonClick()
        {
            Time.timeScale = 1f;
            UIHelperFunctions.SetActiveCanvasGroup(pausePanelCanvasGroup, false);
        }

        public void ExitGameButtonClick()
        {
            Application.Quit(); 
        }

        private void HandleLevelChanged(int levelNumber)
        {
            levelText.text = levelNumber.ToString();
        }
    }
}