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
    /// Interaction logic for ClassesControl.xaml
    /// </summary>
    public partial class ClassesControl : UserControl
    {
        private SqlConnection cn;

        public ClassesControl()
        {
            InitializeComponent();
            BindComboBox(classesComboBox);
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

        public void BindComboBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT designacao FROM GymManagement.AulaInstancia", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "AulaInstancia");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["designacao"].ToString();
        }

        public void BindTrainersBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM GymManagement.Pessoa as P join GymManagement.Instrutor as I on P.nif=I.nif", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Instrutor");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
        }

        public void BindParticipantsBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM GymManagement.Pessoa as P join GymManagement.Cliente as C on P.nif=C.nif", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Cliente");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
        }

        private void SetUIPermissions()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            if (Main.GetCurrenUserType() == "client")
            {
                addButton.Visibility = Visibility.Hidden;
            }
        }

        private int GetSelectedID()
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT codigo FROM GymManagement.AulaInstancia where designacao= '" + classesComboBox.Text + "'";
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

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if(classesComboBox.Text != "")
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

                ShowClass sc = new ShowClass(GetSelectedID());
                Main.LayoutRoot.Children.Clear();
                Main.LayoutRoot.Children.Add(sc);
            }
            else
            {
                MessageBox.Show("You must select an class to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddClass_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            ShowClass sc = new ShowClass(0);
            IEnumerator controls = Main.LayoutRoot.Children.GetEnumerator();

            while (controls.MoveNext())
            {
                if (controls.Current is UserControl)
                {
                    Main.PushStack(controls.Current as UserControl);
                    break;
                }
            }

            sc.className.Content = "New Class";
            BindTrainersBox(sc.instructorBox);
            sc.instructorBox.Text = "";
            sc.timeBox.Text = "";
            sc.durationBox.Text = "";
            sc.vacanciesBox.Text = "";
            sc.nameLabel.Visibility = Visibility.Visible;
            sc.nameBox.Visibility = Visibility.Visible;
            sc.nameBox.Text = "";
            sc.participantsBox.Visibility = Visibility.Hidden;
            sc.participantsLabel.Visibility = Visibility.Hidden;
            sc.showButton.Visibility = Visibility.Hidden;
            sc.instructorBox.IsEnabled = true;
            sc.timeBox.IsEnabled = true;
            sc.durationBox.IsEnabled = true;
            sc.vacanciesBox.IsReadOnly = false;
            sc.nameBox.IsEnabled = true;
            sc.deleteButton.Visibility = Visibility.Hidden;
            sc.editButton.Visibility = Visibility.Hidden;
            sc.saveButton.Visibility = Visibility.Visible;
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(sc);
        }
    }
}
