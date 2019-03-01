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
    /// Interaction logic for ShowClass.xaml
    /// </summary>
    public partial class ShowClass : UserControl
    {
        private SqlConnection cn;
        private int CurrentID;

        public ShowClass(int id)
        {
            InitializeComponent();
            this.CurrentID = id;
            GetClassInfo();
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

        public void BindTrainersBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT nome FROM GymManagement.Pessoa as P join GymManagement.Instrutor as I on P.nif=I.nif", cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Instrutores");
            instructorBox.ItemsSource = ds.Tables[0].DefaultView;
            instructorBox.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            SqlAdapter.Dispose();
            cn.Close();
        }

        public void BindParticipantsBox()
        {
            OpenSqlConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT P.nome FROM(GymManagement.cliente_temAula as A join GymManagement.Pessoa as P on A.nif = P.nif) where codigo_aula = " + this.CurrentID, cn);
            DataSet ds = new DataSet();
            SqlAdapter.Fill(ds, "Participants");
            participantsBox.ItemsSource = ds.Tables[0].DefaultView;
            participantsBox.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
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

        private void GetClassInfo()
        {
            BindTrainersBox();
            if (this.CurrentID != 0)
            {
                OpenSqlConnection();
                string SqlQuery = "SELECT A.designacao, A.hora, A.vagas, A.duracao, P.nome FROM (GymManagement.AulaInstancia as A join GymManagement.instrutor_daAula as D on A.codigo=D.codigo_aula) join GymManagement.Pessoa as P on D.nif=P.nif where A.codigo=" + this.CurrentID;
                SqlCommand cmd = new SqlCommand(SqlQuery, cn);
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                nameBox.Text = reader["designacao"].ToString();
                className.Content = reader["designacao"].ToString();
                instructorBox.Text = reader["nome"].ToString();
                timeBox.Text = reader["hora"].ToString();
                durationBox.Text = reader["duracao"].ToString() + "min";
                vacanciesBox.Text = reader["vagas"].ToString();
                reader.Close();
                cmd.Cancel();
            }

            BindParticipantsBox();

            //Disabling controls to prevent editing
            instructorBox.IsEnabled = false;
            timeBox.IsEnabled = false;
            durationBox.IsEnabled = false;
            vacanciesBox.IsReadOnly = true;
        }

        private void UploadInformation()
        {
            //TODO: Upload to database
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update information?", "Update Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("The information the class \"" + nameBox.Text + "\" was uploaded.", "Class Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageBoxResult.No:
                    Window1 Main = (Window1)Application.Current.MainWindow;
                    ShowClass su = new ShowClass(this.CurrentID);
                    Main.LayoutRoot.Children.Clear();
                    Main.LayoutRoot.Children.Add(su);
                    break;
                default:
                    break;
            }
        }

        private int GetSelectedID()
        {
            OpenSqlConnection();
            string SqlQuery = "SELECT nif FROM GymManagement.Pessoa where nome= '" + participantsBox.Text + "'";
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Enabling controls to allow editing
            instructorBox.IsEnabled = true;
            timeBox.IsEnabled = true;
            durationBox.IsEnabled = true;
            vacanciesBox.IsReadOnly = false;
            //Enabling other controls
            nameLabel.Visibility = Visibility.Visible;
            nameBox.Visibility = Visibility.Visible;
            removeButton.Visibility = Visibility.Visible;
            deleteButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Visible;
        }

        private bool CheckData()
        {
            if (nameBox.Text != "")
            {
                if (instructorBox.Text == "")
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
                MessageBox.Show("One or more of the required fields are empty or incomplete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UploadInformation();
            className.Content = nameBox.Text;
            //Disabling controls to prevent editing
            instructorBox.IsEnabled = false;
            timeBox.IsEnabled = false;
            durationBox.IsEnabled = false;
            vacanciesBox.IsReadOnly = true;
            //Enabling other controls
            participantsLabel.Visibility = Visibility.Visible;
            participantsBox.Visibility = Visibility.Visible;
            showButton.Visibility = Visibility.Visible;
            nameLabel.Visibility = Visibility.Hidden;
            nameBox.Visibility = Visibility.Hidden;
            removeButton.Visibility = Visibility.Hidden;
            deleteButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Visible;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete class \"" + nameBox.Text + "\" from the system?", "Delete Class", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show(nameBox.Text + " as removed from the system.", "Class Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                    Window1 Main = (Window1)Application.Current.MainWindow;
                    ClassesControl cc = new ClassesControl();
                    Main.LayoutRoot.Children.Clear();
                    Main.LayoutRoot.Children.Add(cc);
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(Main.PopStack());
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(participantsBox.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to client \"" + participantsBox.Text + "\" from the class?", "Remove Client", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show(participantsBox.Text + " was removed from the class.", "Client Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                        participantsBox.Text = "";
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

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (participantsBox.Text != "")
            {
                Window1 Main = (Window1)Application.Current.MainWindow;
                IEnumerator controls = Main.LayoutRoot.Children.GetEnumerator();
                
                while(controls.MoveNext())
                {
                    if(controls.Current is UserControl)
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
                MessageBoxResult result = MessageBox.Show("Please select a client from the list to procede.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
    }
}
