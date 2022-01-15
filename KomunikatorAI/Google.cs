using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static KomunikatorAI.szablony;

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

        public static async Task<string> WyślijZaproszenieAsync(string Nadawca, string Odbiorca)
        {
            int warunki = 0;
            Dictionary<string, object> zaproszenie = new Dictionary<string, object>() {
                {"Aktualne", true},
                {"Nadawca", Nadawca},
                {"Odbiorca", Odbiorca},
                {"Zaakceptowane", false}
            };

            try
            {
                CollectionReference kolekt = db.Collection("Konta");
                Query kwerenda = kolekt
                    .WhereEqualTo("Login", Odbiorca);

                QuerySnapshot zwrot = await kwerenda.GetSnapshotAsync();

                if (zwrot.Documents.Count == 1)
                {
                    warunki++;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            try
            {
                CollectionReference kolekt = db.Collection("Relacje");
                Query kwerenda = kolekt
                    .WhereEqualTo("Nadawca", Nadawca)
                    .WhereEqualTo("Odbiorca", Odbiorca)
                    .WhereEqualTo("Aktualne", true);

                QuerySnapshot zwrot = await kwerenda.GetSnapshotAsync();

                if (zwrot.Documents.Count == 0)
                {
                    warunki++;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            try
            {
                CollectionReference kolekt = db.Collection("Relacje");
                Query kwerenda = kolekt
                    .WhereEqualTo("Nadawca", Odbiorca)
                    .WhereEqualTo("Odbiorca", Nadawca)
                    .WhereEqualTo("Aktualne", true);

                QuerySnapshot zwrot = await kwerenda.GetSnapshotAsync();

                if (zwrot.Documents.Count == 0)
                {
                    warunki++;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


            if (warunki == 3)
            {
                try
                {
                    CollectionReference kolekcja = db.Collection("Relacje");
                    await kolekcja.AddAsync(zaproszenie);
                }
                catch
                {
                    return "Błąd Utworzenia Zaproszenia";
                }
                return "Wysłano zaproszenie";
            }
            else
            {
                return "Nie ma takiego Użytkownika lub już zostało wysłane zaproszenie";
            }
        }


        public static async Task<DocumentSnapshot> PobierzRekord(string Kolekcja, string ID)
        {
            try
            {
                DocumentReference warunki = db.Collection(Kolekcja).Document(ID);
                DocumentSnapshot zapytanie = await warunki.GetSnapshotAsync();
                return zapytanie;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<QuerySnapshot> OczekująceZaproszenia(string Login)
        {
            try
            {
                Query zapytanie = db.Collection("Relacje")
                .WhereEqualTo("Aktualne", true)
                .WhereEqualTo("Odbiorca", Login)
                .WhereEqualTo("Zaakceptowane", false);

                QuerySnapshot dane = await zapytanie.GetSnapshotAsync();
                return dane;
            }
            catch (Exception ex) 
            {
                return null;
            };
        }

    }
}
