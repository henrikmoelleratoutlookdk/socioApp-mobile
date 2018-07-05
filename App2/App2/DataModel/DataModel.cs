using App2.Helpers;

namespace App2.Models
{
    // EntityData is in ../Helpers - it adds the required
    // fields for Azure App Service Mobile Apps SDK
    public class Assignment : EntityData
    {
        public string Text { get; set; }
        public bool AssignmentOk { get; set; }
        //public bool Complete { get; set; }
    }
}