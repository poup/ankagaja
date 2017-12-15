using UnityEngine;
using System.Collections.Generic;

public static class Randomizer
{
    public static int Next(int max)
    {
        return Random.Range(0, max);
        
    } 
    
    public static int Next(int min, int max)
    {
        return Random.Range(min, max);
    } 
    
    public static float Next(float min, float max)
    {
        return Random.Range(min, max);
    } 

    public static T PickRandom<T>(this T[] list)
    {
        var index = Next(list.Length);
        return list[index];
        
    }
     public static T PickRandom<T>(this List<T> list)
    {
        var index = Next(list.Count);
        return list[index];
    }
    
    public static T PickAndRemoveRandom<T>(this List<T> list)
    {
        var index = Next(list.Count);
        var value = list[index];
        list.RemoveAt(index);
        return value;
    }
    
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count; i > 1; --i)
        {
            var index = Next(i);
            Swap(list, i - 1, index);
        }
    }


    private static void Swap<T>(List<T> list, int i1, int i2)
    {
        var tmp = list[i1];
        list[i1] = list[i2];
        list[i2] = tmp;
    }

}