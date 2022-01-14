using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static async Task<bool> NoweKontoAsync(string login, string haslo)
        {
            Dictionary<string, object> uzytkownik = new Dictionary<string, object>() {
                {"Login", login},
                {"Haslo", haslo}
            };
            try
            {
                CollectionReference kolekcja = db.Collection("Konta");
                Query kwerenda = kolekcja.WhereEqualTo("Login", login);

                QuerySnapshot zwrot = await kwerenda.GetSnapshotAsync();

                if (zwrot.Documents.Count == 0)
                {
                    await kolekcja.AddAsync(uzytkownik);
                    return true;
                }
                else
                {
                     return false;
                }

            }
            catch
            {
                return false;
            }
        }


        public static async Task<string> Logowanie(string Login, string Haslo)
        {
            try
            {
                CollectionReference kolekt = db.Collection("Konta");
                Query kwerenda = kolekt
                    .WhereEqualTo("Login", Login)
                    .WhereEqualTo("Haslo", Haslo);

                QuerySnapshot zwrot = await kwerenda.GetSnapshotAsync();

                if (zwrot.Documents.Count == 1)
                {
                    return zwrot.Documents[0].Id;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
