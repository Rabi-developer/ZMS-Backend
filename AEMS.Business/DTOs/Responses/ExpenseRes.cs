using IMS.Domain.Entities;

namespace IMS.Business.DTOs.Requests
{
    public class ExpenseRes
    {
        public Guid? Id { get; set; }

        public string? Listid { get; set; }

        public string? Description { get; set; }


        public Guid? ParentAccountId { get; set; }



        public ICollection<Expense>? Children { get; set; } = new List<Expense>();
    }
}