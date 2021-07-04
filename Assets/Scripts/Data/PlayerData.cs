using System;
using UnityEngine;

namespace Data
{
    public class PlayerData : MonoBehaviour
    {
        public event Action<int> OnLevelChanged;

        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt("CurrentLevel", 1);
            private set
            {
                OnLevelChanged?.Invoke(value);
                PlayerPrefs.SetInt("CurrentLevel", value);
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