
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography;



namespace Lib
{
    public class Module
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }

        // Records of study hours against dates
        public Dictionary<DateTime, double> StudyRecords { get; private set; } = new Dictionary<DateTime, double>();

        /// <summary>
        /// Record the study hours for a given date.
        /// </summary>
        public void RecordStudyHours(DateTime date, double hours)
        {
            if (StudyRecords.ContainsKey(date))
                StudyRecords[date] += hours;
            else
                StudyRecords.Add(date, hours);
        }

        /// <summary>
        /// Calculate self-study hours required per week.
        /// </summary>
        public double CalculateSelfStudyHours(int weeks)
        {
            return (double)(Credits * 10) / (weeks - ClassHoursPerWeek);
        }
    }

    /// <summary>
    /// Represents a semester.
    /// </summary>
    public class Semester
    {
        public List<Module> Modules { get; } = new List<Module>();
        public int Weeks { get; set; }
        public DateTime StartDate { get; set; }

        public void AddModule(Module module)
        {
            Modules.Add(module);
        }
    }

    /// <summary>
    /// Provides functionality to manage time.
    /// </summary>
    public class TimeManager
    {
        public Semester CurrentSemester { get; set; } = new Semester();

        /// <summary>
        /// Record study hours for a module on a given date.
        /// </summary>
        public void RecordStudyHours(Module module, DateTime date, double hours)
        {
            module?.RecordStudyHours(date, hours);
        }

        /// <summary>
        /// Get remaining study hours for each module in the current week.
        /// </summary>
        public Dictionary<Module, double> GetRemainingHoursForWeek(DateTime currentDate)
        {
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6);

            return CurrentSemester.Modules.ToDictionary(
                module => module,
                module => module.CalculateSelfStudyHours(CurrentSemester.Weeks) -
                          module.StudyRecords
                                 .Where(record => record.Key >= startOfWeek && record.Key <= endOfWeek)
                                 .Sum(record => record.Value));
        }
        public class Register
        {
            public string? Username { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
           
        }

        public class Login
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }
    }
}