namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;

    private readonly CouponDiscountCalculator _couponDiscountCalculator = new();
    private readonly GiftMessageCardFormatter _giftMessageCardFormatter = new();
    private readonly PaymentAuthorizationStub _paymentAuthorizationStub = new();

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText)
    {
        _couponRaw = couponText;
    }

    public void EnableGiftWrap()
    {
        _giftWrap = true;
    }

    public decimal SubTotal()
    {
        return _lines.Sum(l => l.Price * l.Qty);
    }

    public decimal DiscountAmount()
    {
        return _couponDiscountCalculator.CalculateDiscount( _couponRaw,SubTotal());
    }

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();

        if (_giftWrap)
            total += 4.99m;

        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        return _giftMessageCardFormatter.CreateGiftMessageCard(fromName,  _lines, GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorizationStub.Authorize(GrandTotal(),cardLast4,_lines.Count);
    }
}