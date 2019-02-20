using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Test_forms
{
    public partial class Form1 : Form
    {
        public string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string filePath = string.Empty;
        public string filePathFull = string.Empty;
        public string filePathOnly = string.Empty;
        public string fileNameGlobal = string.Empty;
        public string projectNumber = string.Empty;
        public string filePathCheck = string.Empty;
        public string filePathFullCheck = string.Empty;
        public string filePathOnlyCheck = string.Empty;
        public string fileNameGlobalCheck = string.Empty;
        public bool isLoadedCheckList = false;

        private Dictionary<string, int> DSTVList = new Dictionary<string, int>();

        public string FilePath()
        {
            return filePath;
        }

        public string DesktopPath()
        {
            return desktopPath;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;

            ProjNum();

            bool directoryExists = Directory.Exists(@"D:\Lamparter\" + projectNumber + @"\" + filePath);

            if (directoryExists)
            {
                System.IO.Directory.Delete(@"D:\Lamparter\" + projectNumber + @"\" + filePath, true);
            }

            System.IO.Directory.CreateDirectory(@"D:\Lamparter\" + projectNumber + @"\" + filePath);

            if (isLoadedCheckList == false && fileNameGlobalCheck != string.Empty)
            {
                FillDSTVList(filePathFullCheck);
            }

            /*
            //to delete
            string all = string.Empty;
            
            foreach (KeyValuePair<string, int> kvp in DSTVList)
            {
                all = all + "\nPos " + kvp.Key + " , Length = " + kvp.Value;
            }

            MessageBox.Show(all);
            //to here
            */

            if (isLoadedCheckList == false)
            {
                MessageBox.Show("No list is loaded. If NC is missing check the length in CAM manually");
            } 

            DoIt();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XSR|*.xsr|CAM|*.cam|All|*.*";
            ofd.Title = "Select cam or xsr";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label3.Text = ofd.FileName.ToString();
                filePath = ofd.FileName.ToString();
                filePathFull = ofd.FileName.ToString();

                int lastDotIndex = filePath.LastIndexOf('.');
                int lastSlashIndex = filePath.LastIndexOf(@"\");
                filePathOnly = filePath.Substring(0, lastSlashIndex + 1);
                fileNameGlobal = filePath.Substring(lastSlashIndex + 1, filePath.Length - lastSlashIndex - 1);
                filePath = filePath.Substring(lastSlashIndex + 1, lastDotIndex - lastSlashIndex - 1);
                //label1.Text = filePathOnly + fileNameGlobal;
                label6.Text = "Selected: " + fileNameGlobal;
            }
        }
        
        private void label3_Click(object sender, EventArgs e)
        { 
        }
        
        static int GetNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public string RenameProfile(string profile)
        {
            string newProfile = null;

            if (profile[0] == 'B' && profile[1] == 'L')
            {
                int firstStar = GetNthIndex(profile, '*', 1);
                if (firstStar == -1)
                {
                    newProfile = "BL " + profile.Substring(2, profile.Length - 2);
                }
                else
                {
                    newProfile = "BL " + profile.Substring(2, firstStar - 2);
                }
                ////just test
                label4.Text = firstStar.ToString();
                ////end test
            }
            else
            {
                int firstNumber = profile.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                newProfile = profile.Substring(0, firstNumber) + " " + profile.Substring(firstNumber, profile.Length - firstNumber);
                newProfile = newProfile.Replace("*", "X");
                newProfile = newProfile.Replace("  ", " ");

                if (newProfile.Substring(0, 2) == "RR" || newProfile.Substring(0, 2) == "RO" || newProfile.Substring(0, 3) == "RHS" || newProfile.Substring(0, 2) == "RO")
                {
                    newProfile = newProfile.Replace(".", ",");

                    string temp = newProfile.Substring(newProfile.Length - 3, 3);

                    if (temp[0] == 'X')
                    {
                        newProfile = newProfile + ",0";
                    }
                    else
                    {
                        if (temp[1] == 'X')
                        {
                            newProfile = newProfile + ",0";
                        }
                    }
                }
            }

            ////ne e napravena proverkata za kutienite secheniq RR160*80*5 -> RR 160X80X5,0
            return newProfile;
        }

        static string RenameMainPos(string mainPos)
        {
            string newMainPos = null;

            if (mainPos[0] == 'H')
            {
                newMainPos = mainPos.Substring(1, mainPos.Length - 1);
            }
            else
            {
                newMainPos = mainPos;
            }

            return newMainPos;
        }

        static string RenameName(string name)
        {
            string newName = name;
            newName.Replace("ü", "ue").Replace("ä", "ae").Replace("ö", "oe").Replace("ß", "ss");
            name.Replace("ü", "ue").Replace("ä", "ae").Replace("ö", "oe").Replace("ß", "ss");

            if (name == "PLATE")
            {
                newName = "Blech";
            }

            if (name == "BEAM")
            {
                newName = "Traeger";
            }

            if (name == "COLUMN")
            {
                newName = "Stuetze";
            }

            if (name == "Plate")
            {
                newName = "Blech";
            }

            if (name == "Beam")
            {
                newName = "Traeger";
            }

            if (name == "Column")
            {
                newName = "Stuetze";
            }

            if (name == "plate")
            {
                newName = "Blech";
            }

            if (name == "beam")
            {
                newName = "Traeger";
            }

            if (name == "column")
            {
                newName = "Stuetze";
            }

            if (name == "Träger")
            {
                newName = "Traeger";
            }

            if (name == "Stütze")
            {
                newName = "Stuetze";
            }

            return newName;
        }

        static string GetID()
        {
            string ID = Environment.UserName;

            if (Environment.UserName == "Nikola")
            {
                ID = "PME/NMS";
            }

            if (Environment.UserName == "Goro")
            {
                ID = "PME/GPM";
            }

            if (Environment.UserName == "Aneliya")
            {
                ID = "PME/AGY";
            }

            if (Environment.UserName == "Boyan Abdo")
            {
                ID = "PME/BNA";
            }

            if (Environment.UserName == "Boris")
            {
                ID = "PME/BGP";
            }

            if (Environment.UserName == "Chavdar")
            {
                ID = "PME/CHS";
            }

            if (Environment.UserName == "Delyana")
            {
                ID = "PME/DSY";
            }

            if (Environment.UserName == "Doch")
            {
                ID = "PME/DGG";
            }

            if (Environment.UserName == "Eva")
            {
                ID = "PME/ENA";
            }

            if (Environment.UserName == "Galin")
            {
                ID = "PME/GKK";
            }

            if (Environment.UserName == "Georgi")
            {
                ID = "PME/GSK";
            }

            if (Environment.UserName == "Hristo")
            {
                ID = "PME/HBY";
            }

            if (Environment.UserName == "Ismeth")
            {
                ID = "PME/IMH";
            }

            if (Environment.UserName == "Ivan")
            {
                ID = "PME/ISP";
            }

            if (Environment.UserName == "Joro")
            {
                ID = "PME/GDG";
            }

            if (Environment.UserName == "Maria")
            {
                ID = "PME/MIO";
            }

            if (Environment.UserName == "Mariqna")
            {
                ID = "PME/MDK";
            }

            if (Environment.UserName == "Milko")
            {
                ID = "PME/MSM";
            }

            if (Environment.UserName == "nexo")
            {
                ID = "PME/PGG";
            }

            if (Environment.UserName == "Niki")
            {
                ID = "PME/NVN";
            }

            if (Environment.UserName == "Pepsi")
            {
                ID = "PME/PMY";
            }

            if (Environment.UserName == "Plamen")
            {
                ID = "PME/PDM";
            }

            if (Environment.UserName == "PM")
            {
                ID = "PME/GPM";
            }

            if (Environment.UserName == "Rado")
            {
                ID = "PME/RKG";
            }

            if (Environment.UserName == "Rumy")
            {
                ID = "PME/RAM";
            }

            if (Environment.UserName == "Silvia")
            {
                ID = "PME/SZP";
            }

            if (Environment.UserName == "Soff")
            {
                ID = "PME/SPA";
            }

            if (Environment.UserName == "Stefan")
            {
                ID = "PME/SVD";
            }

            if (Environment.UserName == "Svilen")
            {
                ID = "PME/SSS";
            }

            if (Environment.UserName == "Tedy")
            {
                ID = "PME/TRU";
            }

            if (Environment.UserName == "Tim")
            {
                ID = "PME/TIM";
            }

            if (Environment.UserName == "Tina")
            {
                ID = "PME/RVM";
            }

            if (Environment.UserName == "Vanko")
            {
                ID = "PME/IDD";
            }

            if (Environment.UserName == "Yavor")
            {
                ID = "PME/YMY";
            }

            return ID;
        }

        static string RenameDIN(string DIN)
        {
            if (DIN == "EN-14399-4")
            {
                DIN = "EN 14399-4-6 10.9 HV TZN";
            }

            ////rev9
            if (DIN == "13918")
            {
                DIN = "EN 13918";
            }

            if (DIN == "ISO 4017" || DIN == "4017" || DIN == "ISO4017" || DIN == "933")
            {
                DIN = "ISO 4017-7090-7090-4032";
            }

            if (DIN == "ISO 4014" || DIN == "4014" || DIN == "ISO4014" || DIN == "931")
            {
                DIN = "ISO 4014-7090-7090-4032";
            }

            if (DIN == "ISO4032+ISO7089")
            {
                DIN = "ISO 4032-7090";
            }

            if (DIN == "Mutter-EN-14399-4")
            {
                DIN = "EN 14399-4-6";
            }

            if (DIN == "ISO4014 + 4035" || DIN == "4014+4035")
            {
                DIN = "ISO 4014-7090-7090-4032-4035";
            }

            if (DIN == "ISO4017 + 4035" || DIN == "4017+4035")
            {
                DIN = "ISO 4017-7090-7090-4032-4035";
            }

            return DIN;
        }

        static string RenameBolt(string boltName, string DIN, string boltLength)
        {
            if (DIN == "6914" || DIN == "14399" || DIN == "EN-14399-4" || DIN== "EN 14399-4-6 10.9 HV TZN")
            {
                boltName = boltName.Substring(3, boltName.Length - 3);
                boltName = "6KT SCHR " + boltName + " MU2S";
            }

            if (DIN == "7990")
            {
                boltName = "6KT SCHR " + boltName + " MUS";
            }

            if (DIN == "13918" || DIN == "EN 13918")
            {
                boltName = "SCHR " + boltName.Substring(13, boltName.Length - 13);
            }

            ////rev 9
            if (DIN == "933" || DIN == "ISO 4017" || DIN == "ISO 4017-7090-7093-7042" || DIN == "ISO4017")
            {
                boltName = "6KT SCHR " + boltName + " MU2S";
            }

            //// rev 9
            if (DIN == "931" || DIN == "ISO 4014" || DIN == "ISO 4014-7090-7093-7042" || DIN == "ISO4014")
            {
                boltName = "6KT SCHR " + boltName + " MU2S";
            }

            if (DIN == "ISO4014 + 4035" || DIN == "4014+4035" || DIN == "ISO 4014-7090-7090-4032-4035")
            {
                boltName = "6KT SCHR " + boltName + " 2MU2S";
            }

            if (DIN == "ISO4017 + 4035" || DIN == "4017+4035" || DIN == "ISO 4017-7090-7090-4032-4035")
            {
                boltName = "6KT SCHR " + boltName + " 2MU2S";
            }
            

            if (DIN == "4762")
            {
                boltName = "ZYL.SCHRAUBE " + boltName + "X" + boltLength;
            }

            if (DIN == "Mutter-EN-14399-4")
            {
                boltName = "14399-4-MU" + boltName.Substring(11, 2);
            }

            return boltName;
        }

        public int ProjNum()
        {
            string path = filePathFull;
            string line;
            int counter = 0;
            StreamReader cam = new StreamReader(path, System.Text.Encoding.Default);
            int linesCount = File.ReadAllLines(path).Count();
            string[] camRow = new string[linesCount];

            while ((line = cam.ReadLine()) != null)
            {
                camRow[counter] = line;
                counter++;
            }

            cam.Close();
            projectNumber = camRow[2].Remove(0, 2).Replace(" ", string.Empty);
            return 0;
        }

        public void FillDSTVList(string path)
        {
            StreamReader list = new StreamReader(path, System.Text.Encoding.Default);

            int linesCount = File.ReadAllLines(path).Count();
            string line;

            while ((line = list.ReadLine()) != null)
            {
                string[] rowValues = line.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (rowValues[0] != "pos")
                {
                    if (rowValues[0][0]=='H')
                    {
                        string mainPosNumber = rowValues[0];
                        mainPosNumber = mainPosNumber.Substring(1);
                        DSTVList.Add(mainPosNumber, int.Parse(rowValues[1]));
                    }
                    else
                    {
                        DSTVList.Add(rowValues[0], int.Parse(rowValues[1]));
                    }
                }
            }

            list.Close();
            isLoadedCheckList = true;
        }

        

        public int GetNCLength(string pos)
        {
            int length = 0;
            string ncName = string.Empty;
            string lengthLine = string.Empty;

            if (File.Exists(filePathOnly + pos + ".nc"))
            {
                ncName = filePathOnly + pos + ".nc";
            }
            else if (File.Exists(filePathOnly + "H" + pos + ".nc"))
            {
                ncName = filePathOnly + "H" + pos + ".nc";
            }
            else
            {
                ncName = string.Empty;
            }

            if (ncName != string.Empty)
            {
                StreamReader ncReader = new StreamReader(ncName);
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();
                ncReader.ReadLine();

                lengthLine = ncReader.ReadLine();

                ncReader.Close();

                string[] lengths = lengthLine.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                decimal netLength = Convert.ToDecimal(lengths[0], CultureInfo.InvariantCulture);
                length = Convert.ToInt32(netLength);
                //MessageBox.Show(lengthLine + " : " + netLength + " - " + length);
            }

            //Rev 8
            if (isLoadedCheckList == true)
            {
                int newLength = 0;
                if (!File.Exists(filePathOnly + pos + ".nc"))
                {
                    if (DSTVList.TryGetValue(pos, out newLength))
                    {
                        length = newLength;
                        MessageBox.Show("Length from list: " + pos + ": " + newLength);
                    }
                    else
                    {
                        MessageBox.Show("Check length in cam for pos." + pos + " manually!!!");
                    }
                }
            }
            
            return length;
        }

        public string ChangeLengthInNC(string row)
        {
            string[] lengths = row.Split(new[] { ',' });
            return lengths[0];
        }

        public int FindNetLengthFromList()
        {
            int length = 0;

            Dictionary<string, int> listPosLength = new Dictionary<string, int>();




            return length;
        }

        public int DoIt()
        {
            string path = filePathFull;
            string line;

            ////test if the file exists
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException();
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Greshno ime");
                return 0;
            }

            ////save the file into a string array
            int counter = 0;
            StreamReader cam = new StreamReader(path, System.Text.Encoding.Default);
            int linesCount = File.ReadAllLines(path).Count();
            string[] camRow = new string[linesCount];

            while ((line = cam.ReadLine()) != null)
            {
                camRow[counter] = line;
                counter++;
            }

            cam.Close();

            ////file saved into camRow[]
            path = fileNameGlobal;
            
            ////search for revision in the name of the file
            int indexRev = path.IndexOfAny(new char[] { '_', '-' });
            string rev = string.Empty;
            string fileName = string.Empty;
            if (indexRev != -1)
            {
                int indexRevEnd = path.IndexOf('.');
                rev = path.Substring(indexRev + 1, path.Length - indexRevEnd - indexRev);
                fileName = path.Substring(0, indexRev);
            }
            else
            {
                rev = "00";
                int indexNameEnd = path.IndexOf('.');
                fileName = path.Substring(0, indexNameEnd);
            }

            string ID = GetID();
            DateTime todayDate = DateTime.Now;
            string format = "ddMMyyyy";
            string drawingName = textBox1.Text;

            if (drawingName == string.Empty)
            {
                drawingName = "name";
            }

            string pos = string.Empty, mainPos = string.Empty, profile = string.Empty, length = string.Empty, width = string.Empty, numberMain = string.Empty, number = string.Empty, material = string.Empty, partName = string.Empty, rest = string.Empty;
            string boltName = string.Empty, boltLength = string.Empty, boltPos = string.Empty, DIN = string.Empty;
            path = path.Replace(".xsr", ".cam");

            MessageBox.Show(@"D:\Lamparter\" + projectNumber + @"\" + filePath + @"\" + "rev_" + path);
            StreamWriter revCam = new StreamWriter(@"D:\Lamparter\" + projectNumber + @"\" + filePath + @"\" + "rev_" + path);

            StringBuilder output = new StringBuilder();
            Dictionary<string, string> NRows = new Dictionary<string, string>();

            output.Append("0#").AppendLine();
            output.Append("1#01").AppendLine();
            output.Append(camRow[2]).AppendLine();
            output.Append("3######").AppendLine();
            output.Append("4#" + fileName + "#" + rev).AppendLine();
            output.Append("5#" + ID).AppendLine();
            output.Append("9###" + drawingName + "###" + ID + "#" + todayDate.ToString(format) + "####").AppendLine();

            for (int i = 7; i < linesCount; i++)
            {
                if (camRow[i].Substring(0, 1) == "H")
                {
                    profile = camRow[i].Substring(2, GetNthIndex(camRow[i], '#', 2) - 2);
                    profile = RenameProfile(profile);
                    length = camRow[i].Substring(GetNthIndex(camRow[i], '#', 2) + 1, GetNthIndex(camRow[i], '#', 3) - GetNthIndex(camRow[i], '#', 2) - 1);
                    width = camRow[i].Substring(GetNthIndex(camRow[i], '#', 3) + 1, GetNthIndex(camRow[i], '#', 4) - GetNthIndex(camRow[i], '#', 3) - 1);
                    mainPos = camRow[i].Substring(GetNthIndex(camRow[i], '#', 4) + 1, GetNthIndex(camRow[i], '#', 5) - GetNthIndex(camRow[i], '#', 4) - 1);
                    mainPos = RenameMainPos(mainPos);
                    numberMain = camRow[i].Substring(GetNthIndex(camRow[i], '#', 5) + 1, GetNthIndex(camRow[i], '#', 6) - GetNthIndex(camRow[i], '#', 5) - 1);
                    material = camRow[i].Substring(GetNthIndex(camRow[i], '#', 6) + 1, GetNthIndex(camRow[i], '#', 7) - GetNthIndex(camRow[i], '#', 6) - 1);
                    partName = camRow[i].Substring(GetNthIndex(camRow[i], '#', 9) + 1, GetNthIndex(camRow[i], '#', 10) - GetNthIndex(camRow[i], '#', 9) - 1);
                    partName = RenameName(partName);
                    rest = camRow[i].Substring(GetNthIndex(camRow[i], '#', 10) + 1, GetNthIndex(camRow[i], '#', 25) - GetNthIndex(camRow[i], '#', 10));

                    int ncLength = GetNCLength("H"+mainPos);
                    if (ncLength != 0)
                    {
                        if (ncLength.ToString() != length)
                        {
                            length = ncLength.ToString();
                            //MessageBox.Show(mainPos + ":" + length + "->" + ncLength);
                        }
                    }

                    NRows.Add(mainPos, "N#" + mainPos + ".nc");

                    output.Append("H#" + profile + "#" + length + "#" + width + "#" + mainPos + "#" + numberMain + "#" + material + "###" + partName + "#" + rest).AppendLine();
                }

                if (camRow[i].Substring(0, 1) == "W")
                {
                    profile = camRow[i].Substring(2, GetNthIndex(camRow[i], '#', 2) - 2);
                    profile = RenameProfile(profile);
                    length = camRow[i].Substring(GetNthIndex(camRow[i], '#', 2) + 1, GetNthIndex(camRow[i], '#', 3) - GetNthIndex(camRow[i], '#', 2) - 1);
                    width = camRow[i].Substring(GetNthIndex(camRow[i], '#', 3) + 1, GetNthIndex(camRow[i], '#', 4) - GetNthIndex(camRow[i], '#', 3) - 1);
                    pos = camRow[i].Substring(GetNthIndex(camRow[i], '#', 4) + 1, GetNthIndex(camRow[i], '#', 5) - GetNthIndex(camRow[i], '#', 4) - 1);
                    pos = RenameMainPos(pos);
                    number = camRow[i].Substring(GetNthIndex(camRow[i], '#', 5) + 1, GetNthIndex(camRow[i], '#', 6) - GetNthIndex(camRow[i], '#', 5) - 1);
                    material = camRow[i].Substring(GetNthIndex(camRow[i], '#', 6) + 1, GetNthIndex(camRow[i], '#', 7) - GetNthIndex(camRow[i], '#', 6) - 1);
                    partName = camRow[i].Substring(GetNthIndex(camRow[i], '#', 9) + 1, GetNthIndex(camRow[i], '#', 10) - GetNthIndex(camRow[i], '#', 9) - 1);
                    partName = RenameName(partName);
                    rest = camRow[i].Substring(GetNthIndex(camRow[i], '#', 10) + 1, GetNthIndex(camRow[i], '#', 25) - GetNthIndex(camRow[i], '#', 10));

                    if (!NRows.ContainsKey(pos))
                    {
                        NRows.Add(pos, "N#" + pos + ".nc");
                    }

                    int ncLength = GetNCLength(pos);
                    if(ncLength != 0)
                    {
                        if (ncLength.ToString() != length)
                        {
                            length = ncLength.ToString();
                            //MessageBox.Show(pos + ":" + length + "->" + ncLength);
                        }
                    }

                    output.Append("W#" + profile + "#" + length + "#" + width + "#" + pos + "#" + number + "#" + material + "###" + partName + "#" + rest).AppendLine();
                }

                if (camRow[i].Substring(0, 1) == "A")
                {
                    pos = camRow[i].Substring(2, GetNthIndex(camRow[i], '#', 2) - 2);
                    number = length = camRow[i].Substring(GetNthIndex(camRow[i], '#', 2) + 1, GetNthIndex(camRow[i], '#', 3) - (GetNthIndex(camRow[i], '#', 2) + 1));
                    mainPos = camRow[i].Substring(GetNthIndex(camRow[i], '#', 3) + 1, GetNthIndex(camRow[i], '#', 4) - (GetNthIndex(camRow[i], '#', 3) + 1));
                    mainPos = RenameMainPos(mainPos);

                    output.Append("A#" + pos + "#" + number + "#" + mainPos + "#").AppendLine();
                }

                if (camRow[i].Substring(0, 1) == "S")
                {
                    boltName = camRow[i].Substring(2, GetNthIndex(camRow[i], '#', 2) - 2);
                    boltLength = camRow[i].Substring(GetNthIndex(camRow[i], '#', 2) + 1, GetNthIndex(camRow[i], '#', 3) - (GetNthIndex(camRow[i], '#', 2) + 1));
                    boltPos = camRow[i].Substring(GetNthIndex(camRow[i], '#', 4) + 1, GetNthIndex(camRow[i], '#', 5) - (GetNthIndex(camRow[i], '#', 4) + 1));
                    DIN = camRow[i].Substring(GetNthIndex(camRow[i], '#', 9) + 1, GetNthIndex(camRow[i], '#', 10) - (GetNthIndex(camRow[i], '#', 9) + 1));
                    boltName = RenameBolt(boltName, DIN, boltLength);
                    DIN = RenameDIN(DIN);

                    output.Append("S#" + boltName + "#" + boltLength + "##" + boltPos + "#BOLT_XLS_QTY####" + DIN + "################").AppendLine();
                }
            }

            List<string> nc = new List<string>();

            output.Replace('#', '&');
            using (revCam)
            {
                revCam.Write(output);

                foreach (KeyValuePair<string, string> item in NRows.OrderBy(key => key.Value))
                {
                    revCam.WriteLine(item.Value.ToString().Replace('#', '&'));
                    nc.Add(item.Value.ToString().Replace("N#", string.Empty));
                }
            }

            ////nc
            string ncName = null;
            foreach (var item in nc)
            {
                counter = 0;

                if (File.Exists(filePathOnly + item))
                {
                    ncName = filePathOnly + item;
                }
                else if (File.Exists(filePathOnly + "H" + item))
                {
                    ncName = filePathOnly + "H" + item;
                }
                else
                {
                    ncName = string.Empty;
                    MessageBox.Show(item + " is missing\nMaybe curved beam?!");
                }

                if (ncName != string.Empty)
                {
                    StreamReader ncReader = new StreamReader(ncName);
                    int ncLines = File.ReadAllLines(ncName).Count();
                    string[] ncRow = new string[ncLines];

                    while ((line = ncReader.ReadLine()) != null)
                    {
                        ncRow[counter] = line;
                        counter++;
                    }

                    ncReader.Close();

                    bool metBO = false;
                    StreamWriter ncWriter = new StreamWriter(@"D:\Lamparter\" + projectNumber + @"\" + filePath + @"\" + "rev_" + item);

                    using (ncWriter)
                    {
                        ncWriter.WriteLine(ncRow[0]);
                        ncWriter.WriteLine(ncRow[2]);
                        ////ncWriter.WriteLine(ncRow[3]);
                        ncWriter.WriteLine("  " + fileName);
                        string ncPos = ncRow[4].Replace("  ", string.Empty);

                        ncPos = RenameMainPos(ncPos);
                        ncWriter.WriteLine("  " + ncPos);
                        ncWriter.WriteLine("  " + ncPos);
                        ncWriter.WriteLine(ncRow[6]);
                        ncWriter.WriteLine(ncRow[7]);
                        string ncProfile = ncRow[8].Replace("  ", string.Empty);

                        ncProfile = RenameProfile(ncProfile);
                        ncWriter.WriteLine("  " + ncProfile);
                        for (int i = 9; i < ncLines; i++)
                        {
                            if (i == 10)
                            {
                                ncRow[i] = ChangeLengthInNC(ncRow[i]);
                            }

                            if (ncRow[i] == "BO")
                            {
                                metBO = true;
                            }

                            if (metBO)
                            {
                                ncWriter.WriteLine(ncRow[i].Replace("       5.00", "m      0.00"));
                            }
                            else
                            {
                                ncWriter.WriteLine(ncRow[i]);
                            }
                        }

                        metBO = false;
                    }
                }
            }
        
            return 0;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "CSV|*.csv|All|*.*";
            ofd1.Title = "Select csv";

            if (ofd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label5.Text = "Selected: " + ofd1.FileName.ToString();
                filePathCheck = ofd1.FileName.ToString();
                filePathFullCheck = ofd1.FileName.ToString();

                int lastDotIndex = filePathCheck.LastIndexOf('.');
                int lastSlashIndex = filePathCheck.LastIndexOf(@"\");
                filePathOnlyCheck = filePathCheck.Substring(0, lastSlashIndex + 1);
                fileNameGlobalCheck = filePathCheck.Substring(lastSlashIndex + 1, filePathCheck.Length - lastSlashIndex - 1);
                filePathCheck = filePathCheck.Substring(lastSlashIndex + 1, lastDotIndex - lastSlashIndex - 1);
                label1.Text = filePathOnlyCheck + fileNameGlobalCheck;

                label5.Text = "Selected: " + fileNameGlobalCheck;
            }
        }


    }
}