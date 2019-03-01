using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;

namespace IHC_Project
{
    /// <summary>
    /// Interaction logic for ShowUser.xaml
    /// </summary>
    public partial class ShowUser : UserControl
    {
        private SqlConnection cn;
        private int CurrentID;

        public ShowUser(int id)
        {
            InitializeComponent();
            this.CurrentID = id;
            GetUserInfo();
            SetUIPermissions();
        }

        private SqlConnection GetSGBDConnection()
        {
            string sqlCon = "Data Source=193.136.175.33\\SQLSERVER2012,8293;Initial Catalog=p1g1;User ID=p1g1;Password=JwYML4kr";
            return new SqlConnection(sqlCon);
        }

        private void OpenSqlConnection()
        {
            if (cn == null) cn = GetSGBDConnection();
            if (cn.State != ConnectionState.Open) cn.Open();
        }

        private string GetPersonType(int id)
        {
            OpenSqlConnection();
            //Checks if given id belongs to Cliente table
            string SqlQuery = "SELECT * FROM GymManagement.Cliente where nif=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return "client";
            }
            sda.Dispose();
            dt.Clear();

            //Checks if given id belongs to Instructor table
            SqlQuery = "SELECT * FROM GymManagement.Instrutor where nif=" + id;
            sda = new SqlDataAdapter(SqlQuery, cn);
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return "instructor";
            }
            sda.Dispose();
            dt.Clear();

            //Checks if given id belongs to Nutricionist table
            SqlQuery = "SELECT * FROM GymManagement.Nutricionista where nif=" + id;
            sda = new SqlDataAdapter(SqlQuery, cn);
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return "nutricionist";
            }
            sda.Dispose();
            dt.Clear();

            return "none";
        }

        private void SetUIPermissions()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            if (Main.GetCurrenUserType() == "client")
            {
                deleteButton.Visibility = Visibility.Hidden;
                editButton.Visibility = Visibility.Visible;
            }
            else if (Main.GetCurrenUserType() == "trainer" || Main.GetCurrenUserType() == "nutricionist")
            {
                deleteButton.Visibility = Visibility.Hidden;
                editButton.Visibility = Visibility.Hidden;
            }
        }

        private void GetUserInfo()
        {
            switch (GetPersonType(this.CurrentID))
            {
                case "client":
                    int PackageID = 0;
                    BindComboBox(packageBox);
                    OpenSqlConnection();
                    fidLabel.Content = this.CurrentID.ToString();
                    fidTextBox.Text = this.CurrentID.ToString();
                    //Get data from database
                    string SqlQuery = "SELECT P.nome, P.data_nascimento, P.morada, C.numero_cliente, C.data_inscricao, C.pacote, C.numero_horas FROM (GymManagement.Pessoa as P join GymManagement.Cliente as C on P.nif=C.nif) where P.nif=" + this.CurrentID;
                    SqlCommand cmd = new SqlCommand(SqlQuery, cn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    
                    userName.Content = reader["nome"].ToString();
                    userNameBox.Text = reader["nome"].ToString();
                    userAddressBox.Text = reader["morada"].ToString();
                    birthDate.Text = reader["data_nascimento"].ToString();
                    numberBox.Text = reader["numero_cliente"].ToString();
                    joinedBox.Text = reader["data_inscricao"].ToString();
                    hoursBox.Text = reader["numero_horas"].ToString();
                    PackageID = int.Parse(reader["pacote"].ToString());
                    packageBox.SelectedIndex = PackageID - 1;
                    reader.Close();

                    //Changing necessary control visibilities
                    joinedLabel.Visibility = Visibility.Visible;
                    joinedBox.Visibility = Visibility.Visible;
                    packageLabel.Visibility = Visibility.Visible;
                    packageBox.Visibility = Visibility.Visible;
                    hoursLabel.Visibility = Visibility.Visible;
                    hoursBox.Visibility = Visibility.Visible;
                    //Disable UI elements to prevent editing
                    fidTextBox.IsReadOnly = true;
                    userNameBox.IsReadOnly = true;
                    userAddressBox.IsReadOnly = true;
                    birthDate.IsEnabled = false;
                    numberBox.IsReadOnly = true;
                    joinedBox.IsEnabled = false;
                    hoursBox.IsReadOnly = true;
                    packageBox.IsEnabled = false;

                    //Making the UI elements look beter when disabled
                    birthDate.Opacity = 0.6;
                    joinedBox.Opacity = 0.4;
                    break;
                case "instructor":
                    OpenSqlConnection();
                    fidLabel.Content = this.CurrentID.ToString();
                    fidTextBox.Text = this.CurrentID.ToString();
                    //Get data from database
                    SqlQuery = "SELECT P.nome, P.data_nascimento, P.morada, F.numero_func, F.ordenado, I.qualificacao  FROM (GymManagement.Pessoa as P join GymManagement.Funcionario as F on P.nif=F.nif) join GymManagement.Instrutor as I on P.nif=I.nif where P.nif=" + this.CurrentID;
                    cmd = new SqlCommand(SqlQuery, cn);
                    reader = cmd.ExecuteReader();

                    reader.Read();

                    userName.Content = reader["nome"].ToString();
                    userNameBox.Text = reader["nome"].ToString();
                    userAddressBox.Text = reader["morada"].ToString();
                    birthDate.Text = reader["data_nascimento"].ToString();
                    numberBox.Text = reader["numero_func"].ToString();
                    salaryBox.Text = reader["ordenado"].ToString();
                    areaBox.Text = reader["qualificacao"].ToString();
                    reader.Close();

                    //Changing necessary control visibilities
                    salaryLabel.Visibility = Visibility.Visible;
                    salaryBox.Visibility = Visibility.Visible;
                    areaLabel.Visibility = Visibility.Visible;
                    areaBox.Visibility = Visibility.Visible;
                    //Disable UI elements to prevent editing
                    fidTextBox.IsReadOnly = true;
                    userNameBox.IsReadOnly = true;
                    userAddressBox.IsReadOnly = true;
                    birthDate.IsEnabled = false;
                    numberBox.IsReadOnly = true;
                    salaryBox.IsReadOnly = true;
                    areaBox.IsReadOnly = true;

                    //Making the UI elements look beter when disabled
                    birthDate.Opacity = 0.9;
                    break;
                case "nutricionist":
                    OpenSqlConnection();
                    fidLabel.Content = this.CurrentID.ToString();
                    fidTextBox.Text = this.CurrentID.ToString();
                    //Get data from database
                    SqlQuery = "SELECT P.nome, P.data_nascimento, P.morada, F.numero_func, F.ordenado  FROM (GymManagement.Pessoa as P join GymManagement.Funcionario as F on P.nif=F.nif) where P.nif=" + this.CurrentID;
                    cmd = new SqlCommand(SqlQuery, cn);
                    reader = cmd.ExecuteReader();

                    reader.Read();

                    userName.Content = reader["nome"].ToString();
                    userNameBox.Text = reader["nome"].ToString();
                    userAddressBox.Text = reader["morada"].ToString();
                    birthDate.Text = reader["data_nascimento"].ToString();
                    numberBox.Text = reader["numero_func"].ToString();
                    salaryBox.Text = reader["ordenado"].ToString();
                    
                    reader.Close();

                    //Changing necessary control visibilities
                    salaryLabel.Visibility = Visibility.Visible;
                    salaryBox.Visibility = Visibility.Visible;
                   
                    //Disable UI elements to prevent editing
                    fidTextBox.IsReadOnly = true;
                    userNameBox.IsReadOnly = true;
                    userAddressBox.IsReadOnly = true;
                    birthDate.IsEnabled = false;
                    numberBox.IsReadOnly = true;
                    salaryBox.IsReadOnly = true;

                    //Making the UI elements look beter when disabled
                    birthDate.Opacity = 0.9;
                    break;
            }
        }

        public void BindComboBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT descricao FROM GymManagement.Pacote", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Pacote");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["descricao"].ToString();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Enabling UI elements to allow editing
            fidTextBox.IsReadOnly = false;
            userNameBox.IsReadOnly = false;
            userAddressBox.IsReadOnly = false;
            birthDate.IsEnabled = true;
            numberBox.IsReadOnly = false;
            joinedBox.IsEnabled = true;
            hoursBox.IsReadOnly = false;
            packageBox.IsEnabled = true;
            salaryBox.IsReadOnly = false;
            areaBox.IsReadOnly = false;

            //Making the UI elements look beter when enabled
            birthDate.Opacity = 1;
            joinedBox.Opacity = 1;

            //Changing other UI controls
            deleteButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Visible;
        }

        private bool UploadInformation()
        {
            //TODO: Upload to database
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update information?", "Update Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("The user information of " + userNameBox.Text + " was updated.", "User Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }
        }

        private bool CheckData()
        {
            if(fidTextBox.Text != "")
            {
                if(userNameBox.Text != "")
                {
                    if(numberBox.Text == "")
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
            

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(CheckData())
            {
                MessageBox.Show("One or more required fields are empty or incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (UploadInformation())
            {
                Window1 Main = (Window1)Application.Current.MainWindow;

                switch(Main.GetCurrenUserType())
                {
                    case "admin":
                        //Changing other UI controls
                        deleteButton.Visibility = Visibility.Visible;
                        saveButton.Visibility = Visibility.Hidden;
                        editButton.Visibility = Visibility.Visible;
                        //Disable UI elements to prevent editing
                        fidTextBox.IsReadOnly = true;
                        userNameBox.IsReadOnly = true;
                        userAddressBox.IsReadOnly = true;
                        birthDate.IsEnabled = false;
                        numberBox.IsReadOnly = true;
                        joinedBox.IsEnabled = false;
                        hoursBox.IsReadOnly = true;
                        packageBox.IsEnabled = false;
                        salaryBox.IsReadOnly = false;
                        areaBox.IsReadOnly = true;
                        //Making the UI elements look beter when disabled
                        birthDate.Opacity = 0.6;
                        joinedBox.Opacity = 0.4;
                        //Updating viewing information
                        userName.Content = userNameBox.Text;
                        fidLabel.Content = fidTextBox.Text;
                        break;
                    case "client":
                        //Changing other UI controls
                        deleteButton.Visibility = Visibility.Hidden;
                        saveButton.Visibility = Visibility.Hidden;
                        editButton.Visibility = Visibility.Visible;
                        //Disable UI elements to prevent editing
                        fidTextBox.IsReadOnly = true;
                        userNameBox.IsReadOnly = true;
                        userAddressBox.IsReadOnly = true;
                        birthDate.IsEnabled = false;
                        numberBox.IsReadOnly = true;
                        joinedBox.IsEnabled = false;
                        hoursBox.IsReadOnly = true;
                        packageBox.IsEnabled = false;
                        salaryBox.IsReadOnly = false;
                        areaBox.IsReadOnly = true;
                        //Making the UI elements look beter when disabled
                        birthDate.Opacity = 0.6;
                        joinedBox.Opacity = 0.4;
                        //Updating viewing information
                        userName.Content = userNameBox.Text;
                        fidLabel.Content = fidTextBox.Text;
                        break;
                }
                
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(Main.PopStack());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + userName.Content + " from the system?", "Delete User", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show(userName.Content + " as removed from the system.", "User Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if(typeBox.Text == "")
            {
                MessageBox.Show("Please select a type to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                switch(typeBox.Text)
                {
                    case "Client":
                        typeLabel.Visibility = Visibility.Hidden;
                        typeBox.Visibility = Visibility.Hidden;
                        continueButton.Visibility = Visibility.Hidden;
                        Label1.Visibility = Visibility.Visible;
                        Label2.Visibility = Visibility.Visible;
                        Label3.Visibility = Visibility.Visible;
                        Label4.Visibility = Visibility.Visible;
                        Label5.Visibility = Visibility.Visible;
                        fidTextBox.Visibility = Visibility.Visible;
                        userNameBox.Visibility = Visibility.Visible;
                        userAddressBox.Visibility = Visibility.Visible;
                        birthDate.Visibility = Visibility.Visible;
                        numberBox.Visibility = Visibility.Visible;
                        joinedLabel.Visibility = Visibility.Visible;
                        joinedBox.Visibility = Visibility.Visible;
                        packageLabel.Visibility = Visibility.Visible;
                        packageBox.Visibility = Visibility.Visible;
                        BindComboBox(packageBox);
                        hoursLabel.Visibility = Visibility.Visible;
                        hoursBox.Visibility = Visibility.Visible;
                        break;
                    case "Instructor":
                        typeLabel.Visibility = Visibility.Hidden;
                        typeBox.Visibility = Visibility.Hidden;
                        continueButton.Visibility = Visibility.Hidden;
                        Label1.Visibility = Visibility.Visible;
                        Label2.Visibility = Visibility.Visible;
                        Label3.Visibility = Visibility.Visible;
                        Label4.Visibility = Visibility.Visible;
                        Label5.Visibility = Visibility.Visible;
                        fidTextBox.Visibility = Visibility.Visible;
                        userNameBox.Visibility = Visibility.Visible;
                        userAddressBox.Visibility = Visibility.Visible;
                        birthDate.Visibility = Visibility.Visible;
                        numberBox.Visibility = Visibility.Visible;
                        joinedLabel.Visibility = Visibility.Hidden;
                        joinedBox.Visibility = Visibility.Hidden;
                        packageLabel.Visibility = Visibility.Hidden;
                        packageBox.Visibility = Visibility.Hidden;
                        hoursLabel.Visibility = Visibility.Hidden;
                        hoursBox.Visibility = Visibility.Hidden;
                        salaryLabel.Visibility = Visibility.Visible;
                        salaryBox.Visibility = Visibility.Visible;
                        areaLabel.Visibility = Visibility.Visible;
                        areaBox.Visibility = Visibility.Visible;
                        break;
                    case "Nutricionist":
                        typeLabel.Visibility = Visibility.Hidden;
                        typeBox.Visibility = Visibility.Hidden;
                        continueButton.Visibility = Visibility.Hidden;
                        Label1.Visibility = Visibility.Visible;
                        Label2.Visibility = Visibility.Visible;
                        Label3.Visibility = Visibility.Visible;
                        Label4.Visibility = Visibility.Visible;
                        Label5.Visibility = Visibility.Visible;
                        fidTextBox.Visibility = Visibility.Visible;
                        userNameBox.Visibility = Visibility.Visible;
                        userAddressBox.Visibility = Visibility.Visible;
                        birthDate.Visibility = Visibility.Visible;
                        numberBox.Visibility = Visibility.Visible;
                        joinedLabel.Visibility = Visibility.Hidden;
                        joinedBox.Visibility = Visibility.Hidden;
                        packageLabel.Visibility = Visibility.Hidden;
                        packageBox.Visibility = Visibility.Hidden;
                        hoursLabel.Visibility = Visibility.Hidden;
                        hoursBox.Visibility = Visibility.Hidden;
                        salaryLabel.Visibility = Visibility.Visible;
                        salaryBox.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

    }
}
