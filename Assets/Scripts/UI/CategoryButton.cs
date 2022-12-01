using Script;
using Script.Coroutine;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CategoryButton : MonoBehaviour
    {
        private Button _button;

        private Furniture _furniture;

        // Start is called before the first frame update
        void Start()
        {
            _furniture = FurnitureManager.Instance.GetFurnitureBySlug(name);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(click);
            ChangeButtonColorNormal(Color.white);
        }


        void click()
        {
            print("button pressed");
            if (_furniture.downloadStatus == Status.TOBEDOWNLOADNED)
            {
                ChangeButtonColorNormal(Color.blue);
                _furniture.downloadStatus = Status.DOWNLOADING;
            }

            switch (_furniture.downloadStatus)
            {
                case Status.DOWNLOADED:
                    IsReady();
                    break;
                case Status.TOBEDOWNLOADNED:
                    _furniture.downloadStatus = Status.DOWNLOADING;
                    break;
                default:
                    break;
            }
        }


        private void IsReady()
        {
            if (AssetManager.Instance.IsAssetGathered(_furniture.getSlugName()))
            {
                Instantiate(AssetManager.Instance.InstantiateByRequestReference(_furniture.getSlugName()));
                _furniture.downloadStatus = Status.DOWNLOADED;
            }
        }

        private void ChangeButtonColorNormal(Color c)
        {
            ColorBlock colorBlock = _button.colors;
            colorBlock.normalColor = c;
            _button.colors = colorBlock;
        }

        // Update is called once per frame
        void Update()
        {
            if (_furniture.downloadStatus == Status.DOWNLOADING)
            {
                if (!AssetManager.Instance.IsAssetBeingGathered(_furniture.getSlugName()))
                {
                    if (AssetManager.Instance.IsAssetGathered(_furniture.getSlugName()))
                    {
                        ChangeButtonColorNormal(Color.green);
                        IsReady();
                    }
                    else
                    {
                        AssetManager.Instance.GetAsset(new OBJRequest(_furniture.getUrl(), _furniture.getSlugName()));
                    }
                }
            }
        }
    }
}