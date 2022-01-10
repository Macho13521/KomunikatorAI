using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace KomunikatorAI
{
    public class Google
    {
        public static FirestoreDb db;


        public static void Connect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"konekcik.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("komunikatorai");
        }

        public static string DodajRekord(string Kolekcja, Dictionary<string, object> dane)
        {
            try
            {
                CollectionReference kolekcja = db.Collection(Kolekcja);
                string ID = kolekcja.AddAsync(dane).Result.Id;
                return ID;
            }
            catch
            {
                return null;
            }
        }
    }
}
