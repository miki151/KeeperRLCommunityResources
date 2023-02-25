using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{
    public partial class frmWikiMaintainer : Form
    {

        private string GitFolderPath = "C:\\keeperrl_wiki\\";

        public frmWikiMaintainer()
        {
            InitializeComponent();
            textBox1.Text = GitFolderPath;
        }

        public void NewArticles()
        {
            LoadValidLinks("C:\\keeperrl_wiki\\",ValidLinks);
            string fil = System.IO.File.ReadAllText("C:\\keeperrl_wiki_dump\\wiki_dump.xml");
            List<String> fils = Strings.Split(fil, "=", -1, CompareMethod.Text).ToList();
            int f = -1;
            while (f < fils.Count - 1)
            {
                string nam = "";
                while (nam.Contains("\n") || nam.Trim().Count() == 0 && (f < fils.Count - 1))
                {
                    try
                    {
                        nam = fils[f + 1];
                    }
                    catch
                    {
                    }
                    f++;
                }
                try { nam = "C:\\keeperrl_wiki\\" + nam.Trim(); nam = nam + ".md"; } catch { }
                nam = Strings.Replace(nam, "]", "");
                nam = Strings.Replace(nam, "[", "");
                string entry = "";
                while (entry.Trim().Count() < 20)
                {
                    entry = fils[f + 1];
                    f++;
                }
                entry = Strings.Replace(entry, "*", "\r\n-");
                entry = Strings.Replace(entry, "''", "\r\n''");
                try
                {
                    string text = "";
                    if (System.IO.File.Exists(nam)) text = System.IO.File.ReadAllText(nam);
                    int count = 0;
                    while (count < Strings.Split(entry, "\r\n").Count())
                    {
                        try
                        {
                            Application.DoEvents();
                            if (!text.Contains(Strings.Split(entry, "\r\n")[count]))
                            {
                                text = text + "\r\n" + Strings.Split(entry, "\r\n")[count];
                            }
                        }
                        catch
                        {
                        }
                        count++;
                    }
                    string ValidLink = System.IO.Path.GetFileNameWithoutExtension(nam).Trim();
                    ValidLink = Strings.Join(Strings.Split(ValidLink, " "), "_");
                    ValidLink = Capitalize(ValidLink);
                    if (!ValidLinks.ContainsKey(ValidLink))
                    {
                        string newHeader = "---\r\ntitle: " + Strings.Replace(ValidLink, "_", " ") + "\r\npermalink: " + ValidLink + "/\r\nlayout: wiki\r\n---\r\n";
                        System.IO.File.WriteAllText("C:\\keeperrl_wiki_suggestions\\" + ValidLink+".md", newHeader+ "\r\n"+text);
                    }
                }
                catch { }
                f++;
            }
        }

        private string Capitalize(string Link)
        {
            Link = Strings.Replace(Link, "Category:", "");
            Link = Strings.Replace(Link, ":", "_");
            Link = Link.Trim();
            Link = Strings.Left(Link, 1).ToUpper() + Strings.Right(Link, Strings.Len(Link) - 1);
            Link = Strings.Replace(Link, " ", "_");
            Link = Strings.Replace(Link, "__","_");
            int i = Strings.Asc("a");
            while (i <= Strings.Asc("z"))
            {
                Link = Strings.Replace(Link, "_"+Strings.Chr(i), "_"+Strings.Chr(i).ToString().ToUpper());
                Link = Strings.Replace(Link, "\\" + Strings.Chr(i), "\\" + Strings.Chr(i).ToString().ToUpper());
                i++;
            }
            while (Link.StartsWith("_"))
            {
                Link=Strings.Right(Link, Strings.Len(Link) - 1);
            }
            return Link;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           button1.Enabled = false;
           StandardizeHeaders("C:\\keeperrl_wiki\\","");
           button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            System.IO.DirectoryInfo di = new DirectoryInfo("C:\\keeperrl_wiki\\Missing");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            LoadValidLinks("C:\\keeperrl_wiki_suggestions\\", SuggestedLinks);
            LoadValidLinks("C:\\keeperrl_wiki\\", ValidLinks);
            //CapitalizeValidLinks();
            FixLinksDir("C:\\keeperrl_wiki\\");
            button2.Enabled = true;
        }

        private void CapitalizeValidLinks()
        {
            foreach (KeyValuePair<string,string> kvp in ValidLinks)
            {
                string caps = Capitalize(Strings.Replace(kvp.Value,"C:\\",""));
                caps = "C:\\"+caps;
                System.IO.File.Copy(kvp.Value, caps + ".bak");
                System.IO.File.Delete(kvp.Value);
                System.IO.File.Copy(caps + ".bak",caps);
                System.IO.File.Delete(caps+".bak");
            }
            ValidLinks = new Dictionary<string, string>();
            LoadValidLinks("C:\\keeperrl_wiki\\", ValidLinks);
        }

        private void FixLinksDir(string dir)
        {
            if (System.IO.Path.GetFileName(dir).StartsWith(".")) return;
            if (System.IO.Path.GetFileName(dir).StartsWith("_")) return;
            foreach (String fil in System.IO.Directory.GetFiles(dir))
            {
                if (System.IO.Path.GetExtension(fil) == ".md" && !fil.StartsWith("index"))
                {
                    FixLinks(fil);
                }
            }
            foreach (String subdir in System.IO.Directory.GetDirectories(dir))
            {
                FixLinksDir(subdir);
            }
        }

        private void StandardizeHeaders(string dir, string newOtherItems)
        {
            if (System.IO.Path.GetFileName(dir).StartsWith(".")) return;
            if (System.IO.Path.GetFileName(dir).StartsWith("_")) return;
            foreach (String fil in System.IO.Directory.GetFiles(dir))
            {
                if (fil.EndsWith(".md") && !fil.StartsWith("index"))
                {
                    string oldtext = System.IO.File.ReadAllText(fil);
                    string text = oldtext;
                    string header = "";
                    int headerCount = 0;
                    string firstLink = "";
                    string otherItems = "";
                    bool otherOn = false;
                    foreach (string line in Strings.Split(text, "\r\n"))
                    {
                        if (headerCount < 2)
                        {
                            if (headerCount == 1 && !line.StartsWith("---")) header = header + line + "\r\n";
                            if (line.StartsWith("---")) headerCount++;
                        }
                        if (line.StartsWith("[MainPage]") && firstLink == "") firstLink = line;
                        if (line.StartsWith("Other items in this section"))
                        {
                            otherOn = true;
                        }
                        if (otherOn && line.Trim()!="")
                        {
                            otherItems = otherItems + line + "\r\n";
                        }
                    }
                    string nam = Capitalize(System.IO.Path.GetFileNameWithoutExtension(fil).Trim());
                    string newHeader = "title: " + Strings.Replace(nam, "_", " ") + "\r\npermalink: " + nam + "/\r\nlayout: wiki\r\n";
                    if (headerCount == 0)
                    {
                        newHeader = "---\r\ntitle: " + Strings.Replace(nam, "_", " ") + "\r\npermalink: " + nam + "/\r\nlayout: wiki\r\n---\r\n";
                        text = newHeader + text;
                    }
                    else
                    {
                        text = Strings.Replace(text, header, newHeader);
                    }
                    string siteMap = "[MainPage](/keeperrl_wiki/ \"wikilink\")";
                    foreach (string part in Strings.Split(fil, "\\"))
                    {
                        string partname = Strings.Replace(part, ".md", "");
                        if (partname != "Index")
                        {
                            if (part != "C:" && part != "keeperrl_wiki" && !part.EndsWith(".md"))
                            {
                                string guideFil = Strings.Split(fil, part)[0] + part + "\\"+part+"_Guide.md";
                                if (System.IO.File.Exists(guideFil))
                                {
                                    siteMap = siteMap + ">>[" + part + "](/keeperrl_wiki/" + partname + "_Guide \"wikilink\")";
                                }
                                else
                                {
                                     siteMap = siteMap + ">>[" + part + "](/keeperrl_wiki/" + partname + " \"wikilink\")";
                                }
                            }
                        }
                    }
                    if (firstLink != "")
                    {
                        text = Strings.Replace(text, firstLink+"\r\n", "");
                    }
                    string expectedHeaderAndLink = newHeader + "---" + siteMap;
                    text = Microsoft.VisualBasic.Strings.Replace(text, newHeader + "---" + "\r\n", expectedHeaderAndLink+"\r\n")+ siteMap;
                    text = Microsoft.VisualBasic.Strings.Replace(text, otherItems, "");
                    text = text.Trim();
                    text = text + Strings.Replace(newOtherItems,MakeLink(fil),"");

                    text = Strings.Replace(text, "\r\n" + siteMap, siteMap);
                    text = Strings.Replace(text, "\r\n" + siteMap, siteMap);
                    text = Strings.Replace(text, "\r\n" + siteMap, siteMap);
                    text = Strings.Replace(text, "\r\n" + siteMap, siteMap);
                    text = Strings.Replace(text, "\r\n" + siteMap, siteMap);
                    text = Strings.Replace(text, siteMap, "\r\n\r\n"+siteMap);
                    
                    text = Strings.Replace(text, siteMap + "\r\n", siteMap);
                    text = Strings.Replace(text, siteMap + "\r\n", siteMap);
                    text = Strings.Replace(text, siteMap + "\r\n", siteMap);
                    text = Strings.Replace(text, siteMap + "\r\n", siteMap);
                    text = Strings.Replace(text, siteMap + "\r\n", siteMap);
                    text = Strings.Replace(text, siteMap, siteMap + "\r\n\r\n");
                    if (text != oldtext)
                    {
                        System.IO.File.WriteAllText(fil, text);
                    }
                }
            }
            foreach (String subdir in System.IO.Directory.GetDirectories(dir))
            {
                string nextOtherItems = "Other items in this section\r\n";
                foreach (String fil in System.IO.Directory.GetFiles(subdir))
                {
                    if (fil.EndsWith(".md") && !fil.StartsWith("index"))
                    {
                        nextOtherItems = nextOtherItems + MakeLink(fil);
                    }
                }
                StandardizeHeaders(subdir, nextOtherItems);
            }
        }

        private string MakeLink(string part)
        {
            string partname=System.IO.Path.GetFileNameWithoutExtension(part);
            return "-    [" + Strings.Replace(partname, "_"," ") + "](/keeperrl_wiki/" + Capitalize(partname) + " \"wikilink\")" + "\r\n";
        }

        private void FixLinks(string file)
        {
            string oldText = System.IO.File.ReadAllText(file);
            string text = oldText;
            foreach (string line in Strings.Split(text, "\r\n"))
            {
                if (line.Trim().Contains("wikilink"))
                {
                    string newLine = FixLine(line);
                    text = Microsoft.VisualBasic.Strings.Replace(text, line, newLine);
                }
            }
            text = Strings.Replace(text, "[ /keeperrl_wiki/", "[");
            text = Strings.Replace(text, "[/keeperrl_wiki/", "[");
            text = Strings.Replace(text, "\r\n", "\n");
            text = Strings.Replace(text, "\n", "\r\n");
            text = Strings.Replace(text, "\r\n", "\r");
            text = Strings.Replace(text, "\r", "\r\n");
            if (text != oldText)
            {
                System.IO.File.WriteAllText(file, text);
            }
        }

        private string FixLine(string line)
        {
            if (!line.Contains("(")) return line;
            bool first = true;
            foreach (string link in Strings.Split(line, "("))
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    string alink = Strings.Split(link, " \"wikilink\")")[0];
                    string linkto = FixLink(alink);
                    line = Strings.Replace(line, alink, "/keeperrl_wiki/" + linkto);
                    string targ = "(/keeperrl_wiki/" + linkto + " \"wikilink\")";
                    if (!line.Contains("]" + targ) && line.Contains(targ))
                    {
                        line = Strings.Replace(line, targ, "[" + linkto + "]" + targ);
                    }
                }
            }
            return line;
        }

        private string FixLink(string myLink)
        {
            string link = myLink.Trim();
            if (myLink=="/keeperrl_wiki/Gameplay_Guide")
            {
                link = myLink.Trim(); //Catch cases here!
            }
            if (link == "/keeperrl_wiki/") return "";
            if (!link.EndsWith("/")) link = link + "/";
            if (ValidLinks.Count == 0)
            {
                LoadValidLinks("C:\\keeperrl_wiki\\", ValidLinks);
            }
            link = Strings.Replace(link, " ", "_");
            link = Strings.Replace(link, ":", "");
            if (link == "") return "";
            if (link == "/") return "";
            if (link.StartsWith("/"))
            {
                link= Strings.Right(link, Strings.Len(link) - 1);
            }
            if (ValidLinks.ContainsKey(link)) return link;

            if (link.Contains("/"))
            {
                link = Strings.Split(link, "/")[Strings.Split(link, "/").Count()-2];
            }
            if (ValidLinks.ContainsKey(link)) return link;

            if (link.Contains("#"))
            {
                link = Strings.Split(link, "#")[1];
            }
            if (ValidLinks.ContainsKey(link)) return link;

            if (link.Contains("Category%3A_"))
            {
                link = Strings.Replace(link, "Category%3A_", "") + "_Guide";
            }
            if (ValidLinks.ContainsKey(link)) return link;
            if (link.Contains("Category%3A"))
            {
                link = Strings.Replace(link, "Category%3A", "") + "_Guide";
            }
            if (ValidLinks.ContainsKey(link)) return link;
            if (link.Contains("User%3A"))
            {
                link = "Player_"+Strings.Replace(link, "User%3A", "");
            }
            if (ValidLinks.ContainsKey(link)) return link;
            link = Strings.Left(link, 1).ToUpper() + Strings.Right(link, Strings.Len(link) - 1);
            if (ValidLinks.ContainsKey(link)) return link;
            if (link.EndsWith("s") && !link.EndsWith("ss"))
            {
                link = Strings.Left(link, Strings.Len(link) - 1);
            }
            else if(!link.EndsWith("ss"))
            {
                link = link + "s";
            }
            if (ValidLinks.ContainsKey(link)) return link;
            if (link.EndsWith("s") && !link.EndsWith("ss"))
            {
                link = Strings.Left(link, Strings.Len(link) - 1);
            }
            else if (!link.EndsWith("ss"))
            {
                link = link + "s";
            }
            if (ValidLinks.ContainsKey(link)) return link;
            string caps = Capitalize(link);
            string newHeader = "---\r\ntitle: " + Strings.Replace(caps, "_", " ") + "\r\npermalink: " + caps + "/\r\nlayout: wiki\r\n" + "---" + "\r\n\nThis article has gone missing.\r\n";
            if (SuggestedLinks.ContainsKey(link))
            {
                System.IO.File.Copy("C:\\keeperrl_wiki_suggestions\\" + link + ".md", "C:\\keeperrl_wiki\\Unfinished\\"+link + ".md");
                ValidLinks.Add(link, "C:\\keeperrl_wiki\\Unfinished\\" + link + ".md");
                return caps;
            }
            if (caps != link) newHeader = newHeader + "The capitalisation of the existing article is wrong: " + link + "\r\n";
            System.IO.File.WriteAllText("C:\\keeperrl_wiki\\Missing\\" + caps+".md", newHeader);
            return caps; 
        }

        private Dictionary<string, string> ValidLinks = new Dictionary<string, string>();
        private Dictionary<string, string> SuggestedLinks = new Dictionary<string, string>();
        private Dictionary<string, string> ValidPics = new Dictionary<string, string>();

        private void LoadValidLinks(string dir, Dictionary<string, string> dict)
        {
            if (System.IO.Path.GetFileName(dir).StartsWith(".")) return;
            if (System.IO.Path.GetFileName(dir).StartsWith("_")) return;
            foreach (String fil in System.IO.Directory.GetFiles(dir))
            {
                string key = System.IO.Path.GetFileNameWithoutExtension(fil);
                string nam = fil;
                if (!dict.ContainsKey(key)) dict.Add(key, nam);
            }
            foreach (String subdir in System.IO.Directory.GetDirectories(dir))
            {
                LoadValidLinks(subdir, dict);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewArticles();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadValidPics();
            //CapitalizeValidPics();
            LoadValidLinks("C:\\keeperrl_wiki\\", ValidLinks);
            ImportPictures("C:\\keeperrl_wiki\\");
        }

        private void CapitalizeValidPics()
        {
            return;
            foreach (KeyValuePair<string, string> kvp in ValidPics)
            {
                string caps = Capitalize(kvp.Value);
                System.IO.File.Copy(kvp.Value, kvp.Value + "_bak");
                System.IO.File.Delete(kvp.Value);
                if (System.IO.File.Exists(caps)) System.IO.File.Delete(caps);
                System.IO.File.Copy(kvp.Value + "_bak", caps);
                System.IO.File.Delete(kvp.Value + "_bak");
            }
            ValidPics = new Dictionary<string, string>();
            LoadValidPics();
        }

        private void LoadValidPics()
        {
            {
                foreach (String fil in System.IO.Directory.GetFiles("C:\\keeperrl_wiki_images"))
                {
                    string key = System.IO.Path.GetFileNameWithoutExtension(fil);
                    string nam = fil;
                    if (!ValidPics.ContainsKey(key)) ValidPics.Add(key.ToUpper(), nam);
                }
            }
        }

        private void ImportPictures(string dir)
        { 
            
            if (System.IO.Path.GetFileName(dir).StartsWith(".")) return;
            if (System.IO.Path.GetFileName(dir).StartsWith("_")) return;
            foreach (String fil in System.IO.Directory.GetFiles(dir))
            {
                if (fil.EndsWith(".md"))
                {
                    string text = System.IO.File.ReadAllText(fil);
                    if (text.Trim() == "")
                    {
                        System.IO.File.Delete(fil);
                    }
                    else
                    {
                        string newtext = text;
                        foreach (string pic in ValidPics.Keys)
                        {
                            if (text.ToUpper().Contains(pic.ToUpper() + ".PNG"))
                            {
                                string newFil = "C:\\keeperrl_wiki\\" + Capitalize(pic.ToLower()) + ".png";
                                if (System.IO.File.Exists(ValidPics[pic]) && !System.IO.File.Exists(newFil))
                                {
                                    System.IO.File.Copy(ValidPics[pic], newFil);
                                }
                            }
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, "/keeperrl_wiki/" + Capitalize(pic.ToLower()) + ".png", pic + ".png", 1, -1, CompareMethod.Text);
                            newtext = Strings.Replace(newtext, pic+".png", "/keeperrl_wiki/"+Capitalize(pic.ToLower())+".png",1,-1,CompareMethod.Text);
                        }
                        if (newtext != text)
                        {
                            System.IO.File.WriteAllText(fil, newtext);
                        }
                    }
                }
            }
            foreach (String subdir in System.IO.Directory.GetDirectories(dir))
            {
                ImportPictures(subdir);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dir = "C:\\keeperrl_wiki\\Creature\\Bestiary";
            string gid = "C:\\keeperrl_wiki\\Creature\\Creature_Guide.md";
            foreach (String fil in System.IO.Directory.GetFiles(dir))
            {
                string nam = System.IO.Path.GetFileNameWithoutExtension(fil);
                string namFriendly = Strings.Replace(nam, "_", " ");
                string text = System.IO.File.ReadAllText(fil);
                string link = "[" + namFriendly + "](/keeperrl_wiki/" + nam + " \"wikilink\")";
                System.IO.File.AppendAllText(gid, link + "\r\n");
                foreach (string line in Strings.Split(text, "\r\n"))
                {
                    if (line.Trim().StartsWith("<img src="))
                    {
                        System.IO.File.AppendAllText(gid, line + "\r\n");
                    }
                    if (line.Trim().StartsWith("''"))
                    {
                        System.IO.File.AppendAllText(gid, line + "\r\n");
                    }
                }
                System.IO.File.AppendAllText(gid, "\r\n");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            string text = System.IO.File.ReadAllText("C:\\keeperrl_wiki_dump\\wiki_dump.xml");
            string pageName = "";
            string pageText="";
            bool pageTextOn = false;
            string[] splitter = Strings.Split(text, "\r\n");
            //Need to loop each line and identify pages and write the Text to each page (overwriting previous writes, chronological order)
            int i = 0;
            while (i<splitter.Length)
            {
                string line = splitter[i];
                if (line.Contains("<title>"))
                {
                    pageName = Strings.Split(line, "<title>")[1];
                    pageName = Strings.Split(pageName, "</title>")[0];
                }
                if (line.Contains("<text"))
                {
                    pageTextOn = true;
                }
                if (pageTextOn)
                {
                    pageText = pageText + line + "\r\n";
                }
                if (line.Contains("</text>") && pageText!="")
                {
                    pageTextOn = false;
                    pageText = Strings.Split(pageText, "<text")[1];
                    pageText = Strings.Split(pageText, "</text>")[0];
                    string pageDelete = Strings.Split(pageText, ">")[0];
                    pageText = Strings.Replace(pageText, pageDelete + ">", "");
                    pageName = Capitalize(pageName);
                    string proposedDir = "C:\\keeperrl_wiki\\Old_Wiki\\" + "Old_" + pageName;
                    File.WriteAllText("C:\\keeperrl_wiki\\Old_Wiki\\Old_" + pageName + ".md", pageText);
                    if (!Directory.Exists(proposedDir)) Directory.CreateDirectory(proposedDir);
                    int j = 0;
                    while (File.Exists(proposedDir+ "\\" + pageName + "_" + j.ToString() + ".txt"))
                    {
                        j++;
                    }
                    File.WriteAllText(proposedDir + "\\" + pageName + "_" + j.ToString() + ".txt", pageText);
                    pageText = "";
                }
                i++;
            }
            StandardizeHeaders("C:\\keeperrl_wiki\\", "");
            button6.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            string dir = "C:\\keeperrl_wiki_old\\";
            {
                foreach (String fil in System.IO.Directory.GetFiles(dir))
                {
                    string CreatureDir = "C:\\keeperrl_wiki\\Creature\\Bestiary\\";
                    string text = System.IO.File.ReadAllText(fil);
                    string nam = Capitalize(System.IO.Path.GetFileNameWithoutExtension(fil).Trim());
                    nam = Strings.Replace(nam, "Old_", "");
                    foreach (string line in Strings.Split(text, "\r\n"))
                    {
                        if (line.StartsWith("[[File:") && line.Contains(".png"))
                        {
                            string png = Strings.Split(line,"[[File:")[1];
                            png = Strings.Split(png, ".png")[0];
                            string newline=Strings.Replace(line,line, "<img src=\"/keeperrl_wiki/Adventurer.png\" title=\"fig:/keeperrlwiki/Adventurer.png\" alt=\"/keeperrl_wiki/Adventurer.png\" width=\"100\" />");
                            newline = Strings.Replace(newline, "Adventurer", Capitalize(png));
                            text = Strings.Replace(text, line, newline);
                        }
                    }
                    text = Strings.Replace(text, "*", "-   ");
                    if (!text.Contains("[[Category: Creatures]]"))
                    {
                        System.IO.File.WriteAllText(fil, text);
                    }
                    else
                    {
                        System.IO.File.WriteAllText(CreatureDir + Capitalize(nam) + ".md", text);
                    }
                    System.IO.File.Delete(fil);
                }
            }
            StandardizeHeaders("C:\\keeperrl_wiki\\", "");
            button7.Enabled = true;
        }

        private void btnSetGitRepo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
            directchoosedlg.SelectedPath  = GitFolderPath;
            directchoosedlg.ShowNewFolderButton = false;
            directchoosedlg.Description = "Locate git Repository";
            directchoosedlg.RootFolder = Environment.SpecialFolder.MyComputer;
            if (directchoosedlg.ShowDialog() == DialogResult.OK)
            {
                GitFolderPath = directchoosedlg.SelectedPath;
                textBox1.Text = GitFolderPath;
            }
        }
    }
}
