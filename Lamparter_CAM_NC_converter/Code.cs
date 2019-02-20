using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lamparter_CAM_NC_converter
{
    public class Code
    {
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

        static string RenameProfile(string profile)
        {
            string newProfile = null;

            if (profile[0] == 'B' && profile[1] == 'L')
            {
                int firstStar = GetNthIndex(profile, '*', 1);
                newProfile = "BL " + profile.Substring(2, firstStar - 2);
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

        static int CorrectGrossLengh(int grossLength)
        {
            int newLegth = 0;

            return newLegth;
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

            if
                (Environment.UserName == "Galin")
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

        static string RenameBolt(string boltName, string DIN)
        {
            if (DIN == "6914" || DIN == "14399")
            {
                boltName = boltName.Substring(3, boltName.Length - 3);
                boltName = "6KT SCHR " + boltName + " MU2S";
            }

            if (DIN == "7990")
            {
                boltName = "6KT SCHR " + boltName + " MUS";
            }

            return boltName;
        }

        public int DoIt()
        {
            string path = string.Empty;
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
                Console.WriteLine("Greshno ime");
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
            ////revision is saved in rev

            string ID = GetID();
            DateTime todayDate = DateTime.Now;
            string format = "ddMMyyyy";
            Console.Write("Drawing name: ");
            string drawingName = Console.ReadLine();

            if (drawingName == string.Empty)
            {
                drawingName = "name";
            }

            string pos = string.Empty, mainPos = string.Empty, profile = string.Empty, length = string.Empty, width = string.Empty, numberMain = string.Empty, number = string.Empty, material = string.Empty, partName = string.Empty, rest = string.Empty;
            string boltName = string.Empty, boltLength = string.Empty, boltPos = string.Empty, DIN = string.Empty;
            StreamWriter revCam = new StreamWriter("rev_" + path, true, Encoding.Default);

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
                    length = camRow[i].Substring(GetNthIndex(camRow[i], '#', 2) + 1, GetNthIndex(camRow[i], '#', 3) - GetNthIndex(camRow[i], '#', 2) + 1);
                    width = camRow[i].Substring(GetNthIndex(camRow[i], '#', 3) + 1, GetNthIndex(camRow[i], '#', 4) - GetNthIndex(camRow[i], '#', 3) + 1);
                    mainPos = camRow[i].Substring(GetNthIndex(camRow[i], '#', 4) + 1, GetNthIndex(camRow[i], '#', 5) - GetNthIndex(camRow[i], '#', 4) + 1);
                    mainPos = RenameMainPos(mainPos);
                    numberMain = camRow[i].Substring(GetNthIndex(camRow[i], '#', 5) + 1, GetNthIndex(camRow[i], '#', 6) - GetNthIndex(camRow[i], '#', 5) + 1);
                    material = camRow[i].Substring(GetNthIndex(camRow[i], '#', 6) + 1, GetNthIndex(camRow[i], '#', 7) - GetNthIndex(camRow[i], '#', 6) + 1);
                    partName = camRow[i].Substring(GetNthIndex(camRow[i], '#', 9) + 1, GetNthIndex(camRow[i], '#', 10) - GetNthIndex(camRow[i], '#', 9) + 1);
                    partName = RenameName(partName);
                    rest = camRow[i].Substring(GetNthIndex(camRow[i], '#', 10) + 1, GetNthIndex(camRow[i], '#', 25) - GetNthIndex(camRow[i], '#', 10));

                    NRows.Add(mainPos, "N#" + mainPos + ".nc");

                    output.Append("H#" + profile + "#" + length + "#" + width + "#" + mainPos + "#" + numberMain + "#" + material + "###" + partName + "#" + rest).AppendLine();
                }

                if (camRow[i].Substring(0, 1) == "W")
                {
                    profile = camRow[i].Substring(2, GetNthIndex(camRow[i], '#', 2) - 2);
                    profile = RenameProfile(profile);
                    length = camRow[i].Substring(GetNthIndex(camRow[i], '#', 2) + 1, GetNthIndex(camRow[i], '#', 3) - GetNthIndex(camRow[i], '#', 2) + 1);
                    width = camRow[i].Substring(GetNthIndex(camRow[i], '#', 3) + 1, GetNthIndex(camRow[i], '#', 4) - GetNthIndex(camRow[i], '#', 3) + 1);
                    pos = camRow[i].Substring(GetNthIndex(camRow[i], '#', 4) + 1, GetNthIndex(camRow[i], '#', 5) - GetNthIndex(camRow[i], '#', 4) + 1);
                    pos = RenameMainPos(pos);
                    number = camRow[i].Substring(GetNthIndex(camRow[i], '#', 5) + 1, GetNthIndex(camRow[i], '#', 6) - GetNthIndex(camRow[i], '#', 5) + 1);
                    material = camRow[i].Substring(GetNthIndex(camRow[i], '#', 6) + 1, GetNthIndex(camRow[i], '#', 7) - GetNthIndex(camRow[i], '#', 6) + 1);
                    partName = camRow[i].Substring(GetNthIndex(camRow[i], '#', 9) + 1, GetNthIndex(camRow[i], '#', 10) - GetNthIndex(camRow[i], '#', 9) + 1);
                    partName = RenameName(partName);
                    rest = camRow[i].Substring(GetNthIndex(camRow[i], '#', 10) + 1, GetNthIndex(camRow[i], '#', 25) - GetNthIndex(camRow[i], '#', 10));

                    if (!NRows.ContainsKey(pos))
                    {
                        NRows.Add(pos, "N#" + pos + ".nc");
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

                    boltName = RenameBolt(boltName, DIN);

                    output.Append("S#" + boltName + "#" + boltLength + "##" + boltPos + "#####" + DIN + "################").AppendLine();
                }
            }

            List<string> nc = new List<string>();

            output.Replace('#', '&');
            using (revCam)
            {
                revCam.Write(output);
                foreach (KeyValuePair<string, string> item in NRows.OrderBy(key => key.Value))
                {
                    revCam.WriteLine(item.Value.ToString());
                    nc.Add(item.Value.ToString().Replace("N#", string.Empty));
                }
            }

            ////nc
            string ncName = null;

            foreach (var item in nc)
            {
                counter = 0;
                Console.WriteLine(item);

                if (!File.Exists(item))
                {
                    ncName = "H" + item;
                }
                else
                {
                    ncName = item;
                }

                StreamReader ncReader = new StreamReader(ncName);
                int ncLines = File.ReadAllLines(ncName).Count();
                string[] ncRow = new string[ncLines];

                while ((line = ncReader.ReadLine()) != null)
                {
                    ncRow[counter] = line;
                    counter++;
                }

                ncReader.Close();

                StreamWriter ncWriter = new StreamWriter("rev_" + item);

                using (ncWriter)
                {
                    ncWriter.WriteLine(ncRow[0]);
                    ncWriter.WriteLine(ncRow[2]);
                    ncWriter.WriteLine(ncRow[3]);
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
                        ncWriter.WriteLine(ncRow[i]);
                    }
                }
            }

            return 0;
        }
    }
}
