using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelCompletionCheck : MonoBehaviour
{ 
    public GameEvent onLevelComplete;
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Nana")
        {
             onLevelComplete.Raise(this,true);
        }
    }
}
