using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public static  class Kit
{ 
    public static Color ChooseColor(int level)
    {
        Color color;
        switch (level)
        {
            case 1:
                color = new Color(32f / 255f, 80f / 255f, 0);
                break;
            case 2:
                color = new Color(125f / 255f, 7f / 255f, 143f / 255f);
                break;
            case 3:
                color = new Color(143f / 255f, 12f / 255f, 7f / 255f);
                break;
            default: 
                color = Color.white;
                break;
        }
        return color;
    }
}
