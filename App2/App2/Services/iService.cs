using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Text;
using App2.Models;

namespace App2.Services
{
    public interface IService
    {
        Task Initialize();

        Task<IEnumerable<Assignment>> GetAssignments();

        Task<Assignment> AddAssignment(string text, bool AssignmentOk);

        Task<Assignment> UpdateAssignment(Assignment item);

        Task<bool> DeleteAssignment(Assignment item);

        Task SyncAssignments();
    }
}
