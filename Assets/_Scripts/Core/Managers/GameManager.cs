using System;
using System.Collections;
using Haven.DuneBuggyRacing._Scripts.Core.Managers;
using Haven.DuneBuggyRacing._Scripts.Vehicle;
using Haven.DuneBuggyRacing.Tools;
using UnityEngine;

namespace Haven.DuneBuggyRacing.Core.Managers
{
    // File: GameManager.cs
    // Author: Adrian Caamino
    // Purpose: Manages the whole game. 
    // AI usage: No AI code was generated automatically.

    struct Lap
    {
        
    }
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private float countDownDuration;

        [SerializeField] private Buggy playerBuggy;

        [SerializeField] private int lapToMake ;
        

        [SerializeField] private int _currentLap;

        public event Action<int> OnLapChanged; 
        public int currentLap
        {
            get
            {
                return _currentLap;
            }

            set
            {
                if (value != _currentLap)
                {
                    _currentLap = value;
                    OnLapChanged?.Invoke(value);
                }
            }
        }
        
        private void Start()
        {
            playerBuggy = GameObject.FindGameObjectWithTag("Player").GetComponent<Buggy>();
            StartCoroutine(StartGame());

            Time.timeScale = 1;
        }

        IEnumerator StartGame()
        {
            float time = countDownDuration;

            while (time > 0f)
            {
                UIManager.Instance.UpdateCountDown(time);
                yield return new WaitForSeconds(1f);
                time--;
            }

            UIManager.Instance.UpdateCountDown(time);
            yield return new WaitForSeconds(1f);
            UIManager.Instance.UpdateCountDown(-1);

            playerBuggy.GetComponent<BuggyController>().enableControls = true;

            LaptimeHandler.Instance.startTime = true;
        }

        public void IncreaseLap()
        {
            currentLap++;
            LaptimeHandler.Instance.RecordLaptime();
            if (currentLap >= lapToMake)
            {
                Time.timeScale = 0f;
                UIManager.Instance.EnableCongratsText();
            }
        }
    }
}