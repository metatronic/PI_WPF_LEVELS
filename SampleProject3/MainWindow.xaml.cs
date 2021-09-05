using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleProject3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocaldb;database=SampleProject_db;Integrated security=True");
        #endregion
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            DataTable dt = new DataTable();

            try
            {
                //get employee data from database
                GetDataFromDb(dt);

                if (dt.Rows.Count > 0)
                {
                    List<Employee> lstEmployee = new List<Employee>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Employee emp = new Employee(dr);
                        lstEmployee.Add(emp);
                    }

                    ListViewEmployees.ItemsSource = lstEmployee;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        #endregion
        #region Methods
        private void GetDataFromDb(DataTable dt)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();

            _connection.Open();
            Command.Connection = _connection;
            Command.CommandText = "SELECT e.[EmpId]" +
                                        ",e.[FirstName]" +
                                        ",e.[MiddleName]" +
                                        ",e.[LastName]" +
                                        ",e.[BirthDate]" +
                                        ",e.[Gender]" +
                                        ",e.[Address]" +
                                        ",e.[ContactNumber]" +
                                        ",e.[EmailId]" +
                                        ",e.[JoiningDate]" +
                                        ",e.[ConfirmationDate]" +
                                        ",e.[ExpectedInTime]" +
                                        ",e.[ExpectedOutTime]" +
                                        ",e.[IsResigned]" +
                                        ",e.[Salary]" +
                                        ",e.[Designation]" +
                                        ",e.[DeptId]" +
                                        ", d.[DeptName] AS DeptName" +
                                        " FROM [SampleProject_db].[dbo].[Employee] e" +
                                        " LEFT JOIN Department d ON e.DeptId = d.DeptId";
            Adapter.SelectCommand = Command;
            Adapter.Fill(dt);
            _connection.Close();
        }
        #endregion
    }

    public class Employee
    {
        #region Properties
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public char? Gender { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public bool IsResigned { get; set; }
        public decimal? Salary { get; set; }
        public string Designation { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        #endregion
        #region Constructors

        public Employee(DataRow employeeData)
        {
            EmployeeId = (int)employeeData[0];
            FirstName = employeeData[1].ToString();
            MiddleName = employeeData[2].ToString();
            LastName = employeeData[3].ToString();

            string StringBirthDate = employeeData[4].ToString();
            if (StringBirthDate != "")
            {
                BirthDate = Convert.ToDateTime(StringBirthDate);
            }

            string StringGender = employeeData[5].ToString();
            if (StringGender != "")
            {
                Gender = Convert.ToChar(StringGender);
            }

            Address = employeeData[6].ToString();
            ContactNumber = employeeData[7].ToString();
            EmailId = employeeData[8].ToString();

            string StringJoiningDate = employeeData[9].ToString();
            if (StringJoiningDate != "")
            {
                JoiningDate = Convert.ToDateTime(StringJoiningDate);
            }

            string StringConfirmationDate = employeeData[10].ToString();
            if (StringConfirmationDate != "")
            {
                ConfirmationDate = Convert.ToDateTime(StringConfirmationDate);
            }

            IsResigned = employeeData[13].ToString() == "" ? false : (bool)(employeeData[13]);
            Salary = employeeData[14].ToString() == "" ? null : (decimal?)employeeData[14];
            Designation = employeeData[15].ToString();
            DepartmentId = employeeData[16].ToString() == "" ? null : (int?)employeeData[16];
            DepartmentName = employeeData[17].ToString();
        }

        #endregion
    }
}