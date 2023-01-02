using System;

public class BuyButtonClickedEventArgs : EventArgs
{
	public BuyButton BuyButton { get; private set; }

	public BuyButtonClickedEventArgs(BuyButton buyButton)
    {
        BuyButton = buyButton;
    }
}
