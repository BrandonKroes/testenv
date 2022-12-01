using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;
using Mirror;
using Script.Coroutine;
using Script.Generics;
using TMPro;
using UI;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class FurnitureManager : SingletonMonoBehaviour<FurnitureManager>
{
// TODO: A gigantic array of all the furninture items

    private string activeCategory;

    //Button 


    //SubMeny reference
    public GameObject categoryMenu;
    public GameObject subMenu;
    public GameObject category_prefab;
    public GameObject submenu_prefab;
    private Dictionary<string, List<Furniture>> furnitures;


    // Start is called before the first frame update
    void Start()
    {
        this.furnitures = new Dictionary<string, List<Furniture>>();

        Fill();
        FurnitureCategories();
        FurnitureItems();
        //showAllSubMenuItems();
        //CreateSubMenus();
        //CreateMenu();
        //showAllSubMenuItems();
    }

    public void FurnitureCategories()
    {
        foreach (var furniture_category in this.furnitures)
        {
            var button = Instantiate(category_prefab);
            button.name = furniture_category.Key;
            button.transform.SetParent(categoryMenu.transform,false);
            button.GetComponentInChildren<TextMeshProUGUI>().text = furniture_category.Key;
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 3f);
            button.SetActive(true);
        }
    }

    public void FurnitureItems()
    {
        foreach (var furniture_category in this.furnitures)
        {
            foreach (var furniture in furniture_category.Value)
            {
                print(furniture.getSlugName());
                var button = Instantiate(submenu_prefab);
                button.name = furniture.getSlugName();
                button.transform.SetParent(subMenu.transform,false);
                button.GetComponentInChildren<TextMeshProUGUI>().text = furniture.getSlugName();
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 3f);
                button.SetActive(true);
            }
        }
    }


    public void subMenuChoices()
    {
        RemoveSubMenuChoices();
        var result = new List<Furniture>();
        this.furnitures.TryGetValue(this.activeCategory, out result);
        foreach (var furniture in result)
        {
            var button = Instantiate(submenu_prefab);
            button.GetComponentInChildren<TextMeshProUGUI>().text = furniture.getSlugName();
            button.SetActive(true);
        }
    }

    public void RemoveSubMenuChoices()
    {
        Transform[] ts = categoryMenu.GetComponentsInChildren<Transform>();

        foreach (var result in ts)
        {
            if (result.parent == categoryMenu.transform && result.gameObject.name != this.activeCategory)
            {
                result.gameObject.SetActive(false);
            }
        }
    }

    public void HideMenuItemsExcept(string activeCategory)
    {
        if (this.furnitures.Keys.Contains(activeCategory))
        {
            if (activeCategory == this.activeCategory)
            {
                showAllSubMenuItems();
            }

            this.activeCategory = activeCategory;
            subMenuChoices();
        }
    }

    private void showAllSubMenuItems()
    {
        Transform[] ts = categoryMenu.GetComponentsInChildren<Transform>(true);

        foreach (var result in ts)
        {
            if (result.parent == categoryMenu.transform)
            {
                result.gameObject.SetActive(true);
            }
        }

        hideAllMenuFurniture();
    }

    private void hideAllMenuFurniture()
    {
        Transform[] ts = subMenu.GetComponentsInChildren<Transform>();

        foreach (var result in ts)
        {
            if (result.parent == subMenu.transform)
            {
                Destroy(result.gameObject);
            }
        }
    }

    public void CreateSubMenus()
    {
        foreach (var furniture_category in this.furnitures)
        {
            var button = Instantiate(category_prefab);


            //button.GetComponent<Button>().onClick.AddListener(method);

            //InputHelpers.Button b = button.GetComponent<InputHelpers.Button>();
            //b.onClick.AddListener(delegate
            //{
            //    FurnitureManager.Instance.HideMenuItemsExcept(furniture_category.Key);
            //}); 

            button.name = furniture_category.Key; //GetComponentInChildren<TextMeshPro>().text = furniture_category.Key;
            button.SetActive(true);
        }
    }


    public void TaskOnClick()
    {
        print("TaskOnClick");
    }


    /*
    void GetFurniture(Furniture furniture)
    {

        if (furniture.rQType == ) { }
        
            
            var x = new OBJRequest("https://brandonkroes.com/MMS/drawer/IKEA-Alex_drawer_white-3D.obj",,
                "IKEA-Alex_drawer_white-3DMTL");
        x.ExecuteRequest(_monoBehaviour);

        AssetManager.Instance.GetAsset()

        OBJRequestCoroutine.GetRequest(
            new OBJRequest(
                item.getUrl(),
                item.getSlugName()
                ))
    }

    */
    // Update is called once per frame
    void Update()
    {
    }

    //Fill data for testing
    public void Fill()
    {
        Status s = Status.TOBEDOWNLOADNED;

        Furniture bed1 = new Furniture("IKEA-Alex_drawer_white-3D", "https", s);
        Furniture bed2 = new Furniture("Rashid_bed", "https", s);

        Furniture drawer1 = new Furniture("Setki_Drawer", "https", s);
        Furniture drawer2 = new Furniture("Setki_Drawer", "https", s);

        Furniture door1 = new Furniture("Brandon_door", "https", s);
        Furniture door2 = new Furniture("Brandon_door", "https", s);

        List<Furniture> beds = new List<Furniture>()
        {
            bed1,
            bed2
        };

        List<Furniture> drawers = new List<Furniture>()
        {
            drawer1,
            drawer2
        };

        List<Furniture> doors = new List<Furniture>()
        {
            door1,
            door2
        };

        furnitures = new Dictionary<string, List<Furniture>>()
        {
            { "Bed", beds },
            { "Drawers", drawers },
            { "Doors", doors }
        };
    }
}