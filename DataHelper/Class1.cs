using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataHelper
{
    public class DataAccess
    {
        string emailadd, password, salary;
        //double interestrate1 = 0.00062;
        //double interestrate2 = 0.00065;
        //double interestrate3 = 0.00068;
        //double interestrate4 = 0.00075;
        //double interestrate5 = 0.00080;
        double service = 0.2;

        double loanamount, interest, takehome, monthlyamor, interestrate;
        static string myConStrg = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rhacel Compuesto\source\repos\SalaryLoanCalculator\SalaryLoanCalculator\App_Data\Database1.mdf;Integrated Security=True";
        SqlConnection myConn = new SqlConnection(myConStrg);

        public string Emailadd { get => emailadd; set => emailadd = value; }
        public string Password { get => password; set => password = value; }
        public string Salary { get => salary; set => salary = value; }
        public double Loanamount { get => loanamount; set => loanamount = value; }
        public double Interest { get => interest; set => interest = value; }
        public double Takehome { get => takehome; set => takehome = value; }
        public double Monthlyamor { get => monthlyamor; set => monthlyamor = value; }
        public double Interestrate { get => interestrate; set => interestrate = value; }
        public double Service { get => service; set => service = value; }

        public void InsertRecords(string Email, string pass, string salary)
        {
            myConn.Open();
            SqlCommand savecmd = new SqlCommand("AccountProcedure", myConn);
            savecmd.CommandType = CommandType.StoredProcedure;
            savecmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = Email;
            savecmd.Parameters.Add("@passWord", SqlDbType.NVarChar).Value = pass;
            savecmd.Parameters.Add("@Salary", SqlDbType.NVarChar).Value = salary;
            savecmd.ExecuteNonQuery();
            myConn.Close();
        }

        public bool checkEmail(string email, string password)
        {
            bool found = false;
            myConn.Open();
            SqlCommand checkcmd = new SqlCommand("CheckEmail", myConn);
            checkcmd.CommandType = CommandType.StoredProcedure;
            checkcmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = email;
            checkcmd.Parameters.Add("@passWord", SqlDbType.NVarChar).Value = password;
            SqlDataReader dr;
            dr = checkcmd.ExecuteReader();

            while (dr.Read())
            {
                found = true;
                Emailadd = dr.GetString(0);
                Password = dr.GetString(1);
                Salary = dr.GetString(2);
                break;
            }
            myConn.Close();
            return found;
        }

        public void InsertRecords1(string email, string loanamount, string interest, string takehome, string servicecharge, string monthlyamor)
        {
            myConn.Open();
            SqlCommand savecmd = new SqlCommand("AmountProcedure", myConn);
            savecmd.CommandType = CommandType.StoredProcedure;
            savecmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = email;
            savecmd.Parameters.Add("@LoanAmount", SqlDbType.NVarChar).Value = loanamount;
            savecmd.Parameters.Add("@Interest", SqlDbType.NVarChar).Value = interest;
            savecmd.Parameters.Add("@TakeHomeLoan", SqlDbType.NVarChar).Value = takehome;
            savecmd.Parameters.Add("@ServiceCharge", SqlDbType.NVarChar).Value = servicecharge;
            savecmd.Parameters.Add("@MonthlyAmortization", SqlDbType.NVarChar).Value = monthlyamor;
            savecmd.ExecuteNonQuery();
            myConn.Close();
        }
        public DataSet DisplayMyRecords()
        {

            SqlDataAdapter da = new SqlDataAdapter("DisplayRecords", myConn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.Fill(ds, "AmountTable");
            return ds;
        }


        public void LoanAmount(double basicsal)
        {
            Loanamount = basicsal * 2.5;
        }
        public void CalculateInterest1(double loanamount, int monthtopay)
        {

            Interest = loanamount * monthtopay * 0.0062;
        }
        public void CalculateInterest2(double loanamount, int monthtopay)
        {
            Interest = loanamount * monthtopay * 0.0065;
        }
        public void CalculateInterest3(double loanamount, int monthtopay)
        {
            Interest = loanamount * monthtopay * 0.0068;
        }
        public void CalculateInterest4(double loanamount, int monthtopay)
        {
            Interest = loanamount * monthtopay * 0.0075;
        }
        public void CalculateInterest5(double loanamount, int monthtopay)
        {
            Interest = loanamount * monthtopay * 0.0080;
        }
        public void CalculateTakeHome(double loanamount)
        {
            Takehome = Interest + (Service * loanamount);
        }
        public void CalculateAmortization(double loanamount, int monthtopay)
        {
            Monthlyamor = loanamount / monthtopay;
        }

    }
}