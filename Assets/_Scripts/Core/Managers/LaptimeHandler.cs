using Haven.DuneBuggyRacing.Tools;
using UnityEngine;

namespace Haven.DuneBuggyRacing._Scripts.Core.Managers
{
    // File: LaptimeHandler.cs
    // Author: Adrian Caamino
    // Purpose: Records and displays lap times in formatted text.
    // Date: 2025-10-30
    // AI usage: Record Laptime && FormatTime (Only Refactored and XML Documented by AI)

    public class LaptimeHandler : Singleton<LaptimeHandler>
    {
        private UIManager uiManager;

        [SerializeField] private float currentTime;

        public bool startTime;

        private void Start()
        {
            uiManager = UIManager.Instance;
        }

        private void Update()
        {
            if (!startTime) return;
            
            currentTime += Time.deltaTime;
        }

        /// <summary>
        /// Records and displays the current lap time using formatted time.
        /// </summary>
        public void RecordLaptime()
        {
            string formattedTime = FormatTime(currentTime);
            uiManager.SpawnLaptimeUI(formattedTime);
            currentTime = 0f;
        }

        /// <summary>
        /// Converts a float time value into a formatted string (MM:SS:MS).
        /// </summary>
        private string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

            return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
        }
    }
}