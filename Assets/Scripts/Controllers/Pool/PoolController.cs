using System.Collections.Generic;
using System.Xml.Schema;
using Data.UnityObject;
using Data.UnityObjects;
using Data.ValueObjects;
using DG.Tweening;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer renderer;

        #endregion

        #region Private Variables

        private float totalBall;
        [ShowInInspector] private PoolData _data;
        [ShowInInspector] private byte _collectedCount, _finalCount;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
            totalBall = GameObject.FindGameObjectsWithTag("Collectable").Length;
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .Levels[(int) CoreGameSignals.Instance.onGetLevelValue?.Invoke()]
                .PoolList[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangeThePoolColor;
            UISignals.Instance.onGetScore += OnGetValue;
        }

        private float OnGetValue()
        {
            return _finalCount / totalBall;
        }

        private void OnActivateTweens(int stageValue)
        {
            if (stageValue != stageID) return;
            foreach (var tween in tweens)
            {
                tween.DOPlay();
            }
        }

        private void OnChangeThePoolColor(int stageValue)
        {
            if (stageValue == stageID)
                renderer.material.DOColor(new Color(0.1960784f, 0.0941176f, 0.5607843f), 1).SetEase(Ease.Linear);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnChangeThePoolColor;
            UISignals.Instance.onGetScore -= OnGetValue;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            SetRequiredAmountToText();
        }

        public bool TakeStageResult(byte stageValue)
        {
            if (stageValue == stageID)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }
            return false;
        }

        private void SetRequiredAmountToText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;
            IncreaseCollectedCount();
            SetCollectedCountToText();
            _finalCount++;
            
        }

        private void SetCollectedCountToText()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
        }

        private void IncreaseCollectedCount()
        {
            _collectedCount++;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;
            DecreaseTheCollectedCount();
            SetCollectedCountToText();
        }

        private void DecreaseTheCollectedCount()
        {
            _collectedCount--;
        }
    }
}