using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomunikatorAI
{
    class szablony
    {
        [FirestoreData]
        public class User
        {
            [FirestoreProperty]
            public string Login { get; set; }
            [FirestoreProperty]
            public string Haslo { get; set; }
        }
    }
}
