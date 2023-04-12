using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using HtmlAgilityPack;

class Program
{
    static void Main(string[] args)
    {
        // Load the webpage HTML content
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load("https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/");

        // Extract the h1 tag inner text to use in the output file name
        HtmlNode h1node = doc.DocumentNode.SelectSingleNode("//h1");
        string h1text = h1node.InnerText.Trim();
        string outputFileName = $"{h1text}Output.csv";

        // Select all the p tags in the HTML document
        HtmlNodeCollection pnodes = doc.DocumentNode.SelectNodes("//p");

        // Extract the inner text and href of each a tag within each p tag
        List<(string text, string href)> linkTexts = new List<(string text, string href)>();
        foreach (HtmlNode pNode in pnodes)
        {
            foreach (HtmlNode aNode in pNode.Descendants("a"))
            {
                string href = aNode.GetAttributeValue("href", "");
                string text = aNode.InnerText.Trim();
                linkTexts.Add((text, href));
            }
        }

        // Write the list of tuples to a CSV file
        using (StreamWriter writer = new StreamWriter(outputFileName))
        {
            foreach ((string text, string href) in linkTexts)
            {
                writer.WriteLine($"{text},{href}");
            }
        }

        Console.WriteLine($"CSV file '{outputFileName}' written successfully!");
    }
}
