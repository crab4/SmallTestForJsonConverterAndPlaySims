using ChadinForDigitalSpace.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ChadinForDigitalSpace {
    internal static class JsCreator {
        public static void CreateFileJson(List<Object> list) {
            List<string> file = new List<string>();
            DefaultContractResolver contractResolver = new DefaultContractResolver {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter creator = new StreamWriter(userFolder + @"\Persons.Json", false)) {
            }

            foreach (var member in list) {
                if (member is Person) {
                    string jSon = JsonConvert.SerializeObject(member, new JsonSerializerSettings {
                        ContractResolver = contractResolver,
                    });
                    using StreamWriter creator = new StreamWriter(userFolder + @"\Persons.Json", true);
                    creator.WriteLine(jSon);
                } else {
                    string jSon = JsonConvert.SerializeObject(member, new JsonSerializerSettings {
                        ContractResolver = contractResolver,
                    });
                    using StreamWriter creator = new StreamWriter(userFolder + @"\Persons.Json", true);
                    creator.WriteLine(jSon);
                }
            }
        }
        public static List<Object> ReadFromFileJson() {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using StreamReader reader = new StreamReader(userFolder + @"\Persons.Json");
            List<Object> list = new List<Object>();
            string? output;
            while ((output = reader.ReadLine()) != null) {
                Person tmp = JsonConvert.DeserializeObject<Person>(output);
                if (tmp.Age == 0) {
                    Child child = JsonConvert.DeserializeObject<Child>(output);
                    list.Add(child);
                }else list.Add(tmp);
            }
            return list;
        }
        public static void GiveAnswers(out int pCount, out int pCard, out int cMiddleAge, List<Object> newList) {
            int cAge = 0, cCount = 0;
            pCount = 0;
            pCard = 0;
            foreach (var member in newList) {
                if (member is Person pers) {
                    pCount++;
                    pCard += pers.CreditCardNumbers.Length;
                } else {
                    cCount++;
                    Child tmpChild = (Child)member;
                    Int64 age = tmpChild.BirthDate;
                    DateTime birthday = (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(age).ToLocalTime();
                    var today = DateTime.Today;
                    var years = today.Year - birthday.Year;
                    if (birthday.Date > today.AddYears(-years)) years--;
                    cAge += years;
                }
            }
            cMiddleAge = cAge / cCount;
        }
    }

}
