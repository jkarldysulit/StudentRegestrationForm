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
    public partial class StudentRegistrationForm : Form
    {
        public StudentRegistrationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int day = 1; day <= 31; day++)
            {
                cmbDay.Items.Add(day);
            }
            cmbDay.Text = "-Day-";

            string[] months = { "January", "February", "March", "April", "May", "June",
                                 "July", "August", "September", "October", "November", "December" };
            foreach (string month in months)
            {
                cmbMonth.Items.Add(month);
            }
            cmbMonth.Text = "-Month-";

            for (int year = 1940; year <= DateTime.Now.Year; year++)
            {
                cmbYear.Items.Add(year);
            }
            cmbYear.Text = "-Year-";

            cmbCourse.Items.Add("BS Information Technology");
            cmbCourse.Items.Add("BS Computer Science");
            cmbCourse.Items.Add("BS Accountancy");
            cmbCourse.Items.Add("BS Business Administration");
            cmbCourse.Items.Add("Bachelor in Multimedia Arts");
            cmbCourse.Text = "-Course-";
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputeAge();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputeAge();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputeAge();
        }

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
                    if (birthDate > today.AddYears(-age))
                    {
                        age--;
                    }

                    txtBoxAge.Text = age.ToString();
                }
                catch
                {
                    txtBoxAge.Text = "";
                }
            }
        }

        private void txtBoxAge_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please enter your Last Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Please enter your First Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(middleName))
            {
                MessageBox.Show("Please enter your Middle Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxMiddleName.Focus();
                return;
            }

            if (!rbMale.Checked && !rbFemale.Checked)
            {
                MessageBox.Show("Please select a Gender.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDay.SelectedItem == null)
            {
                MessageBox.Show("Please select a Day.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMonth.SelectedItem == null)
            {
                MessageBox.Show("Please select a Month.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbYear.SelectedItem == null)
            {
                MessageBox.Show("Please select a Year.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCourse.SelectedItem == null)
            {
                MessageBox.Show("Please select a Course.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Invalid date! Please check the Day, Month, and Year.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string gender = rbMale.Checked ? "Male" : "Female";
            string course = cmbCourse.SelectedItem.ToString();
            string age = txtBoxAge.Text;

            string dobDay = cmbDay.SelectedItem.ToString();
            string dobMonth = cmbMonth.SelectedItem.ToString();
            string dobYear = cmbYear.SelectedItem.ToString();
            string dob = dobDay + "/" + dobMonth + "/" + dobYear;

            string message = "Student name: " + firstName + " " + middleName + " " + lastName + "\n" +
                             "Gender: " + gender + "\n" +
                             "Date of birth: " + dob + "\n" +
                             "Age: " + age + "\n" +
                             "Course: " + course;

            MessageBox.Show(message, "Student Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
