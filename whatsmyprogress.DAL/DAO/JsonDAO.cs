using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace whatsmyprogress.DAL.DAO
{
    public class JsonDAO<T> : IDAO<T> where T : class
    {
        string DatabaseAddress { get; set; }
        string TablePath { get; set; }
        List<T> CurrentTable { get; set; }
        public JsonDAO()
        {
            DatabaseAddress = Environment.GetEnvironmentVariable("WMP_DATABASEADDRESS");
            TablePath = $"{DatabaseAddress}/{typeof(T).Name}.json";
            LoadCurrentDatabase();
        }
        public void Add(T entity)
        {
            ((dynamic)entity).Id = GetNewId();

            CurrentTable.Add(entity);
            SaveChanges();
        }
        public IEnumerable<T> Get()
        {
            return CurrentTable;
        }

        public T Get(int id)
        {
            return CurrentTable.Find(x => ((dynamic)x).Id == id);
        }
    
        public void Delete(T entity)
        {
            CurrentTable.Remove(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }  

        public void Update(T entity)
        {
            var dictionaryOfCurrentTable = CurrentTable.ToDictionary(element => ((dynamic)element).Id, element => element);
            dictionaryOfCurrentTable[((dynamic)entity).Id] = entity;
            CurrentTable = dictionaryOfCurrentTable.Values.ToList() as List<T>;

            SaveChanges();
        }

        void LoadCurrentDatabase()
        {
           
            if (File.Exists(TablePath))
                CurrentTable = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(TablePath));
            else
                CreateNewBase();
        }
        void CreateNewBase() {
            CurrentTable = new List<T>();
            File.WriteAllText(TablePath, JsonConvert.SerializeObject(CurrentTable));
        }

        void SaveChanges() {
            File.WriteAllText(TablePath, JsonConvert.SerializeObject(CurrentTable));
        }

        int GetNewId() {
            var newId = CurrentTable.Count() + 1;
            while (CurrentTable.Count(x => ((dynamic)x).Id == newId) > 0)
                newId++;

            return newId;
        }
    }
}
