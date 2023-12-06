using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace AdventOfCode2023
{
    public partial class Day2Form : Form
    {
        OpenFileDialog oFile = new OpenFileDialog();

        public Day2Form()
        {
            InitializeComponent();
        }

        private void Day2_Load(object sender, EventArgs e)
        {

        }

        private void ClearText()
        {
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> text = new List<string>();
            ClearText();

            if (oFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(oFile.FileName);
                    while (!sr.EndOfStream)
                    {
                        text.Add(sr.ReadLine());
                    }
                    foreach (string s in text)
                    {
                        textBox1.AppendText(s + "\n");
                    }

                    Solution1(text);

                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Solution1(List<string> text)
        {
            /*
            *Determine which games would have been possible if the bag had been loaded 
            *with only 12 red cubes, 13 green cubes, and 14 blue cubes. What is the sum 
            *of the IDs of those games?
            */

            string[] line;
            List<Regex> regList = new List<Regex>();
            List<int> gameNumbers = new List<int>();
            int red = 0, green = 0, blue = 0, index = -1, colorValue = 0, solution = 0;
            string colorValueString = "";
            Regex numbers = new Regex("1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20");

            regList.Add(new Regex("red"));
            regList.Add(new Regex("green"));
            regList.Add(new Regex("blue"));
            regList.Add(new Regex("Game"));
            int match = 0;


            foreach (string s in text) //This loops through every string in the .txt
            {
                line = s.Split(',', ';', ':');
                foreach (string s2 in line) Console.WriteLine(s2);

                foreach (string s3 in line) //This loops through each part of delimited string
                {
                    foreach (Regex r in regList)
                    {
                        if (r.IsMatch(s3)) index = regList.IndexOf(r);
                    }
                    colorValue = 0;
                    colorValue = numbers.Match(s3).Index + 1;
                    
                    Console.WriteLine(colorValueString);
                    //colorValue = int.Parse(colorValueString.Trim());

                    switch (index)
                    {
                        case 0:
                            red += colorValue;
                            break;
                        case 1:
                            green += colorValue;
                            break;
                        case 2:
                            blue += colorValue;
                            break;
                        case 3:
                            if (red <= 12 && green <= 13 && blue <= 14) gameNumbers.Add(s.IndexOf(s3) + 1);
                            colorValueString = "";
                            red = 0;
                            green = 0;
                            blue = 0;
                            break;
                    }
                    colorValueString = "";

                }
                

            }
            if (gameNumbers.Count > 0)
            {
                foreach(int i in gameNumbers)
                {
                    solution += i;
                }
                textBox2.AppendText("Part 1 answer is " +  solution + ".\r\n"); 
            }
        }
    } 
}
