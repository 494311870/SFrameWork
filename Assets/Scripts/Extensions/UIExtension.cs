using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework.Components;

namespace SFramework.Extensions
{
    public static class UIExtension 
    {
        /// <summary>
        /// 将组件订阅到游戏外观中
        /// </summary>
        /// <param name="component"></param>
        /// <param name="gameFacade"></param>
        public static void Subscribe(this IComponent component, GameFacade gameFacade)
        {
            Debug.Log(component is null);
            Debug.Log(gameFacade is null);
            gameFacade.AddComponent(component);
        }

        public static void Unsubscriber(this IComponent component, GameFacade gameFacade)
        {
            
        }
    }
}

