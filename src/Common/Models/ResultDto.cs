namespace Common.Models
{
    public class ResultDto<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
