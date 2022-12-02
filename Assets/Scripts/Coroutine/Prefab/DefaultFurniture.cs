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
        private Vector3 _referenceScale;
        private bool _previouslySelected;
        private XRGrabInteractable _xrGrabInteractable;

        private void Start()
        {
            _referenceScale = transform.localScale;
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            _previouslySelected = false;
        }

        private void Update()
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

            if (_xrGrabInteractable.isSelected)
            {
                transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                _previouslySelected = true;
            }
            else if (_previouslySelected)
            {
                transform.localScale = _referenceScale;
                _previouslySelected = false;
            }

            if (transform.hasChanged)
            {
                var uap = new UpdateAssetPosition(name, transform);
                NetworkServer.SendToAll(uap);
                transform.hasChanged = false;
            }
        }
    }
}