using System.IO;

namespace GameCore
{
    public class BaseObject : UnityEngine.Object
    {
        protected virtual void OnSerialize(BinaryWriter writer)
        {

        }

        protected virtual void OnDeSerialize(BinaryReader reader)
        {

        }
    }
}
