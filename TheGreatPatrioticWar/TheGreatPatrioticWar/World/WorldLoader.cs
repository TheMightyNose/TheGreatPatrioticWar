using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace TheGreatPatrioticWar
{
    static partial class World
    {
        //soft code   
        const string TerrainInfoFile = "bla"; 
        
        static Dictionary<string, Dictionary<string, string>> terrainData = new Dictionary<string, Dictionary<string, string>>();

        static void Load(string worldFolder)
        {
           
            var old = Environment.CurrentDirectory;
            Environment.CurrentDirectory = worldFolder;

            
                //Read all the type of terrains 
                var data = File.ReadAllLines("TerrainInfo.txt").Select(line => string.Concat(line.Replace("//","@").TakeWhile(ch => ch != '@').ToArray())).ToArray(); //MAKE THIS SOFT CODED


                string currentTerrain = "";
                foreach (var i in data)
                {
                    //if (i.StartsWith("//"))
                    //    continue;

                    string line = i.Replace(" ", ""); line = line.Replace("\t", "");

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("#"))
                    {
                        currentTerrain = line;
                        terrainData.Add(currentTerrain, new Dictionary<string, string>());
                        continue;
                    }

                    var subStrings = line.Split('=');
                    string name = subStrings.First();
                    string value = subStrings.Last();

                    terrainData[currentTerrain].Add(name, value);
                }

            //Read the world name tags
            data = File.ReadAllLines("NameTags.txt");

            foreach(var i in data)
            {
                string line = i.Replace(" ", ""); line = line.Replace("\t", "");

                var subStrings = line.Split(',');

                int x = int.Parse(subStrings[0]);
                int y = int.Parse(subStrings[1]);
                string tagName = subStrings[2];

                //Check if its inside grid bla bla

                Grid.fields[x, y].tagName = tagName;
            }

            //Load the worldddddd
            //Structure : x, y, faction, terrain, civilians, TANKS, INFANTRY
            //nofaction for no faction

            data = File.ReadAllLines("World.txt");

            int pos_x = 0;
            int pos_y = 1;
            int pos_faction = 2;
            int pos_terrain = 3;
            int pos_civilians = 4;
            int pos_tanks = 5;
            int pos_infantry = 6;

            foreach(String i in data)
            {
                string line = i.Replace(" ", ""); line = line.Replace("\t", "");

                var subStrings = line.Split(',');

                int x = int.Parse(subStrings[pos_x]);
                int y = int.Parse(subStrings[pos_y]);


                //Check if its inside grid bla bla 

                if (subStrings[pos_faction] != /*make this soft code */ "nofaction") 
                {
                    Faction faction = (Faction)typeof(Faction).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).Where(jo => jo.Name == subStrings[pos_faction]).First().GetValue(null);
                    Grid.fields[x, y].owner = faction;

                    var bob = new Army(faction, int.Parse(subStrings[pos_infantry]), int.Parse(subStrings[pos_tanks]));

                    if (bob.Infantry > 0 || bob.Tanks > 0)
                        Grid.fields[x, y].armies.Add(bob);
                }

                Terrain terrain = new Terrain();

                var tInstance = terrain.GetType();

                foreach (var field in tInstance.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (terrainData[subStrings[pos_terrain]].Keys.Contains(field.Name))
                    {

                        Console.WriteLine(field);
                        Global.AssignFieldWithCast(terrain, field, terrainData[subStrings[pos_terrain]][field.Name]);
                    }
                }

                Grid.fields[x, y].terrain = terrain;
                Grid.fields[x, y].civilians = int.Parse(subStrings[pos_civilians]);
               
            }



            Environment.CurrentDirectory = old;
        }
    }
}
