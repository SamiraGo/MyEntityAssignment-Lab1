using MyEntityAssignment.Data;
using MyEntityAssignment.Handler;
using MyEntityAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEntityAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            FillData.StartMyApp();
            RunHandler.RunMyApp();
        }
    }
}
