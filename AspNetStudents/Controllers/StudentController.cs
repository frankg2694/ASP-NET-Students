using System.Data;
using AspNetStudents.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetStudents.Controllers
{
    //http://www.tutorialsteacher.com/mvc/asp.net-mvc-tutorials
    public class StudentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Function performs a Sum with its two parameters
        [HttpPost]
        public JsonResult Sum(string first, string second)
        {
            int firstNumber = int.Parse(first);
            int secondNumber = int.Parse(second);

            int sum = firstNumber + secondNumber;

            //Object returned as 'retData' to JavaScript
            object obj = new { returnedSum = sum };
            return Json(obj);
        }

        //Function does the same as GetStudents. However, this version simulates a DB Call
        [HttpPost]
        public JsonResult GetStudents()
        {
            //This function deserializes the JavaScript Object Array in to a C# Object List
            //Note: The object properties in the JavaScript Object Array and C# Object List MUST HAVE THE SAME NAMES
            //List<Student> studentsList = (List<Student>)JsonConvert.DeserializeObject(students, typeof(List<Student>));

            //List to be returnd to JavaScript
            List<Student> list = new();

            //Make DB call
            DataTable table = SelectAllSudents();

            //Iterate DataTable
            foreach (DataRow dr in table.Rows)
            {
                Student student = new()
                {
                    id = int.Parse(dr["ID"].ToString() ?? "-1"), //If it returns -1, then DB call returned a null
                    firstName = dr["FirstName"].ToString(),
                    lastName = dr["LastName"].ToString()
                };

                list.Add(student);
            }

            //List returned as 'retData' to Javascript
            return Json(list);
        }

        //This function obtains the data from the MySQL Database
        //https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-connection.html
        public DataTable SelectAllSudents()
        {
            /*
             * NOTE: I acknowledge that it is bad practice to have the connection string and SQL query visible to the public.
             * In both a production environment and development environment, I would have these hidden behind a stored procedure since it hides the sensitive information.
             * So please, no judging, lol.          
             */
            DataTable table = new();
            string connStr = "server=localhost;port=3306;database=studentsDB;user=root;password=password";            
            MySqlConnection conn = new(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT * FROM Students";
                MySqlCommand cmd = new(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                DataSet ds = new();
                ds.Tables.Add(table);
                ds.EnforceConstraints = false;
                table.Load(dr);
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return table;
        }

        
        //This function simulates a DB Call and return a hardcoded DataTable
        public DataTable SelectAllSudentsHardCoded()
        {
            DataTable table = new();
            table.Columns.Add("ID", typeof(string)); table.Columns.Add("FirstName", typeof(string)); table.Columns.Add("LastName", typeof(string));

            string id = "1"; string firstName = "Tony"; string lastName = "Stark";
            table.Rows.Add(new object[] { id, firstName, lastName });

            id = "2"; firstName = "Steve"; lastName = "Rogers";
            table.Rows.Add(new object[] { id, firstName, lastName });

            id = "3"; firstName = "Bruce"; lastName = "Banner";
            table.Rows.Add(new object[] { id, firstName, lastName });

            id = "4"; firstName = "Thor"; lastName = "Odinson";
            table.Rows.Add(new object[] { id, firstName, lastName });

            id = "5"; firstName = "Natasha"; lastName = "Romanoff";
            table.Rows.Add(new object[] { id, firstName, lastName });

            id = "6"; firstName = "Clint"; lastName = "Barton";
            table.Rows.Add(new object[] { id, firstName, lastName });

            return table;
        }
        
        
    }
}
