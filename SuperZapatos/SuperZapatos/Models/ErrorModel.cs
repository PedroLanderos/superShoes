namespace SuperZapatos.Models
{
    public class ErrorModel<T>
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public int? TotalElements { get; set; }
    }
}
