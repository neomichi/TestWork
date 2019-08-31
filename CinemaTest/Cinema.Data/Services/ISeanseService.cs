using System.Collections.Generic;
using Cinema.Data.Models;
using Cinema.Data.ViewModels;

namespace Cinema.Data.Services
{
    public interface ISeanseService
    {
        List<SeansesView> GetActualSeances();
        List<Seance> GetAllSeances();


        int GetTest();
    }
}