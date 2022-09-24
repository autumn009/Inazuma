// read resource

using System.Reflection;

const string outputDir = @"c:\delme\testdata";

var words = getWords();
Console.WriteLine(words.Length);


string[] getWords()
{
    var words = Assembly.GetExecutingAssembly().GetManifestResourceStream("CreateTestTextData.words.txt");
    if (words == null) throw new Exception("words not found");

    var wordList = new List<string>();
    var reader = new StreamReader((Stream)words);
    for (; ; )
    {
        var s = reader.ReadLine();
        if (s == null) break;
        wordList.Add(s);
    }
    return wordList.ToArray();
}





