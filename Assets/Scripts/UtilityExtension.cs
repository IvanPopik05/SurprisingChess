using UnityEngine;

namespace DefaultNamespace
{
    public static class UtilityExtension
    {
        public static MeshRenderer GetRenderer(this GameObject obj) => 
            obj.GetComponent<MeshRenderer>();
    }
}