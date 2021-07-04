using System;
using Generic;
using UnityEngine;

namespace Data
{
    public class PlayerData : MonoBehaviour
    {
        public event Action<int> OnLevelChanged;

        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt(Constants.CurrentLevelPlayerPref, 1);
            private set
            {
                OnLevelChanged?.Invoke(value);
                PlayerPrefs.SetInt(Constants.CurrentLevelPlayerPref, value);
            }
        }
        
        public void IncreaseLevel()
        {
            CurrentLevel += 1;
        }

        [ContextMenu("ResetPlayerPrefs")]
        private void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();    
        }
    }
}