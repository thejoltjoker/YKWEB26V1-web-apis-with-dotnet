// C# code​​​​​​‌‌‌‌​‌‌​‌​​​​​​‌‌‌‌​​​‌​​ below
using System;
using System.Text;

// This is how your code will be called.
// You can edit this code to try different testing cases.
string[] teststrings = { "Hello World!", "Race car!", "Rotor", "More cowbell!", "Madam, I'm Adam." };
int palcount = 0;
foreach (string str in teststrings) {
    bool learnerResult = Answer.IsPalindrome(str);
    
    if (learnerResult)
        palcount++;
}
// Write your answer here, and then test your code.

public class Answer {

    // Change these Boolean values to control whether you see 
    // the expected result and/or hints.
    public  static Boolean ShowExpectedResult = false;
    public  static Boolean ShowHints = false;

    // Determine whether a string is a Palindrome
    public static bool IsPalindrome(string thestr) {
        // Your code goes here.
        var normalizedString = new StringBuilder();
        var reversedString = new StringBuilder();
        var charArray = thestr.ToCharArray();
        // Normalize string
        foreach (var c in charArray)
        {
            if (char.IsPunctuation(c) || char.IsWhiteSpace(c)) continue;
            normalizedString.Append(c);
            reversedString.Insert(0,c);
        }
        
        
        // Compare strings
        return normalizedString.ToString().ToLower().Equals(reversedString.ToString().ToLower());
    }
}
