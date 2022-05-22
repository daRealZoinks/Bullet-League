using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<Tab> tabs;
    public List<GameObject> objectsToSwap;


    public Tab selectedTab;

    public void Subscribe(Tab button)
    {
        if (tabs == null)
        {
            tabs = new List<Tab>();
        }

        tabs.Add(button);
    }

    public void OnTabSelected(Tab button)
    {
        selectedTab = button;
        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(i == index);
        }
    }
}