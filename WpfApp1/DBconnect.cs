using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SBCManagementSoftware
{
    class DBconnect
    {
        MySqlConnection conn;
        public DBconnect()
        {
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            String connStr = "server=localhost;user=root;database=test;port=3306;password=root";
            conn = new MySqlConnection(connStr);
            conn.Open();
        }
        public void CloseConnection()
        {
            conn.Close();
        }
        public DataSet GetAllShops()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT ID," +
                "Floor," + "Status," + "Area," +
                "RenterID," + "RenterFirstName," + "RenterLastName," + "RenterPhoneNumber," +
                "RentDuration," + "RentAmount" +
                " FROM shops", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            return ds;
        }
        public DataSet GetAllEmployees()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT ID,FirstName,LastName,Position,Salary,Phone FROM employees", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            return ds;
        }

        public void AddEmployee(string firstName, string lastName, string phoneNumber, string salary, string position)
        {
            String query = $"Insert into employees (FirstName, LastName, Position, Salary, Phone) values ( '{firstName}' , '{lastName}','{position}','{salary}', '{phoneNumber}' ) ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void ModifyEmployee(String dateTime,string firstName, string lastName, string phoneNumber, string salary, string position)
        {
            String query = $"UPDATE employees SET FirstName= '{firstName}', LastName = '{lastName}', Position = '{position}' , Salary = '{salary}', Phone = '{phoneNumber}' WHERE ID = '{dateTime}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void RemoveEmployee(DateTime dateTime)
        {
            //DELETE FROM `test`.`employees` WHERE `ID`='2018-11-27 20:35:54';
            string date = GetTimeStamp(dateTime);
            String query = $"Delete from employees WHERE ID = '{date}' ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public static string GetTimeStamp(DateTime dateTime)
        {
           return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public void RentShop(int iD, string firstName, string lastName, string phoneNumber, string rentDuration)
        {
            String query = $"UPDATE shops SET RenterFirstName= '{firstName}', RenterLastName = '{lastName}', RenterPhoneNumber = '{phoneNumber}' , RentDuration = '{int.Parse(rentDuration)}' , Status = '{1}' WHERE ID = '{iD}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void ClearOutShop(int iD)
        {
            String query = $"UPDATE shops SET RenterFirstName= 'None', RenterLastName = 'None', RenterPhoneNumber = 'None' , RentDuration = '0' , Status = '{0}' WHERE ID = '{iD}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void ModifyShop(int ID, int floor, int areaSize, int rentAmount)
        {
            String query = $"UPDATE shops SET Floor= '{floor}', Area = '{areaSize}', RentAmount = '{rentAmount}' WHERE ID = '{ID}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

    }
}