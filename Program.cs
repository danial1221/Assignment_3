using System;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        // Create XML File
        CreateXmlFile();

        // Read and Display XML File
        ReadXmlFile();

        Console.ReadKey();
    }

    static void CreateXmlFile()
    {
        // Configure XML Writer settings
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t",
            NewLineOnAttributes = false
        };

        using (XmlWriter writer = XmlWriter.Create("GPS.xml", settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("GPS_Log");

            // Write Position element
            writer.WriteStartElement("Position");
            writer.WriteAttributeString("DateTime", DateTime.Now.ToString());
            writer.WriteElementString("x", "65.8973342");
            writer.WriteElementString("y", "72.3452346");

            // Write SatteliteInfo element
            writer.WriteStartElement("SatteliteInfo");
            writer.WriteElementString("Speed", "40");
            writer.WriteElementString("NoSatt", "7");
            writer.WriteEndElement(); // End SatteliteInfo

            writer.WriteEndElement(); // End Position

            // Write Image element
            writer.WriteStartElement("Image");
            writer.WriteAttributeString("Resolution", "1024x800");
            writer.WriteElementString("Path", @"\images\1.jpg");
            writer.WriteEndElement(); // End Image

            writer.WriteEndElement(); // End GPS_Log
            writer.WriteEndDocument();
        }

        Console.WriteLine("XML file 'GPS.xml' created successfully.");
    }

    static void ReadXmlFile()
    {
        Console.WriteLine("\nReading and displaying contents of 'GPS.xml':\n");

        using (XmlReader reader = XmlReader.Create("GPS.xml"))
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        Console.Write($"<{reader.Name}");
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                Console.Write($" {reader.Name}=\"{reader.Value}\"");
                            }
                        }
                        Console.WriteLine(">");
                        break;

                    case XmlNodeType.Text:
                        Console.WriteLine(reader.Value);
                        break;

                    case XmlNodeType.EndElement:
                        Console.WriteLine($"</{reader.Name}>");
                        break;
                }
            }
        }
    }
}

