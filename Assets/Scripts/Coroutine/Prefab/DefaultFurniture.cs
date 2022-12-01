using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Coroutine.Prefab
{
    public class DefaultFurniture : MonoBehaviour
    {
        private List<GameObject> Children;

        // Start is called before the first frame update
        private void Start()
        {


        }


        // Update is called once per frame
        void Update()
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }
}