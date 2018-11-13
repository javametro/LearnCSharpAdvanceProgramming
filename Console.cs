using System;

public enum TimeOfDay{
	Morning = 0,
	Afternoon = 1,
	Evening = 2
}

public class MyFirstClass{
	static int j = 30;
	
	static void Main(string[] args	){
		// Console.WriteLine("Hello from stephen.");
		// Console.ReadLine();

		// var name = "Bugs Bunny";
		// var age = 25;
		// var isRabbit = true;
		
		// Type nameType = name.GetType();
		// Type ageType = age.GetType();
		// Type isRabbitType = isRabbit.GetType();
		
		// Console.WriteLine("name is type " + nameType.ToString());
		// Console.WriteLine("age is type " + ageType.ToString());
		// Console.WriteLine("isRabbit is type " + isRabbitType.ToString());
		
		// int j = 20;
		// Console.WriteLine(j);
		// Console.WriteLine(MyFirstClass.j);
		// return;

		// string s1 = "a string";
		// string s2 = s1;
		// Console.WriteLine("s1 is " + s1);
		// Console.WriteLine("s2 is " + s2);
		// s1 = "another string";
		// Console.WriteLine("s1 is now " + s1);
		// Console.WriteLine("s2 is now " + s2);
		WriteGreeting(TimeOfDay.Morning);
		return ;
		
	}
	
	static void WriteGreeting(TimeOfDay timeOfDay){
		switch(timeOfDay){
			case TimeOfDay.Morning:
				Console.WriteLine("Good Morning");
				break;
			case TimeOfDay.Afternoon:
				Console.WriteLine("Good Afternoon");
				break;
			case TimeOfDay.Evening:
				Console.WriteLine("Good Evening");
				break;
			default:
				Console.WriteLine("Hello!");
				break;
		}
	}
}