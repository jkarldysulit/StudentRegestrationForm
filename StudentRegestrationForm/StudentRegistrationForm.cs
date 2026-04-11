using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegestrationForm
{
    public partial class StudentRegistrationForm : Form
    {
        public StudentRegistrationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int day = 1; day <= 31; day++)
                cmbDay.Items.Add(day);
            cmbDay.Text = "-Day-";

            string[] months = { "January", "February", "March", "April", "May", "June",
                                 "July", "August", "September", "October", "November", "December" };
            foreach (string month in months)
                cmbMonth.Items.Add(month);
            cmbMonth.Text = "-Month-";

            for (int year = 1940; year <= DateTime.Now.Year; year++)
                cmbYear.Items.Add(year);
            cmbYear.Text = "-Year-";

            cmbCourse.Items.Add("BS Information Technology");
            cmbCourse.Items.Add("BS Computer Science");
            cmbCourse.Items.Add("BS Accountancy");
            cmbCourse.Items.Add("BS Business Administration");
            cmbCourse.Items.Add("Bachelor in Multimedia Arts");
            cmbCourse.Text = "-Select program-";
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e) { ComputeAge(); }
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e) { ComputeAge(); }
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e) { ComputeAge(); }

        private void ComputeAge()
        {
            if (cmbDay.SelectedItem != null && cmbMonth.SelectedItem != null && cmbYear.SelectedItem != null)
            {
                try
                {
                    int day = cmbDay.SelectedIndex + 1;
                    int month = cmbMonth.SelectedIndex + 1;
                    int year = int.Parse(cmbYear.SelectedItem.ToString());
                    DateTime birthDate = new DateTime(year, month, day);
                    DateTime today = DateTime.Today;

                    int age = today.Year - birthDate.Year;
                    if (birthDate > today.AddYears(-age)) age--;

                    txtBoxAge.Text = age.ToString();
                }
                catch { txtBoxAge.Text = ""; }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.Title = "Select a Photo";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPhoto.Image = Image.FromFile(ofd.FileName);
                pictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        // Overload 1: Full info (name, gender, DOB, program)
        private void DisplayStudentInfo(string fullName, string gender, string dob, string program)
        {
            string message = "Student name: " + fullName + "\n" +
                             "Gender: " + gender + "\n" +
                             "Date of birth: " + dob + "\n" +
                             "Program: " + program;
            MessageBox.Show(message, "Student Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Overload 2: Name and program only
        private void DisplayStudentInfo(string fullName, string program)
        {
            string message = "Student name: " + fullName + "\n" +
                             "Program: " + program;
            MessageBox.Show(message, "Student Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Overload 3: First + last name and program only
        private void DisplayStudentInfo(string firstName, string lastName, string program)
        {
            string message = "Student name: " + firstName + " " + lastName + "\n" +
                             "Program: " + program;
            MessageBox.Show(message, "Student Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string lastName = textBoxLastName.Text.Trim();
            string firstName = textBoxFirstName.Text.Trim();
            string middleName = txtBoxMiddleName.Text.Trim();

            foreach (char c in lastName + firstName + middleName)
            {
                if (char.IsDigit(c))
                {
                    MessageBox.Show("Invalid input! Names should not contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(lastName)) { MessageBox.Show("Please enter your Last Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); textBoxLastName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(firstName)) { MessageBox.Show("Please enter your First Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); textBoxFirstName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(middleName)) { MessageBox.Show("Please enter your Middle Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtBoxMiddleName.Focus(); return; }
            if (!rbMale.Checked && !rbFemale.Checked) { MessageBox.Show("Please select a Gender.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbDay.SelectedItem == null) { MessageBox.Show("Please select a Day.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbMonth.SelectedItem == null) { MessageBox.Show("Please select a Month.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbYear.SelectedItem == null) { MessageBox.Show("Please select a Year.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbCourse.SelectedItem == null) { MessageBox.Show("Please select a Program.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                int day = cmbDay.SelectedIndex + 1;
                int month = cmbMonth.SelectedIndex + 1;
                int year = int.Parse(cmbYear.SelectedItem.ToString());
                DateTime testDate = new DateTime(year, month, day);
            }
            catch
            {
                MessageBox.Show("Invalid date! Please check the Day, Month, and Year.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string gender = rbMale.Checked ? "Male" : "Female";
            string course = cmbCourse.SelectedItem.ToString();
            string dobDay = cmbDay.SelectedItem.ToString();
            string dobMonth = cmbMonth.SelectedItem.ToString();
            string dobYear = cmbYear.SelectedItem.ToString();
            string dob = dobDay + "/" + dobMonth + "/" + dobYear;
            string fullName = firstName + " " + middleName + " " + lastName;

            // Call all three overloaded methods
            DisplayStudentInfo(fullName, gender, dob, course);           // Overload 1
            DisplayStudentInfo(fullName, course);                         // Overload 2
            DisplayStudentInfo(firstName, lastName, course);              // Overload 3
        }
    }
}