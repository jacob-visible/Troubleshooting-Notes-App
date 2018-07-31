using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WorkApplication
{
    public partial class TroubleshootingApp : Form
    {
        public TroubleshootingApp()
        {
            InitializeComponent();
        }

        public class Config
        {
            /*Pages*/
            public static int Page = 1;
            public static int PageTotal = 1;
            public static int ItemCount = 1;

            /*Load Config*/
            public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\WorkApplication\";//@"C:\Users\*USERNAME*\My Documents\";
            public static string ConfigFile = "";
            public static string MetaData = "";
            public static string Search = "";
            string result = "";
            public static int b1 = 0;
            public static int b2 = 0;
            public static int b3 = 0;
            public static int b4 = 0;
            public static int b5 = 0;
            public static int b6 = 0;
            public static int b7 = 0;
            public static int b8 = 0;
            public static int b9 = 0;
            public static bool AutoCopy = true;
            public static bool ClearOnClick = false;
            public static bool DetailedSteps = true;

            /*Search*/
            public static int[] SearchResults = new int[500];

            /*Solution Tracker*/
            public static string LastPressed = "";

            /*Call the Variable from the config string*/
            public string CallVar(string name)
            {
                if (ConfigFile.Contains(("<" + name + ">")))
                {
                    result = ConfigFile.Substring(ConfigFile.IndexOf("<" + name + ">") + name.Length + 2);
                    result = result.Substring(0, result.IndexOf("</>"));
                    return result;
                }
                else
                {
                    return @"";
                }
            }

            /*Call output resolution text for the item*/
            public string CallVarText(string name)
            {
                if (ConfigFile.Contains(("<t" + name + ">")))
                {
                    result = ConfigFile.Substring(ConfigFile.IndexOf("<t" + name + ">") + name.Length + 3);
                    result = result.Substring(0, result.IndexOf("</>"));
                    result += @"
";
                    return result;
                }
                else
                {
                    return @"";
                }
            }

            public string CallVarCode(string name)
            {
                if (ConfigFile.Contains(("<c" + name + ">")))
                {
                    result = ConfigFile.Substring(ConfigFile.IndexOf("<c" + name + ">") + name.Length + 3);
                    result = result.Substring(0, result.IndexOf("</>"));
                    return result;
                }
                else
                {
                    return @"";
                }
            }

            public string CallMetaData(string name)
            {
                if (ConfigFile.Contains(("<m" + name + ">")))
                {
                    result = ConfigFile.Substring(ConfigFile.IndexOf("<m" + name + ">") + name.Length + 3);
                    result = result.Substring(0, result.IndexOf("</>"));
                    return result;
                }
                else
                {
                    return @"";
                }
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Config.Path + "config.txt"))
            {
                using (StreamReader streamReader = new StreamReader(Config.Path +@"config.txt", Encoding.UTF8))
                {
                    Config.ConfigFile = streamReader.ReadToEnd();
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path + @"\pages\");
                System.IO.Directory.CreateDirectory(Config.Path + @"\pictures\");
                System.IO.File.WriteAllText(Config.Path + @"config.txt", @"Add more by duplicate these line and adding 1 for each new item.

Check you documents for the folder WorkApplication. There in will be the folder pages for html pages.
You can create any html pages you want there. 
They will be opened in the detailed side view if you assign them the name equal to their <m1> MetaData tag with the extension .html.

<1>Insert issue button text</>
<t1>Insert resolution text</>
<c1>Insert resolution code</>
<m1>MetaData Tag</>");
            }

            /*Generate Buttons*/
            int button = 1;
            button = 1 + (9 * (Config.Page - 1));
            Config.b1 = button;
            button1.Text = new Config().CallVar(button.ToString());
            button = 2 + (9 * (Config.Page - 1));
            Config.b2 = button;
            button2.Text = new Config().CallVar(button.ToString());
            button = 3 + (9 * (Config.Page - 1));
            Config.b3 = button;
            button3.Text = new Config().CallVar(button.ToString());
            button = 4 + (9 * (Config.Page - 1));
            Config.b4 = button;
            button4.Text = new Config().CallVar(button.ToString());
            button = 5 + (9 * (Config.Page - 1));
            Config.b5 = button;
            button5.Text = new Config().CallVar(button.ToString());
            button = 6 + (9 * (Config.Page - 1));
            Config.b6 = button;
            button6.Text = new Config().CallVar(button.ToString());
            button = 7 + (9 * (Config.Page - 1));
            Config.b7 = button;
            button7.Text = new Config().CallVar(button.ToString());
            button = 8 + (9 * (Config.Page - 1));
            Config.b8 = button;
            button8.Text = new Config().CallVar(button.ToString());
            button = 9 + (9 * (Config.Page - 1));
            Config.b9 = button;
            button9.Text = new Config().CallVar(button.ToString());

            /*Count Pages*/
            Config.PageTotal = 1;
            Config.ItemCount = 1;
            while (Config.ConfigFile.Contains(@"<" + Config.ItemCount.ToString() + @">"))
            {
                Config.ItemCount++;
            }
            Config.ItemCount--;

            Config.PageTotal = Config.ItemCount / 9;
            if (Config.PageTotal <= 0)
            {
                Config.PageTotal = 1;
            }

            if ((Config.ItemCount - (Config.PageTotal * 9)) > 0)
            {
                Config.PageTotal++;
            }
            PageOf.Text = @"Page " + Config.Page.ToString() + @" of " + Config.PageTotal.ToString();
        }

        /*Search Box*/
        public void Search_TextChanged_1(object sender, EventArgs e)
        {
            Config.Search = Search.Text;
            if (Config.Search.Length > 0)
            {
                /*Blank all buttons text and value*/
                Array.Clear(Config.SearchResults, 0, 500);
                Config.b1 = 0;
                button1.Text = "";
                Config.b2 = 0;
                button2.Text = "";
                Config.b3 = 0;
                button3.Text = "";
                Config.b4 = 0;
                button4.Text = "";
                Config.b5 = 0;
                button5.Text = "";
                Config.b6 = 0;
                button6.Text = "";
                Config.b7 = 0;
                button7.Text = "";
                Config.b8 = 0;
                button8.Text = "";
                Config.b9 = 0;
                button9.Text = "";


                int Count = 1;
                for (int Loop = 1; Loop <= Config.ItemCount; Loop++)
                {
                    
                    if (new Config().CallVar(Loop.ToString()).Contains(Config.Search))
                    {
                        Config.SearchResults[Count] = Loop;
                        if(Count == 1)
                        {
                            Config.b1 = Config.SearchResults[Count];
                            button1.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 2)
                        {
                            Config.b2 = Config.SearchResults[Count];
                            button2.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 3)
                        {
                            Config.b3 = Config.SearchResults[Count];
                            button3.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 4)
                        {
                            Config.b4 = Config.SearchResults[Count];
                            button4.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 5)
                        {
                            Config.b5 = Config.SearchResults[Count];
                            button5.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 6)
                        {
                            Config.b6 = Config.SearchResults[Count];
                            button6.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 7)
                        {
                            Config.b7 = Config.SearchResults[Count];
                            button7.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 8)
                        {
                            Config.b8 = Config.SearchResults[Count];
                            button8.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        if (Count == 9)
                        {
                            Config.b9 = Config.SearchResults[Count];
                            button9.Text = new Config().CallVar(Config.SearchResults[Count].ToString());
                        }
                        Count++;
                    };
                }

                Config.Page = 1;
                //Count Pages
                if ( Count <= 9 )
                {
                    Config.PageTotal = 1;
                }
                else
                {
                    Config.PageTotal = Count / 9;
                    if( (Config.PageTotal * 9) < Count)
                    {
                        Config.PageTotal++;
                    }
                }
                PageOf.Text = @"Page " + Config.Page.ToString() + @" of " + Config.PageTotal.ToString();
            }
            else
            {
                /*Generate Buttons*/
                int button = 1;
                button = 1 + (9 * (Config.Page - 1));
                Config.b1 = button;
                button1.Text = new Config().CallVar(button.ToString());
                button = 2 + (9 * (Config.Page - 1));
                Config.b2 = button;
                button2.Text = new Config().CallVar(button.ToString());
                button = 3 + (9 * (Config.Page - 1));
                Config.b3 = button;
                button3.Text = new Config().CallVar(button.ToString());
                button = 4 + (9 * (Config.Page - 1));
                Config.b4 = button;
                button4.Text = new Config().CallVar(button.ToString());
                button = 5 + (9 * (Config.Page - 1));
                Config.b5 = button;
                button5.Text = new Config().CallVar(button.ToString());
                button = 6 + (9 * (Config.Page - 1));
                Config.b6 = button;
                button6.Text = new Config().CallVar(button.ToString());
                button = 7 + (9 * (Config.Page - 1));
                Config.b7 = button;
                button7.Text = new Config().CallVar(button.ToString());
                button = 8 + (9 * (Config.Page - 1));
                Config.b8 = button;
                button8.Text = new Config().CallVar(button.ToString());
                button = 9 + (9 * (Config.Page - 1));
                Config.b9 = button;
                button9.Text = new Config().CallVar(button.ToString());

                /*Count Pages*/
                Config.PageTotal = 1;
                Config.ItemCount = 1;
                while (Config.ConfigFile.Contains(@"<" + Config.ItemCount.ToString() + @">"))
                {
                    Config.ItemCount++;
                }
                Config.ItemCount--;

                Config.PageTotal = Config.ItemCount / 9;
                if (Config.PageTotal <= 0)
                {
                    Config.PageTotal = 1;
                }

                if ((Config.ItemCount - (Config.PageTotal * 9)) > 0)
                {
                    Config.PageTotal++;
                }
                PageOf.Text = @"Page " + Config.Page.ToString() + @" of " + Config.PageTotal.ToString();
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if ( Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }
            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b1 > 0)
                    {
                        Clipboard.SetText(new Config().CallVar(Config.b1.ToString()));
                    }
                }
            }
            Notes.AppendText(new Config().CallVarText(Config.b1.ToString()));
            Config.LastPressed = Config.b1.ToString();
            if(Config.MetaData.Length <= 250)
            {
                if(Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b1.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b1.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b1.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b1.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b1.ToString()) + ".html");
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Config.MetaData = "";
                Notes.Text = "";
            }

            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if(Config.b2 > 0)
                    {
                        /*if ()
                        {
                            ;
                        }*/
                        if (new Config().CallVarText(Config.b2.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVarText(Config.b2.ToString()));
                        }
                        //Clipboard.SetText(new Config().CallVarText(Config.b2.ToString()));
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b2.ToString()));
            Config.LastPressed = Config.b2.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b2.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b2.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b2.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b2.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b2.ToString()) + ".html");
                    }
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }

            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b3 > 0)
                    {
                        if (new Config().CallVarText(Config.b3.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b3.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b3.ToString()));
            Config.LastPressed = Config.b3.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b3.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b3.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b3.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b3.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b3.ToString()) + ".html");
                    }
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }


            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b4 > 0)
                    {
                        if (new Config().CallVarText(Config.b4.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b4.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b4.ToString()));
            Config.LastPressed = Config.b4.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b4.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b4.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b4.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b7.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b7.ToString()) + ".html");
                    }
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }
            
            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b5 > 0)
                    {

                        if (new Config().CallVarText(Config.b5.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b5.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b5.ToString()));
            Config.LastPressed = Config.b5.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b5.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b5.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b5.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b5.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b5.ToString()) + ".html");
                    }
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }

            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b6 > 0)
                    {
                        if (new Config().CallVarText(Config.b2.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b6.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b6.ToString()));
            Config.LastPressed = Config.b6.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b6.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b6.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b6.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b6.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b6.ToString()) + ".html");
                    }
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }
            
            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b7 > 0)
                    {
                        if (new Config().CallVarText(Config.b7.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b7.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b7.ToString()));
            Config.LastPressed = Config.b7.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b7.ToString());
                }
                else
                {
                    if(Config.MetaData.Contains(new Config().CallMetaData(Config.b7.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b7.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }

            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b7.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b7.ToString()) + ".html");
                    }
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }
            
            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b8 > 0)
                    {
                        if (new Config().CallVarText(Config.b8.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b8.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b8.ToString()));
            Config.LastPressed = Config.b8.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b8.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b8.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b8.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }


            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b8.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b8.ToString()) + ".html");
                    }
                }
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (Config.ClearOnClick == true)
            {
                Config.ClearOnClick = false;
                Notes.Text = "";
            }
            
            if (Config.AutoCopy == true)
            {
                if (Config.MetaData.Length == 0)
                {
                    if (Config.b9 > 0)
                    {
                        if (new Config().CallVarText(Config.b2.ToString()).Length != 0)
                        {
                            Clipboard.SetText(new Config().CallVar(Config.b9.ToString()));
                        }
                    }
                }
            }

            Notes.AppendText(new Config().CallVarText(Config.b9.ToString()));
            Config.LastPressed = Config.b9.ToString();
            if (Config.MetaData.Length <= 250)
            {
                if (Config.MetaData.Length == 0)
                {
                    Config.MetaData = Config.MetaData + new Config().CallMetaData(Config.b9.ToString());
                }
                else
                {
                    if (Config.MetaData.Contains(new Config().CallMetaData(Config.b9.ToString())))
                    {

                    }
                    else
                    {
                        Config.MetaData = Config.MetaData + "-" + new Config().CallMetaData(Config.b9.ToString());
                    }
                }
            }
            else
            {
                Config.MetaData = "";
            }


            if (Config.DetailedSteps == true)
            {
                if (Config.MetaData.Length != 0)
                {
                    if (File.Exists(Config.Path + @"pages\" + new Config().CallMetaData(Config.b9.ToString()) + ".html") == true)
                    {
                        DetailedStepsWeb.Url = new System.Uri(Config.Path + @"pages\" + new Config().CallMetaData(Config.b9.ToString()) + ".html");
                    }
                }
            }
        }

        /*Page Forward*/
        public void ForwardButton_Click(object sender, EventArgs e)
        {
            if (Config.Page < Config.PageTotal)
            {
                Config.Page++;
                PageOf.Text = @"Page " + Config.Page.ToString() + @" of " + Config.PageTotal.ToString();

                /*Generate Buttons*/
                int button = 0;
                if (Search.Text.Length == 0)
                {
                    button = 1 + (9 * (Config.Page - 1));
                    Config.b1 = button;
                    button1.Text = new Config().CallVar(button.ToString());
                    button = 2 + (9 * (Config.Page - 1));
                    Config.b2 = button;
                    button2.Text = new Config().CallVar(button.ToString());
                    button = 3 + (9 * (Config.Page - 1));
                    Config.b3 = button;
                    button3.Text = new Config().CallVar(button.ToString());
                    button = 4 + (9 * (Config.Page - 1));
                    Config.b4 = button;
                    button4.Text = new Config().CallVar(button.ToString());
                    button = 5 + (9 * (Config.Page - 1));
                    Config.b5 = button;
                    button5.Text = new Config().CallVar(button.ToString());
                    button = 6 + (9 * (Config.Page - 1));
                    Config.b6 = button;
                    button6.Text = new Config().CallVar(button.ToString());
                    button = 7 + (9 * (Config.Page - 1));
                    Config.b7 = button;
                    button7.Text = new Config().CallVar(button.ToString());
                    button = 8 + (9 * (Config.Page - 1));
                    Config.b8 = button;
                    button8.Text = new Config().CallVar(button.ToString());
                    button = 9 + (9 * (Config.Page - 1));
                    Config.b9 = button;
                    button9.Text = new Config().CallVar(button.ToString());
                }
                else
                {
                    /*Blank all buttons text and value*/
                    Config.b1 = 0;
                    button1.Text = "";
                    Config.b2 = 0;
                    button2.Text = "";
                    Config.b3 = 0;
                    button3.Text = "";
                    Config.b4 = 0;
                    button4.Text = "";
                    Config.b5 = 0;
                    button5.Text = "";
                    Config.b6 = 0;
                    button6.Text = "";
                    Config.b7 = 0;
                    button7.Text = "";
                    Config.b8 = 0;
                    button8.Text = "";
                    Config.b9 = 0;
                    button9.Text = "";


                    /*button = 1 + (9 * (Config.Page - 1));
                    if (Config.SearchResults[button].ToString().Length >= 1)
                    {
                        Config.b1 = Config.SearchResults[button];
                        button1.Text = new Config().CallVar(Config.SearchResults[button].ToString());
                    }*/

                    //if (Config.SearchResults[button] != null)
                    //{


                    button = 1 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b1);
                        button1.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 2 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b2);
                        button2.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 3 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b3);
                        button3.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 4 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b4);
                        button4.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 5 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b5);
                        button5.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 6 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b6);
                        button6.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 7 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b7);
                        button7.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 8 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b8);
                        button8.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 9 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b9);
                        button9.Text = new Config().CallVar(Config.SearchResults[button].ToString());
                }
            }
        }

        /*Page Back*/
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (Config.Page > 1)
            {
                Config.Page--;
                PageOf.Text = @"Page " + Config.Page.ToString() + @" of " + Config.PageTotal.ToString();

                /*Generate Buttons*/
                int button = 0;
                if (Search.Text.Length == 0)
                {
                    button = 1 + (9 * (Config.Page - 1));
                    Config.b1 = button;
                    button1.Text = new Config().CallVar(button.ToString());
                    button = 2 + (9 * (Config.Page - 1));
                    Config.b2 = button;
                    button2.Text = new Config().CallVar(button.ToString());
                    button = 3 + (9 * (Config.Page - 1));
                    Config.b3 = button;
                    button3.Text = new Config().CallVar(button.ToString());
                    button = 4 + (9 * (Config.Page - 1));
                    Config.b4 = button;
                    button4.Text = new Config().CallVar(button.ToString());
                    button = 5 + (9 * (Config.Page - 1));
                    Config.b5 = button;
                    button5.Text = new Config().CallVar(button.ToString());
                    button = 6 + (9 * (Config.Page - 1));
                    Config.b6 = button;
                    button6.Text = new Config().CallVar(button.ToString());
                    button = 7 + (9 * (Config.Page - 1));
                    Config.b7 = button;
                    button7.Text = new Config().CallVar(button.ToString());
                    button = 8 + (9 * (Config.Page - 1));
                    Config.b8 = button;
                    button8.Text = new Config().CallVar(button.ToString());
                    button = 9 + (9 * (Config.Page - 1));
                    Config.b9 = button;
                    button9.Text = new Config().CallVar(button.ToString());
                }
                else
                {
                    Config.b1 = 0;
                    button1.Text = "";
                    Config.b2 = 0;
                    button2.Text = "";
                    Config.b3 = 0;
                    button3.Text = "";
                    Config.b4 = 0;
                    button4.Text = "";
                    Config.b5 = 0;
                    button5.Text = "";
                    Config.b6 = 0;
                    button6.Text = "";
                    Config.b7 = 0;
                    button7.Text = "";
                    Config.b8 = 0;
                    button8.Text = "";
                    Config.b9 = 0;
                    button9.Text = "";

                    button = 1 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b1);
                        button1.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 2 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b2);
                        button2.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 3 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b3);
                        button3.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 4 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b4);
                        button4.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 5 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b5);
                        button5.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 6 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b6);
                        button6.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 7 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b7);
                        button7.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 8 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b8);
                        button8.Text = new Config().CallVar(Config.SearchResults[button].ToString());

                    button = 9 + (9 * (Config.Page - 1));
                        int.TryParse(Config.SearchResults[button].ToString(), out Config.b9);
                        button9.Text = new Config().CallVar(Config.SearchResults[button].ToString());
                }
            }
        }

        /*Resolve Press*/
        private void ResolveButton_Click(object sender, EventArgs e)
        {
            Notes.AppendText(@"
Issue Resolved.");
            if (Config.MetaData.Length != 0) {
                Notes.AppendText(@"
Tag:" + Config.MetaData);
            }
            if(Config.AutoCopy == true)
            {
                Notes.SelectAll();
                Notes.Copy();
                Notes.DeselectAll();
            }
            Codes.Text = new Config().CallVarCode(Config.LastPressed.ToString());
            Search.Text = "";

            Config.Page = 1;

            /*Count Pages*/
            Config.PageTotal = 1;
            Config.ItemCount = 1;
            while (Config.ConfigFile.Contains(@"<" + Config.ItemCount.ToString() + @">"))
            {
                Config.ItemCount++;
            }
            Config.ItemCount--;

            Config.PageTotal = Config.ItemCount / 9;
            if (Config.PageTotal <= 0)
            {
                Config.PageTotal = 1;
            }

            if ((Config.ItemCount - (Config.PageTotal * 9)) > 0)
            {
                Config.PageTotal++;
            }
            PageOf.Text = @"Page " + Config.Page.ToString() + @" of " + Config.PageTotal.ToString();

            /*Generate Buttons*/
            int button = 1;
            button = 1 + (9 * (Config.Page - 1));
            Config.b1 = button;
            button1.Text = new Config().CallVar(button.ToString());
            button = 2 + (9 * (Config.Page - 1));
            Config.b2 = button;
            button2.Text = new Config().CallVar(button.ToString());
            button = 3 + (9 * (Config.Page - 1));
            Config.b3 = button;
            button3.Text = new Config().CallVar(button.ToString());
            button = 4 + (9 * (Config.Page - 1));
            Config.b4 = button;
            button4.Text = new Config().CallVar(button.ToString());
            button = 5 + (9 * (Config.Page - 1));
            Config.b5 = button;
            button5.Text = new Config().CallVar(button.ToString());
            button = 6 + (9 * (Config.Page - 1));
            Config.b6 = button;
            button6.Text = new Config().CallVar(button.ToString());
            button = 7 + (9 * (Config.Page - 1));
            Config.b7 = button;
            button7.Text = new Config().CallVar(button.ToString());
            button = 8 + (9 * (Config.Page - 1));
            Config.b8 = button;
            button8.Text = new Config().CallVar(button.ToString());
            button = 9 + (9 * (Config.Page - 1));
            Config.b9 = button;
            button9.Text = new Config().CallVar(button.ToString());

            Config.ClearOnClick = true;
            Config.MetaData = "";
        }

        /*Copy to Clipboard button*/
        private void CopyButton_Click(object sender, EventArgs e)
        {
            Notes.SelectAll();
            Notes.Copy();
            Notes.DeselectAll();
        }

        /*Clear button*/
        private void ClearButton_Click(object sender, EventArgs e)
        {
            Notes.Text = "";
            Search.Text = "";
            Config.MetaData = "";
            if(Config.DetailedSteps == true)
            {
                DetailedStepsWeb.Url = new System.Uri("about:blank");
            }
            Codes.Text = "";
        }

        /*keyboard presses*/
        private void TroubleshootingApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (Notes.Focused)
            {

            }
            else
            {
                if (Search.Focused)
                {

                }
                else
                {
                    /*1-9*/
                    if (e.KeyCode == Keys.D1)
                    {
                        button1.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad1)
                    {
                        button1.PerformClick();
                    }
                    if (e.KeyCode == Keys.D2)
                    {
                        button2.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad2)
                    {
                        button2.PerformClick();
                    }
                    if (e.KeyCode == Keys.D3)
                    {
                        button3.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad3)
                    {
                        button3.PerformClick();
                    }
                    if (e.KeyCode == Keys.D4)
                    {
                        button4.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad4)
                    {
                        button4.PerformClick();
                    }
                    if (e.KeyCode == Keys.D5)
                    {
                        button5.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad5)
                    {
                        button5.PerformClick();
                    }
                    if (e.KeyCode == Keys.D6)
                    {
                        button6.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad6)
                    {
                        button6.PerformClick();
                    }
                    if (e.KeyCode == Keys.D7)
                    {
                        button7.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad7)
                    {
                        button7.PerformClick();
                    }
                    if (e.KeyCode == Keys.D8)
                    {
                        button8.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad8)
                    {
                        button8.PerformClick();
                    }
                    if (e.KeyCode == Keys.D9)
                    {
                        button9.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad9)
                    {
                        button9.PerformClick();
                    }

                    /*0 Resolve Press*/
                    if (e.KeyCode == Keys.D0)
                    {
                        ResolveButton.PerformClick();
                    }
                    if (e.KeyCode == Keys.NumPad0)
                    {
                        ResolveButton.PerformClick();
                    }

                    /*Forward and Back*/
                    if (e.KeyCode == Keys.Add)
                    {
                        ForwardButton.PerformClick();
                    }
                    if (e.KeyCode == Keys.Subtract)
                    {
                        BackButton.PerformClick();
                    }

                    if (e.KeyCode == Keys.C && e.Control)
                    {
                        Notes.Copy();
                    }
                }
            }
        }
        
        /*Options Bar top left*/
        private void automaticallyCopyWithResolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Config.AutoCopy == true)
            {
                Config.AutoCopy = false;
                automaticallyCopyWithResolveToolStripMenuItem.Checked = false;
            }
            else
            {
                Config.AutoCopy = true;
                automaticallyCopyWithResolveToolStripMenuItem.Checked = true;
            }
        }

        private void problemSolutionEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Config.Path + @"config.txt");
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TopMost == false)
            {
                this.TopMost = true;
                alwaysOnTopToolStripMenuItem.Checked = true;
            }
            else
            {
                this.TopMost = false;
                alwaysOnTopToolStripMenuItem.Checked = false;
            }
        }

        private void sideDetailedStepsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Config.DetailedSteps == true)
            {
                Config.DetailedSteps = false;
                sideDetailedStepsToolStripMenuItem.Checked = false;
                this.Size = new Size(395, 545);
                DetailedSteps.Visible = false;
            }
            else
            {
                Config.DetailedSteps = true;
                sideDetailedStepsToolStripMenuItem.Checked = true;
                this.Size = new Size(1035, 545);
                DetailedSteps.Visible = true;
            }
        }
    }
}
