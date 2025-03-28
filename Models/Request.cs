namespace BorrowingSystemAPI.Models
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum ReturnStatus
    {
        Pending,
        Returned
    }

    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public Guid RequestedByUserId { get; set; }
        public User RequestedByUser { get; set; }
        public List<RequestItem> RequestItems { get; set; } = new List<RequestItem>();
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;

        public ReturnStatus ReturnStatus { get; set; } = ReturnStatus.Pending;
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}
