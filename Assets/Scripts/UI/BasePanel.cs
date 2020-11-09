using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有面板的基类，提供四种状态的反应
/// </summary>
public class BasePanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup => canvasGroup;


    public virtual void OnEnter()
    {
        SetActive(true);
    }

    public virtual void OnExit()
    {
        SetActive(false);
    }

    public virtual void OnPause()
    {
        CanvasGroup.blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        CanvasGroup.blocksRaycasts = true;
    }


    public void SetActive(bool active)
    {
        CanvasGroup.alpha = active ? 1 : 0;
        CanvasGroup.interactable = active ? true : false;
        CanvasGroup.blocksRaycasts = active ? true : false;
    }
}
