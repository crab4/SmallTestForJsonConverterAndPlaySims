namespace ChadinForDigitalSpace.Models {
	class Child {
		public Int32 Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Int64 BirthDate { get; set; }
		public Gender Gender { get; set; }
		public Child(int id, string firstName, string lastName, long birthDate, Gender gender) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
        }
    }
	
}
