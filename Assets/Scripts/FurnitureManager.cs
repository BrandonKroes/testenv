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

        DummyContent();
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
            button.transform.SetParent(categoryMenu.transform, false);
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
                var button = Instantiate(submenu_prefab);
                button.name = furniture.getSlugName();
                button.transform.SetParent(subMenu.transform, false);
                button.GetComponentInChildren<TextMeshProUGUI>().text = furniture.getSlugName();
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 3f);
                button.SetActive(true);
                button.AddComponent<CategoryButton>();
            }
        }
    }

    public void DeleteFurnitureItems(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }


    //Fill data for testing
    public void DummyContent()
    {
        Status s = Status.TOBEDOWNLOADNED;

        furnitures = new Dictionary<string, List<Furniture>>()
        {
            {
                "Beds", new List<Furniture>()
                {
                    new Furniture("Hopen", "https://brandonkroes.com/MMS/drawer/IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Sorum Queen Bed",
                        "https://brandonkroes.com/MMS/beds/IKEA-Sorum_Queen_Bed_Frame-3D.obj", s),
                    new Furniture("Hemnes",
                        "https://brandonkroes.com/MMS/beds/https://brandonkroes.com/MMS/beds/Hemnes%20Bed.obj", s),
                    new Furniture("Aspelund", "https://brandonkroes.com/MMS/beds/IKEA-Aspelund_Double_Bed-3D.obj", s),
                    new Furniture("Grankulla",
                        "https://brandonkroes.com/MMS/beds/IKEA-Grankulla_Futon_Single_Bed-3D.obj", s),
                }
            } /* ,
            {
                "Drawers", new List<Furniture>()
                {
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s)
                }
            } ,
            {
                "Wardrobe", new List<Furniture>()
                {
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s)
                }
            },
            {
                "Tables", new List<Furniture>()
                {
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s),
                    new Furniture("Alex Small", "IKEA-Alex_drawer_white-3D.obj", s)
                }
            }, */
        };
    }

    public Furniture GetFurnitureBySlug(string slug)
    {
        foreach (var furniture_category in this.furnitures)
        {
            foreach (var furniture in furniture_category.Value)
            {
                if (furniture.getSlugName() == slug)
                {
                    return furniture;
                }
            }
        }

        return new Furniture("", "", Status.TOBEDOWNLOADNED);
    }
}