using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator.Core
{
    /// <summary>
    /// from Bukunedya
    /// </summary>
    //public class PostfixNotationExpression
    //{
    //    private List<string> operators;
    //    public PostfixNotationExpression()
    //    {
    //        operators = new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^", });
    //    }


    //    private byte GetPriority(string s)
    //    {
    //        switch (s)
    //        {
    //            case "(":
    //            case ")":
    //                return 0;
    //            case "+":
    //            case "-":
    //                return 1;
    //            case "*":
    //            case "/":
    //                return 2;
    //            case "^":
    //                return 3;
    //            default:
    //                return 4;
    //        }
    //    }

    //    public string[] ConvertToPostfixNotation(string input)
    //    {
    //        List<string> outputSeparated = new List<string>();
    //        Stack<string> stack = new Stack<string>();

       
    //        var sb = new StringBuilder();
    //        var stringList = new List<string>();
    //        foreach (char item in input.Replace(",", "."))
    //        {

    //            if (item.Equals('_') || char.IsDigit(item) || item.Equals('.'))
    //            {
    //                sb.Append(item.ToString().Replace("_", "-"));
    //                continue;
    //            }
    //            else
    //            {
    //                stringList.Add(sb.ToString());
    //                stringList.Add(item.ToString());
    //                sb = new StringBuilder();
    //            }
    //        }
    //        if (sb.Length > 0) stringList.Add(sb.ToString());
      
    //        foreach (string symb in stringList)
    //        {
                

    //            // if (c.Equals("_")) { symb = "*"; outputSeparated.Add("-1"); }

    //            if (operators.Contains(symb))
    //            {
    //                if (stack.Count > 0 && !symb.Equals("("))
    //                {
    //                    if (symb.Equals(")"))
    //                    {
    //                        string s = stack.Pop();
    //                        while (s != "(")
    //                        {
    //                            outputSeparated.Add(s);
    //                            s = stack.Pop();
    //                        }
    //                    }
    //                    else if (GetPriority(symb) > GetPriority(stack.Peek()))
    //                        stack.Push(symb);
    //                    else
    //                    {
    //                        while (stack.Count > 0 && GetPriority(symb) <= GetPriority(stack.Peek()))
    //                            outputSeparated.Add(stack.Pop());
    //                        stack.Push(symb);
    //                    }
    //                }
    //                else
    //                    stack.Push(symb);
    //            }
    //            else
    //                outputSeparated.Add(symb);
    //        }
    //        if (stack.Count > 0)
    //            foreach (string symb in stack)
    //                outputSeparated.Add(symb);

    //        return outputSeparated.ToArray();
    //    }
    //    public decimal result(string input)
    //    {
    //        var stack = new Stack<string>();
    //        var queue = new Queue<string>(ConvertToPostfixNotation(input));
    //        string str = queue.Dequeue();
    //        while (queue.Count >= 0)
    //        {
    //            if (!operators.Contains(str))
    //            {
    //                //if (str.Equals("_"))
    //                //{
    //                //    stack.Push("-1");
    //                //    var list = queue.ToList();
    //                //    list.Insert(0, "*");
    //                //    queue = new Queue<string>(list);
    //                //}
    //                //else
    //                //{
    //                    stack.Push(str);
    //                    str = queue.Dequeue();
    //                //}
    //            }
    //            else
    //            {
    //                decimal summ = 0;
    //                try
    //                {

    //                    switch (str)
    //                    {

    //                        case "+":
    //                            {
                                  
    //                                decimal a = Helper.ToDecimal(stack.Pop());
    //                                decimal b = Helper.ToDecimal(stack.Pop());
    //                                summ = a + b;
    //                                break;
    //                            }
    //                        case "-":
    //                            {
    //                                decimal a = Helper.ToDecimal(stack.Pop());
    //                                decimal b = Helper.ToDecimal(stack.Pop());
    //                                summ = b - a;
    //                                break;
    //                            }
    //                        case "*":
    //                            {
                                 
    //                                decimal a = Helper.ToDecimal(stack.Pop());
    //                                decimal b = Helper.ToDecimal(stack.Pop());
    //                                summ = b * a;
    //                                break;
    //                            }
    //                        case "/":
    //                            {
    //                                decimal a = Helper.ToDecimal(stack.Pop());
    //                                decimal b = Helper.ToDecimal(stack.Pop());
    //                                summ = b / a;
    //                                break;
    //                            }
    //                        case "^":
    //                            {
    //                                decimal a = Helper.ToDecimal(stack.Pop());
    //                                decimal b = Helper.ToDecimal(stack.Pop());
    //                                summ = Convert.ToDecimal(Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a)), CultureInfo.InvariantCulture);
    //                                break;
    //                            }   
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    throw new Exception(ex.Message);
    //                }
    //                stack.Push(summ.ToString());
    //                if (queue.Count > 0)
    //                    str = queue.Dequeue();
    //                else
    //                    break;
    //            }

    //        }
    //        return Convert.ToDecimal(stack.Pop());
    //    }
    //}
}
