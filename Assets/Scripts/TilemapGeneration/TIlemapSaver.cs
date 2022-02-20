using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Linq;
using BSP;


public class TIlemapSaver : MonoBehaviour
{
    [SerializeField] TilemapGenerator generator;

    public void Save()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "map.xml");

        XDocument doc = new XDocument();
        XElement rootElement = new XElement("map");  // Создаём корневой элемент

        foreach (var item in generator.map)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                XElement leafElement = new XElement("Leaf");
                XAttribute xPosAttribute = new XAttribute("X", item.Value[i].position.x);
                XAttribute yPosAttribute = new XAttribute("Y", item.Value[i].position.y);
                XAttribute widthAttribut = new XAttribute("width", item.Value[i].width);
                XAttribute heightAttribute = new XAttribute("height", item.Value[i].height);
                XAttribute typeAttribute = new XAttribute("type", item.Key);

                leafElement.Add(xPosAttribute);
                leafElement.Add(yPosAttribute);
                leafElement.Add(widthAttribut);
                leafElement.Add(heightAttribute);
                leafElement.Add(typeAttribute);

                rootElement.Add(leafElement);
            }
        }

        XElement seedElement = new XElement("seed", generator.seed);
        rootElement.Add(seedElement);

        doc.Add(rootElement);
        doc.Save(path);
    }

    public void Load()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "map.xml");
        XDocument doc = XDocument.Load(path);

        generator.map.Clear();

        foreach (var item in doc.Element("map").Elements("Leaf"))
        {
            XAttribute xPosAttribute = item.Attribute("X");
            XAttribute yPosAttribute = item.Attribute("Y");
            XAttribute widthAttribute = item.Attribute("width");
            XAttribute heightAttribute = item.Attribute("height");
            XAttribute typeAttribute = item.Attribute("type");

            int x = int.Parse(xPosAttribute.Value);
            int y = int.Parse(yPosAttribute.Value);
            Vector2 position = new Vector2(x, y);

            int width = int.Parse(widthAttribute.Value);
            int height = int.Parse(heightAttribute.Value);

            int type = int.Parse(typeAttribute.Value);

            Leaf leaf = new Leaf(position, width, height);

            if (generator.map.ContainsKey(type))
                generator.map[type].Add(leaf);
            else
                generator.map.Add(type, new List<Leaf> { leaf });
        }

        generator.seed = int.Parse(doc.Element("map").Element("seed").Value);

        generator.LoadMap();
    }
}
