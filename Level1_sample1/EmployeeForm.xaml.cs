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

namespace SampleProject1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EmployeeForm : Window
    {
        #region Properties    
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocaldb;database=SampleProject_db;Integrated security=True");
        #endregion
        #region Constructors
        public EmployeeForm()
        {
            InitializeComponent();
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                Command.Connection = _connection;
                Command.CommandText = "select DeptId , DeptName from Department";
                Adapter.SelectCommand = Command;
                Adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    List<Department> lstDepartment = new List<Department>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        Department d = new Department();
                        d.DepartmentId = (int)dr[0];
                        d.DepartmentName = dr[1].ToString();
                        lstDepartment.Add(d);

                    }

                    foreach (Department dept in lstDepartment)
                    {
                        ComboBoxDepartments.Items.Add(dept);
                    }

                    ComboBoxDepartments.DisplayMemberPath = "DepartmentName";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #region Methods

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FirstName = null;
                if (TextBoxFirstName.Text != null && TextBoxFirstName.Text.Trim() != "")
                {
                    FirstName = "'" + TextBoxFirstName.Text.ToString() + "'";
                }
                else
                {
                    FirstName = "'FirstName'";
                }


                string MiddleName = null;
                if (TextBoxMiddleName.Text != null && TextBoxMiddleName.Text.Trim() != "")
                {
                    MiddleName = "'" + TextBoxMiddleName.Text.ToString() + "'";
                }
                else
                {
                    MiddleName = "NULL";
                }


                string LastName = null;
                if (TextBoxLastName.Text != null && TextBoxLastName.Text.Trim() != "")
                {
                    LastName = "'" + TextBoxLastName.Text.ToString() + "'";
                }
                else
                {
                    LastName = "'LastName'";
                }


                string BirthDate = null;
                if (DatePickerBirthDate.SelectedDate != null)
                {
                    DateTime dt = DatePickerBirthDate.SelectedDate.Value;
                    BirthDate = "'" + dt.Year + "-" + dt.Month + "-" + dt.Day + "'";
                }
                else
                {
                    BirthDate = "NULL";
                }


                string gender = "'M'";
                if ((bool)RadioButtonGenderMale.IsChecked)
                {
                    gender = "'M'";
                }
                else
                {
                    gender = "'F'";
                }


                string Address = null;
                if (TextBoxAddress.Text != null && TextBoxAddress.Text.Trim() != "")
                {
                    Address = "'" + TextBoxAddress.Text + "'";
                }
                else
                {
                    Address = "NULL";
                }


                string ContactNumber = null;
                if (TextBoxContactNumber.Text != null && TextBoxContactNumber.Text.Trim() != "")
                {
                    ContactNumber = "'" + TextBoxContactNumber.Text + "'";
                }
                else
                {
                    ContactNumber = "NULL";
                }


                string EmailId = null;
                if (TextBoxEmailId.Text != null && TextBoxEmailId.Text.Trim() != "")
                {
                    EmailId = "'" + TextBoxEmailId.Text + "'";
                }
                else
                {
                    EmailId = "NULL";
                }


                string JoiningDate = null;
                if (DatePickerJoiningDate.SelectedDate != null)
                {
                    DateTime dt = DatePickerJoiningDate.SelectedDate.Value;
                    JoiningDate = "'" + dt.Year + "-" + dt.Month + "-" + dt.Day + "'";
                }
                else
                {
                    JoiningDate = "NULL";
                }


                string ConfirmationDate = null;
                if (DatePickerConfirmationDate.SelectedDate != null)
                {
                    DateTime dt = DatePickerConfirmationDate.SelectedDate.Value;
                    ConfirmationDate = "'" + dt.Year + "-" + dt.Month + "-" + dt.Day + "'";
                }
                else
                {
                    ConfirmationDate = "NULL";
                }


                string IsResigned = CheckBoxIsResigned.IsChecked == true ? "1" : "0";


                string Salary = null;
                if (TextBoxSalary.Text != null && TextBoxSalary.Text.Trim() != "")
                {
                    Salary = TextBoxSalary.Text;
                }
                else
                {
                    Salary = "NULL";
                }


                string Designation = null;
                if (TextBoxDesignation.Text != null && TextBoxDesignation.Text.Trim() != "")
                {
                    Designation = "'" + TextBoxDesignation.Text + "'";
                }
                else
                {
                    Designation = "NULL";
                }


                string DepatmentId = null;
                if (ComboBoxDepartments.SelectedItem != null)
                {
                    DepatmentId = ((Department)ComboBoxDepartments.SelectedItem).DepartmentId.ToString();
                }
                else
                {
                    DepatmentId = "NULL";
                }

                string strCommand = "INSERT INTO [dbo].[Employee] ([FirstName] ,[MiddleName] ,[LastName] ,[BirthDate] ,[Gender] ,[Address] ,[ContactNumber] ,[EmailId], [JoiningDate], [ConfirmationDate], [IsResigned], [Salary], [Designation], [DeptId]) VALUES (" + FirstName + "," + MiddleName + "," + LastName + "," + BirthDate + "," + gender + "," + Address + "," + ContactNumber + "," + EmailId + "," + JoiningDate + "," + ConfirmationDate + "," + IsResigned + "," + Salary + "," + Designation + "," + DepatmentId + ")";
                SqlCommand Command = new SqlCommand(strCommand, _connection);
                Command.ExecuteNonQuery();
                MessageBox.Show("Employee data saved successfully.", "Save", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }


    public class Department
    {
        #region Properties

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        #endregion
    }
}
