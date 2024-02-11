using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSales
{
    public partial class Dashboard : Form
    {
        SqlConnection cn = new SqlConnection();        
        DBConnect dbcon = new DBConnect();

        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;

            string sdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            DateTime startDate = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);
            DateTime endDate = startDate.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

            DateTime startDateOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            DateTime startDateOfYear = new DateTime(currentDate.Year, 1, 1);
            DateTime endDateOfYear = new DateTime(currentDate.Year, 12, 31, 23, 59, 59);

            // 12months, 31days, 23hrs, 59minutes, 59seconds


            lblDalySale.Text = dbcon.ExtractData("SELECT ISNULL(SUM(total),0) AS total FROM tbCart WHERE status LIKE 'Sold' AND sdate BETWEEN '" + sdate + "' AND '" + sdate + "'").ToString("#,##0.00");
            lblWeeklySale.Text = dbcon.ExtractData("SELECT ISNULL(SUM(total),0) AS total  FROM tbCart " + "WHERE status LIKE 'Sold' " + "AND sdate BETWEEN '" + startDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + endDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'").ToString("#,##0.00");
            lblMonthlySale.Text = dbcon.ExtractData("SELECT ISNULL(SUM(total),0) AS total FROM tbCart WHERE status LIKE 'Sold' AND sdate BETWEEN '" + startDateOfMonth.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + endDateOfMonth.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'").ToString("#,##0.00");
            lblYearlySale.Text = dbcon.ExtractData("SELECT ISNULL(SUM(total),0) AS total FROM tbCart WHERE status LIKE 'Sold' AND sdate BETWEEN '" + startDateOfYear.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + endDateOfYear.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'").ToString("#,##0.00");
            lblTotalProduct.Text = dbcon.ExtractData("SELECT COUNT(*) FROM tbProduct").ToString("#,##0");
            lblStockOnHand.Text = dbcon.ExtractData("SELECT ISNULL(SUM(qty), 0) AS qty FROM tbProduct").ToString("#,##0");
            lblCriticalItems.Text = dbcon.ExtractData("SELECT COUNT(*) FROM vwCriticalItems").ToString("#,##0");
        }

        private void lblDalySale_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblCriticalItems_Click(object sender, EventArgs e)
        {

        }
    }
}
