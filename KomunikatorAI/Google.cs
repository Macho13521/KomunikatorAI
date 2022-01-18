using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<string> WyślijZaproszenieAsync(string Nadawca, string Odbiorca)
        {
            int warunki = 0;
            Dictionary<string, object> zaproszenie = new Dictionary<string, object>() {
                {"Nadawca", Nadawca},
                {"Odbiorca", Odbiorca},
                {"Zaakceptowane", false},
                {"Czat", "brak"}
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
                    .WhereEqualTo("Odbiorca", Odbiorca);

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
                    .WhereEqualTo("Odbiorca", Nadawca);

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

        internal static void AktualizacjaRekordu(string v1, string v2)
        {
            throw new NotImplementedException();
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

        public static async Task<QuerySnapshot> Znajomi(string Login, bool status, bool czytoja=true)
        {
            if (czytoja)
            {
                try
                {
                    Query zapytanie = db.Collection("Relacje")
                    .WhereEqualTo("Odbiorca", Login)
                    .WhereEqualTo("Zaakceptowane", status);

                    QuerySnapshot dane = await zapytanie.GetSnapshotAsync();
                    return dane;
                }
                catch (Exception ex)
                {
                    return null;
                };
            }
            else
            {
                try
                {
                    Query zapytanie = db.Collection("Relacje")
                    .WhereEqualTo("Nadawca", Login)
                    .WhereEqualTo("Zaakceptowane", status);

                    QuerySnapshot dane = await zapytanie.GetSnapshotAsync();
                    return dane;
                }
                catch (Exception ex)
                {
                    return null;
                };
            }

        }

        public static async Task AktualizacjaRekordu(string Kolekcja, string ID, Dictionary<string, object> dane)
        {
            try
            {
                DocumentReference warunki = db.Collection(Kolekcja).Document(ID);
                await warunki.UpdateAsync(dane);
            }
            catch
            {

            }
        }

        public static void UsuwanieRekordu(string Kolekcja, string ID)
        {
            try
            {
                DocumentReference warunki = db.Collection(Kolekcja).Document(ID);
                warunki.DeleteAsync();
            }
            catch
            {

            }
        }

        public static async Task<string> OtwieranieRozmowyAsync(string IDRelacji)
        {
            try
            {
                DocumentReference warunki = db.Collection("Relacje").Document(IDRelacji);
                DocumentSnapshot zapytanie = await warunki.GetSnapshotAsync();

                Dictionary<string, object> Relacja = zapytanie.ToDictionary();

                if (Relacja["Czat"].ToString()!="brak")
                {
                    return Relacja["Czat"].ToString();
                }
                else
                {
                    Dictionary<string, object> Rozmowa = new Dictionary<string, object>() {
                        {"Pierwszy", Relacja["Nadawca"]},
                        {"Drugi", Relacja["Odbiorca"]}
                    };
                    CollectionReference kolekcja = db.Collection("Rozmowy");
                    string IDRozmowy = kolekcja.AddAsync(Rozmowa).Result.Id;

                    DocumentReference dokument = db.Collection("Relacje").Document(IDRelacji);
                    Dictionary<string, object> aktualizacja = new Dictionary<string, object>
                    {
                        {"Czat", IDRozmowy}
                    };
                    await dokument.UpdateAsync(aktualizacja);

                    return IDRozmowy;
                }

            }
            catch
            {
                return "brak";
            }
        }


        public static async Task WyślijWiadomośćAsync(string IDRozmowy, string Nadawca, string treść)
        {
            CollectionReference kolekcja = db.Collection("Rozmowy").Document(IDRozmowy).Collection("Rozmowa");


            Dictionary<string, object> Rozmowa = new Dictionary<string, object>
            {
                {"Nadawca", Nadawca},
                {"Treść", treść},
                {"Czas", Timestamp.GetCurrentTimestamp().ToProto()}
            };
            await kolekcja.AddAsync(Rozmowa);
        }

        public static async Task<QuerySnapshot> PobieranieRozmowyAsync(string IDRozmowy)
        {
            Query zapytanie = db.Collection("Rozmowy").Document(IDRozmowy).Collection("Rozmowa").Limit(10).OrderByDescending("Czas");
            QuerySnapshot dane = await zapytanie.GetSnapshotAsync();

            return dane;
        }


        public static async Task AnalizaJęzykaAsync(string wiadomość)
        {
            string[] słowa = wiadomość.Split(' ', '.', ',', '?', '!', '"', '*');
            List<string> słowo = new List<string>();  

            for(int x=0; x<słowa.Length; x++)
            {
                if (słowa[x]!="" && słowa[x]!=" " && słowa[x]!="." && słowa[x] != "," && słowa[x] != "!" && słowa[x] != "?")
                {
                    słowo.Add(słowa[x]);
                }
            }

            for (int x = 0; x < słowo.Count; x++)
            {
                Query zapytanie = db.Collection("Słownik").WhereEqualTo("Wyraz", słowo[x]);
                QuerySnapshot dane = await zapytanie.GetSnapshotAsync();
                
                if (dane.Documents.Count >= 1)
                {
                    string IDSłowa = dane.Documents.First().Id;

                    if (x == 0)
                    {
                        Console.WriteLine("Zwiększanie popularności słowa");
                        DocumentReference dokument = db.Collection("Słownik").Document(IDSłowa);
                        await dokument.UpdateAsync("Popularność", FieldValue.Increment(1));
                    }

                    if (x+1< słowo.Count)
                    {
                        Query zapytanie2 = db.Collection("Słownik").Document(IDSłowa).Collection("Podpowiedzi").WhereEqualTo("Wyraz", słowo[x+1]);
                        QuerySnapshot dane2 = await zapytanie2.GetSnapshotAsync();

                        if (dane2.Documents.Count >= 1)
                        {
                            Console.WriteLine("Zwiększanie polularności podpowiedzi "+ dane2.Documents.Count);
                            DocumentReference dokument = db.Collection("Słownik").Document(IDSłowa).Collection("Podpowiedzi").Document(dane2.Documents.First().Id);
                            await dokument.UpdateAsync("Popularność", FieldValue.Increment(1));
                        }
                        else
                        {
                            Console.WriteLine("Dodawanie nowej podpowiedzi do istniejącego słowa "+ dane2.Documents.Count);
                            CollectionReference kolekcja = db.Collection("Słownik").Document(IDSłowa).Collection("Podpowiedzi");
                            Dictionary<string, object> Podpowiedź = new Dictionary<string, object>
                            {
                                {"Wyraz", słowo[x+1]},
                                {"Popularność", 1}
                            };
                            await kolekcja.AddAsync(Podpowiedź);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Dodawanie nowego słowa");
                    int pierwsza = 0;
                    if (x == 0)
                    {
                        pierwsza++;
                    }

                    CollectionReference kolekcja = db.Collection("Słownik");
                    Dictionary<string, object> nowesłowo = new Dictionary<string, object>
                        {
                            {"Wyraz", słowo[x]},
                            {"Popularność", pierwsza}
                        };
                    string IDSłowa = kolekcja.AddAsync(nowesłowo).Result.Id;

                    if (x + 1 < słowo.Count)
                    {
                        Console.WriteLine("Dodawanie nowej podpowiedzi");
                        CollectionReference kolekcja2 = db.Collection("Słownik").Document(IDSłowa).Collection("Podpowiedzi");
                        Dictionary<string, object> podpowiedź = new Dictionary<string, object>
                            {
                                {"Wyraz", słowo[x+1]},
                                {"Popularność", 1}
                            };
                        await kolekcja2.AddAsync(podpowiedź);
                    }
                }
            }
        }
    }
}
