using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideable : MonoBehaviour
{
    public GameEventListener LevelFailed_Event;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Nana")
        {
            LevelFailed_Event.OnEventRaised(this, true);
        }
    }
}
