using System.ComponentModel.DataAnnotations;

namespace FundTracker.Model
{
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }

        
        public int Amount { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public Guid tagId { get; set; }
        public string Note {  get; set; }
    }
}
