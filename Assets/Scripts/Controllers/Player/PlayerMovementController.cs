using Data.ValueObjects;
using Keys;
using Managers;
using Sirenix.OdinInspector;
using System;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Collider collider;

        #endregion

        #region Private Variables

        [ShowInInspector] private MovementData _data;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private bool _isInMiniGame = false, _isMiniGameFinished;
        [ShowInInspector] private float _xValue;
        private Vector3 firstTransform, finalPos;
        private float2 _clampValues;

        #endregion

        #endregion

        internal void GetMovementData(MovementData movementData)
        {
            _data = movementData;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (_isReadyToMove)
            {
                MovePlayer();
            }
            else StopPlayerHorizontaly();

            if (_isInMiniGame)
            {
                MiniGameMovement();
            }
            if (_isMiniGameFinished)
            {
                StopPlayerHorizontaly();
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            }
            
        }

        private void StopPlayerHorizontaly()
        {
            rigidbody.velocity = new float3(0, rigidbody.velocity.y, _data.ForwardSpeed);
            rigidbody.angularVelocity = float3.zero;
        }

        private void MovePlayer()
        {
            var velocity = rigidbody.velocity;
            velocity = new float3(_xValue * _data.SidewaysSpeed, velocity.y,
                _data.ForwardSpeed);
            rigidbody.velocity = velocity;

            float3 position;
            position = new float3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
        }

        private void MiniGameMovement()
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y,
                    firstTransform.z + (SliderController.Instance.finalScore * 250 )), 4*Time.deltaTime);
            finalPos = new Vector3(transform.position.x, transform.position.y, firstTransform.z + SliderController.Instance.finalScore * 250);
            if (Vector3.Distance(finalPos, transform.position) < 1)
            {
                _isInMiniGame = false;
                _isMiniGameFinished = true;
            }
        }

        private void StopPlayer()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
            rigidbody.angularVelocity = float3.zero;
        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        } 
        internal void IsInMiniGame(bool condition)
        {
            _isInMiniGame = condition;
            firstTransform = transform.position;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }
        internal void UpdateInputParams(HorizontalnputParams inputParams)
        {
            _xValue = inputParams.HorizontalInputValue;
            _clampValues = new float2(inputParams.HorizontalInputClampNegativeSide,
                inputParams.HorizontalInputClampPositiveSide);
        }

        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }
    }
}