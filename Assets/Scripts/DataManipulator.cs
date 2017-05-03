using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class DataManipulator
{

    private static DataManipulator me = null;

    private DataManipulator()
    {

    }

    public static DataManipulator GetInstance()
    {
        if (me == null)
        {
            me = new DataManipulator();
        }
        return me;
    }

    public void SerializeObject<T>(T serializableObject, string fileName)
    {
        if (serializableObject == null) { return; }
        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
                stream.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public T DeSerializeObject<T>(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) { return default(T); }

        T objectOut = default(T);

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;

            using (StringReader read = new StringReader(xmlString))
            {
                Type outType = typeof(T);

                XmlSerializer serializer = new XmlSerializer(outType);
                using (XmlReader reader = new XmlTextReader(read))
                {
                    objectOut = (T)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return default(T);
        }

        return objectOut;
    }

    public List<string> GetLevel(int level)
    {
        return ReadFile("Assets/Levels/" + level + ".lvl");
    }

    public List<String> ReadFile(string fileName)
    {
        List<string> levelMap = new List<string>();
        string line;
        System.IO.StreamReader file =
           new System.IO.StreamReader(fileName);
        while ((line = file.ReadLine()) != null)
        {
            levelMap.Add(line);
        }
        file.Close();
        return levelMap;
    }

    public void DeleteAllSaves()
    {
        if (File.Exists(PickLevelModel.DATA_FILENAME))
        {
            File.Delete(PickLevelModel.DATA_FILENAME);
        }
    }

}
