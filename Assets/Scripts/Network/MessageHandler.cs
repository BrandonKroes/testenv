using System.Collections.Generic;
using Mirror;
using Script.Coroutine;
using UnityEngine;

namespace Script.Network
{
    public class MessageHandler : MonoBehaviour
    {
        private MonoBehaviour _monoBehaviour;

        private List<UpdateAssetPosition> _updateAssetPositionsQueue;

        private void Start()
        {
            _monoBehaviour = GetComponent<MonoBehaviour>();
        }

        public void RegisterMessages()
        {
            if (!NetworkClient.active) return;
            NetworkClient.RegisterHandler<NewOBJRequest>(MessageOBJRequest, false);
            NetworkClient.RegisterHandler<UpdateAssetPosition>(UpdateAssetPosition, false);
            NetworkClient.RegisterHandler<NewOBJMTLRequest>(MessageNewOBJTMLRequest, false);
        }


        private void UpdateAssetPosition(UpdateAssetPosition updateAssetPosition)
        {
            try
            {
                var obj = GameObject.Find(updateAssetPosition.id);
                obj.transform.position = updateAssetPosition.transform.position;
                obj.transform.localScale = updateAssetPosition.transform.localScale;
            }
            catch
            {
                _updateAssetPositionsQueue.Add(updateAssetPosition);
            }
        }

        private void processQueue()
        {
            List<UpdateAssetPosition> oldUpdates = _updateAssetPositionsQueue;
            _updateAssetPositionsQueue.Clear(); // Ensuring we are not endlessly appending the list.
            
            foreach (var updateAssetPosition in oldUpdates)
            {
                UpdateAssetPosition(updateAssetPosition);
            }
        }

        private void MessageNewOBJTMLRequest(NewOBJMTLRequest newObjmtlRequest)
        {
            new OBJMTLRequest(newObjmtlRequest).ExecuteRequest(_monoBehaviour);
        }

        private void MessageOBJRequest(NewOBJRequest newObjRequest)
        {
            new OBJRequest(newObjRequest).ExecuteRequest(_monoBehaviour);
        }
    }
}