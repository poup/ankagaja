using System;
using System.Collections.Generic;

public static class Randomizer
{
    private static readonly Random m_random = new Random();

    public static int Next(int max)
    {
        return m_random.Next(max);
    } 

    public static T PickRandom<T>(this List<T> list)
    {
        var index = m_random.Next(list.Count);
        return list[index];
    }

}