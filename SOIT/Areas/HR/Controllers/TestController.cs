using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Areas.HR.Controllers
{
    public class TestController : Controller
    {
        // GET: HR/Test
        public ActionResult Index()
        {
            //var matrix = GetMatrix();
            //var numOffices = NumOffices(matrix);

            var initial = Console.ReadLine();
            var goal = Console.ReadLine();
            var minimumConcat = MinimumConcat(initial, goal);
            return View();
        }

        public static int MinimumConcat(string initial, string goal)
        {
            // Place your code here
            char[] initialChar = initial.ToCharArray();
            List<string> seq = new List<string>();
            List<string> matchedSeq = new List<string>();
            for(int i = 0; i < initialChar.Length; i++)
            {
                for(int j = 1; j < initialChar.Length; j++)
                {
                    char a = initialChar[i];
                    char b = initialChar[j];
                    string checkSeq = a.ToString() + b.ToString();
                    seq.Add(checkSeq);
                    if (goal.Contains(checkSeq))
                    {
                        
                    }
                }
            }

            
            
            return 0;
        }

        public static char[][] GetMatrix()
        {
            var rows = 4;//  int.Parse(Console.ReadLine());
            var cols = 5;// int.Parse(Console.ReadLine());

            char[][] matrix = new char[rows][];
            List<string> lines = new List<string>();
            lines.Add("11000");
            lines.Add("11000");
            lines.Add("00100");
            lines.Add("00011");
            
            for (var i = 0; i < rows; i++)
            {
                //var line = Console.ReadLine();
                var line = lines[i];
                matrix[i] = line.ToCharArray();
            }
            return matrix;
        }

        public static int NumOffices(char[][] grid)
        {
            int result = 0;
            // place your code here
            // 1s floor space, 0s walls
            List<char> walls = new List<char>();
            List<char> flor = new List<char>();
            //char[] walls =new char[grid.Length];
            //char[] flor = new char[grid.Length];

            int wallindex = 0, florindex = 0;
            char rowStart = '0';
            foreach (var rows in grid)
            {
                char prev = rowStart;
                foreach (var cols in rows)
                {
                    if (cols != prev && cols == '1')
                    {
                        //flor[florindex] = '1';
                        //florindex++;
                        flor.Add(cols);
                        prev = cols;
                    }
                    else if (cols != prev && cols == '0')
                    {
                        //walls[wallindex] = '0';
                        //wallindex++;
                        walls.Add(cols);
                        prev = cols;
                    }
                }
            }
            result = walls.Count;
            return result;
        }

        public ActionResult MultipleQuestion()
        {
            List<QuestionModel> questions = new List<QuestionModel>();
            return View(questions);
        }

        [HttpPost]
        public ActionResult SaveQuestionAnswer(string[] questionAnswer)
        {
            foreach(var item in questionAnswer)
            {
                int id = Convert.ToInt32(item.Split('_')[0].Trim());
                string answer = item.Split('_')[1].Trim();
            }
            return RedirectToAction("MultipleQuestion");
        }
    }

    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public virtual List<QuestionAnswerModel> randomSample { get; set; }
    }
    public class QuestionAnswerModel
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}