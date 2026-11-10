using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class materialList
{
    static List<Material> materials = new List<Material>();

    public static List<Material> getMaterials()
    {
        return materials;
    }
}