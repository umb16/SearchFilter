using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string s = " ItemForm TestRun CarSpawner " +
                " System.Data.Services.ExpandSegmentCollection" +
                " Activities.OperationParameterInfoCollection" +
                " Activities.WorkflowRoleCollection" +
                " ComponentModel.ActivityCollection" +
                " ComponentModel.Design.ActivityDesignerGlyphCollection" +
                " ActivityTrackingLocationCollection" +
                " ActivityTrackPointCollection" +
                " ExtractCollection" +
                " TrackingAnnotationCollection" +
                " TrackingConditionCollection" +
                " UserTrackingLocationCollection" +
                " UserTrackPointCollection" +
                " WorkflowTrackPointCollection";

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var sb = new StringBuilder(s);

            for (int i = 0; i < 1000; i++)
            {
                sb.Append(s);
            }
            s = sb.ToString();
            string[] strings = s.Split(" ");
            sw.Stop();
            Console.WriteLine(strings.Length + " " + sw.ElapsedMilliseconds);

            sw.Restart();
            var serchData = new SearchFilterLib.SearchData(strings);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            while (true)
            {
                string findText = Console.ReadLine();
                sw.Restart();
                string[] serchResult = serchData.Search(findText);
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
                foreach (var item in serchResult)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}