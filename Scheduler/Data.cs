using Microsoft.AspNetCore.SignalR.Client;
using Scheduler.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public static class Data
    {
        public static HubConnection HUB;
        public const string Address = "http://neuotec.com:51926/mainhub";

        private static readonly string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private const string Filename = "AppData.txt";

        public static List<Day> DAYS = new List<Day>();

        public static void Setup()
        {
            try
            {
                Load();
                UpdateList();
            }
            catch
            {
                CreateNew();
            }
        }

        public static void CreateNew()
        {
            DAYS = new List<Day>();
            for (int i = 0; i < 14; i++)
            {
                DAYS.Add(new Day(DateTime.Now.AddDays(i)));
            }
        }

        public static void UpdateList()
        {
            DAYS.RemoveAll(day => day.Date.Date < DateTime.Now.Date);
            for (int i = DAYS.Count; i < 14; i++)
            {
                DAYS.Add(new Day(DateTime.Now.AddDays(i)));
            }
            for (int i = 0; i < 13; i++)
            {
                if (DAYS[i].Date.Date.Equals(DAYS[i + 1].Date.Date))
                    DAYS.RemoveAt(i + 1);
            }
        }

        public static Task Save()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(DAYS);
            File.WriteAllText(Path.Combine(FolderPath, Filename), json);
            return Task.CompletedTask;
        }

        public static Task Load()
        {
            string json = File.ReadAllText(Path.Combine(FolderPath, Filename));
            DAYS = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Day>>(json);
            return Task.CompletedTask;
        }
    }
}
