using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomunikatorAI
{
    public class szablony
    {
        [FirestoreData]
        public class User
        {
            [FirestoreDocumentId]
            public string ID { get; set; }
            [FirestoreProperty]
            public string Login { get; set; }
            [FirestoreProperty]
            public string Haslo { get; set; }
            [FirestoreProperty]
            public bool Nauczyciel { get; set; }
        }

        [FirestoreData]
        public class propozycja
        {
            [FirestoreDocumentId]
            public string ID { get; set; }
            [FirestoreProperty]
            public string Wyraz { get; set; }
            [FirestoreProperty]
            public int Popularność { get; set; }
        }
    }
}
