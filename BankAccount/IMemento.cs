namespace BankAccount;

public interface IMemento<out TSaved>
{
    TSaved Restore();
}