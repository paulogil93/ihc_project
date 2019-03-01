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
    /// Interaction logic for ShowPlan.xaml
    /// </summary>
    public partial class ShowPlan : UserControl
    {
        private SqlConnection cn;
        private int CurrentID;

        public ShowPlan(int id)
        {
            InitializeComponent();
            CurrentID = id;
            GetPlanInfo();
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

        public void BindDescriptionBox(ComboBox cb)
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT descricao FROM GymManagement.Exercicio", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Exercicio");
            cb.ItemsSource = ds.Tables[0].DefaultView;
            cb.DisplayMemberPath = ds.Tables[0].Columns["descricao"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
        }

        public void BindTrainersBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT nome FROM GymManagement.Pessoa as P join GymManagement.Instrutor as I on P.nif=I.nif", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Instrutores");
            trainersBox.ItemsSource = ds.Tables[0].DefaultView;
            trainersBox.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
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

        private ComboBox GetUIControl(string type, int index)
        {
            Dictionary<string, ComboBox> description = new Dictionary<string, ComboBox>
            {
                { "descriptionBox1", descriptionBox },
                { "descriptionBox2", descriptionBox2 },
                { "descriptionBox3", descriptionBox3 },
                { "descriptionBox4", descriptionBox4 },
                { "descriptionBox5", descriptionBox5 }
            };
            Dictionary<string, ComboBox> duration = new Dictionary<string, ComboBox>
            {
                { "durationBox1", durationBox },
                { "durationBox2", durationBox2 },
                { "durationBox3", durationBox3 },
                { "durationBox4", durationBox4 },
                { "durationBox5", durationBox5 }
            };
            Dictionary<string, ComboBox> series = new Dictionary<string, ComboBox>
            {
                { "seriesBox1", seriesBox },
                { "seriesBox2", seriesBox2 },
                { "seriesBox3", seriesBox3 },
                { "seriesBox4", seriesBox4 },
                { "seriesBox5", seriesBox5 }
            };
            string str_description = "descriptionBox";
            string str_duration = "durationBox";
            string str_series = "seriesBox";

            

            switch (type)
            {
                case "description":
                    return description[str_description + index];
                case "duration":
                    return duration[str_duration + index];
                case "series":
                    return series[str_series + index];
                default:
                    return null;
            }
            
        }

        private void GetPlanInfo()
        {
            BindTrainersBox();
            for (int i = 1; i <= 5; i++)
            {
                BindDescriptionBox(GetUIControl("description", i));
            }

            OpenSqlConnection();
            string SqlQuery = "SELECT P.descricao as planName, Pe.nome, E.descricao, Tr.tempo, tr.sequencias FROM (GymManagement.Plano AS P JOIN GymManagement.plano_tem AS T ON P.plano_id=T.plano_id) JOIN GymManagement.treino_tem AS Tr ON T.treino_id=tr.treino_id JOIN GymManagement.Exercicio AS E ON Tr.exercicio_id=E.codigo join GymManagement.Pessoa as Pe on P.instr_criador=Pe.nif where P.plano_id=" + CurrentID;
            SqlCommand cmd = new SqlCommand(SqlQuery, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            int index = 1;
            while (reader.Read())
            {
                GetUIControl("description", index).Text = reader["descricao"].ToString();
                GetUIControl("description", index).Visibility = Visibility.Visible;
                GetUIControl("duration", index).Text = reader["tempo"].ToString() + "min";
                GetUIControl("duration", index).Visibility = Visibility.Visible;
                GetUIControl("series", index).Text = reader["sequencias"].ToString();
                GetUIControl("series", index).Visibility = Visibility.Visible;

                //Seting labels
                planName.Content = reader["planName"].ToString();
                trainerName.Content = reader["nome"].ToString();
                nameBox.Text = planName.Content.ToString();
                trainersBox.Text = trainerName.Content.ToString();
                index++;
            }
            reader.Close();
        }

        private bool CheckData()
        {
            bool flag = false;

            for (int i = 1; i <= 5; i++)
            {
                if (GetUIControl("description", i).Text == "")
                {
                    if (GetUIControl("duration", i).Text != "")
                    {
                        flag = true;
                    }
                    else
                    {
                        if (GetUIControl("series", i).Text != "")
                        {
                            flag = true;
                        }
                    }
                }
                else
                {
                    if (GetUIControl("duration", i).Text == "")
                    {
                        flag = true;
                    }
                    else
                    {
                        if (GetUIControl("series", i).Text == "")
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData() || nameBox.Text == "" || trainersBox.Text == "")
            {
                MessageBox.Show("One or more required fields are incomplete.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (UploadInformation())
            {
                //Changing other UI controls
                deleteButton.Visibility = Visibility.Visible;
                saveButton.Visibility = Visibility.Hidden;
                editButton.Visibility = Visibility.Visible;
                //Disabling controls
                for (int i = 1; i <= 5; i++)
                {
                    GetUIControl("description", i).IsEnabled = false;
                    GetUIControl("duration", i).IsEnabled = false;
                    GetUIControl("series", i).IsEnabled = false;
                }

                for (int i = 1; i <= 5; i++)
                {
                    if (GetUIControl("description", i).Text == "" && GetUIControl("duration", i).Text == "" && GetUIControl("series", i).Text == "")
                    {
                        GetUIControl("description", i).Visibility = Visibility.Hidden;
                        GetUIControl("duration", i).Visibility = Visibility.Hidden;
                        GetUIControl("series", i).Visibility = Visibility.Hidden;
                    }
                }
                nameLabel.Visibility = Visibility.Hidden;
                nameBox.Visibility = Visibility.Hidden;
                trainerLabel.Visibility = Visibility.Hidden;
                trainersBox.Visibility = Visibility.Hidden;
                planName.Content = nameBox.Text;
                trainerName.Content = trainersBox.Text;
            }
        }

        private bool UploadInformation()
        {
            //TODO: Upload to database
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update information?", "Update Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("The plan information of was updated.", "Plan Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Enabling controls
            for (int i = 1; i <= 5; i++)
            {
                GetUIControl("description", i).Visibility = Visibility.Visible;
                GetUIControl("duration", i).Visibility = Visibility.Visible;
                GetUIControl("series", i).Visibility = Visibility.Visible;
                GetUIControl("description", i).IsEnabled = true;
                GetUIControl("duration", i).IsEnabled = true;
                GetUIControl("series", i).IsEnabled = true;
            }
            nameLabel.Visibility = Visibility.Visible;
            nameBox.Visibility = Visibility.Visible;
            trainerLabel.Visibility = Visibility.Visible;
            trainersBox.Visibility = Visibility.Visible;
            //Changing other UI controls
            deleteButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(Main.PopStack());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete \"" + planName.Content + "\" from the system?", "Delete Plan", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("\"" + planName.Content + "\" was removed from the system.", "Plan Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

    }
}
