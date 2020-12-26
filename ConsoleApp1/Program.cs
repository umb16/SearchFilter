using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        /* public string[] Split()
         {
             string s = "TestXtFor XX 100";
         }*/

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

            for (int i = 0; i < 10000; i++)
            {
                sb.Append(s);
            }
            s = sb.ToString();
            string[] strings = s.Split(" ");
            Console.WriteLine(strings.Length + " " + sw.ElapsedMilliseconds);

            sw.Restart();
            var serchData = new SearchFilterLib.SearchData(strings);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            while (true)
            {
                string findText = Console.ReadLine().ToLower();
                //Недоделанные варианты посика
                /*List<string>[] variants = new List<string>[findText.Length];
                for (int i = 0; i < findText.Length; i++)
                {
                    string cha = findText[i].ToString();
                    variants[i] = new List<string>();
                    if (i > 0)
                    {
                        foreach (var variant in variants[i-1])
                        {
                            variants[i].Add(variant + cha);
                        }
                    }
                    variants[i].Add(cha);
                }*/
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