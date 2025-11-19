using System;
using Haven.DuneBuggyRacing.Core.Managers;
using Haven.DuneBuggyRacing.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Haven.DuneBuggyRacing._Scripts.Core.Managers
{
    // File: UIManager.cs
    // Author: Adrian Caamino
    // Purpose: Handles all UI elements.
    // Date: 2025-10-30
    // AI usage: No AI code was generated automatically.

    public class UIManager:Singleton<UIManager>
    {
        [SerializeField] private TMP_Text speedText;

        [Header("CountDown UI Elements")] [SerializeField]
        private TMP_Text countDownText;
        
        [Header("Lap UI Elements")]
        [SerializeField] private TMP_Text lapText;
        
        [Header("Congrats UI Elements")]
        [SerializeField] private TMP_Text congratsText;

        [Header("Buttons UI Elements")]
        
        [SerializeField] private GameObject buttonParent;
        
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        [Header("Laptime")] [SerializeField] private TMP_Text lapTimePrefab;

        [SerializeField] private Transform lapTimeParent;
        

        
        
        // On start, initialize the quit and restart button.
        private void Start()
        {
            if (!restartButton || !quitButton) 
            {
                Debug.LogError($"Either restart button or quit button is null");
                return;
            }

            InitializeButton(restartButton, OnRestartButtonClicked);
            InitializeButton(quitButton, OnQuitButtonClicked);
            
        }
        
        /// <summary>
        /// Action for restartButton.
        /// reloads the main scene.
        /// </summary>
        private void OnRestartButtonClicked() => SceneManager.LoadScene(0);
        
        /// <summary>
        /// Action for Quit Button
        /// Quits the application. 
        /// </summary>
        private void OnQuitButtonClicked() => Application.Quit();
        
        /// <summary>
        /// Adds Action listener to a button. 
        /// </summary>
        /// <param name="button">The button to add the onClickAction</param>
        /// <param name="onClickAction">The action to perform when the button is clicked.</param>
        private void InitializeButton(Button button, Action onClickAction)
        {
            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() => onClickAction());
        }

        private void OnEnable()
        {
            GameManager.Instance.OnLapChanged += UpdateLapText;
        }
        
        /// <summary>
        /// Updates the speedometer Hud text
        /// </summary>
        /// <param name="currentSpeed"></param>
        public void UpdateSpeedText(float currentSpeed)
        {
            currentSpeed *= 2;
            speedText.text = currentSpeed.ToString("F0") + "km/h";
        }
        /// <summary>
        /// Updates the Start Count down
        /// </summary>
        /// <param name="currentCount">Current Count Down to show to the screen.</param>
        public void UpdateCountDown(float currentCount)
        {
            switch (currentCount)
            {
                case < 0:
                    countDownText.text = "";
                    countDownText.gameObject.SetActive(false);
                    return;
                case < 1:
                    countDownText.text = "GO!";
                    return;
                default:
                    countDownText.text = currentCount.ToString("F0");
                    break;
            }
        }
        
        /// <summary>
        /// Updates the Lap Text
        /// </summary>
        /// <param name="currentLap"></param>
        public void UpdateLapText(int currentLap)
        {
            lapText.text = "Current Lap: " + currentLap.ToString();
        }

        public void EnableCongratsText()
        {
            congratsText.text = "Congrats! You have finished the race!";
            congratsText.gameObject.SetActive(true);

            buttonParent.SetActive(true);
        }

        public void SpawnLaptimeUI(string currentLapTimeSeconds)
        {
            if (!lapTimePrefab) return;
            
            TMP_Text laptime = Instantiate(lapTimePrefab, lapTimeParent);

            laptime.text = $"Lap {GameManager.Instance.currentLap}: {currentLapTimeSeconds}";
        }
    }
}