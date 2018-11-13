using System;
class Client{
	public static void Main(){
		MathLib mathObj = new MathLib();
		Console.WriteLine(mathObj.Add(7, 8));
	}
}