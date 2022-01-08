namespace KsiazeczkaPttk.Domain.Models
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccesful { get; set; }
        public string Message { get; set; }

        private Result(T value, bool isSuccesful, string message = null)
        {
            Value = value;
            IsSuccesful = isSuccesful;
            Message = message;
        }

        public static Result<T> Ok(T value) => new Result<T>(value, true);

        public static Result<T> Error(string message) => new Result<T>(default, false, message);
    }
}
