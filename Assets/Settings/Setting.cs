using System.Collections.Generic;

namespace SFramework.Settings
{
    public class Setting
    {
        /// <summary>
        /// 资源种类 - 路径
        /// </summary>
        public Dictionary<ResourceType, string> ResourcesPath { get; }
            = new Dictionary<ResourceType, string>
            {
                [ResourceType.UI] = $"UI"
            };
    }
}


