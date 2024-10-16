using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    //(для завдання 1 та 2)
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person()
        {
            Name = "No name";
            Age = 1;
        }

        public Person(int age) : this()
        {
            Age = age;
        }

        public Person(string name, int age) : this(age)
        {
            Name = name;
        }
    }

    //(для завдання 3)
    class Family
    {
        private List<Person> members;

        public Family()
        {
            members = new List<Person>();
        }

        public void AddMember(Person member)
        {
            members.Add(member);
        }

        public Person GetOldestMember()
        {
            return members.OrderByDescending(m => m.Age).FirstOrDefault();
        }
    }

    //(для завдання 4)
    class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public Employee(string name, decimal salary, string position, string department, string email = "n/a", int age = -1)
        {
            Name = name;
            Salary = salary;
            Position = position;
            Department = department;
            Email = email;
            Age = age;
        }
    }

    //(для завдання 5)
    class Car
    {
        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionPerKm { get; set; }
        public double DistanceTraveled { get; set; }

        public Car(string model, double fuelAmount, double fuelConsumptionPerKm)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelConsumptionPerKm = fuelConsumptionPerKm;
            DistanceTraveled = 0;
        }

        public bool Drive(double distance)
        {
            double neededFuel = distance * FuelConsumptionPerKm;
            if (neededFuel <= FuelAmount)
            {
                FuelAmount -= neededFuel;
                DistanceTraveled += distance;
                return true;
            }
            return false;
        }
    }

    //(для завдання 6)
    class Engine
    {
        public string Model { get; set; }
        public int Power { get; set; }
        public int? Displacement { get; set; }
        public string Efficiency { get; set; }

        public Engine(string model, int power, int? displacement = null, string efficiency = "n/a")
        {
            Model = model;
            Power = power;
            Displacement = displacement;
            Efficiency = efficiency;
        }
    }

    // Клас Car для завдання 6
    class CarWithEngine
    {
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public int? Weight { get; set; }
        public string Color { get; set; }

        public CarWithEngine(string model, Engine engine, int? weight = null, string color = "n/a")
        {
            Model = model;
            Engine = engine;
            Weight = weight;
            Color = color;
        }
    }

    // Метод для завдання 1 та 2
    static void Task1and2()
    {
        Person person1 = new Person();
        Person person2 = new Person(18);
        Person person3 = new Person("Pesho", 20);

        Console.WriteLine($"{person3.Name} {person3.Age}");
        Console.WriteLine($"{person2.Name} {person2.Age}");
        Console.WriteLine($"{person1.Name} {person1.Age}");
    }

    // Метод для завдання 3 
    static void Task3()
    {
        int n = int.Parse(Console.ReadLine());
        Family family = new Family();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            string name = input[0];
            int age = int.Parse(input[1]);

            Person person = new Person(name, age);
            family.AddMember(person);
        }

        Person oldest = family.GetOldestMember();
        Console.WriteLine($"{oldest.Name} {oldest.Age}");
    }

    // Метод для завдання 4 
    static void Task4()
    {
        int n = int.Parse(Console.ReadLine());
        List<Employee> employees = new List<Employee>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            string name = input[0];
            decimal salary = decimal.Parse(input[1]);
            string position = input[2];
            string department = input[3];

            string email = input.Length > 4 ? input[4] : "n/a";
            int age = input.Length > 5 ? int.Parse(input[5]) : -1;

            employees.Add(new Employee(name, salary, position, department, email, age));
        }

        var highestAvgSalaryDept = employees
            .GroupBy(e => e.Department)
            .OrderByDescending(g => g.Average(e => e.Salary))
            .FirstOrDefault();

        Console.WriteLine($"Highest Average Salary: {highestAvgSalaryDept.Key}");
        foreach (var employee in highestAvgSalaryDept.OrderByDescending(e => e.Salary))
        {
            Console.WriteLine($"{employee.Name} {employee.Salary:F2} {employee.Email} {employee.Age}");
        }
    }

    // Метод для завдання 5
    static void Task5()
    {
        int n = int.Parse(Console.ReadLine());
        Dictionary<string, Car> cars = new Dictionary<string, Car>();

        for (int i = 0; i < n; i++)
        {
            string[] carInfo = Console.ReadLine().Split();
            string model = carInfo[0];
            double fuelAmount = double.Parse(carInfo[1]);
            double fuelConsumptionPerKm = double.Parse(carInfo[2]);

            cars[model] = new Car(model, fuelAmount, fuelConsumptionPerKm);
        }

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            string[] cmdArgs = command.Split();
            string model = cmdArgs[1];
            double distance = double.Parse(cmdArgs[2]);

            if (!cars[model].Drive(distance))
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }

        foreach (var car in cars.Values)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:F2} {car.DistanceTraveled}");
        }
    }

    // Метод для завдання 6 
    static void Task6()
    {
        int n = int.Parse(Console.ReadLine());
        Dictionary<string, Engine> engines = new Dictionary<string, Engine>();

        for (int i = 0; i < n; i++)
        {
            string[] engineInfo = Console.ReadLine().Split();
            string model = engineInfo[0];
            int power = int.Parse(engineInfo[1]);

            int? displacement = engineInfo.Length > 2 && int.TryParse(engineInfo[2], out int disp) ? disp : (int?)null;
            string efficiency = engineInfo.Length > 3 ? engineInfo[3] : "n/a";

            engines[model] = new Engine(model, power, displacement, efficiency);
        }

        int m = int.Parse(Console.ReadLine());
        List<CarWithEngine> cars = new List<CarWithEngine>();

        for (int i = 0; i < m; i++)
        {
            string[] carInfo = Console.ReadLine().Split();
            string model = carInfo[0];
            string engineModel = carInfo[1];

            int? weight = carInfo.Length > 2 && int.TryParse(carInfo[2], out int w) ? w : (int?)null;
            string color = carInfo.Length > 3 ? carInfo[3] : "n/a";

            cars.Add(new CarWithEngine(model, engines[engineModel], weight, color));
        }

        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Model}:");
            Console.WriteLine($"  {car.Engine.Model}:");
            Console.WriteLine($"    Power: {car.Engine.Power}");
            Console.WriteLine($"    Displacement: {(car.Engine.Displacement.HasValue ? car.Engine.Displacement.ToString() : "n/a")}");
            Console.WriteLine($"    Efficiency: {car.Engine.Efficiency}");
            Console.WriteLine($"    Weight: {(car.Weight.HasValue ? car.Weight.ToString() : "n/a")}");
            Console.WriteLine($"    Color: {car.Color}");
        }
    }

    // Основний метод програми
    static void Main(string[] args)
    {
        Console.WriteLine("Виберіть завдання (1-6): ");
        int task = int.Parse(Console.ReadLine());

        switch (task)
        {
            case 1:
            case 2:
                Task1and2();
                break;
            case 3:
                Task3();
                break;
            case 4:
                Task4();
                break;
            case 5:
                Task5();
                break;
            case 6:
                Task6();
                break;
            default:
                Console.WriteLine("Невірний вибір завдання.");
                break;
        }
    }
}
