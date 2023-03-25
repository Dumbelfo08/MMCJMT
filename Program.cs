using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MMCJMT
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isQuiet = false;
			int argIncrease = 0;	
			
			Console.WriteLine("MultiMC Jar Modding Toolbox initialized - Created by Dumbelfo");
			if (args.Length==0) {
				Console.WriteLine("Incorrect syntaxis. Use -h te get help");
				Environment.Exit(0);
			}
			
			if (args[0]=="q" || args[0]=="-q"){
				isQuiet = true;
				argIncrease = 1;
			}
			
			if (args.Length==argIncrease) {
				Console.WriteLine("Incorrect syntaxis. Use -h te get help");
				Environment.Exit(0);
			}

			if (args[0]=="h" || args[0]=="-h" || args[0]=="help"){
				Console.WriteLine("HELP MENU");
				Console.WriteLine("");
				Console.WriteLine("Command arguments:");
				Console.WriteLine("");
				Console.WriteLine("-h                                                        : Get help");
				Console.WriteLine("-v                                                        : See version");
				Console.WriteLine("-a <Path to jar mod> <Multimc instance path>              : Add jar mod to multimc instance");
				Console.WriteLine("-av <Path to jar mod> <Multimc instance path> <version>   : Add jar mod to multimc instance issuing a version that will appear");
				Console.WriteLine("-d <File name of jar mod> <Multimc instance path>         : Delete jar mod in multimc instance. IMPORTANT: This will only work if the jar mod was added using this program");
				Console.WriteLine("-n <Name of instance> <Instance version> <Multimc path>   : Creates new instance with the specified minecraft version");
			}
			else if (args[0]=="v" || args[0]=="-v"){
				Console.WriteLine("Using version 1.2.2");
			}
			else if (args[argIncrease]=="a" || args[argIncrease]=="-a"){
				if (args.Length==argIncrease+3){
					string jarmod = "";
					string outputF = "";
					string jarmodName = "";
					string jarmodFile = "";
					jarmod = args[argIncrease+1];
					outputF = args[argIncrease+2];
					writeIfnotQuiet(isQuiet,"ADD JAR");
					if (File.Exists(jarmod)){
						jarmodFile = System.IO.Path.GetFileName(jarmod);
						jarmodName = jarmodFile.Substring(0, jarmodFile.Length - 4);
						if(Directory.Exists(outputF)){
							Direc(outputF + @"\jarmods");
							Direc(outputF + @"\patches");
							copyFileJar(jarmod, jarmodFile, outputF);
							addJson(jarmodName, jarmodFile, outputF, "1.0");
							addJar(jarmodName, outputF);
							writeIfnotQuiet(isQuiet,"Operation succesful");
						} else {
							Console.WriteLine("Instance folder not found");
						}
					} else {
						Console.WriteLine("File not found");
					}
				}  else {
					Console.WriteLine("Incorrect syntaxis. Use -h te get help");
					Environment.Exit(0);
				}
			}
			else if (args[argIncrease]=="av" || args[argIncrease]=="-av"){
				if (args.Length==argIncrease+4){
					string jarmod = "";
					string outputF = "";
					string jarmodName = "";
					string jarmodFile = "";
					string ver = "1.0";
					jarmod = args[argIncrease+1];
					outputF = args[argIncrease+2];
					ver = args[argIncrease+3];
					writeIfnotQuiet(isQuiet,"ADD JAR VERSION");
					if (File.Exists(jarmod)){
						jarmodFile = System.IO.Path.GetFileName(jarmod);
						jarmodName = jarmodFile.Substring(0, jarmodFile.Length - 4);
						if(Directory.Exists(outputF)){
							Direc(outputF + @"\jarmods");
							Direc(outputF + @"\patches");
							copyFileJar(jarmod, jarmodFile, outputF);
							addJson(jarmodName, jarmodFile, outputF, ver);
							addJar(jarmodName, outputF);
							writeIfnotQuiet(isQuiet,"Operation succesful");
						} else {
							Console.WriteLine("Instance folder not found");
						}
					} else {
						Console.WriteLine("File not found");
					}
				}  else {
					Console.WriteLine("Incorrect syntaxis. Use -h te get help");
					Environment.Exit(0);
				}
			}
			else if (args[argIncrease]=="d" || args[argIncrease]=="-d"){
				if (args.Length==argIncrease+3){
					string outputF = "";
					string jarmodName = "";
					string jarmodFile = "";
					jarmodFile = args[argIncrease+1];
					outputF = args[argIncrease+2];
					writeIfnotQuiet(isQuiet,"DELETE JAR");
					if (Directory.Exists(outputF)){
						jarmodName = jarmodFile.Substring(0, jarmodFile.Length - 4);
						if(File.Exists(outputF + @"\jarmods\"+jarmodFile)){
							
							delFileJar(jarmodFile,outputF);
							delJson(jarmodName, outputF);
							delJar(jarmodName, outputF);
							writeIfnotQuiet(isQuiet,"Operation succesful");
						} else {
							Console.WriteLine("File not found");
						}
					} else {
						Console.WriteLine("Instance Folder not found");
					}
				}  else {
					Console.WriteLine("Incorrect syntaxis. Use -h te get help");
					Environment.Exit(0);
				}
			} 
			else if (args[argIncrease]=="n" || args[argIncrease]=="-n"){
				if (args.Length==argIncrease+4){
					string mmcPath = "";
					string iName = "";
					string iVersion = "";
					string iPath = "";
					
					iName = args[argIncrease+1];
					iVersion = args[argIncrease+2];
					mmcPath = args[argIncrease+3];
					iPath = mmcPath+@"\instances\"+iName.Replace("!","-").Replace("/","-").Replace(@"\","-").Replace("?","-").Replace("+","-");
					
					writeIfnotQuiet(isQuiet,"NEW INSTANCE");
					if (Directory.Exists(mmcPath)){
						if(!Directory.Exists(iPath)){
							Directory.CreateDirectory(iPath);
							newInsFiles(iPath, iVersion, iName);
							
							writeIfnotQuiet(isQuiet,"Operation succesful");
						} else {
							Console.WriteLine("Instance alredy exists!");
						}
					} else {
						Console.WriteLine("MultiMC path not valid");
					}
				}  else {
					Console.WriteLine("Incorrect syntaxis. Use -h te get help");
					Environment.Exit(0);
				}
			}
			else {
				Console.WriteLine("Incorrect syntaxis. Use -h te get help");
				Environment.Exit(0);
			}

        }
		private static void Direc(string path){
			if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return;
            }
		}		
		private static void copyFileJar(string jarmod, string jarmodFile, string outputF) {
			if (File.Exists(outputF + @"\jarmods\"+jarmodFile)){
				File.Delete(outputF + @"\jarmods\"+jarmodFile);
			}
			File.Copy(jarmod, outputF + @"\jarmods\"+jarmodFile);
		}		
		
		private static void addJson(string jarmodName, string jarmodFile, string outputF, string ver){
			List<string> defJson;
			var executingPath = System.AppContext.BaseDirectory;
			
			defJson = File.ReadAllLines(executingPath + "def.json").ToList();
			if (File.Exists(outputF + @"\patches\org.multimc.jarmod."+jarmodName+".json")){
				File.Delete(outputF + @"\patches\org.multimc.jarmod."+jarmodName+".json");
			}
			foreach(string a in defJson){
				File.AppendAllText(outputF + @"\patches\org.multimc.jarmod."+jarmodName+".json", a.Replace("MMCJMTname", jarmodName).Replace("MMCJMTfile", jarmodFile).Replace("MMCJMTversion", ver) + Environment.NewLine);
			}
		}
		
		private static void addJar(string jarmodName, string outputF){
			List<string> defMmc;
			List<string> finMmc;
			var executingPath = System.AppContext.BaseDirectory;
			
			defMmc = File.ReadAllLines(executingPath + "def.mmc").ToList();
				finMmc = File.ReadAllLines(outputF+@"\mmc-pack.json").ToList();
				int mmcNum = File.ReadLines(outputF+@"\mmc-pack.json").Count();
				finMmc[mmcNum-4] = "        },";
				finMmc.InsertRange(mmcNum-3, new string[]{defMmc[0],defMmc[1],defMmc[2],defMmc[3],defMmc[4],defMmc[5]});
				File.Delete(outputF+@"\mmc-pack.json");
				foreach(string a in finMmc){
					File.AppendAllText(outputF+@"\mmc-pack.json", a.Replace("MMCJMTname", jarmodName) + Environment.NewLine);
			}
		}		
		
		private static void delFileJar(string jarmodFile, string outputF){
			if(File.Exists(outputF + @"\jarmods\"+jarmodFile)){
				File.Delete(outputF + @"\jarmods\"+jarmodFile);
			}
		}

		private static void delJson(string jarmodName, string outputF){
			if(File.Exists(outputF + @"\patches\org.multimc.jarmod."+jarmodName+".json")){
				File.Delete(outputF + @"\patches\org.multimc.jarmod."+jarmodName+".json");
			}
		}
		
		private static void delJar(string jarmodName, string outputF){
			List<string> finMmc;
				finMmc = File.ReadAllLines(outputF+@"\mmc-pack.json").ToList();
				int mmcNum = File.ReadLines(outputF+@"\mmc-pack.json").Count();
				File.Delete(outputF+@"\mmc-pack.json");
				int startIndex = finMmc.IndexOf("            \"cachedName\": \""+jarmodName+"\",");
				finMmc.RemoveAt(startIndex+4);
				finMmc.RemoveAt(startIndex+3);
				finMmc.RemoveAt(startIndex+2);
				finMmc.RemoveAt(startIndex+1);
				finMmc.RemoveAt(startIndex);
				finMmc.RemoveAt(startIndex-1);
				finMmc[startIndex-2] = "        }";
				foreach(string a in finMmc){
				File.AppendAllText(outputF+@"\mmc-pack.json", a.Replace("MMCJMTname", jarmodName) + Environment.NewLine);
			}
		}		
		
		private static void newInsFiles(string iPath, string iVersion, string iName){
			List<string> defMmc;
			List<string> defIns;
			var executingPath = System.AppContext.BaseDirectory;
			
			defMmc = File.ReadAllLines(executingPath + "iDef.mmc").ToList();
			if (File.Exists(iPath + @"\mmc-pack.json")){
				File.Delete(iPath + @"\mmc-pack.json");
			}
			foreach(string a in defMmc){
				File.AppendAllText(iPath + @"\mmc-pack.json", a.Replace("MMCJMTver", iVersion) + Environment.NewLine);
			}
			defIns = File.ReadAllLines(executingPath + "iCfg.cfg").ToList();
			if (File.Exists(iPath + @"\instance.cfg")){
				File.Delete(iPath + @"\instance.cfg");
			}
			foreach(string a in defIns){
				File.AppendAllText(iPath + @"\instance.cfg", a.Replace("MMCJMTname", iName) + Environment.NewLine);
			}
		}
	
		private static void writeIfnotQuiet(bool quiet,string message){
			if(!quiet){
				Console.WriteLine(message);
			}
		}
		
    }
}
