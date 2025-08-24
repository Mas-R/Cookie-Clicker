using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSfxButton : MonoBehaviour
{
    public void PlayClickSfx()
    {
        AudioManager.Instance?.PlayClick();
    }
}
