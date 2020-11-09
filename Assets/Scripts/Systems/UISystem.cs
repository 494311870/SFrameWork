using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework.Components;
using System.Linq;
using SFramework.UI;
namespace SFramework.Systems
{
    public class UISystem : BaseSystem
    {
        private Transform canvasTransfrom;
        /// <summary>
        /// 获取当前场景画布的Transfrom
        /// </summary>
        public Transform CanvasTransfrom => canvasTransfrom is null ?
            canvasTransfrom = GameFacade.Instance.Canvas.transform : canvasTransfrom;
        /// <summary>
        /// 按照层级关系存放面板
        /// </summary>
        public Stack<HashSet<BasePanel>> PanelStack { get; } = new Stack<HashSet<BasePanel>>();
        /// <summary>
        /// 用于存放所有的面板
        /// </summary>
        public Dictionary<PanelType, BasePanel> PanelDict { get; } = new Dictionary<PanelType, BasePanel>();

        public override void OnInit()
        {
            base.OnInit();
            Debug.Log("UISystem Init");
            PushPanel(PanelType.MainGame);
        }

        /// <summary>
        /// 推入一个面板并置于新的一层
        /// </summary>
        /// <param name="panelType"></param>
        public void PushPanel(PanelType panelType)
        {
            if (TryGetPanel(panelType, out BasePanel panel))
            {
                //将当前层隐藏，再推入新的一层
                if (PanelStack.Any())
                {
                    foreach (var item in PanelStack.Peek())
                    {
                        item.OnPause();
                    }
                }

                PanelStack.Push(new HashSet<BasePanel> { panel });
                panel.OnEnter();
            }
            
        }

        /// <summary>
        /// 移除最上层所有面板
        /// </summary>
        public void PopPanel()
        {
            if (PanelStack.Any())
            {
                foreach (var panel in PanelStack.Pop())
                    panel.OnExit();

                if (PanelStack.Any())
                {
                    foreach (var panel in PanelStack.Peek())
                        panel.OnResume();
                }
            }
        }

        /// <summary>
        /// 在当前层添加一个面板
        /// </summary>
        /// <param name="panelType"></param>
        public void AddPanel(PanelType panelType)
        {
            if (PanelStack.Any())
            {
                if (TryGetPanel(panelType, out BasePanel panel))
                {
                    PanelStack.Peek().Add(panel);
                    panel.OnEnter();
                }
                    
            }
        }

        /// <summary>
        /// 从当前层移除一个面板
        /// </summary>
        /// <param name="panelType"></param>
        public void RemovePanel(PanelType panelType)
        {
            Debug.Log($"移除了一个{panelType}面板");
            if (TryGetPanel(panelType, out BasePanel panel))
            {
                PanelStack.Peek().Remove(panel);
                panel.OnExit();
                
            }
        }

        /// <summary>
        /// 尝试从内存中或磁盘中获取一个面板
        /// </summary>
        /// <param name="panelType">面板种类</param>
        /// <param name="panel">面板</param>
        /// <returns></returns>
        public bool TryGetPanel(PanelType panelType, out BasePanel panel)
        {
            if (!PanelDict.TryGetValue(panelType, out panel))
            {
                // 面板不存在时，尝试从文件资源中加载
                var path = $"{SGlobal.Setting.ResourcesPath[ResourceType.UI]}/Prefabs/{panelType.ToString()}Panel";
                var panelObject = Resources.Load<GameObject>(path);
                if (panelObject is null)
                {
                    Debug.LogWarning($"{panelType}不存在，请检查资源：{path}");
                    return false;
                }
                else
                {
                    panel = Object.Instantiate(panelObject, CanvasTransfrom).GetComponent<BasePanel>();
                    PanelDict.Add(panelType, panel);
                }
            }

            return true;
        }
    }
}

