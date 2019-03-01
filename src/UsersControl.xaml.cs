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
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace IHC_Project
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UsersControl : UserControl
    {
        private SqlConnection cn;

        public UsersControl()
        {
            InitializeComponent();
            BindComboBox(ComboBox1);
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

        private void SetUIPermissions()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            if (Main.GetCurrenUserType() == "client" || Main.GetCurrenUserType() == "trainer" || Main.GetCurrenUserType() == "nutricionist")
            {
                addButton.Visibility = Visibility.Hidden;
            }
        }

        public void BindComboBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT nome FROM GymManagement.Pessoa", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Pessoa");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
        }

        private int GetSelectedID()
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT nif FROM GymManagement.Pessoa where nome= '" + ComboBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(SqlQuery, cn);
            SqlDataReader reader = null;
            int res = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    res = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("! " + e.ToString());
            }
            
            return res;
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox1.SelectedIndex >= 0)
            {
                Window1 Main = (Window1)Application.Current.MainWindow;
                IEnumerator controls = Main.LayoutRoot.Children.GetEnumerator();

                while (controls.MoveNext())
                {
                    if (controls.Current is UserControl)
                    {
                        Main.PushStack(controls.Current as UserControl);
                        break;
                    }
                }

                ShowUser su = new ShowUser(GetSelectedID());
                Main.LayoutRoot.Children.Clear();
                Main.LayoutRoot.Children.Add(su);
            }
            else
            {
                MessageBox.Show("You must select an user to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            ShowUser su = new ShowUser(0);
            Window1 Main = (Window1)Application.Current.MainWindow;
            IEnumerator controls = Main.LayoutRoot.Children.GetEnumerator();

            while (controls.MoveNext())
            {
                if (controls.Current is UserControl)
                {
                    Main.PushStack(controls.Current as UserControl);
                    break;
                }
            }

            su.userName.Content = "New user";
            su.fidLabel.Content = "";
            su.typeLabel.Visibility = Visibility.Visible;
            su.typeBox.Visibility = Visibility.Visible;
            su.continueButton.Visibility = Visibility.Visible;
            su.Label1.Visibility = Visibility.Hidden;
            su.Label2.Visibility = Visibility.Hidden;
            su.Label3.Visibility = Visibility.Hidden;
            su.Label4.Visibility = Visibility.Hidden;
            su.Label5.Visibility = Visibility.Hidden;
            su.fidTextBox.Visibility = Visibility.Hidden;
            su.userNameBox.Visibility = Visibility.Hidden;
            su.userAddressBox.Visibility = Visibility.Hidden;
            su.birthDate.Visibility = Visibility.Hidden;
            su.numberBox.Visibility = Visibility.Hidden;
            su.joinedBox.Visibility = Visibility.Hidden;
            su.packageBox.Visibility = Visibility.Hidden;
            su.hoursBox.Visibility = Visibility.Hidden;

            su.deleteButton.Visibility = Visibility.Hidden;
            su.editButton.Visibility = Visibility.Hidden;
            su.saveButton.Visibility = Visibility.Visible;
            
            //Adding user control
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(su);
        }
    }
}
