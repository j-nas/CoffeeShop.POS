using System.Globalization;
using CoffeeShop.POS.Controllers;
using CoffeeShop.POS.Models.DTOs;

namespace CoffeeShop.POS.Services;

internal static class ReportService
{
    internal static void CreateReport()
    {
        var orders = OrderController.GetOrders();

        var report = orders
            .GroupBy(x => new { x.CreatedDate.Month, x.CreatedDate.Year })
            .Select(
                grp =>
                    new MonthlyReportDTO
                    {
                        Month =
                            $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(
                            grp.Key.Month
                        )}/{grp.Key.Year}",
                        TotalPrice = grp.Sum(grp => grp.TotalPrice),
                        TotalQuantity = grp.Sum(grp => grp.OrderProducts.Sum(x => x.Quantity)),
                    }
            )
            .ToList();

        UserInterface.ShowReportByMonth(report);
    }
}
