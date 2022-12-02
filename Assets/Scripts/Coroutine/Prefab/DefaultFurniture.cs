using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Script.Network;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Coroutine.Prefab
{
    public class DefaultFurniture : MonoBehaviour
    {
        private List<GameObject> Children;


        private void Update()
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            if (transform.hasChanged)
            {
                var uap = new UpdateAssetPosition(name, transform);
                NetworkServer.SendToAll(uap);
                transform.hasChanged = false;
            }
        }
    }
}