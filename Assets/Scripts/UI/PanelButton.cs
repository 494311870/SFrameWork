using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework.UI;

public class PanelButton : MonoBehaviour
{
    [SerializeField] private PanelType panelType;
    public PanelType PanelType => panelType;

    [SerializeField] private PanelState panelState;
    public PanelState PanelState => panelState;


    /// <summary>
    /// 根据枚举值自动匹配状态转换，如有需要，可以配合动画机一起使用
    /// </summary>
    public void ChangePanelState()
    {
        switch (PanelState)
        {
            case PanelState.None:
                break;
            case PanelState.Enter:
                GameFacade.Instance.UISystem.PushPanel(PanelType);
                break;
            case PanelState.Exit:
                GameFacade.Instance.UISystem.PopPanel();
                break;
            case PanelState.Pause:
                GameFacade.Instance.UISystem.AddPanel(PanelType);
                break;
            case PanelState.Resume:
                GameFacade.Instance.UISystem.RemovePanel(PanelType);
                break;
            default:
                break;
        }
    }
}
