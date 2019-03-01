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
using System.Collections;

namespace IHC_Project
{
    /// <summary>
    /// Interaction logic for PTControl.xaml
    /// </summary>
    public partial class PTControl : UserControl
    {
        private SqlConnection cn;

        public PTControl()
        {
            InitializeComponent();
            GetInfo();
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

        public void BindTrainersBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM GymManagement.Pessoa as P join GymManagement.Instrutor as I on P.nif=I.nif", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Instructors");
            trainersComboBox.ItemsSource = ds.Tables[0].DefaultView;
            trainersComboBox.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
        }

        private int GetSelectedID(string username)
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT nif FROM GymManagement.Pessoa where nome= '" + username + "'";
            SqlCommand cmd = new SqlCommand(SqlQuery, cn);
            SqlDataReader reader = null;
            int res = 0;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
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

        public void BindClientsBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM (GymManagement.Pessoa as P join GymManagement.personally_trains as PT on P.nif=PT.cliente_nif) where PT.instrutor_nif=" + GetSelectedID(trainersComboBox.Text), cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Pessoa");
            clientsComboBox.ItemsSource = ds.Tables[0].DefaultView;
            clientsComboBox.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
        }

        public void BindFullClientsBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM (GymManagement.Pessoa as P join GymManagement.Cliente as C on P.nif=C.nif)", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Pessoa");
            clientsComboBox.ItemsSource = ds.Tables[0].DefaultView;
            clientsComboBox.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
        }

        public void BindFullTrainersBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM (GymManagement.Pessoa as P join GymManagement.Instrutor as I on P.nif=I.nif)", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Pessoa");
            trainersComboBox2.ItemsSource = ds.Tables[0].DefaultView;
            trainersComboBox2.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
        }



        private void GetInfo()
        {
            BindTrainersBox();

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (trainersComboBox.Text != "")
            {
                clientsLabel.Visibility = Visibility.Visible;
                clientsComboBox.Visibility = Visibility.Visible;
                showClient.Visibility = Visibility.Visible;
                removeClient.Visibility = Visibility.Visible;
                BindClientsBox();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a trainer from the list to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void Changed_Event(object sender, SelectionChangedEventArgs e)
        {
            clientsLabel.Visibility = Visibility.Hidden;
            clientsComboBox.Visibility = Visibility.Hidden;
            showClient.Visibility = Visibility.Hidden;
            removeClient.Visibility = Visibility.Hidden;
        }

        private void ShowClient_Click(object sender, RoutedEventArgs e)
        {
            if (clientsComboBox.Text != "")
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

                ShowUser su = new ShowUser(GetSelectedID(clientsComboBox.Text));
                Main.LayoutRoot.Children.Clear();
                Main.LayoutRoot.Children.Add(su);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a client from the list to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void RemoveClient_Click(object sender, RoutedEventArgs e)
        {
            if (clientsComboBox.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to remove client \"" + clientsComboBox.Text + "\" from the trainer list?", "Remove Client", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show(clientsComboBox.Text + " was removed from the trainer.", "Client Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                        clientsComboBox.Text = "";
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a client from the list to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            saveButton.Visibility = Visibility.Visible;
            saveButton.Margin = new Thickness(307,0,100,0);
            backButton.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Visible;
            trainersComboBox.Visibility = Visibility.Hidden;
            trainersComboBox2.Visibility = Visibility.Visible;
            BindFullTrainersBox();
            clientsLabel.Visibility = Visibility.Visible;
            clientsComboBox.Visibility = Visibility.Visible;
            showButton.Visibility = Visibility.Hidden;
            showClient.Visibility = Visibility.Hidden;
            removeClient.Visibility = Visibility.Hidden;
            addButton.Visibility = Visibility.Hidden;
            BindFullClientsBox();
        }

        private void UploadInformation()
        {
            //TODO: Upload to database
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update information?", "Update Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("The new assignment was uploaded.", "Assignment Uploaded", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if(trainersComboBox2.Text != "" && clientsComboBox.Text != "")
            {
                UploadInformation();
                saveButton.Visibility = Visibility.Hidden;
                backButton.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Visible;
                Label1.Visibility = Visibility.Visible;
                Label2.Visibility = Visibility.Hidden;
                trainersComboBox.Visibility = Visibility.Visible;
                BindTrainersBox();
                trainersComboBox2.Visibility = Visibility.Hidden;
                clientsLabel.Visibility = Visibility.Hidden;
                clientsComboBox.Visibility = Visibility.Hidden;
                showButton.Visibility = Visibility.Visible;
                showClient.Visibility = Visibility.Hidden;
                removeClient.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Visible;
                
            }
            else
            {
                MessageBox.Show("Please select both of the values to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            PTControl pt = new PTControl();
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(pt);

            
        }
    }
}
