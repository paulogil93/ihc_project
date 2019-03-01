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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace IHC_Project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SqlConnection cn;
        private Stack BackStack = new Stack();
        private string UserType = "";
        private int UserID = 0;

        public Window1()
        {
            InitializeComponent();
            LoginControl login = new LoginControl();
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(login);
        }

        public int GetCurrentUserID()
        {
            return UserID;
        }

        public void SetCurrentUserID(int id)
        {
            UserID = id;
        }

        public string GetCurrenUserType()
        {
            return UserType;
        }

        public void SetCurrentUserType(string type)
        {
            UserType = type;
        }

        public void PushStack(UserControl uc)
        {
            BackStack.Push(uc);
        }

        public UserControl PopStack()
        {
            return (UserControl) BackStack.Pop();
        }

        private SqlConnection GetSGBDConnection()
        {
            string sqlCon = "Data Source=193.136.175.33\\SQLSERVER2012,8293;Initial Catalog=p1g1;User ID=p1g1;Password=JwYML4kr";
            return new SqlConnection(sqlCon);
        }

        private bool TestSGBDConnection()
        {
            if (cn == null) cn = GetSGBDConnection();
            if (cn.State != ConnectionState.Open)
            {
                try
                {
                    cn.Open();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Could not connect to database: " + e.ToString(), "Connection Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            return cn.State == ConnectionState.Open;
        }

        private void OpenSqlConnection()
        {
            if (cn == null) cn = GetSGBDConnection();
            if (cn.State != ConnectionState.Open) cn.Open();
        }

        private void TestSGBD_Click(object sender, RoutedEventArgs e)
        {
            if (TestSGBDConnection() == true) MessageBox.Show("Connection to database OK!");
        }

        private void About_Click(object sender, RoutedEventArgs e)
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

            About about = new About();
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(about);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            UsersControl uc = new UsersControl();
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(uc);
        }

        private object GetSqlAnswer(string query, string field)
        {
            object result;
            OpenSqlConnection();
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result = reader[field].ToString();
            reader.Close();
            cmd.Cancel();
            return result;

        }

        private void Plans_Click(object sender, RoutedEventArgs e)
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

            if (UserType == "client")
            {
                OpenSqlConnection();
                ShowPlan sp = new ShowPlan(int.Parse(GetSqlAnswer("SELECT plano_id FROM GymManagement.cliente_temPlano where nif=" + UserID, "plano_id").ToString()));
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(sp);
            }
            else
            {
                PlansControl pc = new PlansControl();
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(pc);
            }
        }

        private void Diets_Click(object sender, RoutedEventArgs e)
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

            if (UserType == "client")
            {
                OpenSqlConnection();
                ShowDiet sd = new ShowDiet(int.Parse(GetSqlAnswer("SELECT dieta_id FROM GymManagement.cliente_temDieta where nif=" + UserID, "dieta_id").ToString()));
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(sd);
            }
            else
            {
                DietsControl dc = new DietsControl();
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(dc);
            }
        }

        private void Classes_Click(object sender, RoutedEventArgs e)
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

            if (UserType == "client")
            {
                OpenSqlConnection();
                ShowClass sc = new ShowClass(int.Parse(GetSqlAnswer("SELECT codigo_aula FROM GymManagement.cliente_temAula where nif=" + UserID, "codigo_aula").ToString()));
                sc.participantsLabel.Visibility = Visibility.Hidden;
                sc.participantsBox.Visibility = Visibility.Hidden;
                sc.showButton.Visibility = Visibility.Hidden;
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(sc);
            }
            else
            {
                ClassesControl cc = new ClassesControl();
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(cc);
            }
        }

        private void PT_Click(object sender, RoutedEventArgs e)
        {
            PTControl pt = new PTControl(); 
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(pt);
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            ShowUser su = new ShowUser(UserID);
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(su);
        }
    }
}
