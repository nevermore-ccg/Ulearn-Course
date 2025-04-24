using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting;

public class AccountingModel : ModelBase
{
    private double _price;
    private int _nightsCount;
    private double _discount;
    private double _total;

    public double Price
    {
        get { return _price; }
        set
        {
            if (value < 0) throw new ArgumentException();
            _price = value;
            Notify(nameof(Price));
            Total = CalculateTotal();
        }
    }
    public int NightsCount
    {
        get { return _nightsCount; }
        set
        {
            if (value <= 0) throw new ArgumentException();
            _nightsCount = value;
            Notify(nameof(NightsCount));
            Total = CalculateTotal();
        }
    }
    public double Discount
    {
        get { return _discount; }
        set
        {
            _discount = value;
            if (_discount != CalculateDiscount())
                Total = CalculateTotal();
            Notify(nameof(Discount));
        }
    }
    public double Total
    {
        get { return _total; }
        set
        {
            if (value < 0) throw new ArgumentException();
            _total = value;
            if (_total != CalculateTotal())
            {
                _discount = CalculateDiscount();
                Notify(nameof(Discount));
            }
            Notify(nameof(Total));
        }
    }

    public AccountingModel()
    {
        _price = 0;
        _nightsCount = 1;
        _discount = 0;
        _total = 0;
    }

    public double CalculateTotal() => Price * NightsCount * (1 - Discount / 100);

    public double CalculateDiscount() => (1 - _total / (_price * _nightsCount)) * 100;
}