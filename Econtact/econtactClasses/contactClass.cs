using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        //Getter Setter Properties
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Selecting Data from Database
        public DataTable Select()
        {
            //Steep 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();

            try
            {
                //Steep 2: Writing Sql Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //Inserting Data into Database
        public bool Insert(contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //Steep 1: Connection Database
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                //Steep 2: Create SQL Query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName,LastName,ContactNo,Address,Gender) VALUES (@FirstName,@LastName,@ContactNo,@Address,@Gender)";
                //Creating SQL Command usinsg sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //Method to update data in database from our application
        public bool Update(contactClass c)
        {
            //Create a default return type and set its default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //SQL to update data in our Database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //Create SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs sucessfully then  the value of rows will be greater than zero else its value will be zero
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //Method to Delete Data from Database
        public bool Delete(contactClass c)
        {
            //Create a default return value and set its value to false
            bool isSuccess = false;
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //SQL to delete Data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query run sucessfully then the value of rows is greater than zero else its value is 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                //Close connection
                conn.Close();
            }
            return isSuccess;
        }
    }
}
