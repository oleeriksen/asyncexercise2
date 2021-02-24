using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Async Exercise - type 'q' to quit");

            HttpClient _client = new HttpClient();

            Console.Write(">");
            char input = Console.ReadKey().KeyChar;
            while (input != 'q')
            {
                DateTime start = DateTime.Now;

                Task<HttpResponseMessage> response = _client.GetAsync("https://localhost:5003/numbers/12");

                response.ContinueWith((r) =>
                {
                    string result = r.Result.Content.ReadAsStringAsync().Result;

                    WriteToFile("Result: " + result + " between " + start.ToLongTimeString() + " - " + DateTime.Now.ToLongTimeString());
                });
                Console.Write(">");
                input = Console.ReadKey().KeyChar;
            }
        }

        private static Object mLock = new object();

        private static void WriteToFile(string s)
        {
            lock (mLock)
            {
                string path = @"fromclient.txt";

                StreamWriter sw = File.AppendText(path);

                sw.WriteLine(s);

                sw.Flush();
            }
        }
    }
}
