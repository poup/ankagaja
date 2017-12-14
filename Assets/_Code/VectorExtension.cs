﻿using UnityEngine;

    public static class VectorExtension
    {
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
    }