using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication13.Filters
{
    public class UniqueUsersFilter : IAuthorizationFilter
    {
        private readonly string logFilePath;
        private HashSet<string> uniqueUsers = new HashSet<string>();
        //Конструктор Фільтра
        public UniqueUsersFilter(string logFilePath)
        {
            this.logFilePath = logFilePath;
            try
            {



                if (File.Exists(this.logFilePath))
                {
                    // Читаємо увесь вміст файлу
                    string[] lines = File.ReadAllLines(logFilePath);

                    foreach (string line in lines)
                    {
                        // Додавання кожного рядка як унікального id до HashSet
                        uniqueUsers.Add(line.Replace("userId: ",""));
                    }
                }
                else
                {
                    File.Create(this.logFilePath);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }


        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // В яксості унікального інлифікатора користувача було використано  Connection.Id, яке отримується через context
            string userId = context.HttpContext.Connection.Id.ToString();
            //Умовний блок перевіряє чи існує прочитаний id в колекціїї унікальних значенью Якщо ні, то блок додає даний id  в кінець файлу.
            if (!uniqueUsers.Contains(userId))
            {
                uniqueUsers.Add(userId);

                File.AppendAllText(logFilePath,$"userId: {userId}" + Environment.NewLine);
            }
        }




    }
}
