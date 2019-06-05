namespace Api.Models
{
    public class ResultApi<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
