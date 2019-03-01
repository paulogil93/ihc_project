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
    /// Interaction logic for PlansControl.xaml
    /// </summary>
    public partial class PlansControl : UserControl
    {
        private SqlConnection cn;

        public PlansControl()
        {
            InitializeComponent();
            BindComboBox(plansComboBox);
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
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT descricao FROM GymManagement.Plano", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Plano");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["descricao"].ToString();
        }

        public void BindTrainersBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT nome FROM GymManagement.Pessoa as P join GymManagement.Instrutor as I on P.nif=I.nif", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Exercicio");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
        }

        private void SetUIPermissions()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            if(Main.GetCurrenUserType() == "client")
            {
                addButton.Visibility = Visibility.Hidden;
            }
        }

        private int GetSelectedID()
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT plano_id FROM GymManagement.Plano where descricao= '" + plansComboBox.Text + "'";
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

        private void ShowPlan_Click(object sender, RoutedEventArgs e)
        {
            if (plansComboBox.SelectedIndex >= 0)
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

                ShowPlan sp = new ShowPlan(GetSelectedID());
                Main.LayoutRoot.Children.Clear();
                Main.LayoutRoot.Children.Add(sp);
            }
            else
            {
                MessageBox.Show("You must select an plan to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddPlan_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            ShowPlan NewPlan = new ShowPlan(0);
            IEnumerator controls = Main.LayoutRoot.Children.GetEnumerator();

            while (controls.MoveNext())
            {
                if (controls.Current is UserControl)
                {
                    Main.PushStack(controls.Current as UserControl);
                    break;
                }
            }

            NewPlan.deleteButton.Visibility = Visibility.Hidden;
            NewPlan.editButton.Visibility = Visibility.Hidden;
            NewPlan.saveButton.Visibility = Visibility.Visible;
            NewPlan.descriptionBox.Visibility = Visibility.Visible;
            NewPlan.durationBox.Visibility = Visibility.Visible;
            NewPlan.seriesBox.Visibility = Visibility.Visible;
            NewPlan.descriptionBox2.Visibility = Visibility.Visible;
            NewPlan.durationBox2.Visibility = Visibility.Visible;
            NewPlan.seriesBox2.Visibility = Visibility.Visible;
            NewPlan.descriptionBox3.Visibility = Visibility.Visible;
            NewPlan.durationBox3.Visibility = Visibility.Visible;
            NewPlan.seriesBox3.Visibility = Visibility.Visible;
            NewPlan.descriptionBox4.Visibility = Visibility.Visible;
            NewPlan.durationBox4.Visibility = Visibility.Visible;
            NewPlan.seriesBox4.Visibility = Visibility.Visible;
            NewPlan.descriptionBox5.Visibility = Visibility.Visible;
            NewPlan.durationBox5.Visibility = Visibility.Visible;
            NewPlan.seriesBox5.Visibility = Visibility.Visible;
            NewPlan.nameLabel.Visibility = Visibility.Visible;
            NewPlan.nameBox.Visibility = Visibility.Visible;
            NewPlan.trainerLabel.Visibility = Visibility.Visible;
            NewPlan.trainersBox.Visibility = Visibility.Visible;
            NewPlan.descriptionBox.IsEnabled = true;
            NewPlan.durationBox.IsEnabled = true;
            NewPlan.seriesBox.IsEnabled = true;
            NewPlan.descriptionBox2.IsEnabled = true;
            NewPlan.durationBox2.IsEnabled = true;
            NewPlan.seriesBox2.IsEnabled = true;
            NewPlan.descriptionBox3.IsEnabled = true;
            NewPlan.durationBox3.IsEnabled = true;
            NewPlan.seriesBox3.IsEnabled = true;
            NewPlan.descriptionBox4.IsEnabled = true;
            NewPlan.durationBox4.IsEnabled = true;
            NewPlan.seriesBox4.IsEnabled = true;
            NewPlan.descriptionBox5.IsEnabled = true;
            NewPlan.durationBox5.IsEnabled = true;
            NewPlan.seriesBox5.IsEnabled = true;
            BindTrainersBox(NewPlan.trainersBox);
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(NewPlan);
        }
    }
}
