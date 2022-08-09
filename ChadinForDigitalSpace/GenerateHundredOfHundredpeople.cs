using ChadinForDigitalSpace.Models;

namespace ChadinForDigitalSpace {
	//ID - уникальный для всех.
	//Имена - греческие титаны
	//Фамилии - болгарские
	//Transport& squence ID - random of 100
	//CreditNumber card Можно прикрутить GrahamKing Card generator, но я не уверен, что это нужно. Ничего о валидности данных не было в тз (= Но если нужно - прицеплю
	// Можно сделать более сложную систему создания сущностей, но всё упирается в необходимость и времязатраты.
	//20% населения согласно статистике дети, но у нас этот процент будет чуть выше. ИДеализированный мир(=
	internal class GenerateHundredOfHundredpeople {
		private static List<string> ListOfcards = new List<string>();
		private static List<string> ListOfPhones = new List<string>();
		static int Id = 0;
		private static string[] greekTitanMale = new string[] {
			"Гиперион", "Иапет", "Кей", "Криос", "Кронос", "Океан",
			"Прометей", "Атлас", "Гелиос", "Лето", "Менетий","Астерий", "Эос", "Эпитемей", "Кратос"
		};
		private static string[] greekTitanFemale = new string[] {
			 "Мнемосина", "Рея", "Тейя", "Тефида",
			"Феба", "Фемида", "Селена", "Электра", "Ихтиония"
		};
		private static string[] familyes = new string[] {
			"Угринов", "Узунов","Узунски", "Унчев","Урманов", "Урумов", "Ушатов", "Ушев"
		};
		private static string[] familyesF = new string[] {
			"Угринова", "Узунова","Узунски", "Унчева","Урманова", "Урумова", "Ушатова", "Ушева"
		};

		public List<string> GeneratorGenerations(int numberOfPeople) {
			List<string> peoples = new List<string>();
			Random random = new Random();
			while (peoples.Count < numberOfPeople) {
				int marryAtUniversity = random.Next(3);
				int childs = random.Next(4);
				if (marryAtUniversity == 0) {
					peoples.Add("Person-Married");
					peoples.Add("Person-Married");
					int pointer = peoples.Count - 2;
					if(childs > 0) {
						while (childs-- > 0) {
							peoples.Add("Child");
							peoples[pointer] += "-Children";
							peoples[pointer + 1] += "-Children";
						}
                    }
                } else {
					peoples.Add("Person-Unmarried");
					int pointer = peoples.Count - 1;
					childs -= 1;
                    if (childs > 0) {
                        while (childs-- > 0) {
							peoples.Add("Child");
							peoples[pointer] += "-Children";
                        }
                    }
                }
			}
			return peoples;
        }


		public List<Object> Generator(int numberOfPeople) {
            List<Object> list = new List<Object>();
			Random random = new Random();
			List<string> information =GeneratorGenerations(numberOfPeople);
			while (list.Count < numberOfPeople) {
				string[] info = information[list.Count].Split('-');
				if (info[0] == "Person") {
					Guid guid;
					Gender firstGen = GiveGender();
					string firstName, secondName;
					string[] phoneNumbers, numberCred;
					int age, sequence;
					double salary;
					Int64 birthDay;
					GiveBasePerson(out guid, out firstName, out secondName, out sequence, out numberCred,
						out phoneNumbers, out age, out birthDay, out salary, firstGen);


					List<Child> childrens = GiveChildrens(secondName, info.Length - 2);
					for (var i = 0; i < childrens.Count; i++)
						list.Add(childrens[i]);


					if (info[1] == "Unmarried") {
						Person newPerson = new Person(++Id, guid,firstName,secondName,sequence,
							numberCred, age,phoneNumbers,birthDay,salary,false,firstGen, childrens.ToArray());
						list.Add(newPerson);
                    } else {
						Person newPerson = new Person(++Id, guid, firstName, secondName, sequence,
							numberCred, age, phoneNumbers, birthDay, salary, false, firstGen, childrens.ToArray());
						list.Add(newPerson);

						string secName = new string(secondName);


						GiveBasePerson(out guid, out firstName, out secondName, out sequence, out numberCred,
						out phoneNumbers, out age, out birthDay, out salary, firstGen==Gender.Male ? Gender.Female : Gender.Male);
						
						newPerson = new Person(++Id, guid, firstName, secName, sequence,
							numberCred, age, phoneNumbers, birthDay, salary, false, firstGen, childrens.ToArray());
						list.Add(newPerson);
					}
                    
                }
			}
			while (list.Count > 10000) {
				list.RemoveAt(10000);
			}
			return list;
        }
		private void GiveBasePerson (out Guid gui, out string name, out string secondName, out int seq,
			out string[] card, out string[]phone, out int age, out Int64 date, out double sal,Gender gender) {
			Random random = new Random();
			gui = Guid.NewGuid();
			GiveName(out name, out secondName, gender);
			seq = random.Next(Int32.MaxValue);
			card = new string[random.Next(3)];
			for (var i = 0; i < card.Length; i++) {
				card[i] = GiveCreditCard();
			}
			phone = new string[random.Next(3)];
			for (var i = 0; i < phone.Length; i++) {
				phone[i] = GivePhone();
			}
			date = GiveBirthDate(out age);
			sal= random.Next(Int32.MaxValue - 30000) + 30000;
		}
		List<Child> GiveChildrens(string secondName, int number) {
			if (number < 1)
				return new List<Child>();
			List<Child> children = new List<Child>();
			for(var i=0; i< number; i++) {
				children.Add(GiveChild(secondName));
            }
			return children;
        }
		private Gender GiveGender() {
			Random random = new Random();
			return random.Next(2)==0 ? Gender.Male : Gender.Female;
        }
		private void GiveName(out string name, out string secName, Gender curGen) {
			Random random = new Random();
			if (curGen == Gender.Male) {
				name = greekTitanMale[random.Next(greekTitanMale.Length)];
				secName = familyes[random.Next(familyes.Length)];
			} else {
				name = greekTitanFemale[random.Next(greekTitanFemale.Length)];
				secName = familyesF[random.Next(familyesF.Length)];
			}
		}
		private string GiveCreditCard() {
			Random rand = new Random();
			int j = 0;
			while (j++ < 200) {
				int[] digits = new int[16];
				for (var i = 0; i < 16; i++) {
					digits[i] = rand.Next(10);
				}
				string cardNumber = string.Empty;
				for (var i = 0; i < 4; i++) {
					for (var x = 0; x < 4; x++)
						cardNumber += $"{digits[i * 4 + x]}";
					cardNumber += " ";
				}
				if (!ListOfcards.Contains(cardNumber)) {
					ListOfcards.Add(cardNumber);
					return cardNumber;
				}

			}
			throw new Exception("can't generate cardNumber");
		}
		private string GivePhone() {
			Random rand = new Random();
			int j = 0;

            while (j++ < 200) {
				int country = rand.Next(998) + 1;
                int[] digits = new int[10];
				for(var i =0; i < 10; i++) {
                    digits[i] = rand.Next(10);
                }
				string phoneNumber = $"+{country}-({digits[0]}{digits[1]}{digits[2]})-" +
					$"{digits[3]}{digits[4]}{digits[5]}-{digits[6]}{digits[7]}-{digits[8]}{digits[9]}";
                if (!ListOfPhones.Contains(phoneNumber)){
					ListOfPhones.Add(phoneNumber);
					return phoneNumber;
                }
            }
			throw new Exception("can't generate phone number");
		}
        
		private Int64 GiveBirthDate(out int age) {
			Random rand = new Random();

			DateTime start = new DateTime(1960, 1, 1);
			int range = (DateTime.Today - start).Days - 6574;
			DateTime birthDay = start.AddDays(rand.Next(range));
			age = (DateTime.Today - birthDay).Days / 365;
			return (new DateTimeOffset(birthDay)).ToUnixTimeSeconds();
		}
		private Int64 GiveChildBirthDate() {
			Random rand = new Random();
			DateTime start = DateTime.Today.AddYears(-18);
			DateTime birthDay = start.AddDays(rand.Next(6570));
			return (new DateTimeOffset(birthDay)).ToUnixTimeSeconds();
        }
		private Child GiveChild(string secondname) {
			Random random = new Random();
			Gender gender = GiveGender();
			string firstName, freeMemory;
			GiveName(out firstName, out freeMemory,gender);

			return new Child(++Id, firstName, secondname, GiveChildBirthDate(), random.Next(2) == 0 ? Gender.Male : Gender.Female);
        }
    }
}


/*public Int32 Id { get; set; }
		public Guid TransportId { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int32 SequenceId { get; set; }
		public String[] CreditCardNumbers { get; set; }
		public Int32 Age { get; set; }
		public String[] Phones { get; set; }
		public Int64 BirthDate { get; set; }
		public Double Salary { get; set; }
		public Boolean IsMarred { get; set; }
		public Gender Gender { get; set; }
		public Child[] Children { get; set; }

public Int32 Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }
		public Gender Gender { get; set; }*/