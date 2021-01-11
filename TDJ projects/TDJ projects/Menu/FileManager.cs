﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace TDJ_Project.Menu
{
    public class FileManager
    {
        enum LoadType { Attributes, Contents };

        LoadType loadType;

        List<string> tempAttributes, tempContents;

        bool identifierFound = false;

        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains("Load="))
                    {
                        tempAttributes = new List<string>();
                        line = line.Remove(0, line.IndexOf("=") + 1);
                        loadType = LoadType.Attributes;
                    }
                    else
                    {
                        loadType = LoadType.Contents;
                    }

                    tempContents = new List<string>();

                    string[] lineArray = line.Split(']');
                    foreach (string li in lineArray)
                    {
                        string newLine = li.Trim('[', ' ', ']');

                        if (newLine != String.Empty)
                        {
                            if (loadType == LoadType.Contents)
                                tempContents.Add(newLine);
                            else
                                tempAttributes.Add(newLine);
                        }
                    }

                    if (loadType == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempAttributes);
                    }
                }
            }
        }

        
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, string identifier)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if(line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else if(line.Contains("Load=") && line.Contains(identifier))
                    {
                        identifierFound = true;
                        continue;
                    }

                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            tempAttributes = new List<string>();
                            line.Remove(0, line.IndexOf("=") + 1);
                            loadType = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            loadType = LoadType.Contents;
                        }

                        string[] lineArray = line.Split(']');
                        foreach (string li in lineArray)
                        {
                            string newLine = li.Trim('[', ' ', ']');

                            if (newLine != String.Empty)
                            {
                                if (loadType == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    tempAttributes.Add(newLine);
                            }
                        }

                        if (loadType == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempAttributes);
                        }
                    }
                }
            }
        }
        
    }  
}
