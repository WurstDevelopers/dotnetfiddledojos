using System;
using System.Linq;

// Questions
// What is an interface? 
// Why is it useful? 

public interface IStringFormatter
{
    string LowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord(string word);

    string ReplaceSpecificLetterInSentence(string sentence, char newLetter, char oldLetter);
}

//STRING FORMATTER THINGS
public class StringFormatterTests
{
    private StringFormatter sut;

    public StringFormatterTests()
    {
        sut = new StringFormatter();

        Console.WriteLine("Run String Formatter Tests");


        ShouldLowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord();
        ShouldLowerCaseAllLettersWhenTheFirstLetterIsNotC();
        SpecificLetterShouldBeReplacedAndSentenceShouldBeLowerCaseWithTheFirstWordCapitalized();
    }

    public void ShouldLowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord()
    {
        var word = "CHUCK";
        var result = this.sut.LowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord(word);

        Assert.AreEqual("Chuck", result);
    }

    public void ShouldLowerCaseAllLettersWhenTheFirstLetterIsNotC()
    {
        var word = "Murderpaws";
        var result = this.sut.LowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord(word);

        Assert.AreEqual("murderpaws", result);
    }

    public void SpecificLetterShouldBeReplacedAndSentenceShouldBeLowerCaseWithTheFirstWordCapitalized()
    {
        var sentence = "NOW IS ZHE ZIME FOR ALL GOOD MEN ZO COME ZO ZHE AID OF ZHEIR COUNZRY.";
        var newLetter = 'T';
        var oldLetter = 'Z';

        var result = this.sut.ReplaceSpecificLetterInSentence(sentence, newLetter, oldLetter);

        Assert.AreEqual("Now is the time for all good men to come to the aid of their country.", result);
    }
}


public class StringFormatter : IStringFormatter
{
    public string LowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord(string word)
    {
        var wordToLower = word.ToLower();

        if (wordToLower.StartsWith("c"))
        {
            wordToLower = "C" + wordToLower.Substring(1, word.Length - 1);
        }

        return wordToLower;
    }

    public string ReplaceSpecificLetterInSentence(string sentence, char newLetter, char oldLetter)
    {
        var newSentence = sentence.Replace(oldLetter, newLetter);

        return newSentence.Substring(0, 1) + newSentence.Substring(1, sentence.Length - 1).ToLower();
    }
}




//STRING MANIPULATOR THINGS 


public class StringManipulator
{
    private IStringFormatter stringFormatter;

    public StringManipulator(IStringFormatter stringFormatter)
    {
        this.stringFormatter = stringFormatter;
    }

    public string ReverseLetters(string stringToReverse)
    {
        return new string(stringToReverse.Reverse().ToArray());
    }

    public string ReverseWordOrder(string stringToReverse)
    {
        var arrayOfWordsToReverse = stringToReverse.Split(',');
        var reversedOrderString = "";

        for (int i = arrayOfWordsToReverse.Length - 1; i >= 0; i--)
        {
            if (i > 0)
            {
                reversedOrderString += arrayOfWordsToReverse[i].Trim() + ", ";
            }
            else
            {
                reversedOrderString += arrayOfWordsToReverse[i].Trim();
            }
        }

        return reversedOrderString;
    }

    public string DisplayWordsOnNewLineAndPadWithAstericks(string sentence)
    {
        var arrayOfNames = sentence.Split(',');
        var result = "";

        for (int i = 0; i < arrayOfNames.Length; i++)
        {
            var name = arrayOfNames[i].Trim();
            var paddedWord = this.LowercaseAndPadWord(name);

            if (IsNotLastWord(i, arrayOfNames))
            {
                // result += paddedWord + '\n';
                paddedWord += '\n';
            }
            //else
            //{
            //    result += paddedWord;
            //}
            result += paddedWord;

        }

        return result;
    }

    private string LowercaseAndPadWord(string name)
    {
        //var lowerCasedWord = this.LowerCaseAllLettersExceptForCWhenItIsTheFirstLetterOfTheWord(name);
        //var paddedWord = this.PadWordWithAstericks(lowerCasedWord);
        //return paddedWord;
        return null;
    }

    private static bool IsNotLastWord(int i, string[] arrayOfNames)
    {
        return i != (arrayOfNames.Length - 1);
    }

    public string PadWordWithAstericks(string word)
    {
        var wordLength = word.Length;
        var numOfAstericks = 14 - wordLength;
        var firstHalfAstericksCount = numOfAstericks / 2;
        var secondHalfAsterickCount = numOfAstericks - firstHalfAstericksCount;
        var firstSetOfAstericks = "";
        var secondSetOfAstericks = "";

        if (wordLength > 14)
        {
            return word.Substring(0, 14);
        }
        else
        {
            for (int i = 0; i < firstHalfAstericksCount; i++)
            {
                firstSetOfAstericks += "*";
            }

            for (int i = 0; i < secondHalfAsterickCount; i++)
            {
                secondSetOfAstericks += "*";
            }

            return firstSetOfAstericks + word + secondSetOfAstericks;
        }
    }
}



//RUNNER

public class Program
{
    public static void Main()
    {
        new StringFormatterTests();

        var StringManipulator = new StringManipulator(new StringFormatter());
        
        Console.WriteLine(StringManipulator.DisplayWordsOnNewLineAndPadWithAstericks("Blue"));
        Console.ReadLine();
    }
}


//Test "Framework"

public static class Assert
{
    public static void AreEqual(string expected, string actual)
    {

        if (expected == actual)
        {
            Console.WriteLine("Test Passed");
        }
        else
        {
            Console.WriteLine(expected + " does not equal " + actual);
        }
    }
}