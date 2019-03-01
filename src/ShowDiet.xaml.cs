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
    /// Interaction logic for ShowDiet.xaml
    /// </summary>
    public partial class ShowDiet : UserControl
    {
        private SqlConnection cn;
        private int CurrentID;

        public ShowDiet(int id)
        {
            CurrentID = id;
            InitializeComponent();
            GetDietInfo();
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

        public void BindFoodBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT nome FROM GymManagement.Alimento", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Alimento");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
        }

        private void SetUIPermissions()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            if (Main.GetCurrenUserType() == "client")
            {
                editButton.Visibility = Visibility.Hidden;
                deleteButton.Visibility = Visibility.Hidden;
            }
        }

        private void GetDietInfo()
        {
            if (CurrentID != 0)
            {
                BindFoodBox(foodBox1);
                BindFoodBox(foodBox2);
                BindFoodBox(foodBox3);
                BindFoodBox(foodBox4);
                BindFoodBox(foodBox5);
                OpenSqlConnection();
                string SqlQuery = "SELECT nome FROM GymManagement.Dieta where numero=" + CurrentID;
                SqlCommand cmd = new SqlCommand(SqlQuery, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                dietName.Content = reader["nome"].ToString();
            }
            else
            {
                dietName.Content = "New diet";
                BindFoodBox(foodBox1);
                BindFoodBox(foodBox2);
                BindFoodBox(foodBox3);
                BindFoodBox(foodBox4);
                BindFoodBox(foodBox5);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(Main.PopStack());
        }
    }
}
