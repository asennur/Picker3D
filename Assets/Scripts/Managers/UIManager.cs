using Signals;
using UnityEngine;
using Enums;

public class UIManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables

    #endregion

    #endregion
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialized;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccesful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
    }


    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialized;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccesful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void OnLevelInitialized(int levelValue)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
        UISignals.Instance.onSetNewLevelValaue?.Invoke(levelValue);
    }
    
    private void OnLevelSuccesful()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
    }
    
    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
    }

    public void NextLevel()
    {
        CoreGameSignals.Instance.onNextLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
    }

    public void RestartLevel()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
    }

    public void Play()
    {
        CoreGameSignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
    }
    
    private void OnReset()
    {
        CoreUISignals.Instance.onCloseAllPanels?.Invoke();
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }

}
