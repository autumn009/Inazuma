// read resource

using System.Reflection;
using System.Text;

const string outputDir = @"c:\delme\testdata";
Directory.CreateDirectory(outputDir);
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

string[] words = getWords("CreateTestTextData.words.txt");
Console.WriteLine($"Detect {words.Length} words in resource");
string[] lineData = getWords("CreateTestTextData.lines.txt");
Console.WriteLine($"Detect {words.Length} lines in resource");

Encoding[] encodings = { Encoding.ASCII, Encoding.UTF8, Encoding.Unicode, Encoding.GetEncoding("Shift_JIS"), Encoding.GetEncoding("ISO-2022-JP"), Encoding.GetEncoding("EUC-JP") };
int[] lines = { 10, 100, 1000, 10000, 100000, 1000000 };

foreach (var e in encodings)
{
    foreach (var l in lines)
    {
        CreateOneData(l, e, e == Encoding.ASCII);
    }
}

Console.WriteLine("All Done");

void CreateOneData(int lines, Encoding encoding, bool forceLineDataOnly = false)
{
    var fullpath = Path.Combine(outputDir, $"Test {lines} lines by {encoding.EncodingName}.txt");
    Console.WriteLine($"Writing: {fullpath}");
    using TextWriter writer = new StreamWriter(fullpath, false, encoding);

    writer.WriteLine("The quick brown fox jumps over the lazy dog");
    for (int i = 1; i < lines; i++)
    {
        if (forceLineDataOnly || Random.Shared.Next(10) < 1)
        {
            writer.WriteLine(lineData[Random.Shared.Next(lineData.Length)]);
        }
        else
        {
            int lineLength = Random.Shared.Next(1024);
            StringBuilder sb = new StringBuilder();
            for (; ; )
            {
                if (sb.Length > lineLength) break;
                sb.Append(words[Random.Shared.Next(words.Length)]);
            }
            writer.WriteLine(sb.ToString());
        }
    }
}

string[] getWords(string resname)
{
    var words = Assembly.GetExecutingAssembly().GetManifestResourceStream(resname);
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





