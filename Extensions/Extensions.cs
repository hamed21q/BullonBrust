using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public static class Extensions
{
    /*public static Entity ToEntity(this Collider2D c)
    {
        return c.ToEntity<Entity>();
    }
    public static T ToEntity<T>(this Collider2D c) where T : Entity
    {
        if (c.gameObject.GetComponent<T>() != null)
            return c.gameObject.GetComponent<T>();
        else if (c.gameObject.GetComponentInChildren<T>() != null)
            return c.gameObject.GetComponentInChildren<T>();
        else if (c.gameObject.GetComponentInParent<T>() != null)
            return c.gameObject.GetComponentInParent<T>();
        return null;
    }*/

    public static byte[] ToBytes(this string bytes)
    {
        return Convert.FromBase64String(bytes);
    }
    public static string ToBytes(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    public static Vector3 ApplyGrid(this Vector3 vector, float grid)
    {
        return new Vector3(
            Mathf.Round(vector.x / grid) * grid,
            Mathf.Round(vector.y / grid) * grid,
            Mathf.Round(vector.z / grid) * grid
        );
    }

    public static Vector3 NormalizeEulerAngles(this Vector3 eulerAngles)
    {
        return new Vector3(
            eulerAngles.x > 180 ? 360 - eulerAngles.x : eulerAngles.x,
            eulerAngles.y > 180 ? 180 - eulerAngles.y : eulerAngles.y,
            eulerAngles.z > 180 ? 360 - eulerAngles.z : eulerAngles.z);
    }

    public static string[] ToStrings(this IEnumerable enumerable)
    {
        List<string> re = new List<string>();
        foreach (var item in enumerable)
            re.Add(item.ToString());
        return re.ToArray();
    }

    public static Texture2D ToTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public static T DeepCopy<T>(this T other)
    {
        if (other == null) return default(T);
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, other);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }

    public static bool Validate(this GameObject obj, Filter filter)
    {
        return Validate(obj, filter.layerMask, filter.tags);
    }
    public static bool Validate(this GameObject obj, int layer, string[] tags)
    {
        return layer == (layer | (1 << obj.layer)) || tags.Contains(obj.tag);
    }
    public static bool Validate(this GameObject obj, string[] tags)
    {
        return tags.Contains(obj.tag);
    }
    public static bool Validate(this GameObject obj, int layer)
    {
        return layer == (layer | (1 << obj.layer));
    }

    public static float Average(this float[] numbers)
    {
        float re = 0;
        foreach (var num in numbers)
            re += num;
        return re / numbers.Length;
    }

    public static float Map(this float value, float min, float max, float newMin = 0, float newMax = 1)
    {
        return (value - min) / (max - min) * (newMax - newMin) + newMin;
    }

    public static float MoveValue(this float value, float target, float speed, float smooth)
    {
        return Mathf.MoveTowards(value, Mathf.Lerp(value, target, smooth * Time.timeScale), speed * Time.timeScale);
    }
    public static Vector3 MoveValue(this Vector3 value, Vector3 target, float speed, float smooth)
    {
        return Vector3.MoveTowards(value, Vector3.Lerp(value, target, smooth * Time.timeScale), speed * Time.timeScale);
    }
    public static Quaternion MoveValue(this Quaternion value, Quaternion target, float speed, float smooth)
    {
        return Quaternion.RotateTowards(value, Quaternion.Lerp(value, target, smooth * Time.timeScale), speed * Time.timeScale);
    }

    public static T Random<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
    public static T Random<T>(this List<T> array)
    {
        return array[UnityEngine.Random.Range(0, array.Count)];
    }

    public static void ApplyDirectionDrag(this Rigidbody2D rigidbody, Vector2 direction, float velocityMultiper)
    {
        var delta = rigidbody.velocity * (1 - velocityMultiper);
        delta.x *= Mathf.Abs(direction.x);
        delta.y *= Mathf.Abs(direction.y);
        rigidbody.velocity -= delta;
    }

    public static Vector2 Rotate(this Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public static float Round(this float number, float round = 10)
    {
        return Mathf.Round(number * round) / round;
    }

    public static string f(this string str, params object[] args)
    {
        return string.Format(str, args);
    }
}

[System.Serializable]
public class Filter
{
    public LayerMask layerMask;
    public string[] tags = new string[] { "Player" };
}