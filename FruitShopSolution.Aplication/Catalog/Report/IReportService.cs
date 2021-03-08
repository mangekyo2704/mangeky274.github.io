using FruitShopSolution.ViewModel.Catalog.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Report
{
    public interface IReportService
    {
        Task<ReportViewModel> GetRevenuePerDay(DateTime date);
        Task<ReportViewModel> GetRevenueFromDays(DateTime date1, DateTime date2);
    }
}
