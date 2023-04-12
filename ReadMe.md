#####################################################################################################################
an explination of the "extrract" console app code
#####################################################################################################################
***The purpose of this code is to extract the inner text and href of each a tag within each p tag from the webpage at the given URL, and save the extracted data to a CSV file.***

***Let's go through the code step-by-step:

Import necessary namespaces:***

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using HtmlAgilityPack;

***This code imports the required namespaces to work with HtmlAgilityPack, System.IO and System.Collections.Generic classes.***
#####################################################################################################################
***Create the HtmlWeb object to load the webpage HTML content:***

HtmlWeb web = new HtmlWeb();
HtmlDocument doc = web.Load("https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/");
***This code creates an HtmlWeb object and loads the HTML content of the webpage at the given URL into an HtmlDocument object.***
#####################################################################################################################
***Extract the h1 tag inner text to use in the output file name:***

HtmlNode h1node = doc.DocumentNode.SelectSingleNode("//h1");
string h1text = h1node.InnerText.Trim();
string outputFileName = $"{h1text}Output.csv";
***This code selects the first h1 node from the HTML document using the SelectSingleNode method and extracts its inner text using the InnerText property. It then trims any whitespace from the string and appends "Output.csv" to it to form the output file name.***
#####################################################################################################################
***Select all the p tags in the HTML document:***

HtmlNodeCollection pnodes = doc.DocumentNode.SelectNodes("//p");
***This code selects all p nodes from the HTML document using the SelectNodes method and stores them in an HtmlNodeCollection object.***
#####################################################################################################################
***Extract the inner text and href of each a tag within each p tag:***

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
***This code creates an empty list of tuples, where each tuple contains two strings: the inner text and href of an a tag. It then iterates through each p node in the pnodes collection and extracts the inner text and href of each a tag within it using the Descendants method to find all descendants of the pNode with the tag name a. The GetAttributeValue method is used to extract the value of the href attribute, while the InnerText property is used to extract the inner text of the a tag. The extracted text and href values are then added to the linkTexts list as a tuple.***
#####################################################################################################################
***Write the list of tuples to a CSV file:***

using (StreamWriter writer = new StreamWriter(outputFileName))
{
    foreach ((string text, string href) in linkTexts)
    {
        writer.WriteLine($"{text},{href}");
    }
}
***This code opens a StreamWriter to the output file specified in outputFileName and writes each tuple in the linkTexts list to the file as a comma-separated pair of values (i.e. "inner text,href").***
#####################################################################################################################
**Output a success message:**

Console.WriteLine($"CSV file '{outputFileName}' written successfully!");
    }
}