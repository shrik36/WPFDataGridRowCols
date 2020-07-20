using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPF_Datagrid
{
    public class VMMainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public VMMainWindow()
        {
            MouseDownCMD = new RelayCommand(MouseDown);

            LoadStudents();
        }


        public ICommand MouseDownCMD { get; }

        public ObservableCollection<Student> _Student;
        public ObservableCollection<Student> Students
        {
            get { return _Student; }
            set { _Student = value; OnPropertyChanged(nameof(Students)); }
        }

        private Student _selectedStudent { get; set; }
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; OnPropertyChanged(nameof(SelectedStudent)); }
        }

        public void MouseDown(object obj)
        {
            if (SelectedStudent == null) return; // simple validation to skip if nothing selected.

            if (obj is System.Windows.Controls.SelectionChangedEventArgs args && args != null)
            {
                if (args.Source is System.Windows.Controls.DataGrid dtg && dtg != null)
                {
                    int idx = Students.IndexOf(SelectedStudent);

                    if (idx > -1) idx++; // index is ZERO based so adding 1.

                    MessageBox.Show("Selected Row:" + idx + Environment.NewLine + "Column Name: " + dtg.CurrentCell.Column.Header.ToString() + Environment.NewLine + "Column Index: " + dtg.CurrentCell.Column.DisplayIndex, "Student Name: " + SelectedStudent.Name + ", Address: " + SelectedStudent.Address);
                }
            }
        }

        private void LoadStudents()
        {
            List<Student> mystudents = new List<Student>();

            for (int i = 0; i < 20; i++)
            {
                mystudents.Add(new Student { Id = i + 1, Name = "Student " + (i + 1), Address = "Address " + (i + 1) });
            }

            Students = new ObservableCollection<Student>(mystudents);
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// When property is changed this method will fire PropertyChanged Event
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
