using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI ammoCount_txt;
    public TextMeshProUGUI ammoCount2_txt;
    public GameObject LevelCompleted_Panel;

    private void Start()
    {
        LevelCompleted_Panel.SetActive(false);
    }
    public void LevelCompleted(Component component, object data)
    {
        Debug.Log(component);
        LevelCompleted_Panel.SetActive(true);
    }
    public void Ammo(Component sender, object data)
    { 
        if (data is int)
        {
            int count = (int)data;
            ammoCount_txt.text = count.ToString();
            ammoCount2_txt.text = count.ToString();
        }
    }
}
