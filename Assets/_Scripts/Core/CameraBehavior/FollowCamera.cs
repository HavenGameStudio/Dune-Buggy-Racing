using System;
using UnityEngine;

namespace Haven.DuneBuggyRacing.Core.CameraBehavior
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
        [SerializeField] private float smoothSpeed = 5f;

        [SerializeField] private Vector3 rotationOffset;
        private void Start()
        {
            if (!target)
            {
                Debug.LogWarning("Target is null! Trying to find Player");
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }


        private void LateUpdate()
        {
            if (!target) return;

            Vector3 desiredPosition = target.position + target.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
        }

    }

}
