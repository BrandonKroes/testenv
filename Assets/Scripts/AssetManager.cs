using System.Collections.Generic;
using System.Runtime.InteropServices;
using Script.Coroutine;
using Script.Generics;
using UnityEditor.DeviceSimulation;
using UnityEngine;

namespace Script
{
    public class AssetManager : SingletonMonoBehaviour<AssetManager>
    {
        public GameObject sceneInstantiator;
        private Dictionary<string, IAssetRequest> _assets;
        private MonoBehaviour _monoBehaviour;
        private List<string> _outstanding_requests;


        private void Start()
        {
            _monoBehaviour = GetComponent<MonoBehaviour>();
            _assets = new Dictionary<string, IAssetRequest>();
            _outstanding_requests = new List<string>();
        }

        public void GetAsset(IAssetRequest request)
        {
            _outstanding_requests.Add(request.GetRequestReference());
            request.ExecuteRequest(_monoBehaviour);
        }

        public void SetAsset(IAssetRequest request)
        {
            if (!_assets.ContainsKey(request.GetRequestReference()))
            {
                _outstanding_requests.Remove(request.GetRequestReference());

                _assets.Add(request.GetRequestReference(), request);

                _assets[request.GetRequestReference()].GetPayload().transform.SetParent(sceneInstantiator.transform);
                _assets[request.GetRequestReference()].GetPayload().transform.localScale =
                    new Vector3(0.01f, 0.01f, 0.01f);
                _assets[request.GetRequestReference()].GetPayload().transform.position = new Vector3(0f, 10f, 0f);

                _assets[request.GetRequestReference()].GetPayload().SetActive(true);
            }
        }

        public GameObject InstantiateByRequestReference(string rqref)
        {
            return _assets[rqref].GetPayload();
        }

        public bool IsAssetGathered(string slug)
        {
            
            print(slug);
            if (_assets.Count > 0)
            {
                foreach (var item in this._assets)
                {
                    if (item.Key == slug)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsAssetBeingGathered(string slug)
        {
            print(slug);
            if (_outstanding_requests.Count > 0)
            {
                foreach (var item in this._outstanding_requests)
                {
                    if (item == slug)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}