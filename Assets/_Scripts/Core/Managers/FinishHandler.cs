using System;
using Haven.DuneBuggyRacing.Core.Managers;
using UnityEngine;

namespace Haven.DuneBuggyRacing
{
    // File: Buggy.cs
    // Author: Adrian Caamino
    // Purpose: Handles the detection when the player passes the finish line. 
    // AI usage: No AI code was generated automatically.
    [RequireComponent(typeof(Collider))]
    public class FinishHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;

            GameManager.Instance.IncreaseLap();
        }
    }
}
