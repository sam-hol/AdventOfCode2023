using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;



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

            string[] matchStrings;
            int index = 0, solution = 0, solution2 = 0, fewR, fewG, fewB, red, green, blue;
            bool goodmatch = false;
            string digits;

            foreach (string s in text) //This loops through every string in the .txt
            {
                matchStrings = s.Trim().Split(',', ';', ':');
                goodmatch = true;
                index++;
                fewR = 0;
                fewG = 0;
                fewB = 0;

                foreach (string l in matchStrings)
                {
                    digits = new String(l.Where(Char.IsDigit).ToArray());
                    if (l.Contains("red"))
                    {
                        red = int.Parse(digits.ToString());
                        if (fewR < red) fewR = red;
                        if (red > 12)
                        {
                            goodmatch = false;
                        }

                    }
                    if (l.Contains("green"))
                    {
                        green = int.Parse(digits.ToString());
                        if (fewG < green) fewG = green;
                        if (green > 13)
                        {
                            goodmatch = false;
                        }
                    }
                    if (l.Contains("blue"))
                    {
                        blue = int.Parse(digits.ToString());
                        if (fewB < blue) fewB = blue;
                        if (blue > 14)
                        {
                            goodmatch = false;
                        }
                    }
                }
                solution2 += (fewR * fewG * fewB);
                if (goodmatch)
                {
                    solution += index;
                }
            }
            textBox2.AppendText("Solution for part 1 is " + solution + ".\r\n");
            textBox2.AppendText("Solution for part 2 is " + solution2 + ".\r\n");
        }
    }
}
