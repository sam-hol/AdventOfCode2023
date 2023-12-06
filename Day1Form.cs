using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2023
{
    public partial class Day1Form : Form
    {
        MainForm parent;
        OpenFileDialog oFile = new OpenFileDialog();

        public Day1Form()
        {
            InitializeComponent();
        }

        public void SetParent(MainForm p) {
            parent = p;
        }

        private void Day1Form_Load(object sender, EventArgs e)
        {
            //this.FormClosing += new FormClosingEventHandler(GoHome);
        }

        private void GoHome(object sender, FormClosingEventArgs e)
        {
                e.Cancel = true;
                //this.parent.Show();
                //MessageBox.Show("TEST");
                this.Close();
            
        }

        private void GetResult(List<string> text)
        {
            int index = 0;
            int result = 0;
            string temp = "";
            int pholder = 0;
            List<int> nums = new List<int>();

            foreach (string s in text)
            {
                index = 0;
                nums.Clear();
                foreach (char c in s)
                {
                    
                    if (int.TryParse(c.ToString(), out pholder))
                    {
                        //nums.Append(pholder);
                        nums.Add(pholder);
                        index++;
                        //textBox2.AppendText(index.ToString());
                    }
                }
                //Console.WriteLine(nums.Count());
                //if (nums.Count() > 1) temp = nums[0].ToString().Trim() + nums.Last().ToString().Trim();
                //else if (nums.Count > 0) temp = nums[0].ToString().Trim();
                if (nums.Count != 0)
                {
                    temp = nums[0].ToString().Trim() + nums.Last().ToString().Trim();
                    result += int.Parse(temp);
                }

            }
            textBox2.AppendText("Part 1's answer is " + result.ToString() + ".");
        }

        private int checkMatch(string s)
        {
            int index = 0;

            string[] list = new string[] { "one" , "two", "three", "four", "five",
            "six", "seven", "eight", "nine", "eno", "owt", "eerht", "ruof", "evif",
            "xis", "neves", "thgie", "enin"};

            List<Regex> regexList = new List<Regex>();

            foreach (string l in list)
            {
                regexList.Add(new Regex(l));
            }

            foreach (Regex r in regexList)
            {
                if (r.IsMatch(s))
                {
                    index = regexList.IndexOf(r) + 1;
                    if (index > 9) index -= 9;
                    return index;
                }
            }
            return -1;
        }

        private void GetResult2(List<string> text)
        {
            int result = 0;
            int index = 0;
            string temp = "", temp2 = "";
            string reverse;
            int pholder = 0;
            //List<int> nums = new List<int>();
            int[] nums = new int[2];
            List<char> chars = new List<char>();


            foreach (string s in text)
            {
                Console.WriteLine(s);
                //nums.Clear();
                temp = "";
                temp2 = "";
                foreach (char c in s)
                {
                    temp2 += c;
                    pholder = checkMatch(temp2);
                    if (pholder > 0)
                    {
                        nums[0] = pholder;
                        break;
                    }
                    if (int.TryParse(c.ToString(), out pholder))
                    {
                        Console.WriteLine("PARSING AN INT HERE");
                        pholder = int.Parse(c.ToString());
                        if (pholder > 0) nums[0] = pholder;
                        break;
                    }
                    Console.WriteLine("GOING FORWARD: TEMP2 IS " + temp2);
                    
                }

                temp2 = "";
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    temp2 += s[i].ToString();
                    pholder = checkMatch(temp2);
                    if (pholder > 0)
                    {
                        nums[1] = pholder;
                        break;
                    }
                    if (int.TryParse(s[i].ToString(), out pholder))
                    {
                        pholder = int.Parse(s[i].ToString());
                        if (pholder > 0) nums[1] = pholder;
                        break;
                    }

                }



                //if (nums.Count != 0)
                //{
                    Console.WriteLine("nums 0 is " + nums[0].ToString() + " nums last is " + nums[1].ToString());
                    result += (nums[0] * 10);
                    result += nums[1];
                    //temp = nums[0].ToString().Trim() + nums.Last().ToString().Trim();
                    Console.WriteLine(temp);
                    //result += int.Parse(temp);
                //}

            }
            textBox2.AppendText("\r\nPart 2's answer is " + result.ToString() + ".");
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
                    GetResult(text);
                    GetResult2(text);
                    
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
