using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxDemo.Controllers
{
    public class UserController : Controller
    {

        public static List<UserModel> users = new List<UserModel>
        {
            new UserModel { Id = 1, Name = "Alice", Email = "abcd@ad.com" },
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Json(users);
        }
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return Json(user);
            }
            return Json(new { success = false, message = "User not found." });
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserModel newUser)
        {
            users.Add(newUser);
            Console.WriteLine($"User: {newUser.Name} {newUser.Email}");
            Console.WriteLine(users);
            return Json(new { success = true, message = "User added successfully." });
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserModel updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                Console.WriteLine(users);
                return Json(new { success = true, message = "User updated successfully." });
            }
            return Json(new { success = false, message = "User not found." });
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                Console.WriteLine(users);
                return Json(new { success = true, message = "User deleted successfully." });
            }
            return Json(new { success = false, message = "User not found." });
        }


    }
}
