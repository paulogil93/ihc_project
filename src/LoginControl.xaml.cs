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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private SqlConnection cn;
        private Button details = null;

        public LoginControl()
        {
            InitializeComponent();
            SetUI();
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

        private void SetUI()
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            List<int> index = new List<int>();
            foreach (var child in Main.stackPanel.Children)
            {
                if (child is Button btn)
                {
                    if (btn.Name == "detailsButton")
                    {
                        details = btn;
                        index.Add(Main.stackPanel.Children.IndexOf(btn));
                    }
                }
            }

            foreach (int idx in index)
            {
                if (idx != 0)
                {
                    Main.stackPanel.Children.RemoveAt(idx - 1);
                }
                else
                {
                    Main.stackPanel.Children.RemoveAt(0);
                }
            }
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private string GetUserType(string UserName)
        {
            string result;
            OpenSqlConnection();
            string SqlQuery = "SELECT type from GymManagement.Login where username= '" + UserName + "'";
            SqlCommand cmd = new SqlCommand(SqlQuery, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result = reader["type"].ToString();
            cmd.Cancel();
            reader.Close();
            return result;
        }

        private int GetID()
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT person_id FROM GymManagement.Login where username= '" + username.Text + "'";
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            Main.SetCurrentUserType(GetUserType(username.Text));

            if (Main.GetCurrenUserType() != "admin")
            {
                Main.SetCurrentUserID(GetID());
            }

            OpenSqlConnection();
            string sqlquery = "SELECT * FROM GymManagement.Login where username= '" + username.Text  + "' and userpass= '" + CreateMD5(password.Password) + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sqlquery, cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                
                switch (GetUserType(username.Text))
                {
                    case "admin":
                        List<int> index = new List<int>();
                        HomeControl hc = new HomeControl(username.Text);
                        Main.usersButton.Visibility = Visibility.Visible;
                        Main.plansButton.Visibility = Visibility.Visible;
                        Main.dietsButton.Visibility = Visibility.Visible;
                        Main.classesButton.Visibility = Visibility.Visible;
                        Main.ptButton.Visibility = Visibility.Visible;
                        Main.LayoutRoot.Children.Clear();
                        Main.LayoutRoot.Children.Add(hc);
                        break;
                    case "client":
                        Main = (Window1)Application.Current.MainWindow;
                        Main.usersButton.Visibility = Visibility.Visible;
                        Main.detailsButton.Visibility = Visibility.Visible;
                        Main.plansButton.Visibility = Visibility.Visible;
                        Main.dietsButton.Visibility = Visibility.Visible;
                        Main.classesButton.Visibility = Visibility.Visible;
                        Main.ptButton.Visibility = Visibility.Visible;
                        index = new List<int>();

                        foreach(var child in Main.stackPanel.Children)
                        {
                            if (child is Button btn)
                            {
                                if (btn.Name == "ptButton" || btn.Name == "usersButton")
                                {
                                    index.Add(Main.stackPanel.Children.IndexOf(btn));
                                }
                            }
                        }

                        foreach(int idx in index)
                        {
                            if (idx != 0)
                            {
                                Main.stackPanel.Children.RemoveAt(idx - 1);
                            }
                            else
                            {
                                Main.stackPanel.Children.RemoveAt(0);
                            }
                        }

                        Main.stackPanel.Children.Insert(0, details);

                        foreach (var child in Main.stackPanel.Children)
                        {
                            if (child is Button btn)
                            {
                                btn.Width += 15;
                            }
                        }

                        hc = new HomeControl(username.Text);
                        Main.PushStack(hc);
                        
                        Main.LayoutRoot.Children.Clear();
                        Main.LayoutRoot.Children.Add(hc);
                        break;
                    case "trainer":
                        index = new List<int>();
                        foreach (var child in Main.stackPanel.Children)
                        {
                            if (child is Button btn)
                            {
                                if (btn.Name == "detailsButton" || btn.Name == "dietsButton")
                                {
                                    index.Add(Main.stackPanel.Children.IndexOf(btn));
                                }
                            }
                        }

                        foreach (int idx in index)
                        {
                            if (idx != 0)
                            {
                                Main.stackPanel.Children.RemoveAt(idx);
                            }
                            else
                            {
                                Main.stackPanel.Children.RemoveAt(0);
                            }
                        }

                        foreach (var child in Main.stackPanel.Children)
                        {
                            if (child is Button btn)
                            {
                                btn.Width += 15;
                            }
                        }

                        Main = (Window1)Application.Current.MainWindow;
                        hc = new HomeControl(username.Text);
                        Main.usersButton.Visibility = Visibility.Visible;
                        Main.plansButton.Visibility = Visibility.Visible;
                        Main.classesButton.Visibility = Visibility.Visible;
                        Main.ptButton.Visibility = Visibility.Visible;
                        Main.LayoutRoot.Children.Clear();
                        Main.LayoutRoot.Children.Add(hc);
                        break;
                    case "nutricionist":
                        List<Button> rmList = new List<Button>();
                        foreach (var child in Main.stackPanel.Children)
                        {
                            if (child is Button btn)
                            {
                                if (btn.Name == "plansButton" || 
                                    btn.Name == "classesButton" || btn.Name == "ptButton")
                                {
                                    rmList.Add(btn);
                                }
                            }
                        }

                        foreach (Button btn in rmList)
                        {
                            Main.stackPanel.Children.Remove(btn);
                        }
                        
                        foreach (var child in Main.stackPanel.Children)
                        {
                            if (child is Button btn)
                            {
                                btn.Width += 60;
                            }
                        }
                        
                        Main = (Window1)Application.Current.MainWindow;
                        hc = new HomeControl(username.Text);
                        Main.usersButton.Visibility = Visibility.Visible;
                        Main.plansButton.Visibility = Visibility.Visible;
                        Main.dietsButton.Visibility = Visibility.Visible;
                        Main.classesButton.Visibility = Visibility.Visible;
                        Main.ptButton.Visibility = Visibility.Visible;
                        Main.LayoutRoot.Children.Clear();
                        Main.LayoutRoot.Children.Add(hc);
                        break;
                }
                
            }
            else
            {
                warningLabel.Visibility = System.Windows.Visibility.Visible;
                username.Clear();
                password.Clear();
            }
            
        }

        private void KeyEnter_Event(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            e.Handled = true;
            Login_Click(sender, e);
        }
    }
}
