using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Xps.Packaging;

namespace SBCManagementSoftware
{
    class DBconnect
    {
        MySqlConnection conn;
        private static readonly Random getrandom = new Random();
        public DBconnect()
        {
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            String connStr = "server=localhost;user=root;database=sbc;port=3306;password=root";
            conn = new MySqlConnection(connStr);
            conn.Open();
        }
        public void CloseConnection()
        {
            conn.Close();
        }

        /** 
         * int Floor
         * int Id
         * byte[] ShopImage
         * int Status
         * DateTime BeginDate
         * DateTime EndDate
         * int Duration
         * int Price
         * int Total
         * int Tax
         * string RenterFirstName
         * string RenterLastName
         * string Name
         * string City 
         * string SubCity
         * string County
         * string HouseNumber
         */

        public DataSet GetAllShops()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT" +
                " Floor, Id, ShopImage, Status," +
                " BeginDate, EndDate," +
                " Duration, Price, Total, Tax," +
                " RenterFirstName, RenterLastName, Name, City, SubCity, County, HouseNumber " +
                " FROM shops", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            CloseConnection();
            return ds;
        }
        public XpsDocument createRentDoc(int Floor, int iD, DateTime beginDate, int duration, int price, string firstName, string lastName, string Name, string city, string subcity, string county, string houseNumber)
        {
            ConnectToDatabase();
            XpsDocument docToRead = getLegalDoc("Rental");
            CloseConnection();
            if (docToRead == null)
            {
                MessageBox.Show("Could not find rental document in the database");
                return null;
            }
            int n = 0;
            lock (getrandom)
            {
                n = getrandom.Next(int.MinValue, int.MaxValue);
            }
            String destinationXpsPath = Environment.CurrentDirectory + "\\" + n + ".xps";
            XpsModification xpsModification = new XpsModification();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            keyValuePairs[$"`"] = $"{subcity}  {city}";
            keyValuePairs[$"~"] = $"{county}";
            keyValuePairs[$"!"] = $"{houseNumber}";
            keyValuePairs["@"] = $"{Floor}";
            keyValuePairs["#"] = $"{iD}";
            keyValuePairs["$"] = $"{Name}";
            keyValuePairs["^"] = $"{beginDate.ToShortDateString()}";
            DateTime EndDate = beginDate.AddMonths(duration);
            keyValuePairs["&"] = $"{EndDate.ToShortDateString()}";
            keyValuePairs["*"] = $"{duration}";
            keyValuePairs["?"] = $"{price}";
            keyValuePairs["+"] = $"{firstName} {lastName}";
            keyValuePairs["}"] = $"{price * duration}";
            keyValuePairs["|"] = $"{price * duration * 0.15}";
            XpsDocument document = xpsModification.CreateNewXPSFromSource(docToRead, destinationXpsPath, keyValuePairs);
            docToRead.Close();
            return document;

        }

        public static string GetTimeStamp(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public void RentShop(int Floor, int iD, int duration, int price, string firstName, string lastName, string Name, string city, string subcity, string county, string houseNumber)
        {
            DateTime beginDate = DateTime.Now;
            DateTime endDate = beginDate.AddMonths(duration);
            String query = $"UPDATE shops SET " +
                $"BeginDate= '{GetTimeStamp(beginDate)}', " +
                $"EndDate= '{GetTimeStamp(endDate)}', " +
                $"Duration = '{duration}', " +
                $"Price = '{price}', " +
                $"Total = '{price * duration}', " +
                $"Tax = '{price * duration * 0.15}', " +
                $"RenterFirstName = '{firstName}', " +
                $"RenterLastName = '{lastName}' , " +
                $"Name = '{Name}' , " +
                $"City = '{city}' , " +
                $"SubCity = '{subcity}' , " +
                $"County = '{county}' , " +
                $"HouseNumber = '{houseNumber}' , " +
                $"Status = '{1}' " +
                $"WHERE " +
                $"( ID = '{iD}' and Floor = '{Floor}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
            DocumentViewer documentViewer = new DocumentViewer();
            XpsDocument xps= createRentDoc(Floor, iD, beginDate, duration, price, firstName, lastName, Name, city, subcity, county, houseNumber);
            if (xps!=null){
                documentViewer.DocViewer.Document = xps.GetFixedDocumentSequence();
                documentViewer.Show();
            }
        }

        public void ExtendRent(int floor,int iD, DateTime endDate, int rentDuration)
        {
            String query = $"UPDATE shops SET " +
                $"Duration = '{rentDuration}' " +
                $"EndDate= '{GetTimeStamp(endDate)}', " +
                $"WHERE " +
                $"(Floor = '{floor}' and ID = '{iD}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void ClearOutShop(int floor, int iD)
        {
            DateTime dateTime = DateTime.Now;

            String query = $"UPDATE shops SET " +
                $"BeginDate= '{GetTimeStamp(dateTime)}', " +
                $"EndDate= '{GetTimeStamp(dateTime)}', " +
                $"Duration = '{0}', " +
                $"Total = '{0}', " +
                $"Tax = '{0}', " +
                $"RenterFirstName = 'None', " +
                $"RenterLastName = 'None' , " +
                $"Name = 'None' , " +
                $"City = 'None' , " +
                $"SubCity = 'None' , " +
                $"County = 'None' , " +
                $"HouseNumber = '{0}' , " +
                $"Status = '{0}' " +
                $"WHERE " +
                $"( ID = '{iD}' and Floor = '{floor}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void AddShop(int floor, int iD, byte[] imageByte, int price)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("Insert into Shops (Floor, Id, ShopImage, Price) values (@fl,@id,@sh,@pr)", conn);
            mySqlCommand.Parameters.AddWithValue("@fl", floor);
            mySqlCommand.Parameters.AddWithValue("@id", iD);
            mySqlCommand.Parameters.AddWithValue("@sh", imageByte);
            mySqlCommand.Parameters.AddWithValue("@pr", price);
            mySqlCommand.ExecuteNonQuery();
            CloseConnection();
        }
        public void ModifyShop(int floor, int iD, byte[] imageByte, int price)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("UPDATE shops SET Price=(@p) , ShopImage=(@s) where (Id=(@id) and floor=(@fl))", conn);
            mySqlCommand.Parameters.AddWithValue("@p", price);
            mySqlCommand.Parameters.AddWithValue("@s", imageByte);
            mySqlCommand.Parameters.AddWithValue("@id", iD);
            mySqlCommand.Parameters.AddWithValue("@fl", floor);
            mySqlCommand.ExecuteNonQuery();
            CloseConnection();
        }

        public void RemoveShop(int iD, int floor)
        {
            String query = $"Delete from shops WHERE (ID = '{iD}' and floor = '{floor}') ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public DataSet GetAllEmployees()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT ID, FirstName," + "LastName," + "Position,"+ "Salary," + "Phone,"+ "Region," + "SubCity,"+ "Image FROM employees", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            CloseConnection();
            return ds;
        }
        public DataSet GetAllEmployees(String name)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT ID, FirstName, LastName, Position, Salary, Phone, Region, SubCity, Image FROM employees where Position='{name}'", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            CloseConnection();
            return ds;
        }
        public void AddEmployee(string firstName, string lastName, string phoneNumber, string salary, string position,string Region, string subCity, byte[] imageByte)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO EMPLOYEES(FirstName,LastName,Position,Salary,Phone,Region,SubCity,Image) values (@f,@l,@pos,@s,@p,@reg,@sub,@i)", conn);
            mySqlCommand.Parameters.AddWithValue("@f", firstName);
            mySqlCommand.Parameters.AddWithValue("@l", lastName);
            mySqlCommand.Parameters.AddWithValue("@pos", position);
            mySqlCommand.Parameters.AddWithValue("@s", salary);
            mySqlCommand.Parameters.AddWithValue("@p", phoneNumber);
            mySqlCommand.Parameters.AddWithValue("@reg", Region);
            mySqlCommand.Parameters.AddWithValue("@sub", subCity);
            mySqlCommand.Parameters.AddWithValue("@i", imageByte);
            mySqlCommand.ExecuteNonQuery();
            CloseConnection();
            DocumentViewer documentViewer = new DocumentViewer();
            documentViewer.DocViewer.Document = createEmployeeDoc(firstName, lastName, phoneNumber, salary, position, Region, subCity).GetFixedDocumentSequence();
            documentViewer.Show();
        }
        public void ModifyEmployee(String dateTime, string firstName, string lastName, string phoneNumber, string salary, string position, string region, string subCity, byte[] imageByte)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("UPDATE EMPLOYEES SET FirstName=(@f) , LastName=(@l) , Position=(@pos) , Salary=(@s) , Phone=(@p), Region=(@reg), SubCity=(@sub) , Image=(@i) where ID=(@id)", conn);
            mySqlCommand.Parameters.AddWithValue("@id", dateTime);
            mySqlCommand.Parameters.AddWithValue("@f", firstName);
            mySqlCommand.Parameters.AddWithValue("@l", lastName);
            mySqlCommand.Parameters.AddWithValue("@pos", position);
            mySqlCommand.Parameters.AddWithValue("@s", salary);
            mySqlCommand.Parameters.AddWithValue("@p", phoneNumber);
            mySqlCommand.Parameters.AddWithValue("@reg", region);
            mySqlCommand.Parameters.AddWithValue("@sub", subCity);
            mySqlCommand.Parameters.AddWithValue("@i", imageByte);
            mySqlCommand.ExecuteNonQuery();
            CloseConnection();
        }
        public void RemoveEmployee(DateTime dateTime)
        {
            string date = GetTimeStamp(dateTime);
            String query = $"Delete from employees WHERE ID = '{date}' ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public XpsDocument createEmployeeDoc(string firstName, string lastName, string phoneNumber, string salary, string position, string Region, string subCity)
        {
            ConnectToDatabase();
            XpsDocument docToRead = getLegalDoc("Employee");
            CloseConnection();

            int n = 0;
            lock (getrandom)
            {
                n = getrandom.Next(int.MinValue, int.MaxValue);
            }
            String destinationXpsPath = Environment.CurrentDirectory + "\\" + n + ".xps";
            XpsModification xpsModification = new XpsModification();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs["Employee_Name"] = $"{firstName}  {lastName}";
            keyValuePairs["Employee_Phone"] = $"{phoneNumber}";
            keyValuePairs["Employee_Date"] = $"{Convert.ToString(DateTime.Now.ToShortDateString())}";
            keyValuePairs["Employee_Salary"] = $"{salary}";
            keyValuePairs["Employee_Position"] = $"{position}";
            keyValuePairs["Employee_Address"] = $"{subCity},  {Region}";
            XpsDocument document = xpsModification.CreateNewXPSFromSource(docToRead, destinationXpsPath, keyValuePairs);
            docToRead.Close();
            return document;
        }




        public System.Windows.Xps.Packaging.XpsDocument getLegalDoc(String name)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT Template,Size FROM docs where Type='{name}'", conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            if (!mySqlDataReader.HasRows)
            {
                MessageBox.Show("No template currently in database.");
                mySqlDataReader.Close();
                CloseConnection();
                return null;
            }
            else
            {
                mySqlDataReader.Read();
                UInt32 FileSize = mySqlDataReader.GetUInt32(mySqlDataReader.GetOrdinal("Size"));
                if (FileSize > 0)
                {
                    byte[] rawData = new byte[FileSize];
                    mySqlDataReader.GetBytes(mySqlDataReader.GetOrdinal("Template"), 0, rawData, 0, (int)FileSize);
                    mySqlDataReader.Close();
                    CloseConnection();
                    Random random = new Random();
                    int n = random.Next(int.MinValue, int.MaxValue);
                    String path = Environment.CurrentDirectory + "\\" + n + ".xps";
                    FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Write(rawData, 0, (int)FileSize);
                    fileStream.Close();
                    System.Windows.Xps.Packaging.XpsDocument xpsDoc = new System.Windows.Xps.Packaging.XpsDocument(path, FileAccess.ReadWrite);
                    return xpsDoc;
                }
                else
                {
                    return null;
                }
            }
        }

        public void modifyLegalDoc(String path, String type)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            byte[] docBytes = new byte[fileStream.Length];
            fileStream.Read(docBytes, 0, System.Convert.ToInt32(docBytes.Length));
            fileStream.Close();
            MySqlCommand mySqlCommand = new MySqlCommand("UPDATE DOCS SET Template=(@t), SIZE=(@s) where Type=(@r)", conn);
            mySqlCommand.Parameters.AddWithValue("@r", type);
            mySqlCommand.Parameters.AddWithValue("@t", docBytes);
            mySqlCommand.Parameters.AddWithValue("@s", System.Convert.ToInt32(docBytes.Length));
            mySqlCommand.ExecuteNonQuery();
            CloseConnection();
        }
        public void modifyLegalDocAndReplace(String backup, String newFile)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT Template,Size FROM docs where Type='{backup}'", conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            if (!mySqlDataReader.HasRows)
            {
                MessageBox.Show("No template currently in database.");
                mySqlDataReader.Close();
                CloseConnection();
                return;
            }
            else
            {
                mySqlDataReader.Read();
                UInt32 FileSize = mySqlDataReader.GetUInt32(mySqlDataReader.GetOrdinal("Size"));
                if (FileSize > 0)
                {
                    byte[] rawData = new byte[FileSize];
                    mySqlDataReader.GetBytes(mySqlDataReader.GetOrdinal("Template"), 0, rawData, 0, (int)FileSize);
                    mySqlDataReader.Close();
                    MySqlCommand mySqlCommand = new MySqlCommand("UPDATE DOCS SET Template=(@t), SIZE=(@s) where Type=(@r)", conn);
                    mySqlCommand.Parameters.AddWithValue("@r", newFile);
                    mySqlCommand.Parameters.AddWithValue("@t", rawData);
                    mySqlCommand.Parameters.AddWithValue("@s", System.Convert.ToInt32(rawData.Length));
                    mySqlCommand.ExecuteNonQuery();
                    CloseConnection();
                    return;
                }
                else
                {
                    return;
                }
            }
        }
    }
}