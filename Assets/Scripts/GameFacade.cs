using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework.Components;
using SFramework.Systems;
public class GameFacade : MonoBehaviour
{
    // 单例模式
    public static GameFacade Instance { get; private set; }
    private GameFacade() { }

    // 游戏画布
    [SerializeField] private Canvas canvas;

    public Canvas Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }

    private Client Client { get; set; }

    public Dictionary<ComponentType, List<IComponent>> ComponentDict { get; } = new Dictionary<ComponentType, List<IComponent>>();

    public UISystem UISystem { get; } = new UISystem();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //Client = new Client();
        //var test = new UISystem();
        //Debug.Log("GameFacade Init");
        //test.OnInit();
        UISystem.OnInit();

    }

    // Update is called once per frame
    void Update()
    {

    }




    public void AddComponent(IComponent component)
    {
        Debug.Log("??");
        if (ComponentDict.ContainsKey(component.ComponentType))
        {
            ComponentDict[component.ComponentType].Add(component);
        }
        else
        {
            ComponentDict.Add(component.ComponentType, new List<IComponent> { component });
        }
    }

    public void RemoveComponent(IComponent component)
    {
        if (ComponentDict.ContainsKey(component.ComponentType))
        {
            ComponentDict[component.ComponentType].Remove(component);
        }
        else
        {
            Debug.LogWarning($"在移除组件：{component.ComponentType}时遇到了预期外的错误");
        }
    }
}
