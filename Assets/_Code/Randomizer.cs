using System;
using System.Collections.Generic;

public static class Randomizer
{
    private static readonly Random m_random = new Random();

    public static int Next(int max)
    {
        return m_random.Next(max);
    } 

    public static T PickRandom<T>(this T[] list)
    {
        var index = m_random.Next(list.Length);
        return list[index];
        
    }
     public static T PickRandom<T>(this List<T> list)
    {
        var index = m_random.Next(list.Count);
        return list[index];
    }
    
    public static T PickAndRemoveRandom<T>(this List<T> list)
    {
        var index = m_random.Next(list.Count);
        var value = list[index];
        list.RemoveAt(index);
        return value;
    }
    
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count; i > 1; --i)
        {
            var index = m_random.Next(i);
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