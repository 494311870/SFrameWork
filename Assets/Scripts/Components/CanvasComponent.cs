using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework.Components;
using SFramework.Extensions;

public class CanvasComponent : MonoBehaviour, IComponent
{
    public ComponentType ComponentType { get; } = ComponentType.Canvas;

    [SerializeField] private Canvas canvas;

    public Canvas Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }

    private void Start()
    {
        this.Subscribe(GameFacade.Instance);
    }
    

    private void OnDestroy()
    {
        this.Unsubscriber(GameFacade.Instance);
    }

}

