using Feleves_feladat.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat.Services
{
    public class DbService : IDbService
    {
        private readonly SQLiteAsyncConnection db;

        public DbService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Workout>().Wait();
            db.CreateTableAsync<Exercise>().Wait();
            db.CreateTableAsync<PerformedSet>().Wait();
            db.CreateTableAsync<ExerciseTemplate>().Wait();

            App.Current.MainPage.DisplayAlert("DB Path", dbPath, "OK");
            Debug.WriteLine($" SQLite database path: {dbPath}");
        }
    }
}
