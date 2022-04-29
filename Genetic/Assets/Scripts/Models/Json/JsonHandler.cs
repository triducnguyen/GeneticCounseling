using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles JSON Serialization and Deserialization.</summary>
public static class JsonHandler
{
    /// <summary>Serializes the specified object.</summary>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>Serialized object (json text).</returns>
    public static string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    /// <summary>Deserializes the specified json.</summary>
    /// <typeparam name="T">Type to deserialize to.</typeparam>
    /// <param name="json">The json to deserialize.</param>
    /// <returns>Deserialized object (c# object).</returns>
    public static T Deserialize<T>(string json) where T : class
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}