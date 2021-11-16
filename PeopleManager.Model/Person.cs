﻿using System;

namespace PeopleManager.Model
{
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? Age { get; set; }
		public string Email { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
