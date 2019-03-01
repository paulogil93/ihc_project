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
    /// Interaction logic for DietsControl.xaml
    /// </summary>
    public partial class DietsControl : UserControl
    {
        private SqlConnection cn;

        public DietsControl()
        {
            InitializeComponent();
            BindComboBox(dietsComboBox);
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
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT nome FROM GymManagement.Dieta", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Dieta");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
        }

        private int GetSelectedID()
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT numero FROM GymManagement.Dieta where nome= '" + dietsComboBox.Text + "'";
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

        private void SetUIPermissions()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            if (Main.GetCurrenUserType() == "client")
            {
                addButton.Visibility = Visibility.Hidden;
            }
        }

        private void ShowDiet_Click(object sender, RoutedEventArgs e)
        {
            if (dietsComboBox.Text != "")
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
                
                ShowDiet sd = new ShowDiet(GetSelectedID());
                Main.LayoutRoot.Children.Clear();
                Main.LayoutRoot.Children.Add(sd);
            }
            else
            {
                MessageBox.Show("Please select a diet from the list to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddDiet_Click(object sender, RoutedEventArgs e)
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

            ShowDiet sd = new ShowDiet(0);
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(sd);
        }
    }
}
