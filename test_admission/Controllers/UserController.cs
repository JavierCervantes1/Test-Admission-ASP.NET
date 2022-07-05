using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using test_admission.Models;
using System.Text;

namespace test_admission.Controllers
{
    public class UserController : Controller
    {
        MySqlConnection con = new MySqlConnection("server=127.0.0.1;port=3306;user=root;password=;database=db_testadmission");
        //private readonly ApplicationDbContext _context;
        /*public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listusers = await _context.User.ToListAsync();
            return Ok(listusers);
        }*/

        public string AllUsers()
        {
            string query = "SELECT users.*, positions.Name FROM users " +
                " JOIN positions ON users.Cod_position = positions.Id";
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataSet ds = new DataSet();
                ds.Merge(dt);
                StringBuilder JsonString = new StringBuilder();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    JsonString.Append("[");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        JsonString.Append("{");
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (j < ds.Tables[0].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                            }
                            else if (j == ds.Tables[0].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                            }
                        }
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            JsonString.Append("}");
                        }
                        else
                        {
                            JsonString.Append("},");
                        }
                    }
                    JsonString.Append("]");

                    var users = JsonString.ToString();

                    return users;//Json(new { users });
                }
                else
                {
                    return null;
                }
                //return Ok(dt);
            }
            //DataTable users 
            //
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Store([FromBody] User user)
        {
            string query = 
                "INSERT INTO users (Firstname, Surname, Identification, Birthday, Salary, Cod_position)" +
                "VALUES ('"+user.Firstname+"', '"+user.Surname+"', '"+user.Identification+"', '"+
                    user.Birthday+"', '"+user.Salary+"', '"+user.Cod_position+"');";
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            con.Close();

            string message = "Usuario Creado.";

            return Json(new { message });
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                string query = "DELETE FROM users WHERE users.id = " + id;
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                con.Close();

                string message = "Usuario Eliminado.";

                return Json(new { message });
            }
            catch
            {
                return View();
            }
        }
    }
}
