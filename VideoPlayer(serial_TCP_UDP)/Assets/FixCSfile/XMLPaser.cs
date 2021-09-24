using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

static public class XMLPaser
{

    static public T Load<T>(string FileLocation, T data) where T : class
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream XML_F = new FileStream(FileLocation, FileMode.Open);
            return data = (T)serializer.Deserialize(XML_F);
       
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
 
        }

        
        return default(T);
    }

    static public void Save<T>(string FileLocation, T data) where T : class
    {
        try
        {
            FileStream XML_F = new FileStream(FileLocation, FileMode.OpenOrCreate);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(XML_F, data);
            File.WriteAllText(FileLocation, XML_F.ToString());

        }
        catch (Exception e)
        {
            Debug.LogWarning( e);

        }

    }
}
