using UnityEngine;

public class Tab : MonoBehaviour
{
    public TabGroup tabGroup;

    public void Select()
    {
        tabGroup.OnTabSelected(this);
    }

    void Start()
    {
        tabGroup.Subscribe(this);
    }
}