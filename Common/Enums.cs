﻿namespace G_UserInterface.Common;

public class Enums
{
    public enum TransactionType
    {
        Sell = 1,
        Buy = 2,
        Windrow = 3,
        Deposit = 4,
        Exchange = 5
    }
    public enum TransactionMode
    {
        Offline = 1,
        Online = 2,
    }
    public enum Unit
    {
        Rial = 1,
        Gram = 2,
    }
    public enum Currency
    {
        Money = 1,
        Gold = 2,
    }
}