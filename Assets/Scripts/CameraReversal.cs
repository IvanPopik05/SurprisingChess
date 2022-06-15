using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraReversal : MonoBehaviour
    {
        [Header("White pieces")]
        [SerializeField] private Vector3 _whitePos;
        [SerializeField] private Vector3 _whiteRotate;
        
        [Header("Black pieces")]
        [SerializeField] private Vector3 _blackPos;
        [SerializeField] private Vector3 _blackRotate;

        [SerializeField] private float _softnessMove = 0.7f;

        public void ChangeLocationCamera(bool isWhite)
        {
            if (isWhite)
            {
                StartCoroutine(CoroutineChangePosition(transform.position, _whitePos));
                StartCoroutine(CoroutineChangeRotation(transform.rotation, _whiteRotate));
            }
            else
            {
                StartCoroutine(CoroutineChangePosition(transform.position, _blackPos));
                StartCoroutine(CoroutineChangeRotation(transform.rotation, _blackRotate));
            }
        }

        private IEnumerator CoroutineChangePosition(Vector3 startPos, Vector3 endPos)
        {
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime * _softnessMove;
                transform.position = Vector3.Lerp(startPos,endPos,t);
                yield return null;
            }

            transform.position = endPos;

            yield return null;
        }
        
        private IEnumerator CoroutineChangeRotation(Quaternion startRotate, Vector3 endRotate)
        {
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime * _softnessMove;
                transform.rotation = Quaternion.Lerp(startRotate, Quaternion.Euler(endRotate), t);
                yield return null;
            }

            transform.rotation = Quaternion.Euler(endRotate);

            yield return null;
        }
    }
}