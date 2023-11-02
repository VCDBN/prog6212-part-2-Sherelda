using Lib;
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
using Microsoft.Data.SqlClient;

namespace WPFPROG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeManager _timeManager = new TimeManager();
        //conntion sting 
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lab_services_student\\Documents\\registerDB.mdf;Integrated Security=True;Connect Timeout=30";
        public MainWindow()
        {
            InitializeComponent();
        }
        //button for adding modules
        private void OnAddModule(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ModuleCode.Text) ||
               string.IsNullOrWhiteSpace(ModuleName.Text) ||
               string.IsNullOrWhiteSpace(ModuleCredits.Text) ||
               string.IsNullOrWhiteSpace(ModuleHours.Text))
            {
                MessageBox.Show("All fields must be filled in to add a module.");
                return;
            }//id else statement used 

            if (!int.TryParse(ModuleCredits.Text, out int credits) ||
                !int.TryParse(ModuleHours.Text, out int classHours))
            {
                MessageBox.Show("Please enter valid numbers for credits and class hours.");
                return;
            }

            var module = new Module
            {
                Code = ModuleCode.Text,
                Name = ModuleName.Text,
                Credits = credits,
                ClassHoursPerWeek = classHours
            };

            if (_timeManager.CurrentSemester == null)
                _timeManager.CurrentSemester = new Semester();

            _timeManager.CurrentSemester.AddModule(module);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Modules WHERE ModuleCode=@code";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@code", ModuleCode.Text);
                    int existingModules = (int)checkCmd.ExecuteScalar();

                    if (existingModules > 0)
                    {
                        MessageBox.Show("This module code already exists in the database.");
                        return;
                    }
                    //for sql tables
                    string insertQuery = "INSERT INTO Modules (code, name, module_credits, hours_per_week) VALUES (@code, @name, @module_credits, @hours_per_week)";
                    SqlCommand cmd = new SqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@code", ModuleCode.Text);
                    cmd.Parameters.AddWithValue("@name", ModuleName.Text);
                    cmd.Parameters.AddWithValue("@module_credits", int.Parse(ModuleCredits.Text));
                    cmd.Parameters.AddWithValue("@hours_per_week", int.Parse(ModuleHours.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Module added successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while saving the module: " + ex.Message);
                }
            }


            UpdateModuleListView();
            UpdateModuleSelector();

         
            ModuleCode.Clear();
            ModuleName.Clear();
            ModuleCredits.Clear();
            ModuleHours.Clear();
        }
        private void UpdateModuleListView()
        {
            ModuleListView.Items.Clear();
            foreach (var module in _timeManager.CurrentSemester.Modules)
            {
                ModuleListView.Items.Add(new
                {
                    Code = module.Code,
                    Name = module.Name,
                    SelfStudyHours = module.CalculateSelfStudyHours(_timeManager.CurrentSemester.Weeks)
                });
            }
        }

        private void UpdateModuleSelector()
        {
            StudyModuleSelector.Items.Clear();
            foreach (var module in _timeManager.CurrentSemester.Modules)
            {
                StudyModuleSelector.Items.Add(module);
            }


            StudyModuleSelector.Items.Clear();
            foreach (var module in _timeManager.CurrentSemester.Modules)
            {
                StudyModuleSelector.Items.Add(module);
            }


        }
        private void OnRecordStudy(object sender, RoutedEventArgs e)
        {
            var selectedModule = StudyModuleSelector.SelectedItem as Module;
            if (selectedModule == null)
            {
                MessageBox.Show("Please select a module to record study hours.");
                return;
            }

            if (!double.TryParse(HoursStudiedText.Text, out double hoursStudied))
            {
                MessageBox.Show("Please enter a valid number for study hours.");
                return;
            }

            _timeManager.RecordStudyHours(selectedModule, StudyDate.SelectedDate ?? DateTime.Now, hoursStudied);

            // For demonstration, after recording study hours, we can show the remaining hours for the current week for the selected module
            var remainingHours = _timeManager.GetRemainingHoursForWeek(DateTime.Now);
            if (remainingHours.ContainsKey(selectedModule))
            {
                MessageBox.Show($"Remaining hours for {selectedModule.Code} this week: {remainingHours[selectedModule]}");
            }

            HoursStudiedText.Clear();  // Clear the hours studied input for better UX
        }

        private void OnSetSemesterDetails(object sender, RoutedEventArgs e)
        {
            if (_timeManager.CurrentSemester == null)
                _timeManager.CurrentSemester = new Semester();

            if (!int.TryParse(SemesterWeeks.Text, out int weeks))
            {
                MessageBox.Show("Please enter a valid number for weeks.");
                return;
            }

            _timeManager.CurrentSemester.Weeks = weeks;
            _timeManager.CurrentSemester.StartDate = StartDate.SelectedDate ?? DateTime.Now;

            UpdateModuleListView();  // Recalculate self-study hours when weeks change
            UpdateModuleSelector();
        }

        private void ModuleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedModuleData = e.AddedItems[0] as dynamic; // Using dynamic as it's an anonymous type

                if (selectedModuleData != null)
                {
                    MessageBox.Show($"Selected module: {selectedModuleData.Name}");
                }
            }
        }
    }
}