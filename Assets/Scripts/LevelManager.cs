using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Retro_GO;
    public GameObject[] Future_GO;
    public static bool isRetro = true;

    private void Start()
    {
        isRetro = true;
    }
    public void ShiftEnvironment(Component component, object data)
    {
        if (data is Vector3)
        { 
            if (isRetro)
            {
                isRetro = false;
                // play SFX
                // vfx
                // post proc transition

                // replace assets
                for (int i = 0; i < Retro_GO.Length; i++)
                {
                    Retro_GO[i].SetActive(false);
                }
                for (int i = 0; i < Future_GO.Length; i++)
                {
                    Future_GO[i].SetActive(true);
                }
            }
            else
            {
                isRetro = true; 
                for (int i = 0; i < Retro_GO.Length; i++)
                {
                    Retro_GO[i].SetActive(true);
                }
                for (int i = 0; i < Future_GO.Length; i++)
                {
                    Future_GO[i].SetActive(false);
                }
            }
        }
    }
}
