using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using App2.Models;
using System.Linq;

namespace App2.Services
{
    public class MockService : IService
    {
        List<Assignment> Items { get; set; } = new List<Assignment>();

        public MockService()
        {
            if (Items.Count == 0)
                Items = Assignments();
        }

        public Task<Assignment> AddAssignment(string text, bool AssignmentActive)
        {
            var item = new Assignment { Text = text, AssignmentOk = AssignmentActive };
            Items.Add(item);
            return Task.FromResult(item);
        }

        public Task<Assignment> UpdateAssignment(Assignment item)
        {
            var oldItem = Items.FirstOrDefault(x => x.Id == item.Id);
            Items.Remove(oldItem);
            Items.Add(item);
            
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAssignment(Assignment item)
        {
            Items.Remove(item);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Assignment>> GetAssignments()
        {
            IEnumerable<Assignment> AssignmentList = Items.AsEnumerable();
            return Task.FromResult(AssignmentList);
        }

        public Task Initialize()
        {
            return null;
        }

        public Task SyncAssignments()
        {
            return null;
        }
        
        List<Assignment> Assignments()
        {
            var items = new List<Assignment>();

            var assignment1 = new Assignment { Text = "Stå Op", AssignmentOk = false };
            var assignment2 = new Assignment { Text = "Gøre rent", AssignmentOk = false };

            items.Add(assignment1);
            items.Add(assignment2);

            return items;
        }
    }
}

