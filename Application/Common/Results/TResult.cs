namespace Application.Common.Results
{
    public class Result<T> : Result
    {
        private readonly T _value;

        protected internal Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value => IsSuccess ? _value : throw new InvalidOperationException("No value for failure result.");


        public static Result<TResult> Success<TResult>(TResult value) => new(value, true, Error.None);
        public static Result<TResult> Failure<TResult>(Error error) => new(default, false, error);
    }
}