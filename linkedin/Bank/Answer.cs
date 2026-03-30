// C# code​​​​​​‌‌‌‌​‌‌​‌​​​​‌​​‌​‌‌‌​​‌​ below

using System;

// Write your answer here, and then test your code.
// Your job is to implement the findLargest() method.

public class Answer
{
    // Change these Boolean values to control whether you see 
    // the expected result and/or hints.
    public static Boolean ShowExpectedResult = true;
    public static Boolean ShowHints = true;
}

public class BankAccount
{
    protected decimal _balance;
    string _firstName;
    string _lastName;


    public BankAccount(string firstName, string lastName, decimal balance = 0.0m)
    {
        _balance = balance;
        _firstName = firstName;
        _lastName = lastName;
    }


    public decimal Balance
    {
        get => _balance;
        set => _balance = value;
    }

    public string AccountOwner
    {
        get => $"{_firstName} {_lastName}";
    }

    public void Deposit(decimal amount)
    {
        _balance += amount;
    }

    public virtual void Withdraw(decimal amount)
    {
        _balance -= amount;
    }
}

public class CheckingAcct : BankAccount
{
    public CheckingAcct(string firstName, string lastName, decimal balance = 0.0m)
        : base(firstName, lastName, balance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (amount > _balance)
        {
            const decimal fee = 35.0m;
            _balance -= fee + amount;
        }
        else
        {
            _balance -= amount;
        }
    }
}

public class SavingsAcct : BankAccount
{
    private decimal _interestRate;
    private int _withdrawals;

    public SavingsAcct(string firstName, string lastName, decimal interestRate = 0.0m, decimal balance = 0.0m)
        : base(firstName, lastName, balance)
    {
        _interestRate = interestRate;
        _withdrawals = 0;
    }

    public decimal InterestRate
    {
        get => _interestRate;
        set => _interestRate = value;
    }

    public void ApplyInterest()
    {
        decimal interest = _balance * _interestRate;
        _balance += interest;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount > _balance)
        {
            return;
        }

        _balance -= amount;
        _withdrawals++;

        if (_withdrawals > 3)
        {
            _balance -= 2.0m;
        }
    }
}