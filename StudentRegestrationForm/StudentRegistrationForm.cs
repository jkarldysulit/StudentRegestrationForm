using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StudentRegestrationForm
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void StudentForm_Load(object sender, EventArgs e)
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

            cmbCourse.Items.Add("Bachelor of Science in Information Technology");
            cmbCourse.Items.Add("Bachelor of Science in Computer Science");
            cmbCourse.Items.Add("Bachelor of Science in Accountancy");
            cmbCourse.Items.Add("Bachelor of Science in Business Administration");
            cmbCourse.Items.Add("Bachelor in Multimedia Arts");
            cmbCourse.Text = "-Select program-";

            pictureBoxStudent.SizeMode = PictureBoxSizeMode.Zoom;
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
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Select a Photo";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void DisplayStudentInfo(string fullName, string gender, string dob, string program)
        {
            string message = "Student name: " + fullName + "\n" +
                             "Gender: " + gender + "\n" +
                             "Date of birth: " + dob + "\n" +
                             "Program: " + program;
            MessageBox.Show(message, "Student Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisplayStudentInfo(string fullName, string program)
        {
            string message = "Student name: " + fullName + "\n" +
                             "Program: " + program;
            MessageBox.Show(message, "Student Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
                    MessageBox.Show("Invalid input! Names should not contain numbers.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please enter your Last Name.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLastName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Please enter your First Name.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(middleName))
            {
                MessageBox.Show("Please enter your Middle Name.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxMiddleName.Focus(); return;
            }
            if (!rbMale.Checked && !rbFemale.Checked)
            {
                MessageBox.Show("Please select a Gender.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbDay.SelectedItem == null)
            {
                MessageBox.Show("Please select a Day.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbMonth.SelectedItem == null)
            {
                MessageBox.Show("Please select a Month.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbYear.SelectedItem == null)
            {
                MessageBox.Show("Please select a Year.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCourse.SelectedItem == null)
            {
                MessageBox.Show("Please select a Program.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int day = cmbDay.SelectedIndex + 1;
                int month = cmbMonth.SelectedIndex + 1;
                int year = int.Parse(cmbYear.SelectedItem.ToString());
                DateTime testDate = new DateTime(year, month, day);
            }
            catch
            {
                MessageBox.Show("Invalid date! Please check the Day, Month, and Year.",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string studGender = rbMale.Checked ? "Male" : "Female";
            string studCourse = cmbCourse.SelectedItem.ToString();

            string dobDay = cmbDay.SelectedItem.ToString();
            string dobMonth = cmbMonth.SelectedItem.ToString();
            string dobYear = cmbYear.SelectedItem.ToString();
            string dob = dobDay + "/" + dobMonth + "/" + dobYear;

            string fullName = firstName + " " + middleName + " " + lastName;

            DisplayStudentInfo(fullName, studGender, dob, studCourse);
            DisplayStudentInfo(fullName, studCourse);
            DisplayStudentInfo(firstName, lastName, studCourse);
        }
    }
}