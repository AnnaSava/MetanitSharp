using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Validation
    {
        public static void Run()
        {
            char key;

            while (true)
            {
                printMenu();

                key = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (key)
                {
                    case 'v':
                        ValidationDemo.Display();
                        break;
                    case 'a':
                        ValidationAttributes.Display();
                        break;
                    case 'c':
                        CustomValidationAttributes.Display();
                        break;
                    case 'i':
                        AutoValidation.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("V - валидация");
            Console.WriteLine("A - атрибуты валидации");
            Console.WriteLine("C - создание атрибутов валидации");
            Console.WriteLine("I - самовалидация модели");
            Console.WriteLine("X - выход из раздела");
        }
    }

    class ValidationDemo
    {
        public static void Display()
        {
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст:");
            int age = Int32.Parse(Console.ReadLine());

            var user = new User { Name = name, Age = age };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
        }

        public class User
        {
            [Required]
            public string Id { get; set; }
            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string Name { get; set; }
            [Required]
            [Range(1, 100)]
            public int Age { get; set; }
        }
    }

    class ValidationAttributes
    {
        public static void Display()
        {
            User user1 = new User { Id = "", Name = "Tom", Age = -22 };
            Validate(user1);

            RegisterModel model = new RegisterModel { Password = "123", ConfirmPassword = "123 " };
            Validate(model);
        }

        static void Validate(User user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                Console.WriteLine("Пользователь прошел валидацию");
        }

        static void Validate(RegisterModel regModel)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(regModel);
            if (!Validator.TryValidateObject(regModel, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                Console.WriteLine("Регистрационная модель прошла валидацию");
        }

        public class User
        {
            [Required(ErrorMessage = "Идентификатор пользователя не установлен")]
            public string Id { get; set; }
            [Required(ErrorMessage = "Не указано имя пользователя")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
            public string Name { get; set; }
            [Required]
            [Range(1, 100, ErrorMessage = "Недопустимый возраст")]
            public int Age { get; set; }
            [Required]
            [RegularExpression(@"^\+[2-9]\d{3}-\d{3}-\d{4}$", ErrorMessage = "Номер телефона должен иметь формат +xxxx-xxx-xxxx")]
            public string Phone { get; set; }
        }

        public class RegisterModel
        {
            [Required]
            public string Login { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }
    }
    
    class CustomValidationAttributes
    {
        public static void Display()
        {
            User user1 = new User { Id = "", Name = "Tom", Age = -22 };
            Validate(user1);
            Console.WriteLine();
            User user2 = new User { Id = "d3io", Name = "Alice", Age = 33 };
            Validate(user2);
        }
        static void Validate(User user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                Console.WriteLine("Пользователь прошел валидацию");
        }
        [UserValidation]
        public class User
        {
            [Required]
            public string Id { get; set; }
            [Required]
            [UserName]
            public string Name { get; set; }
            [Required]
            public int Age { get; set; }
        }

        public class UserNameAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value != null)
                {
                    string userName = value.ToString();
                    if (!userName.StartsWith("T"))
                        return true;
                    else
                        this.ErrorMessage = "Имя не должно начинаться с буквы T";
                }
                return false;
            }
        }

        public class UserValidationAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                User user = value as User;
                if (user.Name == "Alice" && user.Age == 33)
                {
                    this.ErrorMessage = "Имя не должно быть Alice и возраст одновременно не должен быть равен 33";
                    return false;
                }
                return true;
            }
        }
    }

    class AutoValidation
    {
        public static void Display()
        {
            User user = new User { Id = "", Name = "Tom", Age = -22 };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine("Пользователь прошел валидацию");
            }
        }

        class User : IValidatableObject
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                List<ValidationResult> errors = new List<ValidationResult>();

                if (string.IsNullOrWhiteSpace(this.Name))
                    errors.Add(new ValidationResult("Не указано имя"));

                if (string.IsNullOrWhiteSpace(this.Id))
                    errors.Add(new ValidationResult("Не указан идентификатор пользователя"));

                if (this.Age < 1 || this.Age > 100)
                    errors.Add(new ValidationResult("Недопустимый возраст"));

                return errors;
            }
        }
    }
}
