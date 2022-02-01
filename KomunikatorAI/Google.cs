using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KomunikatorAI.szablony;
using static KomunikatorAI.Logowanie;

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
                {"Haslo", haslo},
                {"Aktywowane", false},
                {"Nauczyciel", false}
  
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


        public static async Task<QuerySnapshot> Logowanie(string Login, string Haslo)
        {
            try
            {
                CollectionReference kolekt = db.Collection("Konta");
                Query kwerenda = kolekt
                    .WhereEqualTo("Login", Login)
                    .WhereEqualTo("Haslo", Haslo)
                    .WhereEqualTo("Aktywowane", true);

                QuerySnapshot dane = await kwerenda.GetSnapshotAsync();

                if (dane.Documents.Count == 1)
                {
                    return dane;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static async Task<string> WyślijZaproszenieAsync(string Odbiorca)
        {
            int warunki = 0;
            Dictionary<string, object> zaproszenie = new Dictionary<string, object>() {
                {"Nadawca", użytkownik.Login},
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
                    .WhereEqualTo("Nadawca", użytkownik.Login)
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
                    .WhereEqualTo("Odbiorca", użytkownik.Login);

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

        public static async Task<QuerySnapshot> Znajomi(bool status, bool czytoja=true)
        {
            if (czytoja)
            {
                try
                {
                    Query zapytanie = db.Collection("Relacje")
                    .WhereEqualTo("Odbiorca", użytkownik.Login)
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
                    .WhereEqualTo("Nadawca", użytkownik.Login)
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

        public static async Task WyślijWiadomośćAsync(string IDRelacji, string treść)
        {
            CollectionReference kolekcja = db.Collection("Relacje").Document(IDRelacji).Collection("Rozmowa");


            Dictionary<string, object> Rozmowa = new Dictionary<string, object>
            {
                {"Nadawca", użytkownik.Login},
                {"Treść", treść},
                {"Czas", Timestamp.GetCurrentTimestamp().ToProto()}
            };
            await kolekcja.AddAsync(Rozmowa);
        }

        public static async Task<QuerySnapshot> PobieranieRozmowyAsync(string IDRelacji)
        {
            Query zapytanie = db.Collection("Relacje").Document(IDRelacji).Collection("Rozmowa").Limit(10).OrderByDescending("Czas");
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
                    if (x + 1 < słowo.Count)
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


        public static async Task<QuerySnapshot> SugestiaAsync(string wiadomość)
        {
            string[] słowa = wiadomość.Split(' ', '.', ',', '?', '!', '"', '*');
            List<string> słowo = new List<string>();


            for (int x = 0; x < słowa.Length; x++)
            {
                if (słowa[x] != "" && słowa[x] != " " && słowa[x] != "." && słowa[x] != "," && słowa[x] != "!" && słowa[x] != "?")
                {
                    słowo.Add(słowa[x]);
                }
            }

            int sztuki = słowo.Count;

            if (wiadomość.Last().ToString() == " ")
            {
                sztuki++;
                słowo.Add("");
                Console.WriteLine("ostatnia jest spacja");
            }


            if (słowo.Count > 1)
            {
                //Console.WriteLine("Pobieranie podpowiedzi do słowa: "+ słowo[słowo.Count - 2]);
                Query zapytanie = db.Collection("Słownik").WhereEqualTo("Wyraz", słowo[sztuki - 2].ToString());
                QuerySnapshot dane = await zapytanie.GetSnapshotAsync();

                if (dane.Documents.Count > 0)
                {
                    try
                    {
                        Query zapytanie2 = db.Collection("Słownik").Document(dane.Documents.First().Id).Collection("Podpowiedzi").OrderBy("Wyraz").WhereGreaterThanOrEqualTo("Wyraz", słowo[sztuki - 1].ToString()).WhereLessThanOrEqualTo("Wyraz", słowo[sztuki - 1].ToString() + '\uf8ff');
                        QuerySnapshot dane2 = await zapytanie2.GetSnapshotAsync();

                        //Console.WriteLine("Znalazłem podpowiedź do słowa: " + dane.Documents.First().Id);
                        return dane2;
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                    
                }
                else
                {
                    //Console.WriteLine("Nie mogę znaleźć podpowiedzi do tego słowa");
                }
            }
            else
            {
                //Console.WriteLine("Pobieranie podpowiedzi na rozpoczęcie zdania"+ słowo[słowo.Count - 1].ToString());

                try
                {
                    Query zapytanie = db.Collection("Słownik").OrderBy("Wyraz").WhereGreaterThanOrEqualTo("Wyraz", słowo[sztuki - 1].ToString()).WhereLessThanOrEqualTo("Wyraz", słowo[sztuki - 1].ToString()+'\uf8ff');
                    QuerySnapshot dane = await zapytanie.GetSnapshotAsync();

                    return dane;
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                
            }
            return null;
        }
    }
}
