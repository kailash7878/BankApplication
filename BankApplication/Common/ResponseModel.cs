namespace BankApplication.Common
{
    public class ResponseModel
    {
        public bool IsSuccess {  get; set; }
        public object Data { get; set; }
        public string message {  get; set; }
    }
}
