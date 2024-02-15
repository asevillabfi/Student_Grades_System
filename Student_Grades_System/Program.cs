using System;

namespace StudentGradesSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            GradesSystem s = new GradesSystem();
            int choice;

            // Loops the choices 
            do
            {
                DisplayMenu();
                choice = GetChoice();
                switch (choice)
                {
                    case 1:
                        // Enroll students
                        s.Enroll();
                        break;
                    case 2:
                        // Enter student grades
                        s.EnterGrades();
                        break;
                    case 3:
                        // Show student grades
                        s.ShowGrades();
                        break;
                    case 4:
                        // Show the top student
                        s.TopSudent();
                        break;
                    case 5:
                        // Exit console
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        // User picking none of the choices above
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 5.");
                        break;
                }
            } while (choice != 5);
        }

        // The Menu
        static void DisplayMenu()
        {
            Console.WriteLine("\n\nWelcome to the Student Grades System!");
            Console.WriteLine("[1]Enroll Students");
            Console.WriteLine("[2]Enter Student Grades");
            Console.WriteLine("[3]Show Student Grades");
            Console.WriteLine("[4]Show Top Student");
            Console.WriteLine("[5]Exit");
        }

        // User input in choosing from the menu
        static int GetChoice()
        {
            int choice;
            while (true)
            {
                Console.Write("\nEnter Choice [1-5]: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid format. Please enter a valid number.");
                }
            }
        }
    }

    // Variables Declaration
    class Student
    {
        public string Name { get; set; }
        public int ScienceGrade { get; set; }
        public int MathGrade { get; set; }
        public int EnglishGrade { get; set; }
        public double AverageGrade { get { return (ScienceGrade + MathGrade + EnglishGrade) / 3.0; } }
    }

    class GradesSystem
    {
        // Array of students enrolled
        private Student[] students;

        // Total number of students can be enrolled
        private int totalStudents;

        // Index position of the next student added in the 'students' array
        private int currentStudentIndex;

        // Constructor to initialize the 'totalStudents' and 'students' array
        public GradesSystem()
        {
            Console.Write("Enter total number of students: ");
            totalStudents = int.Parse(Console.ReadLine());
            students = new Student[totalStudents];
            currentStudentIndex = 0;
        }

        // Case 1: Enroll method to add students to the 'students' array
        public void Enroll()
        {
            do
            {
                Console.WriteLine("\nEnroll Student");
                Student student = new Student();

                // Inputting Name/s
                Console.Write("Enter Student Name: ");
                student.Name = Console.ReadLine();

                // Check if Name is null or empty
                while (string.IsNullOrEmpty(student.Name))
                {
                    Console.WriteLine("Invalid name. Please enter a valid name.");
                    Console.Write("\nEnter Student Name: ");
                    student.Name = Console.ReadLine();
                }

                students[currentStudentIndex] = student;
                currentStudentIndex++;

                // Check total of students has been met
                if (currentStudentIndex >= totalStudents)
                {
                    Console.WriteLine("No more students can be enrolled.");
                    return;
                }

                Console.Write("Enter Again [Y/N]: \n");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);
        }

        // Case 2: Method to enter grades for enrolled students
        public void EnterGrades()
        {
            foreach (var student in students)
            {
                if (student == null)
                    break;

                do
                {
                    Console.WriteLine("\nEnter Student Grades");
                    Console.WriteLine($"Student: {student.Name}");

                    // Inputting grades
                    Console.Write("Enter grade for Science: ");
                    student.ScienceGrade = int.Parse(Console.ReadLine());
                    Console.Write("Enter grade for Math: ");
                    student.MathGrade = int.Parse(Console.ReadLine());
                    Console.Write("Enter grade for English: ");
                    student.EnglishGrade = int.Parse(Console.ReadLine());

                    Console.Write("\nEnter Again [Y - same student /M - menu /N - next student]: ");
                    var key = Console.ReadKey().Key;

                    // Return to menu
                    if (key == ConsoleKey.M)
                        return;
                    // Continue entering grades for the same student
                    else if (key == ConsoleKey.Y)
                        continue;
                    // Proceed to the next student
                    else if (key == ConsoleKey.N)
                        break; 

                } while (true);
            }
        }

        // Case 3: Show grades of enrolled students
        public void ShowGrades()
        {
            Console.WriteLine("\nStudent Grades");
            Console.WriteLine("Name\tScience\tMath\tEnglish\tAve.");
            foreach (var student in students)
            {
                if (student == null)
                    break;

                Console.WriteLine($"{student.Name}\t{student.ScienceGrade}\t{student.MathGrade}\t{student.EnglishGrade}\t{student.AverageGrade:F2}");
            }
        }

        // Case 4: Show the top student among the enrolled students
        public void TopSudent()
        {
            double maxAverage = 0;
            string topStudent = "";

            foreach (var student in students)
            {
                if (student == null)
                    break;

                if (student.AverageGrade > maxAverage)
                {
                    maxAverage = student.AverageGrade;
                    topStudent = student.Name;
                }
            }
            Console.WriteLine($"\nTop Student: {topStudent}");
        }
    }
}