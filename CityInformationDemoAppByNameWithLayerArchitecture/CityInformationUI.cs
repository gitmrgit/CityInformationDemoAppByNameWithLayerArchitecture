using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CityInformationDemoAppByNameWithLayerArchitecture.Model;
using CityInformationDemoAppByNameWithLayerArchitecture.BLL;

namespace CityInformationDemoAppByNameWithLayerArchitecture
{
    public partial class CityInformationUI : Form
    {
        CityManager cityManager = new CityManager();
        bool isUpdateMode = false;

        public CityInformationUI()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (isUpdateMode)
            {
                if (countryNameTextBox.Text == "" || aboutTextBox.Text == "")
                {
                    MessageBox.Show("Enter the information");
                }
                else
                {
                    City city = new City();
                    city.CityName = cityNameTextBox.Text;
                    city.About = aboutTextBox.Text;
                    city.Country = countryNameTextBox.Text;
                    if (cityManager.UpdateCityByName(city) < 1)
                    {
                        MessageBox.Show("Not updated");
                    }
                    else 
                    {
                        MessageBox.Show("Updated.");
                        isUpdateMode = false;
                        cityNameTextBox.Enabled = true;
                        saveButton.Text = "Save";

                        LoadAllCityInListView();
                    }

                    
                }
            }
            else 
            {
                if (countryNameTextBox.Text == "" || aboutTextBox.Text == "" || cityNameTextBox.Text =="")
                {
                    MessageBox.Show("Enter the information");
                }
                else
                {
                    City city = new City();
                    city.CityName = cityNameTextBox.Text;
                    city.About = aboutTextBox.Text;
                    city.Country = countryNameTextBox.Text;                   
                    
                    MessageBox.Show(cityManager.SaveCity(city));

                    LoadAllCityInListView();
                }
            
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (citySearchRadioButton.Checked)
            {
                if (searchTextBox.Text == "")
                {
                    MessageBox.Show("Please enter the city name to search.");
                }
                else
                {
                    allCityListView.Items.Clear();
                    List<City> cityList = cityManager.SearchCityByCityName(searchTextBox.Text);

                    int count = 1;
                    foreach (City city in cityList)
                    {
                        ListViewItem listViewItem = new ListViewItem(count.ToString());
                        listViewItem.SubItems.Add(city.CityName.ToString());
                        listViewItem.SubItems.Add(city.About.ToString());
                        listViewItem.SubItems.Add(city.Country.ToString());
                        
                        listViewItem.Tag = city;
                        allCityListView.Items.Add(listViewItem);

                        count++;
                        
                    }


                }
            }
            else if (countrySearchRadioButton.Checked)
            {
                if (searchTextBox.Text == "")
                {
                    MessageBox.Show("Please enter the country name to search.");
                }
                else
                {
                    allCityListView.Items.Clear();
                    List<City> cityList = cityManager.SearchCityByCountryName(searchTextBox.Text);

                    int count = 1;
                    foreach (City city in cityList)
                    {
                        ListViewItem listViewItem = new ListViewItem(count.ToString());
                        listViewItem.SubItems.Add(city.CityName.ToString());
                        listViewItem.SubItems.Add(city.About.ToString());
                        listViewItem.SubItems.Add(city.Country.ToString());

                        listViewItem.Tag = city;
                        allCityListView.Items.Add(listViewItem);
                        count++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select country/city to search.");
            }
        }

        private void CityInformationUI_Load(object sender, EventArgs e)
        {
            LoadAllCityInListView();
        }

        private void LoadAllCityInListView() 
        {
            allCityListView.Items.Clear();
            List<City> cityList = cityManager.LoadAllCity();
            int serialNumber = 1;
            foreach (City city in cityList) 
            {
                ListViewItem listViewItem = new ListViewItem(serialNumber.ToString());
                listViewItem.SubItems.Add(city.CityName.ToString());
                listViewItem.SubItems.Add(city.About.ToString());
                listViewItem.SubItems.Add(city.Country.ToString());

                allCityListView.Items.Add(listViewItem);

                serialNumber++;
            
            }
        
        }

        private void allCityListView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem listViewItem = allCityListView.SelectedItems[0];

            cityNameTextBox.Text = listViewItem.SubItems[1].Text;
            aboutTextBox.Text = listViewItem.SubItems[2].Text;
            countryNameTextBox.Text = listViewItem.SubItems[3].Text;

            isUpdateMode = true;
            saveButton.Text = "Update";
            cityNameTextBox.Enabled = false;

        }

        private void ClearAllField() 
        {
            cityNameTextBox.Text = aboutTextBox.Text = countryNameTextBox.Text = "";
        }

        private void countrySearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadAllCityInListView();
        }
    }
}
